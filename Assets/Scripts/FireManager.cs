using System.Collections.Generic;
using UnityEngine;

public class FireManager : MonoBehaviour
{
    public SensorManager sensorManager;
    public GameObject fireObject;
    public EventLogger eventLogger;

    public bool fireDetected = false;
    public SensorData fireSensor;

    public Queue<SensorData> fireQueue = new Queue<SensorData>();

    private bool coolingDown = false;
    private float cooldownTimer = 0f;

    void Update()
    {
        // Cooldown
        if (coolingDown)
        {
            cooldownTimer += Time.deltaTime;

            if (cooldownTimer >= 20f)
            {
                coolingDown = false;
                cooldownTimer = 0f;

                Debug.Log("Cooldown finished");
            }
        }

        // Scan all sensors
        foreach (SensorData sensor in sensorManager.sensors)
        {
            // Check whether sensor cooldown has ended
            if (
                sensor.inCooldown &&
                Time.time >= sensor.cooldownEndTime
            )
            {
                sensor.inCooldown = false;

                eventLogger.AddLog(
                    sensor.name + " cooldown finished"
                );
            }

            // Detect a new fire
            if (
                sensor.fireRisk == "HIGH" &&
                !sensor.fireReported &&
                 !sensor.inCooldown

               )
            {
                sensor.fireReported = true;

                fireQueue.Enqueue(sensor);

                eventLogger.AddLog(
                    "Fire detected at " + sensor.name
                );
            }
        }

        // Start next mission
        if (!fireDetected && fireQueue.Count > 0)
        {
            fireSensor = fireQueue.Dequeue();

            fireDetected = true;

            fireObject.transform.position =
                fireSensor.transform.position + new Vector3(0, 5f, 0);

            fireObject.SetActive(true);

            eventLogger.AddLog(
                "Responding to " + fireSensor.name
            );
        }
    }

    public void ExtinguishFire()
    {
        fireDetected = false;

        fireObject.SetActive(false);

        fireSensor.fireReported = false;

        // Start 60 second cooldown for this sensor
        fireSensor.inCooldown = true;
        fireSensor.cooldownEndTime = Time.time + 60f;

        fireSensor = null;

        eventLogger.AddLog(
            "Fire extinguished"
        );

        coolingDown = true;
        cooldownTimer = 0f;
    }


}
