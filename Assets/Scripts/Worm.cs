using System.Collections;
using System.Collections.Generic;
//using System.Diagnostics;
using UnityEngine;

public class Worm : Entity
{
    private void Start()
    {
        health = 3;
    }

    //[SerializeField] private int lives = 3;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject == Hero.Instance.gameObject)
        {
            Hero.Instance.GetDamage();
            health--;
            Debug.Log("Worm left " + health);
        }

        if (health < 1)
            Die();
    }
}
