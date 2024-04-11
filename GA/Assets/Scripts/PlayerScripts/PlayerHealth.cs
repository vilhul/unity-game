using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : NetworkBehaviour
{
    public GameObject spectatorCamera;
    public NetworkVariable<float> currentHealth = new NetworkVariable<float>();
    public NetworkVariable<bool> alive = new NetworkVariable<bool>();
    public Vector3 newPosition = new Vector3(0, 800, 0);

    private float maxHealth = 100f;
    private float healthPercentage;
    private float newWidth;
    private float maxWidth = 500f;
    public Image healthBarImage;
    public TextMeshProUGUI healthText;


    public override void OnNetworkSpawn()
    {

        alive.Value = true;
        currentHealth.Value = 100;
        spectatorCamera = GameObject.FindWithTag("Spectator Camera");
<<<<<<< Updated upstream

=======
>>>>>>> Stashed changes

    }


<<<<<<< Updated upstream

    public void Update() {

        LimitHealth();
        UpdateHealthBar();
        newWidth = maxWidth * healthPercentage;
        ChangeWidth(newWidth);
        UpdateHealthText();

        if (GameManager.Instance.gameState != GameManager.GameState.playing) return;
        Transform objTransform = GetComponent<Transform>();
        if (currentHealth.Value <= 0) {
            if (IsServer) {
                alive.Value = false;
            }

            spectatorCamera.GetComponent<Camera>().depth = 50f;
            objTransform.position = newPosition;

        }


=======
    
    public void Update()
    {
        
            LimitHealth();
            UpdateHealthBar();
            newWidth = maxWidth * healthPercentage;
            ChangeWidth(newWidth);
            UpdateHealthText();

            if (GameManager.Instance.gameState != GameManager.GameState.playing) return;
            Transform objTransform = GetComponent<Transform>();
            if (currentHealth.Value <= 0) {
                if (IsServer){
                    alive.Value = false;
                }

                spectatorCamera.GetComponent<Camera>().depth = 50f;
                objTransform.position = newPosition;

            }

        
>>>>>>> Stashed changes
    }

    void UpdateHealthBar()
    {
        healthPercentage = currentHealth.Value / maxHealth;

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
            healthText.text = currentHealth.Value.ToString("0") + " / " + maxHealth.ToString("0");
        }
    }
    void LimitHealth()
    {
        currentHealth.Value = Mathf.Clamp(currentHealth.Value, 0f, 100); // Ensure currentHealth is within the valid range.
    }
}
