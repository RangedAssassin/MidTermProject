using UnityEngine;

public class TurretStateController : MonoBehaviour
{
    private TurretState currentState;

    [Header("Player Details")]
    [SerializeField] private Transform playerTarget;
    [SerializeField] private LayerMask playerLayer;
    [SerializeField] private bool playerInTrigger = false;

    [Header("Turret References")]
    [SerializeField] private LineRenderer lineRenderer;
    [SerializeField] private Transform weaponPoint;
    [SerializeField] private Transform turretHead;
    [SerializeField] private SphereCollider turretSphereCollider;
    [SerializeField] private AudioSource audioSource;
    //[SerializeField] private BoxCollider turretBoxCollider;

    [Header("Turret Settings")]
    [Range(0.1f, 1f)][SerializeField] private float turretDamage = 0.5f;
    [SerializeField] private float scanSpeed = 10f;
    [SerializeField] private float rotationMin = -90f;
    [SerializeField] private float rotationMax = 90f;
    [SerializeField] private float threshold = 1.5f;

    private void Awake()
    {
        currentState = new TurretIdle(this);
    }
    void Start()
    {
        currentState.EnterState();
    }
    void Update()
    {
        currentState.UpdateState(playerInTrigger);
    }
    public void SwitchState(TurretState state)
    {
        currentState.ExitState();
        currentState = state;
        currentState.EnterState();
    }
    private void OnTriggerStay(Collider other)
    {
        //if (other.CompareTag("Player"))
        //{
        //    playerInTrigger = true;
        //}

        if (other.CompareTag("Player"))
        {
            if (audioSource != null && !audioSource.isPlaying)
            {
                SoundManager.SingleTon.PlaySound(audioSource);
            }
            float playerY = other.transform.position.y;  // Get player's Y position
            float targetY = weaponPoint.transform.position.y;  // Replace with your target Y position

            if (Mathf.Abs(playerY - targetY) <= threshold)
            {
                playerInTrigger = true;
            }
            else
            {
                playerInTrigger = false;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInTrigger = false;
            if (audioSource != null)
            { 
                SoundManager.SingleTon.StopSound(audioSource);
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(turretHead.transform.position,turretSphereCollider.radius);
    }

    //Getter Methods

    public Transform GetTurretHead() => turretHead;
    public SphereCollider GetTurretSphereCollider() => turretSphereCollider;
    //public BoxCollider GetTurretBoxCollider() => turretBoxCollider;
    public LineRenderer GetLineRenderer() => lineRenderer;
    public Transform GetWeaponPoint() => weaponPoint;
    public Transform GetPlayerTarget() => playerTarget;
    public LayerMask GetPlayerLayer() => playerLayer;
    public float GetScanSpeed() => scanSpeed;
    public float GetRotationMin() => rotationMin;
    public float GetRotationMax() => rotationMax;
    public bool IsPlayerInTrigger() => playerInTrigger;
    public float GetTurretDamage() => turretDamage;
}
