using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EnemyAttackAi : MonoBehaviour
{
    public Transform attackPoint;
    public float moveSpeed = 3f;

    public float attackRange = 0.5f;
    public int attackDamage = 40;

    public float attakRate = 0.5f;
    private float nextAttackTime = 0f;
    private float nextMoveTime = 0f;

    public LayerMask enemyLayers;

    public float obstacleDetectionRange = 3f;
    public LayerMask obstacleLayers;


    [Header("Testing stuff")]
    public Transform target;
    public Rigidbody2D rb;
    private Collider2D ownCollider;
    
    public int targetAngle;

    public Dictionary<int, int> movePattern = new Dictionary<int, int>
    {
        {6, 0},
        {5, 1},
        {4, 3},
        {3, 5},
        {2, 9},
        {1, 5},
        {0, 3},
        {11, 5},
        {10, 18},
        {9, 5},
        {8, 3},
        {7, 1}
    };
    private Dictionary<int, int> obstacleAvoiding = new Dictionary<int, int>();
    private Dictionary<int, int> moveSum = new Dictionary<int, int>();

    private Animator animator;

    private void Start()
    {
        animator = GetComponentInChildren<Animator>();
        ownCollider = GetComponentInChildren<Collider2D>();

        obstacleAvoiding.Add(0, 0);
        obstacleAvoiding.Add(1, 1);
        obstacleAvoiding.Add(2, 2);
        obstacleAvoiding.Add(3, 3);
        obstacleAvoiding.Add(4, 9);
        obstacleAvoiding.Add(5, 2);
        obstacleAvoiding.Add(6, 0);
        obstacleAvoiding.Add(7, 2);
        obstacleAvoiding.Add(8, 15);
        obstacleAvoiding.Add(9, 3);
        obstacleAvoiding.Add(10, 2);
        obstacleAvoiding.Add(11, 1);
    }
    public void Strafe(Rigidbody2D rb, Transform target)
    {
        if (Time.time >= nextMoveTime)
        {

            Vector2 pos = rb.position;

            Vector2 targetDirection = (Vector2)target.position - pos;
            targetAngle = ((int)Mathf.Round(Mathf.Atan2(targetDirection.y, targetDirection.x) * Mathf.Rad2Deg / 30) + 6) % 12;

            foreach (int angle in movePattern.Keys)
            {
                moveSum[angle] = movePattern[(targetAngle - angle + 12) % 12];
            }

            Collider2D[] nearbyObstacles = Physics2D.OverlapCircleAll(pos, obstacleDetectionRange, obstacleLayers);

            foreach (Collider2D obstacle in nearbyObstacles)
            {
                if (obstacle != ownCollider)
                {
                    Vector2 obstacleDirection = obstacle.ClosestPoint(pos) - pos;
                    int obstacleAngle = ((int)Mathf.Round(Mathf.Atan2(obstacleDirection.y, obstacleDirection.x) * Mathf.Rad2Deg / 30) + 6) % 12;
                    foreach (int angle in obstacleAvoiding.Keys)
                    {
                        moveSum[angle] += obstacleAvoiding[(obstacleAngle - angle + 12) % 12];
                    }
                }
            }

            var sortedElements = moveSum.OrderBy(kvp => kvp.Value);

            int movementAngle = sortedElements.Last().Key;
            Vector2 movement = (Quaternion.Euler(0, 0, movementAngle * 30) * new Vector3(-1, 0)) * moveSpeed;

            rb.AddForce(movement);

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
        Vector3 pos = new Vector3(rb.position.x, rb.position.y);
        foreach(int angle in moveSum.Keys)
        {
            Gizmos.DrawLine(pos, pos + (Quaternion.Euler(0, 0, angle * 30 - 180) * new Vector3(moveSum[angle]/3 , 0)));
        }
        //Gizmos.DrawLine(pos, pos + (Quaternion.Euler(0, 0, targetAngle*30) * new Vector3(-1,0)));
    }
}
