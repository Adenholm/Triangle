using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    private Animator animator;
    private PlayerMovement pm;

    public Transform attackPoint;
    public LayerMask enemyLayers;

    private IElementAttack elementAttack;

    public float attackRange = 0.5f;
    public int attackDamage = 40;

    public float attakRate = 0.5f;
    private float nextAttackTime = 0f;



    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        pm = GetComponent<PlayerMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time >= nextAttackTime)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                Attack();
                nextAttackTime = Time.time + 1f / attakRate;
                pm.FreezeMovement(nextAttackTime);
            }
            if (Input.GetKeyDown(KeyCode.LeftShift) && elementAttack.Equals(null))
            {
                elementAttack.Attack(attackDamage);
                nextAttackTime = Time.time + 1f / attakRate;
                pm.FreezeMovement(nextAttackTime);
            }
        }
    }

    private void Attack()
    {
        animator.SetTrigger("Attack");

        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);

        foreach(Collider2D enemy in hitEnemies)
        {
            enemy.GetComponent<IHealth>().TakeDamage(attackDamage, Element.NONE);
        }
    }

    private void OnDrawGizmosSelected()
    {
        if (attackPoint == null) return;

        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
}
