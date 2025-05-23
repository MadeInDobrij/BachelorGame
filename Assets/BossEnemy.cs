using UnityEngine;

public class BossEnemy : MonoBehaviour
{
    [Header("Combat Settings")]
    public float attackRange = 2f;
    public float chaseRange = 6f;
    public float moveSpeed = 2f;
    public float attackCooldown = 2f;
    public int maxHealth = 100;

    [Header("References")]
    public Transform player;
    public Animator animator;

    [Header("Debug Info")]
    public bool isAttacking = false;
    public bool playerInRange = false;

    private int currentHealth;
    private float nextAttackTime = 0f;
    private bool isDead = false;
    private Rigidbody2D rb;
    public AttackSystem asS;
    public GameObject finalBossPanel;
    public GameObject playerCanvas;
    private void Start()
    {
        currentHealth = maxHealth;
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (isDead || player == null) return;

        float distance = Vector2.Distance(transform.position, player.position);
        AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);
        isAttacking = stateInfo.IsName("attack") || stateInfo.IsTag("attack");
        playerInRange = distance <= attackRange;

        FacePlayer();

        if (distance <= attackRange)
        {
            rb.velocity = Vector2.zero;
            animator.SetBool("isWalking", false);

            if (Time.time >= nextAttackTime && !isAttacking)
            {
                animator.SetTrigger("attack");
                OnAttackPlayer();
                nextAttackTime = Time.time + attackCooldown;
            }
        }
        else if (distance <= chaseRange)
        {
            // Walk toward player
            Vector2 direction = (player.position - transform.position).normalized;
            rb.velocity = new Vector2(direction.x * moveSpeed, rb.velocity.y);
            animator.SetBool("isWalking", true);
        }
        else
        {
            rb.velocity = Vector2.zero;
            animator.SetBool("isWalking", false);
        }
    }

    private void FacePlayer()
    {
        if (player.position.x < transform.position.x)
            transform.localScale = new Vector3(-1, 1, 1); // face left
        else
            transform.localScale = new Vector3(1, 1, 1);  // face right
    }

    public void TakeDamage(int damage)
    {
        if (isDead) return;

        currentHealth -= damage;
        Debug.Log("Enemy3 took damage! Current HP: " + currentHealth);
        animator.SetTrigger("damage");

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        isDead = true;
        rb.velocity = Vector2.zero;
        animator.SetTrigger("death");
        animator.SetBool("isWalking", false);
        Debug.Log("Enemy3 has died!");
        
        finalBossPanel.SetActive(true);
        playerCanvas.SetActive(false);
    }

    // Call from animation event
    public void OnAttackPlayer()
    {
        Debug.Log("Enemy3 attacked the player!");
        // Implement player damage logic here
        asS.DamageByEnemy1(true,true);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, chaseRange);
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}
