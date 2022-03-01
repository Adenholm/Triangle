using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public Element element;
    public int damage = 10;
    public GameObject hitEffect;
    public LayerMask enemyLayers;

    private void Start()
    {
        Destroy(this.gameObject, 4f);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            Physics2D.IgnoreCollision(collision.gameObject.GetComponent<Collider2D>(), this.GetComponent<Collider2D>());
        }
        else
        {
            GameObject effect = Instantiate(hitEffect, transform.position, Quaternion.identity);
            Destroy(effect, 5f);
            Destroy(gameObject);

            if (enemyLayers == (enemyLayers.value| 1 << collision.gameObject.layer))
            {
                collision.gameObject.GetComponent<IAttackable>().TakeDamage(damage, element);
            }
        }
    }
}
