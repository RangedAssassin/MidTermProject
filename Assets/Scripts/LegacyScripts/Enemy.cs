using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    [SerializeField] private Transform[] _targetPoints;
    [SerializeField] private Transform _enemy;
    [SerializeField] private float _playerCheckDistance;
    [SerializeField] private float _checkRadius = 0.4f;

    int _currentTarget = 0;

    private NavMeshAgent _agent;

    public bool isIdle = true;
    public bool isPlayerFound;
    public bool isCloseToPlayer;

    public Transform _player;

    private void Start()
    {
        _agent = GetComponent<NavMeshAgent>();
        //tell agent to where the first point is.
        _agent.destination = _targetPoints[_currentTarget].position;
    }

    private void Update()
    {
        if (isIdle)
        {
            Idle();

        }
        else if (isPlayerFound)
        {
            if (isCloseToPlayer)
            {
                AttackPlayer();    
            }
            else
            {
                FollowPlayer();
            }
        }
    }

    void Idle()
    {
        //choose the next destination in nthe array
        if (_agent.remainingDistance < 0.1f)
        {
            _currentTarget++;
            if (_currentTarget >= _targetPoints.Length)
            { 
                _currentTarget = 0;
                _agent.destination = _targetPoints[_currentTarget].position;
            }
        }

        //check for player
        if (Physics.SphereCast(_enemy.position,_checkRadius,transform.forward, out RaycastHit hit, _playerCheckDistance))
        {
            if (hit.transform.CompareTag("Player"))
            {
                Debug.Log("Player Found");
                isIdle = false;
                isPlayerFound = true;
                _player = hit.transform;
                _agent.destination = _player.position;
            }
        }
    }

    void FollowPlayer()
    {
        if (_player != null)
        {
            //logis for if we are to far away
            if (Vector3.Distance(transform.position, _player.position) > 10)
            {
                isPlayerFound = false;
                isIdle = true;
            }

            //close to player
            if (Vector3.Distance(transform.position, _player.position) < 2)
            {
                isCloseToPlayer = true;
             
            }
            else
            {
                isCloseToPlayer = false;
            }

            _agent.destination = _player.position;
        }
        else
        {
            isPlayerFound = false;
            isIdle = true;
            isCloseToPlayer = false;
        }
    }

    void AttackPlayer()
    {
        if (_player != null)
        {
            Debug.Log("Attacking!!!");
            if (Vector3.Distance(transform.position, _player.position) > 2)
            {
                isCloseToPlayer = false;
            }
        }
        else
        {
            Debug.Log("No player found in game!!");
        }
    }
}
