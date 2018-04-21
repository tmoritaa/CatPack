using System;
using System.Collections.Generic;
using System.Linq;

using UnityEngine;

public class GameEntity : MonoBehaviour
{
    public int Health
    {
        get; private set;
    }

    public virtual void Damage(int dmgAmount) {
        Debug.Log("damaged");
        // TODO: later implement damaging and death logic
        // Probably want a onDeath virtual function for customizable death response
    }

    public void DestroySelf() {
        GameObject.Destroy(this.gameObject);
    }
}
