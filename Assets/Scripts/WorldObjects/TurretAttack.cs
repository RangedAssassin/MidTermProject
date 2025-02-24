using UnityEngine;
public class TurretAttack : TurretState
{
    private HealthSystem playerHealth;
    private Transform playerTarget;
    private LayerMask playerLayer;
    private LineRenderer lineRenderer;
    private Transform weaponPoint;
    private Transform turretHead;
    private SphereCollider turretSphereCollider;
    private float scanSpeed;

    
    public TurretAttack(TurretStateController turretController) : base(turretController)
    {
        base.turretController = turretController;

        playerTarget = turretController.GetPlayerTarget();
        playerLayer = turretController.GetPlayerLayer();
        lineRenderer = turretController.GetLineRenderer();
        weaponPoint = turretController.GetWeaponPoint();
        turretHead = turretController.GetTurretHead();
        turretSphereCollider = turretController.GetTurretSphereCollider();
    }

    public override void EnterState()
    {
        playerHealth = PlayerInput.Instance.GetComponent<HealthSystem>();
        lineRenderer.enabled = true;
    }
    public override void ExitState()
    {
        lineRenderer.enabled = false;
    }
    public override void UpdateState(bool playerInTrigger)
    {
        LookAtTarget();
        ShootPlayer();

        if (playerInTrigger == false)
        {
            turretController.SwitchState(new TurretIdle(turretController));
        }
    }
    public void LookAtTarget()
    {
        Vector3 direction = playerTarget.position - turretHead.position;
        float angle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;
        turretHead.transform.rotation = Quaternion.Euler(0, angle, 0);
    }


    public void ShootPlayer()
    {
        Ray customRay = new Ray(weaponPoint.position, weaponPoint.transform.forward);
        RaycastHit tempHit;

        if (Physics.Raycast(customRay, out tempHit, turretSphereCollider.radius, playerLayer))
        {

            if (tempHit.collider.CompareTag("Player"))
            {
                Debug.Log("Damaging the player");
                //playerHealth.DecreaseHealth(1f);
            }
            else
            {
                Debug.Log("Target is being shielded!!");
            }
        }
        Vector3 startPoint = weaponPoint.position;
        Vector3 endPoint = new Vector3(tempHit.point.x,weaponPoint.position.y,tempHit.point.z);
        lineRenderer.SetPosition(0, startPoint);
        lineRenderer.SetPosition(1, endPoint);
    }
}
