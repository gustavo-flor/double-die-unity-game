using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaypointFollower : MonoBehaviour
{
    [SerializeField] private GameObject[] waypoints;
    [SerializeField] private float moveSpeed = 2f;

    private int currentWaypointIndex = 0;

    // Update is called once per frame
    private void Update()
    {
        Vector3 currentPosition = transform.position;
        GameObject currentWaypoint = waypoints[currentWaypointIndex];
        if (Vector2.Distance(currentWaypoint.transform.position, currentPosition) < .1f)
        {
            currentWaypointIndex++;
            if (currentWaypointIndex == waypoints.Length)
            {
                currentWaypointIndex = 0;
            }
            currentWaypoint = waypoints[currentWaypointIndex];
        }
        transform.position = Vector2.MoveTowards(currentPosition, currentWaypoint.transform.position, Time.deltaTime * moveSpeed);
    }
}
