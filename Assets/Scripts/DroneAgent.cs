using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Sensors;
using Unity.MLAgents.Actuators;

public class DroneAgent : Agent
{
    public override void OnEpisodeBegin()
    {
        Debug.Log("Episode Started");
    }

    public override void CollectObservations(VectorSensor sensor)
    {
        // Drone position
        sensor.AddObservation(transform.position.x);
        sensor.AddObservation(transform.position.z);
    }

    public override void OnActionReceived(ActionBuffers actions)
    {
        // Will implement later
    }
}