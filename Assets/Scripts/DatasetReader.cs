using System.Collections.Generic;
using UnityEngine;

public class DatasetReader : MonoBehaviour
{
    public TextAsset csvFile;

    private List<string[]> rows = new List<string[]>();

    void Start()
    {
        string[] lines = csvFile.text.Split('\n');

        foreach (string line in lines)
        {
            if (string.IsNullOrWhiteSpace(line))
                continue;

            string[] row = line.Split(',');

            int temp;

            // Skip header rows or invalid rows
            if (!int.TryParse(row[0], out temp))
                continue;

            rows.Add(row);
        }

        Debug.Log("Rows loaded = " + rows.Count);
    }

    public string[] GetRow(int index)
    {
        Debug.Log("Index = " + index);
        Debug.Log("Rows Count = " + rows.Count);

        if (rows.Count == 0)
        {
            Debug.LogError("NO ROWS LOADED!");
            return new string[0];
        }

        if (index < 0)
            index = 0;

        if (index >= rows.Count)
            index = 0;

        return rows[index];
    }
}