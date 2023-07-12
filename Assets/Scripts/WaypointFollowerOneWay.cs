using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaypointFollowerOneWay : MonoBehaviour
{
    [SerializeField] private GameObject[] waypoints;
    private int currentWaypointIndex = 0;

    [SerializeField] private float speed = 2f;
    [SerializeField] private float cooldown = 3f;

    private void Start()
    {
        StartCoroutine(FollowWaypoints());
    }

    private IEnumerator FollowWaypoints()
    {
        while (true)
        {
            // Move towards the current waypoint
            while (Vector2.Distance(waypoints[currentWaypointIndex].transform.position, transform.position) >= .1f)
            {
                transform.position = Vector2.MoveTowards(transform.position, waypoints[currentWaypointIndex].transform.position, Time.deltaTime * speed);
                yield return null; // Wait for the next frame
            }

            // Increment waypoint index
            currentWaypointIndex++;

            // If we've gone through all waypoints
            if (currentWaypointIndex >= waypoints.Length)
            {
                // Reset waypoint index
                currentWaypointIndex = 0;

                // Instantly move back to the first waypoint
                transform.position = waypoints[0].transform.position;

                // Wait for cooldown period
                yield return new WaitForSeconds(cooldown);
            }
        }
    }
}
