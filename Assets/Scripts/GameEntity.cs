using System;
using System.Collections.Generic;
using System.Linq;

using UnityEngine;

public abstract class GameEntity : MonoBehaviour
{
    [SerializeField]
    protected int startingHealth = 1;

    protected bool beingDestoryed = false;

    protected bool dead = false;

    public int Health
    {
        get; protected set;
    }
    
    protected virtual void Awake() {
        Health = startingHealth;
    }

    public virtual void Damage(int dmgAmount) {
        if (dead) {
            return;
        }

        Health -= dmgAmount;

        Health = Math.Max(0, Health);

        if (Health > 0) {
            onDamage();
        } else {
            dead = true;
            onDeath();
        }
    }
    
    protected abstract void onDamage();
    protected abstract void onDeath();

    protected void destroySelf() {
        if (!beingDestoryed) {
            GameObject.Destroy(this.gameObject);
            beingDestoryed = true;
        }
    }
}
