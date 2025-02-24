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

    [Header("Turret Scanning Settings")]
    [SerializeField] private float scanSpeed = 10f;
    [SerializeField] private float rotationMin = -90f;
    [SerializeField] private float rotationMax = 90f;

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
        if (other.CompareTag("Player"))
        {
            playerInTrigger = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInTrigger = false;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(turretSphereCollider.transform.position,turretSphereCollider.radius);
    }

    //Getter Methods

    public Transform GetTurretHead() => turretHead;
    public SphereCollider GetTurretSphereCollider() => turretSphereCollider;
    public LineRenderer GetLineRenderer() => lineRenderer;
    public Transform GetWeaponPoint() => weaponPoint;
    public Transform GetPlayerTarget() => playerTarget;
    public LayerMask GetPlayerLayer() => playerLayer;
    public float GetScanSpeed() => scanSpeed;
    public float GetRotationMin() => rotationMin;
    public float GetRotationMax() => rotationMax;
    public bool IsPlayerInTrigger() => playerInTrigger;
}
