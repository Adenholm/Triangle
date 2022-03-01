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

    void Start()
    {
        animator = GetComponentInChildren<Animator>();
        currentHealth = maxHealth;
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

        //Disable
        Destroy(gameObject, 3f);
    }
}
