using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour, IAttackable
{
    [Header("Battle")]
    public Element element;
    public LayerMask attackTargets;
    public LayerMask fellowEnemies;

    private Transform target;

    [Header("Trigger zones")]
    public int detectionradius = 8;
    public int abandonDistance = 15;
    public int pathfindingActivationDistance = 6;   // The distance from the target that the pathfinding gets activated again
    public int reachedTargetDistance = 3;           // The distance from the target where the attackai gets activated

    private bool reachedTarget = false;

    [Header("Spawn Area")]
    public int spawnRadius = 10;
    public int maxDistanceFromSpawn = 50;

    private bool atSpawnArea = true;
    private Vector2 spawnpoint;

    [Header("Gizmos")]
    public bool drawGizmos = false;

    private float nextMoveTime;
    private bool isFacingLeft = true;


    [Header("Particle Effects")]
    public GameObject poisonPartciels;
    public GameObject fireParticles;
    public GameObject lightningParticles;
    public GameObject waterParticles;
    public GameObject iceParticles;
    public GameObject plantParticles;
    public GameObject steamParticles;
    public GameObject hitParticles;

    private int timesDamageIsTaken = 0;
    private int damageTakenOverTime = 0;
    private bool isAlreadyStruck = false;
    private GameObject damagingParticles;


    private Rigidbody2D rb;
    private Animator animator;
    private Health health;
    private AiPath aiPath;
    private EnemyAttackAi attackAi;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponentInChildren<Animator>();
        health = GetComponent<Health>();
        aiPath = GetComponent<AiPath>();
        attackAi = GetComponent<EnemyAttackAi>();

        spawnpoint = rb.position;
    }

    // Update is called once per frame
    void Update()
    {
        Collider2D collider = Physics2D.OverlapCircle(rb.position, detectionradius, attackTargets);
        if (collider != null)
        {
            if (target != collider.transform)
            {
                animator.SetTrigger("Startle");
                FreezeMovement(Time.time + 1f);
            }
            target = collider.transform;
        }
    }

    void FixedUpdate()
    {
        if(Time.time < nextMoveTime)
        {
            rb.velocity = Vector2.zero;
            return;
        }

        Vector2 pos = rb.position;

        float distanceFromSpawn = Vector2.Distance(pos, spawnpoint);
        atSpawnArea = distanceFromSpawn < spawnRadius;
  
        float speed = rb.velocity.x;

        if (target != null)
        {
            float distanceFromTarget = Vector2.Distance(pos, target.position);
            float dir = target.position.x - pos.x;

            if (!atSpawnArea && (distanceFromTarget > abandonDistance || distanceFromSpawn > maxDistanceFromSpawn))
            {
                ReturnToSpawn();
            }
            else if (distanceFromTarget < reachedTargetDistance)
            {
                reachedTarget = true;
                aiPath.stopMovingTowardsTarget();
                attackAi.Strafe(rb, target);
                speed = Mathf.Abs(speed) * Mathf.Sign(dir);
            }
            else if(reachedTarget && distanceFromTarget < pathfindingActivationDistance)
            {
                attackAi.Strafe(rb, target);
                speed = Mathf.Abs(speed) * Mathf.Sign(dir);
            }
            else
            {
                reachedTarget = false;
                aiPath.moveTowardsTarget(target);
            }
        }
        else if (!atSpawnArea) ReturnToSpawn();
        else IdleMovement();

        
        animator.SetFloat("Speed", Mathf.Abs(speed));

        if ((speed > 0.1f && isFacingLeft) || (speed < -0.1f && !isFacingLeft))
        {
            Flip();
        }

        
    }

    private void IdleMovement()
    {
        aiPath.stopMovingTowardsTarget();
    }

    private void ReturnToSpawn()
    {
        target = null;
        aiPath.moveTowardsTarget(spawnpoint);
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

    public void TakeDamage(int damage, Element element)
    {
        switch (element)
        {
            case Element.POISON:
                damagingParticles = poisonPartciels;
                timesDamageIsTaken = (int)(5 * ElementHandler.DamageConverter(this.element, element));
                damageTakenOverTime = (int)(damage * ElementHandler.DamageConverter(this.element, element));
                StartCoroutine("DamageOverTime");
                break;
            case Element.FIRE:
            case Element.FIREWALL:
                damagingParticles = fireParticles;
                timesDamageIsTaken = (int)(5 * ElementHandler.DamageConverter(this.element, element));
                damageTakenOverTime = (int)(damage * ElementHandler.DamageConverter(this.element, element));
                StartCoroutine("DamageOverTime");
                break;

            case Element.LIGHTNING:
                if (!isAlreadyStruck)
                {
                    spawnParticle(Instantiate(lightningParticles));
                    health.TakeDamage((int)(damage * ElementHandler.DamageConverter(this.element, element)));

                    Collider2D[] hitFellowEnemies = Physics2D.OverlapCircleAll(rb.position, 2f, fellowEnemies);
                    foreach (Collider2D enemy in hitFellowEnemies)
                    {
                        enemy.GetComponent<IAttackable>().TakeDamage(damage, Element.LIGHTNING);
                    }
                    isAlreadyStruck = true;
                }
                break;

            case Element.ICE:
                health.TakeDamage((int)(damage * ElementHandler.DamageConverter(this.element, element)));
                FreezeMovement(Time.time + 3f * ElementHandler.DamageConverter(this.element, element));
                spawnParticle(Instantiate(iceParticles));
                break;

            case Element.PLANTTHROW:
            case Element.PLANT:
                health.TakeDamage((int)(damage * ElementHandler.DamageConverter(this.element, element)));
                FreezeMovement(Time.time + 3f * ElementHandler.DamageConverter(this.element, element));
                spawnParticle(Instantiate(plantParticles));
                break;

            case Element.STEAM:
                health.TakeDamage((int)(damage * ElementHandler.DamageConverter(this.element, element)));
                spawnParticle(Instantiate(steamParticles));
                break;

            case Element.WATER:
                health.TakeDamage((int)(damage * ElementHandler.DamageConverter(this.element, element)));
                spawnParticle(Instantiate(waterParticles));
                break;

            default:
                health.TakeDamage((int)(damage * ElementHandler.DamageConverter(this.element, element)));
                spawnParticle(Instantiate(hitParticles));
                break;
        }
    }



    IEnumerator DamageOverTime()
    {
        for(int i = 0; i < timesDamageIsTaken; i++)
        {
            health.TakeDamage(damageTakenOverTime);
            spawnParticle(Instantiate(damagingParticles));
            yield return new WaitForSeconds(2f);
        }
    }

    private GameObject spawnParticle(GameObject particles)
    {
        particles.transform.SetParent(this.transform);
        particles.transform.position = rb.position;

        particles.SetActive(true);

        ParticleSystem ps = particles.GetComponent<ParticleSystem>();

        if (ps != null)
        {
            var main = ps.main;
            if (main.loop)
            {
                ps.gameObject.AddComponent<CFX_AutoStopLoopedEffect>();
                ps.gameObject.AddComponent<CFX_AutoDestructShuriken>();
            }
        }

        return particles;
    }

    private void OnDrawGizmosSelected()
    {
        if (drawGizmos)
        {
            Gizmos.DrawWireSphere(spawnpoint, maxDistanceFromSpawn);
            Gizmos.DrawWireSphere(spawnpoint, spawnRadius);
            Gizmos.DrawWireSphere(rb.position, pathfindingActivationDistance);
            Gizmos.DrawWireSphere(rb.position, abandonDistance);
            Gizmos.DrawWireSphere(rb.position, detectionradius);
        }
    }
}
