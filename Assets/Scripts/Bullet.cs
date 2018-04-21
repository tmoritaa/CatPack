using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {
    [SerializeField]
    private Rigidbody2D body;

    private float range;

    private Vector2 origPos;

    private Vector2 impulseVec;

    private bool shotByEnemy = false;

    private bool beingDestroyed = false;

    private bool applyImpulse = false;

    private GameObject ownerGO;

    void Update() {
        float dist = (this.transform.position - (Vector3)origPos).magnitude;

        if (!beingDestroyed && dist >= range) {
            destroySelf();
        }
    }

    void FixedUpdate() {
        if (applyImpulse) {
            this.body.AddForce(impulseVec, ForceMode2D.Impulse);
            applyImpulse = false;
        }
    }

    public void Fire(GameObject shotOwner, Vector2 startPos, Vector2 shootDir, float shootMag, float bulletRange, bool _shotByEnemy) {
        ownerGO = shotOwner;
        origPos = startPos;
        range = bulletRange;
        shotByEnemy = _shotByEnemy;

        this.transform.position = origPos;

        impulseVec = shootDir.normalized * shootMag;
        applyImpulse = true;
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (!beingDestroyed && collision.gameObject != ownerGO) {
            if ((shotByEnemy && (collision.tag == "Player" || collision.tag == "Cat"))
                || (!shotByEnemy && collision.tag == "Enemy"))
            {
                GameEntity entity = collision.GetComponent<GameEntity>();
                entity.Damage(1);
            }

            destroySelf();
        }
    }

    private void destroySelf() {
        beingDestroyed = true;
        Destroy(this.gameObject);
    }
}
