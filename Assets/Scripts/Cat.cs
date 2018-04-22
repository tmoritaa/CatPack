using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Cat : PlayerEntity {
    [SerializeField]
    private float moveMag = 25f;

    [SerializeField]
    private float runMag = 75f;

    [SerializeField]
    private OrdersFrame ordersFramePrefab;

    [SerializeField]
    private Transform ordersFrameRoot;

    [SerializeField]
    private GameObject ordersFollowGO;

    [SerializeField]
    private CatHealthDisplay catHealthPrefab;

    [SerializeField]
    private Transform healthFrameRoot;

    [SerializeField]
    private GameObject healthFollowsGO;

    private Rigidbody2D body;

    private OrdersFrame orderObj;

    private Animator animator;

    private CatHealthDisplay catHealthObj;

    private Dictionary<Order.OrderType, Order> orderDict = new Dictionary<Order.OrderType, Order>();

    private Order curOrder = null;

    private bool shouldSetupNewOrder = false;
    private Order.OrderType newOrderId = Order.OrderType.Wander;

    protected override void Awake() {
        base.Awake();

        body = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

        orderObj = Instantiate(ordersFramePrefab, ordersFrameRoot, false);
        orderObj.Init(this, ordersFollowGO);

        catHealthObj = Instantiate(catHealthPrefab, healthFrameRoot, false);
        catHealthObj.Init(this, healthFollowsGO);

        displayOrders(false);
    }

    void Start() {
        orderDict.Add(Order.OrderType.Wander, new WanderOrder(this));
        orderDict.Add(Order.OrderType.Move, new MoveOrder(this));
        orderDict.Add(Order.OrderType.Attack, new AttackOrder(this));

        ReturnToDefaultOrder();
    }

    void Update() {
        if (dead) {
            return;
        }

        if (shouldSetupNewOrder) {
            setupNewOrder(newOrderId);
            shouldSetupNewOrder = false;
        }

        if (curOrder != null) {
            curOrder.PerformOrderUpdate();

            if (curOrder.Done()) {
                ReturnToDefaultOrder();
            }
        }
    }

    void FixedUpdate() {
        body.velocity = new Vector2();

        if (curOrder != null && !dead) {
            curOrder.PerformOrderFixedUpdate();
        }
    }

    void OnDrawGizmos() {
        if (curOrder != null) {
            curOrder.DrawGizmos();
        }
    }

    public void CatClicked() {
        if (dead) {
            return;
        }

        displayOrders(true);

        InputManager.Instance.OnRightMouseUp += hideOrders;
    }

    public void OrderButtonClicked(Order.OrderType orderId) {
        if (dead) {
            return;
        }

        newOrderId = orderId;
        shouldSetupNewOrder = true;

        InputManager.Instance.OnRightMouseUp -= hideOrders;
        displayOrders(false);
    }

    public void MoveTowardsPos(Vector2 pos, bool run) {
        Vector2 diffVec = (pos - (Vector2)this.transform.position).normalized;
        setVelocity(diffVec * (run ? runMag : moveMag));
    }

    public void MoveInDir(Vector2 dir, bool run) {
        setVelocity(dir.normalized * (run ? runMag : moveMag));
    }

    public void ReturnToDefaultOrder() {
        if (curOrder == null || curOrder.OrderId != Order.OrderType.Wander) {
            setupNewOrder(Order.OrderType.Wander);
        }
    }

    protected override void onDamage() {
        base.onDamage();

        animator.SetTrigger("Damaged");
    }

    protected override void onDeath() {
        // TODO: play death animation, then kill self. For now just destroy self
        cleanup();
        body.velocity = new Vector2();

        dead = true;

        animator.SetTrigger("Dead");

        StartCoroutine(destroySelfAfterSeconds(1.5f));
    }

    private void setVelocity(Vector2 vel) {
        if (dead) {
            return;
        }

        body.velocity = vel;

        if (vel.magnitude > 0) {
            animator.SetTrigger("Move");

            if (vel.x > 0) {
                Vector3 scale = this.transform.localScale;
                scale.z = -1;
                this.transform.localScale = scale;
            }
        } else {
            animator.SetTrigger("Idle");
        }
    }

    private void cleanup() {
        if (curOrder != null) {
            curOrder.Cleanup();
        }

        if (orderObj.IsDisplayed) {
            InputManager.Instance.OnRightMouseUp -= hideOrders;
        }

        Destroy(orderObj.gameObject);
        Destroy(catHealthObj.gameObject);

        orderObj = null;
        catHealthObj = null;
    }

    private void setupNewOrder(Order.OrderType id) {
        if (curOrder != null) {
            curOrder.Cleanup();
        }
        
        curOrder = orderDict[id];
        curOrder.PrepareForInput();
    }

    private void displayOrders(bool show) {
        if (orderObj != null) {
            orderObj.SetShowState(show);
        }
    }

    private void hideOrders() {
        displayOrders(false);
    }

    private IEnumerator destroySelfAfterSeconds(float secs) {
        yield return new WaitForSeconds(secs);

        GameObject.Destroy(this.gameObject);
    }
}
