using UnityEngine;

public class TurretIdle : TurretState
{
    private Transform turretHead;
    private float scanSpeed;
    private float rotationMin;
    private float rotationMax;


    public TurretIdle(TurretStateController turretController) : base(turretController)
    {
        base.turretController = turretController;
        
        scanSpeed = turretController.GetScanSpeed();
        rotationMin = turretController.GetRotationMin();
        rotationMax = turretController.GetRotationMax();
        turretHead = turretController.GetTurretHead();

    }
    public override void EnterState()
    {

    }
    public override void ExitState()
    {

    }
    public override void UpdateState(bool playerInTrigger)
    {
        ScanRoom();

        if (playerInTrigger)
        {
            turretController.SwitchState(new TurretAttack(turretController));
        }
    }

    public void ScanRoom()
    {
        float rotation = Mathf.PingPong(Time.time * scanSpeed, rotationMax - rotationMin) + rotationMin;

        Quaternion targetRotation = Quaternion.Euler(0, rotation, 0);
        turretHead.transform.rotation = Quaternion.Lerp(turretHead.transform.rotation, targetRotation, Time.deltaTime * scanSpeed);
    }

}

