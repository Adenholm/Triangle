using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    public float speed = 5f;
    private float moveLimiter = 0.7f;
    private bool isFacingLeft = true;
    private float nextMoveTime;

    private Rigidbody2D rb;
    private Animator animator;

    Vector2 movement;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        animator.SetFloat("Speed", movement.sqrMagnitude);
    }

    private void FixedUpdate()
    {
        if (Time.time >= nextMoveTime)
        {
            if (movement.x != 0 && movement.y != 0)
            {
                movement *= moveLimiter;
            }

            rb.MovePosition(rb.position + movement * speed * Time.fixedDeltaTime);

            if ((movement.x > 0 && isFacingLeft) || (movement.x < 0 && !isFacingLeft))
            {
                Flip();
            }
        }
    }

    public void FreezeMovement(float nextMoveTime)
    {
        this.nextMoveTime = nextMoveTime;
    }

    private void Flip()
    {
        isFacingLeft = !isFacingLeft;

        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }
}
