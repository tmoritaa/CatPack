using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(CanvasGroup))]
public abstract class PlayerEntity : GameEntity
{
    [SerializeField]
    protected float invincibleDur = 0.75f;

    protected CanvasGroup canvasGroup;

    protected bool invincible = false;

    protected override void Awake() {
        base.Awake();

        canvasGroup = GetComponent<CanvasGroup>();
    }

    public override void Damage(int dmgAmount) {
        if (!invincible) {
            base.Damage(dmgAmount);
        }
    }

    protected override void onDamage() {
        StartCoroutine(performInvincibility());
    }

    private IEnumerator performInvincibility() {
        invincible = true;
        canvasGroup.alpha = 0.5f;

        yield return new WaitForSeconds(invincibleDur);

        canvasGroup.alpha = 1.0f;
        invincible = false;
    }
}
