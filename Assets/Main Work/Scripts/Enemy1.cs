using UnityEngine;

public class Enemy1 : MonoBehaviour
{
    [Header("Patrol Settings")]
    public float leftX;
    public float rightX;
    public float moveSpeed = 2f;

    [Header("Attack Settings")]
    public float attackRange = 2f;
    public float attackCooldown = 1.5f;

    [Header("References")]
    public Transform player;
    public Animator animator;

    [SerializeField] private bool movingRight = true;
    private float nextAttackTime;

    [Header("Debug")]
    public bool isAttacking = false;
    public bool isPlayerInAttackRange = false;
    public AttackSystem attackSystem;
    private void Update()
    {
        if (player == null) return;

       
        AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);
        isAttacking = stateInfo.IsTag("attack"); 

        float distanceToPlayer = Vector2.Distance(transform.position, player.position);

       
        isPlayerInAttackRange = distanceToPlayer <= attackRange;

        if (isPlayerInAttackRange)
        {
            animator.SetBool("isWalking", false);

            if (Time.time >= nextAttackTime)
            {
                Attack();
                nextAttackTime = Time.time + attackCooldown;
            }

            FaceTarget(player.position);
        }
        else
        {
            Patrol();
        }
    }

    private void Patrol()
    {
        animator.SetBool("isWalking", true);

        Vector3 pos = transform.position;

        if (movingRight)
        {
            pos.x += moveSpeed * Time.deltaTime;
            if (pos.x >= rightX)
            {
                pos.x = rightX;
                movingRight = false;
            }
        }
        else
        {
            pos.x -= moveSpeed * Time.deltaTime;
            if (pos.x <= leftX)
            {
                pos.x = leftX;
                movingRight = true;
            }
        }

        transform.position = pos;

        transform.localScale = new Vector3(movingRight ? 1 : -1, 1, 1);
    }

    private void FaceTarget(Vector3 targetPosition)
    {
        if (targetPosition.x < transform.position.x)
            transform.localScale = new Vector3(-1, 1, 1); // Face left
        else
            transform.localScale = new Vector3(1, 1, 1);  // Face right
    }

    private void Attack()
    {
        animator.SetTrigger("attack");
    

        attackSystem.DamageByEnemy1(isAttacking, isPlayerInAttackRange);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}
