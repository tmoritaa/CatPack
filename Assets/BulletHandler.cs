using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletHandler : MonoBehaviour {
    private static BulletHandler instance;
    public static BulletHandler Instance
    {
        get {
            return instance;
        }
    }

    [SerializeField]
    private Bullet bulletPrefab;

    [SerializeField]
    private Transform bulletsRoot;

    void Awake() {
        instance = this;
    }

    public Bullet CreateBullet() {
        Bullet bullet = Instantiate<Bullet>(bulletPrefab, bulletsRoot);

        return bullet;
    }
}
