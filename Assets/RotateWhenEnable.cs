using UnityEngine;

public class RotateWhenEnable : MonoBehaviour
{
    [SerializeField] private float rotationSpeed = 10f; // Tốc độ xoay

    private void FixedUpdate() => Rotate();

    private void Rotate()
    {
        float newRotation = transform.eulerAngles.z + rotationSpeed * Time.fixedDeltaTime;
        Quaternion newQuaternion = Quaternion.Euler(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y, newRotation);
        transform.rotation = newQuaternion;
    }
}
