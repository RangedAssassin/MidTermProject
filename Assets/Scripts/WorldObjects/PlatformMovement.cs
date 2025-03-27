using UnityEngine;

public class PlatformMovement : MonoBehaviour
{
    [SerializeField] private Transform startPosition;
    [SerializeField] private Transform endPosition;
    [SerializeField] private float travelTimeFromPointToPoint = 2f;

    [SerializeField] private Rigidbody platformRigidbody;
    private Vector3 lastPosition;
    private Vector3 velocity;

    private CharacterController playerCharacterController;

    private void Start()
    {
        lastPosition = transform.position;
    }

    private void FixedUpdate()
    {
        // Move platform smoothly between two points
        float t = Mathf.PingPong(Time.time / travelTimeFromPointToPoint, 1f);
        Vector3 newPosition = Vector3.Lerp(startPosition.position, endPosition.position, t);

        // Calculate velocity
        velocity = (newPosition - lastPosition) / Time.fixedDeltaTime;
        lastPosition = newPosition;

        // Move the platform
        platformRigidbody.MovePosition(newPosition);

    }

}



