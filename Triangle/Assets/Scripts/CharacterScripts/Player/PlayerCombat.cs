using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour, IAttackable
{

    [Header("Attack settings")]
    public Transform attackPoint;
    public LayerMask enemyLayers;

    public Weapon weapon;
    public Element activeElement = Element.NONE;
    private IElementAttack[] elementAttacks; 

    public float attakRate = 0.5f;
    private float nextAttackTime = 0f;

    public float power = 1f;

    [Header("Helath")]
    public int maxHealth = 100;
    private int currentHealth;

    private Animator animator;
    private PlayerMovement pm;


    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponentInChildren<Animator>();
        pm = GetComponent<PlayerMovement>();
        elementAttacks = GetComponents<IElementAttack>();

        currentHealth = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time >= nextAttackTime && weapon != null && Input.GetKeyDown(KeyCode.Mouse0))
        {
            Attack();
            nextAttackTime = Time.time + 1f / attakRate;
            pm.FreezeMovement(nextAttackTime);
            
        }
        if (Input.GetKeyDown(KeyCode.Mouse1) && activeElement != Element.NONE)
        {
            elementAttacks[(int)activeElement].Attack(power, enemyLayers);
        }
    }

    private void Attack()
    {
        animator.SetTrigger("Attack");

        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, weapon.attackRange, enemyLayers);

        foreach(Collider2D enemy in hitEnemies)
        {
            enemy.GetComponent<IAttackable>().TakeDamage(weapon.attackDamage, weapon.element);
        }
    }


    public void TakeDamage(int damage, Element element)
    {
        Debug.Log("You took " + damage + " in damage");
        damage = (int)ElementHandler.DamageConverter(activeElement, element);
        currentHealth -= damage;

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        Debug.Log("You died");
        //Die animation
    }
    private void OnDrawGizmosSelected()
    {
        if (attackPoint == null || weapon == null) return;

        Gizmos.DrawWireSphere(attackPoint.position, weapon.attackRange);
    }
}
