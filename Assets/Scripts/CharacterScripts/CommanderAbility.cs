using UnityEngine;

public class CommanderAbility : MonoBehaviour
{
    [SerializeField] private LayerMask compatibleWithCommands;
    [SerializeField] private CompanionController companion;
    
    [SerializeField] private GameObject waypointPrefab;

    void Awake()
    {
        if (companion == null)
        {
            companion = FindObjectOfType<CompanionController>();
        }
    }
    public void Command()
    {
        Instantiate(waypointPrefab,transform.position,Quaternion.identity);
        companion.GiveCommand(new MoveCommand(transform.position));
    }
}
