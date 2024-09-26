using UnityEngine;

public class VectorRotation : MonoBehaviour
{
    // The vector to rotate (initially pointing right)
    [SerializeField]
    private Vector2 currentVector = new Vector2(1, 0);

    [SerializeField]
    private float magnitude;

    // Target angle in degrees (90 degrees to point upwards)
    private float targetAngle = 90f;

    // Rotation speed in degrees per second
    public float rotationSpeed = 45f; // You can adjust this

    // Current angle
    private float currentAngle = 0f;

    void Update()
    {
        // Calculate how much to rotate this frame
        float angleThisFrame = rotationSpeed * Time.deltaTime;

        // Increment the current angle
        currentAngle += angleThisFrame;

        // Clamp the current angle so it doesn't exceed the target
        currentAngle = Mathf.Min(currentAngle, targetAngle);

        // Convert the current angle to radians
        float angleInRadians = currentAngle * Mathf.Deg2Rad;

        // Rotate the vector by the current angle while maintaining its magnitude
        currentVector = new Vector2(Mathf.Cos(angleInRadians), Mathf.Sin(angleInRadians));
        magnitude = currentVector.magnitude;
        // For visualization, draw the current vector
        Debug.DrawLine(Vector3.zero, currentVector, Color.red);

        // Optionally, you can log the current vector for debugging
        Debug.Log("Current Vector: " + currentVector);
    }
}