using UnityEngine;

public class LookAtTarget : MonoBehaviour
{
    public Transform target; // The target the object will look at
    public float rotationSpeed = 1.0f; // Speed of the rotation (higher = faster)
    public float minYRotation = -45f; // Minimum Y-axis rotation (set in Inspector)
    public float maxYRotation = 45f;  // Maximum Y-axis rotation (set in Inspector)

    private void Update()
    {
        if (target != null)
        {
            // Calculate the direction to the target
            Vector3 direction = target.position - transform.position;

            // Zero out the Y-axis to ensure the object only rotates on the Y-axis
            direction.y = 0;

            // Calculate the target rotation
            Quaternion targetRotation = Quaternion.LookRotation(direction);

            // Clamp the Y-axis rotation
            float clampedYRotation = Mathf.Clamp(targetRotation.eulerAngles.y, minYRotation, maxYRotation);

            // Smoothly rotate to the clamped Y-axis rotation
            Quaternion smoothedRotation = Quaternion.Euler(0, clampedYRotation, 0);
            transform.rotation = Quaternion.Slerp(transform.rotation, smoothedRotation, Time.deltaTime * rotationSpeed);
        }
    }
}