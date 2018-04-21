using System;
using System.Collections.Generic;
using System.Linq;

using UnityEngine;

class BasicEnemy : Enemy
{
    [SerializeField]
    private float moveMag = 75f;

    [SerializeField]
    private RectTransform rect;

    private Vector2 curDir = new Vector2();

    void OnTriggerEnter2D(Collider2D collision) {
        if (collision.tag == "Player" || collision.tag == "Cat") {
            GameEntity entity = collision.GetComponent<GameEntity>();
            entity.Damage(1);

            onDeath();
        }
    }

    protected override void performGameLogic() {
        Vector3 playerPos = ObjectRefHolder.Instance.PlayerRef.transform.position;

        Vector2 newDir = (playerPos - this.transform.position).normalized;

        float width = rect.sizeDelta.x;
        float angle = Vector2.SignedAngle(new Vector2(0, 1), newDir);
        Vector2 leftDir = new Vector2(0, 1).Rotate(angle).Rotate(90f).normalized;
        Vector2 rightDir = new Vector2(0, 1).Rotate(angle).Rotate(-90f).normalized;

        int layerMask = LayerMask.GetMask("Enemy");
        RaycastHit2D leftHit = Physics2D.Raycast(this.transform.position + (Vector3)(leftDir * width/2f), newDir, moveMag * 2f, layerMask);
        RaycastHit2D rightHit = Physics2D.Raycast(this.transform.position + (Vector3)(rightDir * width / 2f), newDir, moveMag * 2f, layerMask);

        // We don't worry about the case where both colliders hit, since we don't care that much about overlap. We just don't want them to all jumble up all the time.
        if (leftHit.collider != null && rightHit.collider == null) {
            newDir = (newDir * 0.5f + rightDir * 0.5f).normalized;
        } else if (leftHit.collider == null && rightHit.collider == null) {
            newDir = (newDir * 0.5f + leftDir * 0.5f).normalized;
        }

        curDir = newDir;
    }

    protected override void performMovement() {
        body.velocity = curDir * moveMag;
    }

    protected override void onDamage() {
        // Do nothing since only one health
    }

    protected override void onDeath() {
        base.onDeath();

        performDeathLogic();
    }

    private void performDeathLogic() {
        // TODO: for now. Later add explosion animation then destroy self.

        destroySelf();
    }
}
