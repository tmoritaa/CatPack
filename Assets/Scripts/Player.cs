using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Player : GameEntity {
    [SerializeField]
    private float moveMag = 100f;

    private Rigidbody2D body;

    private Vector2 curDir = new Vector2();

    void Awake() {
        body = GetComponent<Rigidbody2D>();
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
	}

    void FixedUpdate() {
        body.velocity = curDir * moveMag;
    }
}
