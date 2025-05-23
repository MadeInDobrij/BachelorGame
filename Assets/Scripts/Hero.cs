using System;
using System.Collections;
//using System.Collections.Generic;
//using System.Management.Instrumentation;
//using System.Runtime.InteropServices;
//using System.Diagnostics;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class Hero : Entity
{
    [SerializeField] private float speed = 3f;
    [SerializeField] private int health;
    [SerializeField] private float jumpforce = 15f;
  [SerializeField]  private bool isGrounded = false;

    [SerializeField] private Image[] hearts;

    [SerializeField] private Sprite aliveHeart;
    [SerializeField] private Sprite deadHeart;

    public bool isAttacking = false;
    public bool isRecharged = true;

    public Transform attackPos;
    public float attackRange;
    public LayerMask enemy;
    public Joystick joystick;



    private Rigidbody2D rb;
    private Animator anim;
    private SpriteRenderer sprite;
    public bool isInAttackAnimation => State == States.Attack; // New public boolean to check attack animation state
    public BossEnemy bossEnemy;
    public static Hero Instance { get; set; }
    public AudioSource hitSound;
    
    public Transform groundCheck;
    public float groundCheckRadius = 0.2f;
    public LayerMask groundLayer;
    public bool CanBeDamaged = true;
    public SpriteRenderer makeIndamageable;
   
    private States State
    {
        get { return (States)anim.GetInteger("State"); }
        set { anim.SetInteger("State", (int)value); }
    }

    public void Attack()
    {
        if (isGrounded && isRecharged)
        {
            State = States.Attack;
            isAttacking = true;
            if (bossEnemy != null)
            {
                float distance = Vector3.Distance(this.transform.position, bossEnemy.gameObject.transform.position);
                if (distance <= 2.2)
                {
                    bossEnemy.TakeDamage(10);
                }
            }

            isRecharged = false;

            StartCoroutine(AttackAnimation());
            StartCoroutine(AttackCoolDown());
        }
    }

    private void OnAttack()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(attackPos.position, attackRange, enemy);

        for (int i = 0; i < colliders.Length; i++)
        {
            colliders[i].GetComponent<Entity>().GetDamage();
        }
    }

    private void Awake()
    {
        CanBeDamaged = true;
        base.health = 5;
        health = base.health;
        Instance = this;
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        sprite = GetComponentInChildren<SpriteRenderer>();
        isRecharged = true;
        
    }

    private void FixedUpdate()
    {
       isGrounded =  CheckGround();
        //if (isAttacking)
            //if (!isGrounded) State = States.jump;
    }

    private void Update()
    {
        
        if (isGrounded && !isAttacking) State = States.Idle;
        if (!isAttacking && joystick.Horizontal != 0)
            Run();
        if (!isAttacking && isGrounded && joystick.Vertical > 0.5f)
            Jump();

        if (health > base.health)
            health = base.health;

        for (int i = 0; i < hearts.Length; i++)
        {
            if (i < health)
                hearts[i].sprite = aliveHeart;
            else
            {
                hearts[i].sprite = deadHeart;

            }

            if (i < base.health)
                hearts[i].enabled = true;
            else
                hearts[i].enabled = false;
        }

        if (transform.position.y < -6f)
            GetDamage();
        
    }

    private void Run()
    {
        if (isGrounded) State = States.Run;

        Vector3 dir = transform.right * joystick.Horizontal;
        transform.position = Vector3.MoveTowards(transform.position, transform.position + dir, speed * Time.deltaTime);
        sprite.flipX = dir.x < 0.0f;
    }

    private void Jump()
    {
        
            rb.velocity = Vector2.up * jumpforce;
        
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPos.position, attackRange);
    }

    private IEnumerator AttackAnimation()
    {
        yield return new WaitForSeconds(0.6f);
        isAttacking = false;
    }

    private IEnumerator AttackCoolDown()
    {
        yield return new WaitForSeconds(0.5f);
        isRecharged = true;
    }

    private bool CheckGround()
    {
        return Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);
      
    }

    public override void GetDamage()
    {
        if(!CanBeDamaged)
            return;
        CanBeDamaged = false;
        Color baseColor = makeIndamageable.color;
        float H, s, v;
        Color.RGBToHSV(baseColor, out H, out s, out v);
        Color newColor = Color.HSVToRGB(H, s, v);
        newColor.a = 0.4f; // 40% opacity (i.e., 60% less visible)
        makeIndamageable.color = newColor;


        health -= 1;
        Debug.Log("Heart Decreased");
        hitSound.Play();    
        if (health == 0)
        {
            foreach (var h in hearts)
                h.sprite = deadHeart;
            Die();
            if (health == 0)
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
        }

        StartCoroutine(resetDamage());
    }

    IEnumerator resetDamage()
    {
        yield return new WaitForSeconds(0.3f);
        CanBeDamaged = true;
        Color baseColor = makeIndamageable.color;
        float h, s, v;
        Color.RGBToHSV(baseColor, out h, out s, out v);
        Color newColor = Color.HSVToRGB(h, s, v);
        newColor.a = 1f; // 40% opacity (i.e., 60% less visible)
        makeIndamageable.color = newColor;



    }

    public void ExitMain()
    {
        SceneManager.LoadScene(0);
    }

}


public enum States
{
    Idle,
    Run,
    Jump,
    Attack
}
