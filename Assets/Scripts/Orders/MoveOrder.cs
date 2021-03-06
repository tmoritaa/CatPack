﻿using System;
using System.Collections.Generic;
using System.Linq;

using UnityEngine;

public class MoveOrder : Order
{
    private const float distMagToStop = 5f;

    private bool waitingForInput = false;

    private Vector2 targetPos = new Vector2();

    public MoveOrder(Cat catRef) : base(OrderType.Move, catRef) {}

    public override void PrepareForInput() {
        waitingForInput = true;

        InputManager.Instance.OnLeftMouseDown += onMouseDown;
        InputManager.Instance.OnRightMouseUp += owningCat.ReturnToDefaultOrder;

        InputManager.Instance.Cursor.SetImageToTarget();
    }

    public override void PerformOrderFixedUpdate() {
        if (!waitingForInput) {
            owningCat.MoveTowardsPos(targetPos, true);
        }
    }

    public override void PerformOrderUpdate() {
        // Do nothing
    }

    public override bool Done() {
        if (!waitingForInput) {
            Vector2 diffVec = targetPos - (Vector2)owningCat.transform.position;

            return diffVec.magnitude < distMagToStop;
        }

        return false;
    }

    public override void Cleanup() {
        if (waitingForInput) {
            InputManager.Instance.OnLeftMouseDown -= onMouseDown;
            InputManager.Instance.OnRightMouseUp -= owningCat.ReturnToDefaultOrder;
        }

        InputManager.Instance.Cursor.SetImageToDefault();
        waitingForInput = false;
    }

    public override void DrawGizmos() {
        Color origColor = Gizmos.color;

        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(targetPos, 30);

        Vector2 diffVec = targetPos - (Vector2)owningCat.transform.position;

        Gizmos.DrawLine(owningCat.transform.position, owningCat.transform.position + (Vector3)(diffVec.normalized * 100f));

        Gizmos.color = origColor;
    }

    private void onMouseDown(Vector2 mousePos) {
        targetPos = mousePos;

        Cleanup();
    }
}
