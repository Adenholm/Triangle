using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* 
 * A Class that keeps count of the health and plays an items hurt and die animations and drops items when they die
 */
public class Health : MonoBehaviour
{


    private Animator animator;

    public int maxHealth = 100;
    private int currentHealth;
    private Inventory inventory;

    public Item.ItemType itemType0;
    public int amount0;

    public Item.ItemType itemType1;
    public int amount1;


    void Start()
    {
        animator = GetComponentInChildren<Animator>();
        currentHealth = maxHealth;
        inventory = new Inventory();

        Item item0 = new Item { itemType = itemType0, amount = amount0 };
        Item item1 = new Item { itemType = itemType1, amount = amount1 };

        inventory.AddItem(item0);
        inventory.AddItem(item1);

    }

    public void TakeDamage(int damage)
    {
        Debug.Log("Enemy took damage");
        currentHealth -= damage;
        if (animator != null)
            animator.SetTrigger("Hurt");

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        //TODO 

        Debug.Log("Enemy died");
        if(animator != null)
            animator.SetTrigger("Die");
        //Die animation

        //Drop Items
        Vector3 position = gameObject.transform.position;
        inventory.DropItems(position);
        Destroy(gameObject);

        //Disable
        Destroy(gameObject, 5f);
    }
}
