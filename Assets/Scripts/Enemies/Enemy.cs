using System;
using System.Collections.Generic;
using System.Linq;

using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public abstract class Enemy : GameEntity
{
    protected Rigidbody2D body;

    protected virtual void Awake() {
        body = this.GetComponent<Rigidbody2D>();
    }

    protected void Update() {
        performGameLogic();
    }

    protected void FixedUpdate() {
        performMovement();
    }

    protected abstract void performGameLogic();
    protected abstract void performMovement();
}