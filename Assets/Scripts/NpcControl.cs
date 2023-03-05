using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCMovement : MonoBehaviour
{
    public Transform[] waypoints;
    public float maxMoveSpeed = 2f;
    private int currentWaypointIndex = 0;
    private int previousWaypointIndex = 0;
    private float moveSpeed;

    private void Start()
    {
        if (waypoints.Length == 0)
        {
            Debug.LogError("No waypoints found!");
            return;
        }

        transform.position = waypoints[Random.Range(0,waypoints.Length)].position;
    }

    private void Update()
    {
        if (waypoints.Length == 0)
        {
            return;
        }

        MoveToNextWaypoint();
    }

    private void MoveToNextWaypoint()
    {
        Vector2 targetDirection = ((Vector2)waypoints[currentWaypointIndex].position - (Vector2)transform.position).normalized;

        moveSpeed = NextFloat(2, maxMoveSpeed);
        transform.Translate(targetDirection * moveSpeed * Time.deltaTime);

        if (Vector2.Distance(transform.position, waypoints[currentWaypointIndex].position) < 0.1f)
        {
            previousWaypointIndex = currentWaypointIndex;
            currentWaypointIndex = Random.Range(0, waypoints.Length);

            if (currentWaypointIndex >= waypoints.Length)
            {
                currentWaypointIndex = 0;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag != "NPCs" && collision.tag != "Player")
        {
            if (currentWaypointIndex == previousWaypointIndex)
            {
                currentWaypointIndex = Random.Range(0, waypoints.Length);
            }
            else
            {
                currentWaypointIndex = previousWaypointIndex;
            }
            if (currentWaypointIndex >= waypoints.Length)
            {
                currentWaypointIndex = 0;
            }
        }
    }

    static float NextFloat(float min, float max)
    {
        System.Random random = new System.Random();
        double val = (random.NextDouble() * (max - min) + min);
        return (float)val;
    }
}  