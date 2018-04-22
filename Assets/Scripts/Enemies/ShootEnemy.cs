using System;
using System.Collections.Generic;
using System.Linq;

using UnityEngine;

public class ShootEnemy : Enemy
{
    [SerializeField]
    private float moveMag = 75f;

    [SerializeField]
    private float range = 300;

    [SerializeField]
    private float shootRate = 1;

    [SerializeField]
    private float shootMag = 200;

    [SerializeField]
    private RectTransform rect;

    private Vector2 curDir = new Vector2();

    private float deltaTimeSinceLastShot = 0;

    void OnTriggerEnter2D(Collider2D collision) {
        if (collision.tag == "Player" || collision.tag == "Cat") {
            GameEntity entity = collision.GetComponent<GameEntity>();
            entity.Damage(1);

            onDeath();
        }
    }

    protected override void performGameLogic() {
        Vector3 playerPos = ObjectRefHolder.Instance.PlayerRef.transform.position;

        Vector2 diffVec = playerPos - this.transform.position;

        bool canShoot = deltaTimeSinceLastShot > shootRate;

        Vector2 newDir = new Vector2();
        if (canShoot && diffVec.magnitude < range) {
            Bullet bullet = BulletHandler.Instance.CreateBullet();
            bullet.Fire(this.gameObject, this.gameObject.transform.position, diffVec.normalized, shootMag, range, true);

            deltaTimeSinceLastShot = 0;
        } else if (diffVec.magnitude >= range - 100f) {
            newDir = diffVec.normalized;

            float width = rect.sizeDelta.x;
            float angle = Vector2.SignedAngle(new Vector2(0, 1), newDir);
            Vector2 leftDir = new Vector2(0, 1).Rotate(angle).Rotate(90f).normalized;
            Vector2 rightDir = new Vector2(0, 1).Rotate(angle).Rotate(-90f).normalized;

            int layerMask = LayerMask.GetMask("Enemy");
            RaycastHit2D leftHit = Physics2D.Raycast(this.transform.position + (Vector3)(leftDir * width / 2f), newDir, moveMag * 2f, layerMask);
            RaycastHit2D rightHit = Physics2D.Raycast(this.transform.position + (Vector3)(rightDir * width / 2f), newDir, moveMag * 2f, layerMask);

            // We don't worry about the case where both colliders hit, since we don't care that much about overlap. We just don't want them to all jumble up all the time.
            if (leftHit.collider != null && rightHit.collider == null) {
                newDir = (newDir * 0.5f + rightDir * 0.5f).normalized;
            } else if (leftHit.collider == null && rightHit.collider == null) {
                newDir = (newDir * 0.5f + leftDir * 0.5f).normalized;
            }
        }

        deltaTimeSinceLastShot += Time.deltaTime;
        curDir = newDir;
    }

    protected override void performMovement() {
        body.velocity = curDir * moveMag;

        if (curDir.magnitude == 0) {
            animator.SetTrigger("Idle");
        } else {
            animator.SetTrigger("Move");
        }

        base.performMovement();
    }

    protected override void onDamage() {
        // Do nothing since only one health
    }
}
