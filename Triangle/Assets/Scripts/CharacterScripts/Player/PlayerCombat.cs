using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour, IAttackable
{

    [Header("Attack settings")]
    public Transform attackPoint;
    public LayerMask enemyLayers;

    public Weapon weapon;
    public Element activeElement = Element.NONE;
    private List<Element> baseElementsList;
    private IElementAttack[] elementAttacks; 

    public float attakRate = 0.5f;
    private float nextAttackTime = 0f;

    public float power = 1f;

    [Header("Health")]
    public int maxHealth = 100;
    private int currentHealth;

    private Animator animator;
    private PlayerMovement pm;

    public ElementUI elementui;

    public float armourBuffer = 0.7f;

    public GameObject weaponSprite;
    private bool weaponIsEquiped = false;
    public bool armorIsEquiped = false;
    private SortRenderer sortRenderer;


    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponentInChildren<Animator>();
        pm = GetComponent<PlayerMovement>();
        elementAttacks = GetComponents<IElementAttack>();
        sortRenderer = GetComponent<SortRenderer>();

        elementui = GameObject.Find("ElementUI").GetComponent<ElementUI>();

        currentHealth = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time >= nextAttackTime && weapon != null && Input.GetKeyDown(KeyCode.Mouse0) && weaponIsEquiped)
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
        if (armorIsEquiped)
            damage = (int)(damage * armourBuffer);

        damage = (int)(damage * ElementHandler.DamageConverter(activeElement, element));
        currentHealth -= damage;
        Debug.Log(currentHealth);
        Debug.Log(currentHealth / (float)maxHealth);
        UIHealthbar.instance.SetValue(currentHealth / (float)maxHealth);

        Debug.Log("You took " + damage + " in damage");

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

    public void EquipWeapon()
    {
        weaponIsEquiped = true;
        weaponSprite.active = true;
        sortRenderer.Rerun();
    }

    public void SetElement(Element newElement)
        {
            elementui.SetBaseElementImage(newElement);
            baseElementsList = elementui.GetBaseElements();

            if( baseElementsList.Count == 2) 
            { 
            var baseElementsSet = new HashSet<Element>(baseElementsList);
            activeElement = ElementHandler.GetElement(baseElementsSet);
            elementui.SetActivElementImage(activeElement);
            }
            
        }
    }
