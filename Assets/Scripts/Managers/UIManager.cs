using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    private HealthSystem playerHealth;
    [SerializeField] private TextMeshProUGUI healthText;

    // Start is called before the first frame update
    void Start()
    {
        playerHealth = PlayerInput.Instance.GetComponent<HealthSystem>();
        playerHealth.OnLifeChanged += UpdateHealthText;
        playerHealth.OnDead += DisplayDeathScreen;
    }

    void DisplayDeathScreen()
    {
        //set active
    }

    void UpdateHealthText(float healthToDisplay)
    {
        healthText.text = "Health: " + healthToDisplay.ToString();
    }
}
