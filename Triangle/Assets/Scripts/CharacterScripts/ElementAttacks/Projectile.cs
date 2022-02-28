using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public Element element;
    public int damage = 10;
    public GameObject hitEffect;
    public LayerMask enemyLayers;
    private void OnCollisionEnter2D(Collision2D collision)
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
