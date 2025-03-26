using UnityEngine;

public class PushableObject : MonoBehaviour
{
    //[SerializeField] private float pushForce = 10f;

    //private void OnCollisionEnter(Collision collision)
    //{
    //    Debug.Log($"Collided with: {collision.gameObject.name}"); // Check if collision happens

    //    if (collision.gameObject.CompareTag("Player")) // Ensure the Player has this tag
    //    {
    //        Debug.Log("Collision with player detected!");

    //        Rigidbody rb = GetComponent<Rigidbody>();
    //        if (rb != null)
    //        {
    //            Debug.Log("Rigidbody found, applying force...");

    //            Vector3 pushDirection = collision.contacts[0].normal * -1; // Push away from the player
    //            pushDirection.y = 0; // Keep movement horizontal

    //            rb.AddForce(pushDirection * pushForce, ForceMode.VelocityChange);
    //        }
    //        else
    //        {
    //            Debug.Log("No Rigidbody found on this object!");
    //        }
    //    }
    //}
}



