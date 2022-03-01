using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rock : MonoBehaviour, IAttackable
{
    public Health health;
    public GameObject particleEffect;


    public void TakeDamage(int damage, Element element)
    {
        if(element == Element.NONE)
        {
                health.TakeDamage(damage);
                spawnParticle(Instantiate(particleEffect, this.transform));
        }
    }

    private GameObject spawnParticle(GameObject particles)
    {
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
