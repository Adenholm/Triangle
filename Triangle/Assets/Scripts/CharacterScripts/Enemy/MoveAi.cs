using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

/*
 * A enemy ai-movement script that defines the movement behavoir for enemies when they get close to the player.
 * It uses a map of different directions with different wheights that represents the way the enemy wants to move in.
 * It then checks if there's a obstacle or other enmey in the way and then chooses the best way to go.
 * 
 * Author Hanna Adenholm
 */
public class MoveAi : MonoBehaviour
{
    public float obstacleDetectionRange = 3f;
    public LayerMask obstacleLayers;

    public bool drawGizmos = false;

    private Rigidbody2D rb;
    private Collider2D ownCollider;

    private Dictionary<int, int> moveSum = new Dictionary<int, int>();
    private Dictionary<int, int> obstacleAvoidingPattern = new Dictionary<int, int>
    {
        {6, 0},
        {5, 2},
        {4, 9},
        {3, 3},
        {2, 2},
        {1, 1},
        {0, 0},
        {11, 1},
        {10, 2},
        {9, 3},
        {8, 12},
        {7, 2}
    };

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        ownCollider = GetComponentInChildren<Collider2D>();
    }

    public void Move(Transform target, Dictionary<int, int> movePattern, float speed)
    {
        Vector2 pos = rb.position;

        Vector2 targetDirection = (Vector2)target.position - pos;
        int targetAngle = ((int)Mathf.Round(Mathf.Atan2(targetDirection.y, targetDirection.x) * Mathf.Rad2Deg / 30) + 6) % 12;

        foreach (int angle in movePattern.Keys)
        {
            moveSum[angle] = movePattern[(targetAngle - angle + 12) % 12];
        }

        Collider2D[] nearbyObstacles = Physics2D.OverlapCircleAll(pos, obstacleDetectionRange, obstacleLayers);

        foreach (Collider2D obstacle in nearbyObstacles)
        {
            if (obstacle != ownCollider)
            {
                Vector2 obstacleDirection = obstacle.ClosestPoint(pos) - pos;
                int obstacleAngle = ((int)Mathf.Round(Mathf.Atan2(obstacleDirection.y, obstacleDirection.x) * Mathf.Rad2Deg / 30) + 6) % 12;
                foreach (int angle in obstacleAvoidingPattern.Keys)
                {
                    moveSum[angle] += obstacleAvoidingPattern[(obstacleAngle - angle + 12) % 12];
                }
            }
        }

        var sortedElements = moveSum.OrderBy(kvp => kvp.Value);

        int movementAngle = sortedElements.Last().Key;
        Vector2 movement = (Quaternion.Euler(0, 0, movementAngle * 30) * new Vector3(-1, 0)) * speed;

        rb.AddForce(movement);
    }

    private void OnDrawGizmosSelected()
    {
        if (drawGizmos)
        {
            Vector3 pos = new Vector3(rb.position.x, rb.position.y);
            foreach (int angle in moveSum.Keys)
            {
                Gizmos.DrawLine(pos, pos + (Quaternion.Euler(0, 0, angle * 30 - 180) * new Vector3(((float)moveSum[angle]) / 3, 0)));
            }
            //Gizmos.DrawLine(pos, pos + (Quaternion.Euler(0, 0, targetAngle*30) * new Vector3(-1,0)));
        }
    }
}
