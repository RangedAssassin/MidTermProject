using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthSystem : MonoBehaviour
{
    [SerializeField] private float currentHealth;
    [SerializeField] private float maxHealth;

    //Observer Pattern(Actions)
    public Action<float> OnLifeChanged;
    public Action OnDead;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
    }

    public float GetCurrentHealth()
    {
        return currentHealth;
    }

    public void IncreaseHealth(float toIncrease)
    {
        currentHealth += toIncrease;
        OnLifeChanged?.Invoke(currentHealth);
    }

    public void DecreaseHealth(float toDecrease)
    {
        currentHealth -= toDecrease;
        OnLifeChanged?.Invoke(currentHealth);

        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
        if (currentHealth <= 0)
        {
            OnDead?.Invoke();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
