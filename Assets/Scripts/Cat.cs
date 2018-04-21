using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cat : MonoBehaviour {
    [SerializeField]
    private float moveMag = 25f;

    [SerializeField]
    private float runMag = 75f;

    [SerializeField]
    private GameObject orderObj;

    private Rigidbody2D body;

    private Dictionary<Order.OrderType, Order> orderDict = new Dictionary<Order.OrderType, Order>();

    private Order curOrder = null;

    private bool shouldSetupNewOrder = false;
    private Order.OrderType newOrderId = Order.OrderType.Wander;

    void Awake() {
        body = GetComponent<Rigidbody2D>();
        displayOrders(false);
    }

    void Start() {
        orderDict.Add(Order.OrderType.Wander, new WanderOrder(this));
        orderDict.Add(Order.OrderType.Move, new MoveOrder(this));

        InputManager.Instance.OnRightMouseUp += returnToDefaultOrder;

        returnToDefaultOrder();
    }

    void Update() {
        if (shouldSetupNewOrder) {
            setupNewOrder(newOrderId);
            shouldSetupNewOrder = false;
        }

        if (curOrder != null) {
            curOrder.PerformOrderUpdate();

            if (curOrder.Done()) {
                returnToDefaultOrder();
            }
        }
    }

    void FixedUpdate() {
        body.velocity = new Vector2();

        if (curOrder != null) {
            curOrder.PerformOrderFixedUpdate();
        }
    }

    public void CatClicked() {
        displayOrders(true);
    }

    public void OrderButtonClicked(int orderNumber) {
        newOrderId = (Order.OrderType)orderNumber;
        shouldSetupNewOrder = true;

        displayOrders(false);
    }

    public void MoveTowardsPos(Vector2 pos, bool run) {
        Vector2 diffVec = (pos - (Vector2)this.transform.position).normalized;

        body.velocity = diffVec * (run ? runMag : moveMag);
    }

    public void MoveInDir(Vector2 dir, bool run) {
        body.velocity = dir.normalized * (run ? runMag : moveMag);
    }

    private void returnToDefaultOrder() {
        if (curOrder == null || curOrder.OrderId != Order.OrderType.Wander) {
            setupNewOrder(Order.OrderType.Wander);
        }
    }

    private void setupNewOrder(Order.OrderType id) {
        if (curOrder != null) {
            curOrder.Cleanup();
        }
        
        curOrder = orderDict[id];
        curOrder.PrepareForInput();
    }

    private void displayOrders(bool show) {
        orderObj.SetActive(show);
    }

    void OnDrawGizmos() {
        if (curOrder != null) {
            curOrder.DrawGizmos();
        }
    }
}
