using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/*
 * A enemy ai-movement script that defines the movement behavoir for enemies when they get close to the player.
 * It uses a map of different directions with different wheights that represents the way the enemy wants to move in.
 * It then checks if there's a obstacle or other enmey in the way and then chooses the best way to go.
 * 
 * Author Hanna Adenholm
 */
public class EnemyAttackAi : MonoBehaviour
{
    public Transform attackPoint;
    public float moveSpeed = 15f;

    public float attackRange = 0.5f;
    public int attackDamage = 40;

    public float attakRate = 0.5f;
    private float nextAttackTime = 0f;
    private float nextMoveTime = 0f;

    public LayerMask enemyLayers;

    public Dictionary<int, int> movePattern = new Dictionary<int, int>
    {
        {6, 0},
        {5, 1},
        {4, 3},
        {3, 5},
        {2, 10},
        {1, 5},
        {0, 3},
        {11, 5},
        {10, 13},
        {9, 5},
        {8, 3},
        {7, 1}
    };

    private Animator animator;
    private Rigidbody2D rb;
    private MoveAi moveAi;
    private AudioSource audioSource;

    private void Start()
    {
        animator = GetComponentInChildren<Animator>();
        rb = GetComponent<Rigidbody2D>();
        moveAi = GetComponent<MoveAi>();
        audioSource = GetComponent<AudioSource>();
    }
    public void Strafe(Rigidbody2D rb, Transform target)
    {
        if (Time.time >= nextMoveTime)
        {
            moveAi.Move(target, movePattern, moveSpeed);

            if (Time.time >= nextAttackTime)
            {
                Attack();
            }
        }
    }

    private void Attack()
    {
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);

        if (hitEnemies.Length != 0)
        {
            audioSource.Play();

            animator.SetTrigger("Attack");
            rb.velocity = Vector3.zero;
            nextAttackTime = Time.time + 1f * attakRate;
            nextMoveTime = Time.time + 1f;

            foreach (Collider2D enemy in hitEnemies)
            {
                enemy.GetComponent<IAttackable>().TakeDamage(attackDamage, Element.NONE);
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        if (attackPoint == null) return;

        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
}
