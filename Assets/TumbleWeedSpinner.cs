using UnityEngine;

public class TumbleWeedSpinner : MonoBehaviour
{
    public float spinSpeed = 360f; // Speed of the spin in degrees per second

    private void Update()
    {
        // Start spinning the wheel
        Spin();
    }

    private void Spin()
    {
        // Rotate the wheel continuously
        transform.Rotate(new Vector3(spinSpeed * Time.deltaTime,0,0), Space.World);
    }
}