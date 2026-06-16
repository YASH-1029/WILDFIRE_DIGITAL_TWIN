using UnityEngine;

public class SensorData : MonoBehaviour
{
    public float temperature;
    public float humidity;
    public float windSpeed;
    public float rain;

    public string fireRisk;

    public bool fireReported = false;
    public bool hasSpread = false;
    public float fireStartTime = 0f;
    public bool inCooldown = false;
    public float cooldownEndTime = 0f;

    public float fwi;

    public DatasetReader datasetReader;

    public int currentRow;

    private Renderer sphereRenderer;
    

    void Start()
    {
    

        sphereRenderer = GetComponentInChildren<Renderer>();

        // Wait 1 second before first call
        InvokeRepeating("GenerateData", 1f, 5f);
    }

    void GenerateData()
    {
        if (fireReported)
            return;

        Debug.Log(gameObject.name + " currentRow = " + currentRow);

        string[] row = datasetReader.GetRow(currentRow);

        if (row.Length < 14)
            return;

        temperature = float.Parse(row[3]);
        humidity = float.Parse(row[4]);
        windSpeed = float.Parse(row[5]);
        rain = float.Parse(row[6]);
        fwi = float.Parse(row[12]);

        currentRow++;

       

        UpdateSensorColor();
    }

    

    public void UpdateSensorColor()
    {
        if (sphereRenderer == null)
            return;

        if (fireRisk == "HIGH")
            sphereRenderer.material.color = Color.red;
        else
            sphereRenderer.material.color = Color.blue;
    }
}