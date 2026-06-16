using UnityEngine;

public class SensorManager : MonoBehaviour
{
    public SensorData[] sensors;

    public SensorData activeSensor;

    void Update()
    {
        float maxFWI = -1;

        foreach (SensorData sensor in sensors)
        {
            if (sensor.fwi > maxFWI)
            {
                maxFWI = sensor.fwi;
                activeSensor = sensor;
            }
        }
    }
}