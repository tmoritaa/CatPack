using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class IdleOrder : Order
{
    public IdleOrder(Cat catRef) : base(OrderType.Idle, catRef) {}

    public override void PrepareForInput() {
        // Do nothing
    }

    public override void PerformOrderFixedUpdate() {
        // Do nothing
    }

    public override void PerformOrderUpdate() {
        // Do nothing
    }

    public override void Cleanup() {
        // Do nothing
    }

    public override void DrawGizmos() {
        // Do nothing
    }
}
