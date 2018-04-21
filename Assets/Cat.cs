using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cat : MonoBehaviour {
    [SerializeField]
    private float moveMag = 75f;

    [SerializeField]
    private GameObject orderObj;

    private Rigidbody2D body;

    private Dictionary<Order.OrderType, Order> orderDict = new Dictionary<Order.OrderType, Order>();

    private Order curOrder = null;

    private bool shouldSetupNewOrder = false;
    private Order.OrderType newOrderId = Order.OrderType.Idle;

    void Awake() {
        body = GetComponent<Rigidbody2D>();
        displayOrders(false);
    }

    void Start() {
        orderDict.Add(Order.OrderType.Idle, new IdleOrder(this));
        orderDict.Add(Order.OrderType.Move, new MoveOrder(this));

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

    public void MoveTowardsPos(Vector2 pos) {
        Vector2 diffVec = (pos - (Vector2)this.transform.position).normalized;

        body.velocity = diffVec * moveMag;
    }

    private void returnToDefaultOrder() {
        setupNewOrder(Order.OrderType.Idle);
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
