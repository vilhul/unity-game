using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using TMPro;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarUI : NetworkBehaviour
{
    private float maxHealth = 100f;
    private float healthPercentage;
    private float newWidth;
    private float maxWidth = 500f;
    public Image healthBarImage;
    public TextMeshProUGUI healthText;
    public GameObject healthItems;


    public override void OnNetworkSpawn()
    {
        if (IsOwner)
        {
            healthItems.SetActive(true);

        } else
        {

            healthItems.SetActive(false);
        }

    }

    void Update()
    {
        LimitHealth();
        UpdateHealthBar();
        newWidth = maxWidth * healthPercentage;
        ChangeWidth(newWidth);
        UpdateHealthText();
    }

    void UpdateHealthBar()
    {
        healthPercentage = GetComponent<PlayerHealth>().currentHealth.Value / maxHealth;

    }

    void ChangeWidth(float newWidth)
    {

        if (healthBarImage != null)
        {
            RectTransform rectTransform = healthBarImage.GetComponent<RectTransform>();

            rectTransform.sizeDelta = new Vector2(newWidth, rectTransform.sizeDelta.y);
        }
    }

    void UpdateHealthText()
    {

        if (healthText != null)
        {
            healthText.text = GetComponent<PlayerHealth>().currentHealth.Value.ToString("0") + " / " + maxHealth.ToString("0");
        }
    }
    void LimitHealth()
    {
        GetComponent<PlayerHealth>().currentHealth.Value = Mathf.Clamp(GetComponent<PlayerHealth>().currentHealth.Value, 0f, 100); // Ensure currentHealth is within the valid range.
    }

}
