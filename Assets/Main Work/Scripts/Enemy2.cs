using UnityEngine;

public class Enemy2 : MonoBehaviour
{
    [Header("Attack Settings")]
    public float attackRange = 2f;
    public float attackCooldown = 1.5f;

    [Header("References")]
    public Transform player;
    public Animator animator;
    public SpriteRenderer spriteRenderer; 

    [Header("Debug")]
    public bool isAttacking = false;          
    public bool isPlayerInAttackRange = false;  

    private float nextAttackTime = 0f;
   
    private void Update()
    {
        if (player == null) return;

      
        FacePlayer();

      
        float distanceToPlayer = Vector2.Distance(transform.position, player.position);
        isPlayerInAttackRange = distanceToPlayer <= attackRange;

       
        AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);
        isAttacking = stateInfo.IsName("attack") || stateInfo.IsTag("attack");

     
        if (isPlayerInAttackRange && Time.time >= nextAttackTime)
        {
            animator.SetTrigger("attack");
            nextAttackTime = Time.time + attackCooldown;
        }
    }

    private void FacePlayer()
    {
        if (player.position.x > transform.position.x)
        {
            // Player is to the right, face right
            spriteRenderer.flipX = false;
        }
        else if (player.position.x < transform.position.x)
        {
            // Player is to the left, face left
            spriteRenderer.flipX = true;
        }
        // If x positions are equal, maintain current facing
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}