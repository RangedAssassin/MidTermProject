using UnityEngine;
using UnityEngine.Events;

public class ShootingTarget : MonoBehaviour , IPuzzlePiece
{
    [SerializeField] private bool isShot = false;
    [SerializeField] private bool enablePlatform = false;
    [SerializeField] private Light colorOfLight;
    [SerializeField] private MeshRenderer shadeMaterial;
    [SerializeField] private PlatformMovement platformScript;
    [SerializeField] private AudioSource audioSource;

    public UnityEvent OnTargetHit = new UnityEvent();


    private void Awake()
    {
        if (platformScript != null)
            enablePlatform = platformScript.enabled;
        if (audioSource != null)
        {
            audioSource = GetComponent<AudioSource>();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Projectile"))
        {
            OnTargetHit?.Invoke();

            audioSource.Play();

            if (platformScript != null)
                platformScript.enabled = true;

            isShot = true;

            if (colorOfLight != null)
                colorOfLight.color = Color.green;

            if (shadeMaterial != null)
            {
                shadeMaterial.material.color = Color.green;
                shadeMaterial.material.SetColor("_EmissionColor", Color.green);
            }
        }
    }

    public bool IsCorrect()
    {
        return isShot;
    }

}
