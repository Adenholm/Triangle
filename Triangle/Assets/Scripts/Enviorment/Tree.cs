using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tree : MonoBehaviour, IAttackable
{
    public Health health;

    private int timesDamageIsTaken = 0;
    private int damageTakenOverTime = 0;
    public GameObject fireParticles;
    public GameObject startFireParticles;

    public void TakeDamage(int damage, Element element)
    {
        if (element == Element.NONE)
        {
            health.TakeDamage(damage);
        }
        else if(element == Element.FIRE || element == Element.FIREWALL)
        {
            spawnParticle(Instantiate(startFireParticles, this.transform));
            timesDamageIsTaken = 5;
            damageTakenOverTime = damage;
            StartCoroutine("Burn");
        }
    }

    IEnumerator Burn()
    {
        for (int i = 0; i < timesDamageIsTaken; i++)
        {
            health.TakeDamage(damageTakenOverTime);
            spawnParticle(Instantiate(fireParticles, this.transform));
            yield return new WaitForSeconds(2f);
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
