using UnityEngine;

public class DronePatrol : MonoBehaviour
{
    public Transform[] waypoints;
    public float speed = 10f;

    public FireManager fireManager;
    public EventLogger eventLogger;

    private int currentWaypoint = 0;

    private bool reachedFire = false;
    private float hoverTimer = 0f;

    private bool dispatchedLogged = false;
    private bool hoveringLogged = false;

    void Update()
    {
        if (fireManager.fireDetected)
        {
            Vector3 targetPosition = new Vector3(
                fireManager.fireSensor.transform.position.x,
                transform.position.y,
                fireManager.fireSensor.transform.position.z
            );

            if (!reachedFire)
            {
                if (!dispatchedLogged)
                {
                    eventLogger.AddLog("Drone dispatched");
                    dispatchedLogged = true;
                }

                transform.position = Vector3.MoveTowards(
                    transform.position,
                    targetPosition,
                    speed * Time.deltaTime
                );

                if (Vector3.Distance(transform.position, targetPosition) < 0.5f)
                {
                    reachedFire = true;
                    hoverTimer = 0f;

                    eventLogger.AddLog("Drone reached fire");
                }
            }
            else
            {
                if (!hoveringLogged)
                {
                    eventLogger.AddLog("Hovering over fire");
                    hoveringLogged = true;
                }

                hoverTimer += Time.deltaTime;

                if (hoverTimer >= 10f)
                {
                    fireManager.ExtinguishFire();

                    FindNearestWaypoint();

                    reachedFire = false;
                    hoverTimer = 0f;

                    dispatchedLogged = false;
                    hoveringLogged = false;

                    eventLogger.AddLog("Patrol resumed");
                }
            }

            return;
        }

        if (waypoints.Length == 0)
            return;

        Transform target = waypoints[currentWaypoint];

        transform.position = Vector3.MoveTowards(
            transform.position,
            target.position,
            speed * Time.deltaTime
        );

        if (Vector3.Distance(transform.position, target.position) < 0.5f)
        {
            currentWaypoint = (currentWaypoint + 1) % waypoints.Length;
        }
    }

    void FindNearestWaypoint()
    {
        float minDistance = Mathf.Infinity;
        int nearestIndex = 0;

        for (int i = 0; i < waypoints.Length; i++)
        {
            float distance = Vector3.Distance(
                transform.position,
                waypoints[i].position
            );

            if (distance < minDistance)
            {
                minDistance = distance;
                nearestIndex = i;
            }
        }

        currentWaypoint = nearestIndex;
    }
}