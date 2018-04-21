﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Cat : GameEntity {
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

    protected override void Awake() {
        base.Awake();

        body = GetComponent<Rigidbody2D>();
        displayOrders(false);
    }

    void Start() {
        orderDict.Add(Order.OrderType.Wander, new WanderOrder(this));
        orderDict.Add(Order.OrderType.Move, new MoveOrder(this));
        orderDict.Add(Order.OrderType.Attack, new AttackOrder(this));

        ReturnToDefaultOrder();
    }

    void Update() {
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

        if (curOrder != null) {
            curOrder.PerformOrderFixedUpdate();
        }
    }

    void OnDrawGizmos() {
        if (curOrder != null) {
            curOrder.DrawGizmos();
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

    public void ReturnToDefaultOrder() {
        if (curOrder == null || curOrder.OrderId != Order.OrderType.Wander) {
            setupNewOrder(Order.OrderType.Wander);
        }
    }

    protected override void onDamage() {
        // TODO: for now do nothing. Later trigger animation and pause time as well as invincibility
    }

    protected override void onDeath() {
        // TODO: play death animation, then kill self. For now just destroy self
        GameObject.Destroy(this.gameObject);
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
}
