using System.Collections;
using System.Collections.Generic;
//using System.Diagnostics;
using UnityEngine;

public class AttackSystem : Entity
{
    private void Start()
    {
        health = 3;
    }

    public Hero hero;

    //[SerializeField] private int lives = 3;

  /*  private void OnCollisionEnter2D(Collision2D collision)
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
*/
    public void DamageByEnemy1(bool isAttacking, bool playerIsinRange)
    {
        Debug.Log("Give Damage");
        Hero.Instance.GetDamage();
        if(hero.isInAttackAnimation)
        health--;
        Debug.Log("Worm left " + health);
        if (health < 1)
            Die();
        
    }

    
}