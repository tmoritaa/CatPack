using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public abstract class Enemy : GameEntity
{
    [SerializeField]
    protected int score;

    [SerializeField]
    protected int damage = 1;

    [SerializeField]
    protected AudioSource explosionAudioSource;

    protected Animator animator;

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

    protected virtual void OnTriggerEnter2D(Collider2D collision) {
        if (collision.tag == "Player" || collision.tag == "Cat") {
            GameEntity entity = collision.GetComponent<GameEntity>();
            entity.Damage(damage);

            onDeath();
        }
    }

    protected abstract void performGameLogic();

    protected virtual void performMovement() {
        Vector2 vel = ObjectRefHolder.Instance.PlayerRef.transform.position - this.transform.position;

        if (Mathf.Abs(vel.x) > 5) {
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
        explosionAudioSource.Play();

        StartCoroutine(WaitForDeath());
    }

    protected IEnumerator WaitForDeath() {
        yield return new WaitForSeconds(1);
        destroySelf();
    }
}