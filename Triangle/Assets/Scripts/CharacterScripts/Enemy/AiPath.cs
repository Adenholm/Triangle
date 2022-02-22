using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

/*
 * A class that moves the enemy along the path that the seeker scripts generates
 * It's based on Brackeys enemy ptahfinding tutorial: https://www.youtube.com/watch?v=jvtFUfJ6CP8
 * Although there are some modifications
 * 
 * @Author Hanna Adenholm
 */
public class AiPath : MonoBehaviour
{
    private Transform target;
    private Vector3 targetVector;

    public float speed = 600f;
    public float nextWaypointDistance = 3f;
    public int endReachedDistance = 3;

    private Path path;
    private int currentWaypoint = 0;

    private Seeker seeker;
    private Rigidbody2D rb;


    void Start()
    {
        seeker = GetComponent<Seeker>();
        rb = GetComponent<Rigidbody2D>();

        InvokeRepeating("UpdatePath", 0f, 0.5f);
    }

    public void moveTowardsTarget(Transform newTarget)
    {
        
        target = newTarget;
        targetVector = Vector3.zero;

    }

    public void moveTowardsTarget(Vector3 newTarget)
    {
        targetVector = newTarget;
        target = null;
    }

    public void stopMovingTowardsTarget()
    {
         targetVector = Vector3.zero;
         target = null;
         path = null;
      
    }

    void UpdatePath()
    {
        if (seeker.IsDone())
        {
            if (target != null)
                seeker.StartPath(rb.position, target.position, OnPathComplete);

            else if (targetVector != Vector3.zero)
                seeker.StartPath(rb.position, targetVector, OnPathComplete);
        }
    }

    void OnPathComplete(Path p)
    {
        if (!p.error)
        {
            path = p;
            currentWaypoint = 0;
        }
    }


    void FixedUpdate()
    {
        if (path == null || currentWaypoint >= path.vectorPath.Count - endReachedDistance)
            return;

        Vector2 direction = ((Vector2)path.vectorPath[currentWaypoint] - rb.position).normalized;
        Vector2 force = direction * speed * Time.fixedDeltaTime;

        rb.AddForce(force);

        float distance = Vector2.Distance(rb.position, path.vectorPath[currentWaypoint]);

        if (distance < nextWaypointDistance)
        {
            currentWaypoint++;
        }
    }
}
