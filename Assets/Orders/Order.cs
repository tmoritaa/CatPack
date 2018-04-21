using System;
using System.Collections.Generic;
using System.Linq;

using UnityEngine;

public abstract class Order
{
    public enum OrderType
    {
        Idle = 0,
        Move,
    }

    protected OrderType orderId;
    public OrderType OrderId
    {
        get {
            return orderId;
        }
    }

    protected Cat owningCat;

    public Order(OrderType id, Cat catRef) {
        orderId = id;
        owningCat = catRef;
    }

    public abstract void PrepareForInput();
    public abstract void PerformOrderUpdate();
    public abstract void PerformOrderFixedUpdate();
    public abstract void Cleanup();

    public abstract void DrawGizmos();
}
