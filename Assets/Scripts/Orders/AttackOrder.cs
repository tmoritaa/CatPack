using System;
using System.Collections.Generic;
using System.Linq;

using UnityEngine;

class AttackOrder : Order
{
    private const float shootUpdatePeriod = 1f;

    private const float orderPeriod = 3f;

    private bool waitingForInput = false;

    private Vector2 shootDir = new Vector2();

    private float deltaTimeSinceLastShot = 9999f;
    private float deltaTimeSinceStart = 0f;

    public AttackOrder(Cat catRef) : base(OrderType.Attack, catRef) {}

    public override void PrepareForInput() {
        InputManager.Instance.OnLeftMouseUp += onMouseUp;
        InputManager.Instance.OnRightMouseUp += owningCat.ReturnToDefaultOrder;

        InputManager.Instance.Cursor.SetImageToTarget();

        waitingForInput = true;
        deltaTimeSinceStart = 0f;
        deltaTimeSinceLastShot = 9999f;
    }

    public override void PerformOrderFixedUpdate() {
        // Do nothing.
    }

    public override void PerformOrderUpdate() {
        if (!waitingForInput) {
            if (deltaTimeSinceLastShot >= shootUpdatePeriod) {
                Bullet bullet = BulletHandler.Instance.CreateBullet();
                bullet.Fire(owningCat.gameObject, owningCat.transform.position, shootDir, 500, 1000, false);

                deltaTimeSinceLastShot = 0f;
            }

            deltaTimeSinceLastShot += Time.deltaTime;
            deltaTimeSinceStart += Time.deltaTime;
        }
    }

    public override void Cleanup() {
        if (waitingForInput) {
            InputManager.Instance.OnLeftMouseUp -= onMouseUp;
            InputManager.Instance.OnRightMouseUp -= owningCat.ReturnToDefaultOrder;
        }

        InputManager.Instance.Cursor.SetImageToDefault();
        waitingForInput = false;
    }

    public override bool Done() {
        return deltaTimeSinceStart >= orderPeriod;
    }

    public override void DrawGizmos() {
        // Do nothing
    }

    private void onMouseUp(Vector2 mousePos) {
        shootDir = (mousePos - (Vector2)owningCat.transform.position).normalized;
        Cleanup();
    }
}
