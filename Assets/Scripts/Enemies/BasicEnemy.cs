using System;
using System.Collections.Generic;
using System.Linq;

using UnityEngine;

class BasicEnemy : Enemy
{
    [SerializeField]
    private float moveMag = 75f;

    private Vector2 curDir = new Vector2();

    void OnTriggerEnter2D(Collider2D collision) {
        if (collision.tag == "Player" || collision.tag == "cat") {
            GameEntity entity = collision.GetComponent<GameEntity>();
            entity.Damage(1);

            DestroySelf();
        }
    }

    protected override void performGameLogic() {
        Vector3 playerPos = ObjectRefHolder.Instance.PlayerRef.transform.position;

        curDir = (playerPos - this.transform.position).normalized;
    }

    protected override void performMovement() {
        body.velocity = curDir * moveMag;
    }
}
