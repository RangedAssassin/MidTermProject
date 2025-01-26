using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PooledObject : MonoBehaviour
{
    [SerializeField] private float timeToReset = 15f;
    [SerializeField] private float currentTimer = 0f;

    [SerializeField] private Rigidbody bulletRigidbody;

    private ObjectPooling linkedPool;
    public void LinkToPool(ObjectPooling pool)
    {
        linkedPool = pool;
    }
    private void OnEnable()
    {
        currentTimer = 0;
    }

    private void Update()
    {
        if (currentTimer < timeToReset)
        {
            currentTimer += Time.deltaTime;
        }
        else
        {
            currentTimer = 0;
            bulletRigidbody.velocity = Vector3.zero;
            linkedPool.ResetBullet(this);
        }
    }

    public Rigidbody GetRigidbody()
    {
        return bulletRigidbody;
    }
}
