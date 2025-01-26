using System.Collections;
using System.Collections.Generic;
using UnityEngine;

///<summary>
///This ability is specific for AI characters which can attack the player in different ways
///</summary>

public class AttackAbility : MonoBehaviour
{
    [SerializeField] private float _damageToGive;
    [SerializeField] private float _attackCooldown;

    private bool _isAttacking;

    private float _attackTimer;
    private HealthSystem _targetToAttack;
    

    // Start is called before the first frame update
    void Start()
    {
        //Debug.Log("Started Attack With another script");
    }

    // Update is called once per frame
    void Update()
    {
        if (_isAttacking)
        {
            _attackTimer += Time.deltaTime;
            if (_attackTimer >= _attackCooldown)
            { 
                Attack();
                _attackTimer = 0;
            }
         
        }
    }

    public void StartAttack(Transform target)
    {
        _targetToAttack = target.GetComponent<HealthSystem>();
        //Debug.Log("Started Attack With another script");
        _isAttacking = true;
    }

    public void Attack()
    { 
        if (_targetToAttack)
        {
            _targetToAttack.DecreaseHealth(_damageToGive);
        }
        //deal damage to target

    }

    public void StopAttack()
    {
        //Debug.Log("Stopped Attack With another script");
        _isAttacking = false;
    }
}
