using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrdinaryElemAttack : MonoBehaviour, IElementAttack
{
    public Element element;
    
    public Transform attackPoint;
    public float attackRange = 1f;
    public int attackDamage = 40;
    public float cooldownTime = 5f;
    public List<GameObject> particleEffects;

    private float nextAttackTime;

    private Animator animator;
    private Rigidbody2D rb;


    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponentInChildren<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    public void Attack(float power, LayerMask enemyLayers)
    {
        if (Time.time > nextAttackTime)
        {
            animator.SetTrigger("ElementAttack");

            Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);

            foreach (Collider2D enemy in hitEnemies)
            {
                enemy.GetComponent<IAttackable>().TakeDamage(attackDamage, element);
            }

            foreach (GameObject effect in particleEffects)
            {
                spawnParticle(Instantiate(effect));
            }
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
}
