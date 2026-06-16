using UnityEngine;

public class PredictionManager : MonoBehaviour
{
    public SensorData[] sensors;

    public PythonModelManager pythonModel;

    void InvokePrediction()
    {
        SensorData criticalSensor = sensors[0];

        foreach (SensorData sensor in sensors)
        {
            if (sensor.fwi > criticalSensor.fwi)
            {
                criticalSensor = sensor;
            }
        }

        string prediction =
            pythonModel.PredictFire(
                criticalSensor.temperature,
                criticalSensor.humidity,
                criticalSensor.windSpeed,
                criticalSensor.rain
            );

        if (prediction == "FIRE")
            criticalSensor.fireRisk = "HIGH";
        else
            criticalSensor.fireRisk = "LOW";

        criticalSensor.UpdateSensorColor();
    }

    void Start()
    {
        InvokeRepeating(nameof(InvokePrediction), 2f, 5f);
    }
}