using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class EnemyAi : MonoBehaviour
{
    public Rigidbody2D rb;
    public Transform target;
    public int forgetDistance = 15;             // The distance from target where the enemy stops attacking and returns to its spawn
    public int attackDistance = 8;              // The distance from the player where it starts attacking
    public int aStarActivationDistance = 6;     // The distance from the target that the pathfinding gets activated again
    public int reachedTargetDistance = 3;

    public int maxDistanceFromSpawn = 50;       // The max Distance the enemy can be before it returns to it's spawn
    public int spawnRadius = 10;

    public bool drawGizmos = false;

    private bool reachedTarget = false;
    private bool isAttacking = false;
    private bool atSpawnArea = true;
    private Vector2 spawnpoint;

    private float nextMoveTime;
    private bool isFacingLeft = true;

    private Animator animator;
    private PathDestinationSetter destinationSetter;
    private AIPath aiPath;
    private EnemyAttackAi attackAi;

    void Start()
    {
        animator = GetComponentInChildren<Animator>();
        destinationSetter = GetComponent<PathDestinationSetter>();
        aiPath = GetComponent<AIPath>();
        attackAi = GetComponent<EnemyAttackAi>();

        spawnpoint = rb.position;
    }

    void FixedUpdate()
    {
        if (Time.time >= nextMoveTime)
        {
            Vector2 pos = rb.position;

            float distanceFromTarget = Vector2.Distance(pos, target.position);
            float distanceFromSpawn = Vector2.Distance(pos, spawnpoint);

            atSpawnArea = distanceFromSpawn < spawnRadius;

            float speed = rb.velocity.x;
            float dir = target.position.x - pos.x;

            if (!atSpawnArea && (distanceFromSpawn > maxDistanceFromSpawn || distanceFromTarget > forgetDistance))
            {
                ReturnToSpawn();
                speed = aiPath.desiredVelocity.x * 10;
            }
            else if (!isAttacking && distanceFromTarget < attackDistance)
            {
                animator.SetTrigger("Startle");
                FreezeMovement(Time.time + 1f);
                isAttacking = true;
                reachedTarget = true;
            }
            else if (isAttacking && reachedTarget && distanceFromTarget > aStarActivationDistance && distanceFromTarget < forgetDistance)
            {
                MoveToTarget();
                speed = Mathf.Abs(aiPath.desiredVelocity.x * 10) * Mathf.Sign(dir);
            }
            else if (reachedTarget && distanceFromTarget < aStarActivationDistance)
            {
                MoveAroundTarget();
                speed = Mathf.Abs(speed) * Mathf.Sign(dir);
            }
            else if(distanceFromTarget < reachedTargetDistance)
            {
                reachedTarget = true;
            }
            else if (distanceFromSpawn < spawnRadius)
            {
                IdleMovement();
            }

            animator.SetFloat("Speed", Mathf.Abs(speed));

            if ((speed > 0.1f && isFacingLeft) || (speed < -0.1f && !isFacingLeft))
            {
                Flip();
            }
        }
    }


    void MoveToTarget()
    {
        reachedTarget = false;
        destinationSetter.targetPosition = Vector2.zero;
        destinationSetter.target = target;
        aiPath.enabled = true;
    } 
    void MoveAroundTarget()
    {
        aiPath.enabled = false;
        attackAi.Strafe(rb, target);
    }

    void ReturnToSpawn()
    {
        destinationSetter.targetPosition = spawnpoint;
        aiPath.enabled = true;
        isAttacking = false;
        reachedTarget = false;
    }

    void IdleMovement()
    {
        destinationSetter.targetPosition = Vector2.zero;
        aiPath.enabled = false;
        rb.velocity = Vector2.zero;
        reachedTarget = false;
    }

    private void Flip()
    {
        isFacingLeft = !isFacingLeft;

        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }

    public void FreezeMovement(float nextMoveTime)
    {
        this.nextMoveTime = nextMoveTime;
    }

    private void OnDrawGizmosSelected()
    {
        if (drawGizmos)
        {
            Gizmos.DrawWireSphere(spawnpoint, maxDistanceFromSpawn);
            Gizmos.DrawWireSphere(spawnpoint, spawnRadius);
            Gizmos.DrawWireSphere(rb.position, aStarActivationDistance);
            Gizmos.DrawWireSphere(rb.position, forgetDistance);
            Gizmos.DrawWireSphere(rb.position, attackDistance);
        }
    }
}
