﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Player : GameEntity {
    [SerializeField]
    private float moveMag = 100f;

    [SerializeField]
    private float timeSlowdownVal = 0.33f;

    [SerializeField]
    private float timeBarDepletionPerSec = 33;

    [SerializeField]
    private float timeBarRechargePerSec = 25;

    [SerializeField]
    private float minTimeBarRequiredForActivation = 20;

    [SerializeField]
    private float maxTimeBar = 100f;

    private Rigidbody2D body;

    private Vector2 curDir = new Vector2();

    private bool timeStopped = false;

    private float curTimeBar = 0;

    protected override void Awake() {
        base.Awake();

        body = GetComponent<Rigidbody2D>();

        curTimeBar = maxTimeBar;
    }

    void Update () {
        Vector2 newDir = new Vector2();

		if (Input.GetKey(KeyCode.W)) {
            newDir.y += 1;
        }
        if (Input.GetKey(KeyCode.S)) {
            newDir.y -= 1;
        }

        if (Input.GetKey(KeyCode.A)) {
            newDir.x -= 1;
        }
        if (Input.GetKey(KeyCode.D)) {
            newDir.x += 1;
        }

        curDir = newDir.normalized;

        handleTimeStopLogic();
	}

    void FixedUpdate() {
        body.velocity = curDir * moveMag;
    }

    protected override void onDamage() {
        Debug.Log("Player damaged");
        // TODO: for now do nothing. Later trigger animation and pause time as well as invincibility
    }

    protected override void onDeath() {
        Debug.Log("Player dead");
        // TODO: for now do nothing. Later display game over screen
    }

    private void handleTimeStopLogic() {
        if (!timeStopped && curTimeBar >= minTimeBarRequiredForActivation && Input.GetKeyDown(KeyCode.Space)) {
            timeStopped = true;
        } else if (timeStopped && Input.GetKeyUp(KeyCode.Space)) {
            timeStopped = false;
        }

        if (timeStopped) {
            curTimeBar = Mathf.Clamp(curTimeBar - timeBarDepletionPerSec * Time.unscaledDeltaTime, 0, maxTimeBar);
        } else {
            curTimeBar = Mathf.Clamp(curTimeBar + timeBarRechargePerSec * Time.unscaledDeltaTime, 0, maxTimeBar);
        }

        if (curTimeBar == 0) {
            timeStopped = false;
        }

        Time.timeScale = timeStopped ? timeSlowdownVal : 1;

        Debug.Log(curTimeBar);
    }
}
