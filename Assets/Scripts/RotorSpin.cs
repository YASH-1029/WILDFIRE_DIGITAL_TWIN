using UnityEngine;

public class RotorSpin : MonoBehaviour
{
    public float rotationSpeed = 1000f;

    void Update()
    {
        transform.Rotate(
            Vector3.up,
            rotationSpeed * Time.deltaTime
        );
    }
}