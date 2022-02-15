using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour, IHealth
{
    private Animator animatior;
    private Health health;

    public Element element;

    // Start is called before the first frame update
    void Start()
    {
        animatior = GetComponentInChildren<Animator>();
        health = GetComponent<Health>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TakeDamage(int damage, Element element)
    {
        health.TakeDamage(damage, element);
    }

    private void Die()
    {
        Debug.Log("Enemy died");
        //Die animation

        //Drop Items

        //Disable
    }
}
