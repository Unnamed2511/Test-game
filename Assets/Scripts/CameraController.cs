using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private Joystick fireJoystick;
    private readonly float smoothFactor = 0.25f;
    private Vector3 offset = new Vector3(0, 0, -10);
    private Vector3 velocity = Vector3.zero;

    private void FixedUpdate()
    {
        Vector3 joystickRotation = (Vector3.right * fireJoystick.Horizontal + Vector3.up * fireJoystick.Vertical);
        Vector3 targetPosition = joystickRotation == Vector3.zero ? player.position + offset : player.position + offset + (joystickRotation * 1.5f);
        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothFactor);
    }
}
