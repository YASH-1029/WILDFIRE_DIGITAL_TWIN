using UnityEngine;
using TMPro;

public class DashboardManager : MonoBehaviour
{
    public SensorManager sensorManager;

    public TMP_Text temperatureText;
    public TMP_Text humidityText;
    public TMP_Text windSpeedText;
    public TMP_Text fireRiskText;

    public TMP_Text fwiText;
    public TMP_Text activeSensorText;

    public FireManager fireManager;
    void Update()
    {
        if (sensorManager.activeSensor == null)
            return;

        SensorData sensor;

        if (fireManager.fireDetected)
        {
            sensor = fireManager.fireSensor;
        }
        else
        {
            sensor = sensorManager.activeSensor;
        }

        if (sensor == null)
            return; ;

        if (
    fireManager != null &&
    fireManager.fireDetected &&
    fireManager.fireSensor != null
)
        {
            activeSensorText.text =
                "Responding to " + sensor.name;
        }
        else
        {
            activeSensorText.text =
                "Monitoring " + sensor.name;
        }

        temperatureText.text = "Temperature : " + sensor.temperature.ToString("F1") + " °C";

        humidityText.text = "Humidity : " + sensor.humidity.ToString("F1") + " %";

        windSpeedText.text = "Wind Speed : " + sensor.windSpeed.ToString("F1") + " km/h";

        fwiText.text = "FWI : " + sensor.fwi.ToString("F1");

        fireRiskText.text = "Fire Risk : " + sensor.fireRisk;
    }
}