using UnityEngine;

public abstract class TurretState
{
    protected TurretStateController turretController;
    public TurretState(TurretStateController turretController)
    {
        this.turretController = turretController;
    }
    public abstract void EnterState();
    public abstract void UpdateState(bool playerInTrigger);
    public abstract void ExitState();
}
