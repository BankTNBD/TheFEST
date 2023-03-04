using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCMovement : MonoBehaviour
{
    public Transform[] waypoints;
    public float moveSpeed = 2f;
    private int currentWaypointIndex = 0;
    private int previousWaypointIndex = 0;
    private void Start()
    {
        if (waypoints.Length == 0)
        {
            Debug.LogError("No waypoints found!");
            return;
        }

        transform.position = waypoints[0].position;
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

        transform.Translate(targetDirection * moveSpeed * Time.deltaTime);

        if (Vector2.Distance(transform.position, waypoints[currentWaypointIndex].position) < 0.1f)
        {
            previousWaypointIndex = currentWaypointIndex;
            currentWaypointIndex = Random.Range(0, waypoints.Length);

            Debug.Log("Moving to waypoint " + currentWaypointIndex);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        currentWaypointIndex = previousWaypointIndex;
        /*if (currentWaypointIndex >= waypoints.Length)
        {
            currentWaypointIndex = 0;
        }*/

        Debug.Log("Colliding with obstacle. Moving to waypoint " + currentWaypointIndex);
        
    }
}
