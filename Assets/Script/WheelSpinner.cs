using UnityEngine;

public class WheelSpinner : MonoBehaviour
{
    public float spinSpeed = 360f; // Speed of the spin in degrees per second

    private void Update()
    {
        // Start spinning the wheel
        SpinWheel();
    }

    private void SpinWheel()
    {
        // Rotate the wheel continuously
        transform.Rotate(Vector3.forward, spinSpeed * Time.deltaTime);
    }
}