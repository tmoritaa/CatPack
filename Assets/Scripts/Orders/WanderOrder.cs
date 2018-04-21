using System;
using System.Collections.Generic;
using System.Linq;

using UnityEngine;

public class WanderOrder : Order
{
    private const float dirChoiceUpdatePeriod = 1.5f;

    private float deltaTimeSinceLastDirChoice = 9999;

    private Vector2 moveDir;

    public WanderOrder(Cat catRef) : base(OrderType.Wander, catRef) {}

    public override void PrepareForInput() {
        // Do nothing.
    }

    public override void PerformOrderFixedUpdate() {
        owningCat.MoveInDir(moveDir, false);
    }

    public override void PerformOrderUpdate() {
        if (deltaTimeSinceLastDirChoice > dirChoiceUpdatePeriod) {
            float angle = GlobalRandom.GetRandomNumber(0, 360);

            moveDir = new Vector2(0, 1).Rotate(angle);

            deltaTimeSinceLastDirChoice = 0;
        }

        deltaTimeSinceLastDirChoice += Time.deltaTime;
    }

    public override void Cleanup() {
        // Do nothing
    }

    public override bool Done() {
        return false;
    }

    public override void DrawGizmos() {
        // Do nothing
    }
}
