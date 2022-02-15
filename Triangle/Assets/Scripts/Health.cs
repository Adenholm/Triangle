using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* 
 * A Class that keeps count of the health and plays an items hurt and die animations and drops items when they die
 */
public class Health : MonoBehaviour
{
    private Animator animatior;

    public int maxHealth = 100;
    private int currentHealth;

    void Start()
    {
        animatior = GetComponentInChildren<Animator>();
        currentHealth = maxHealth;
    }

    public void TakeDamage(int damage, Element element)
    {
        currentHealth -= damage;

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        //TODO 

        Debug.Log("Enemy died");
        //Die animation

        //Drop Items

        //Disable
    }
}
