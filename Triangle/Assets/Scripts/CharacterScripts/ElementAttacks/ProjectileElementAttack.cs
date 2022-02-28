using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileElementAttack : MonoBehaviour, IElementAttack
{
    public Element element;

    public Transform firePoint;
    public float projectileForce = 20f;
    public float cooldownTime = 5f;
    public List<GameObject> particleEffects;

    public Camera cam;
    public GameObject projectilePrefab;

    private float nextAttackTime;

    private Animator animator;
    private Rigidbody2D rb;

    private Vector2 mousePos;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponentInChildren<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }
    
    public void Attack(float power, LayerMask enemyLayers)
    {
        if (Time.time > nextAttackTime)
        {
            mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
            animator.SetTrigger("ElementAttack");

            Vector2 shootDir = mousePos - (Vector2)firePoint.position;
            float angle = Mathf.Atan2(shootDir.y,shootDir.x) * Mathf.Rad2Deg;

            GameObject projectile = Instantiate(projectilePrefab, firePoint.position, firePoint.rotation);
            Rigidbody2D prb = projectile.GetComponent<Rigidbody2D>();

            Vector2 movement = (Quaternion.Euler(0, 0, angle) * new Vector3(1, 0)) * projectileForce;

            prb.AddForce(movement, ForceMode2D.Impulse);
        }
    }
}
