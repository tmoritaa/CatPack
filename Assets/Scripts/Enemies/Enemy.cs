using System;
using System.Collections.Generic;
using System.Linq;

using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public abstract class Enemy : GameEntity
{
    [SerializeField]
    private int score;

    protected Rigidbody2D body;

    protected override void Awake() {
        base.Awake();
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

    protected override void onDeath() {
        ScoreHandler.Instance.AddScore(score);
    }
}