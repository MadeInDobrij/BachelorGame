using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Security.Cryptography;
using UnityEngine;

public class WalkingMonster : Entity
{
    private float speed = 5f;
    private Vector3 dir;
    private SpriteRenderer sprite;
    //protected int lives;

    private void Start()
    {
        dir = transform.right;
        health = 4;
    }

    void Update()
    {
        Move();
    }

    private void Move()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position + transform.up * 0.1f + transform.right * dir.x * 0.7f, 0.1f);

        if (colliders.Length > 0) dir *= -1f;
        transform.position = Vector3.MoveTowards(transform.position, transform.position + dir, Time.deltaTime);
        //sprite.flipX = dir.x > 0.0f;
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject == Hero.Instance.gameObject)
        {
            Hero.Instance.GetDamage();
        }
        if (health < 1)
            Die();
    }
}
