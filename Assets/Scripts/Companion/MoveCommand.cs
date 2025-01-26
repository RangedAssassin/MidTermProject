using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCommand : Command
{
    private Vector3 target;
    public override void Cancel()
    {

    }



    public override void Execute()
    {
        //if (companionController.GetNavMeshAgent().hasPath) return;

        companionController.GetNavMeshAgent().SetDestination(target);
    }

    public override bool IsCommandComplete()
    {
        float distance = Vector3.Distance(target, companionController.transform.position);
        Debug.Log(distance);

        return Vector3.Distance(target, companionController.transform.position) < 0.4f;
    }

    public MoveCommand(Vector3 position)
    {
        target = position;
    }
}
