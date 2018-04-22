﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public abstract class Enemy : GameEntity
{
    [SerializeField]
    private int score;

    private Animator animator;

    protected Rigidbody2D body;

    protected Collider2D collider2d;

    protected override void Awake() {
        base.Awake();
        body = this.GetComponent<Rigidbody2D>();
        animator = this.GetComponent<Animator>();
        collider2d = this.GetComponent<Collider2D>();
    }

    protected void Update() {
        if (!dead) {
            performGameLogic();
        }
    }

    protected void FixedUpdate() {
        body.velocity = new Vector2();
        if (!dead) {
            performMovement();
        }
    }

    protected abstract void performGameLogic();

    protected virtual void performMovement() {
        Vector2 vel = body.velocity;

        if (Mathf.Abs(vel.x) > 0) {
            Vector3 localScale = this.transform.localScale;
            localScale.x = (vel.x > 0) ? -1 : 1;
            this.transform.localScale = localScale;
        }
    }

    protected override void onDeath() {
        dead = true;
        collider2d.enabled = false;
        ScoreHandler.Instance.AddScore(score);
        animator.SetTrigger("Dead");
        StartCoroutine(WaitForDeath());
    }

    protected IEnumerator WaitForDeath() {
        yield return new WaitForSeconds(1);
        destroySelf();
    }
}