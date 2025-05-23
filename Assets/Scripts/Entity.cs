using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity : MonoBehaviour
{
    protected int health;

    public virtual void GetDamage()
    {
        health--;
        if (health < 1)
            Die();
    }

    
    public virtual void Die()
    {
        Destroy(this.gameObject);
    }
}
