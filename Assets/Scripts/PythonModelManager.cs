using Process = System.Diagnostics.Process;
using ProcessStartInfo = System.Diagnostics.ProcessStartInfo;
using UnityEngine;

public class PythonModelManager : MonoBehaviour
{
    public string PredictFire(
        float temperature,
        float humidity,
        float windSpeed,
        float rain)
    {
        ProcessStartInfo start = new ProcessStartInfo();

        start.FileName = @"E:\anaconda3\python.exe";

        start.Arguments =
            "\"E:/UNITY PROJECTS/WILDFIRE_DIGITAL_TWIN/ml_model/unity_predict.py\" "
            + temperature + " "
            + humidity + " "
            + windSpeed + " "
            + rain;

        start.UseShellExecute = false;

        start.RedirectStandardOutput = true;

        start.RedirectStandardError = true;

        start.CreateNoWindow = true;

        Process process = Process.Start(start);

        string result = process.StandardOutput.ReadToEnd();

        string errors = process.StandardError.ReadToEnd();

        process.WaitForExit();

        Debug.Log("OUTPUT = " + result);

        Debug.Log("ERRORS = " + errors);

        return result.Trim();
    }
}