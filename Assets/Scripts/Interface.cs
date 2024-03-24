using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Interface : MonoBehaviour
{
    [SerializeField] private Image healthImage;
    [SerializeField] private TextMeshProUGUI playerGoldText;
    [SerializeField] private PlayerHealth playerHealth;
    [SerializeField] private Gold playerGold;
    [SerializeField] private ItemInteractions itemInteractions;
    [SerializeField] private GameObject pickUpButton;

    [SerializeField] private Image bloodImage;
    [SerializeField] private TextMeshProUGUI killCounter;
    [SerializeField] private GameObject deathPanel;
    [SerializeField] private GameObject interfacePanel;

    private int killValue;
    private float alphaColor;
    private void OnEnable()
    {
        GlobalEventManager.OnAddKillCount += AddKillCount;
    }
    private void OnDisable()
    {
        GlobalEventManager.OnAddKillCount -= AddKillCount;
    }
    private void FixedUpdate()
    {
        PlayerHealthOnInterface();
        PlayerGoldOnInterface();
        PickUpButton();
        DeathPanel();
        BloodScreen();
    }
    public void BackToMenu() => SceneManager.LoadScene(0);

    private void PlayerHealthOnInterface() 
    {
        healthImage.fillAmount = (float)playerHealth.currentPlayerHealth / playerHealth.maxPlayerHealth;
    }
    private void PlayerGoldOnInterface()
    {
        playerGoldText.text = playerGold.gold.ToString();
    }
    private void AddKillCount()
    {
        killValue++;
        killCounter.text = killValue.ToString();
    }
    private void PickUpButton()
    {
        pickUpButton.SetActive(itemInteractions.isEnable);
    }
    private void DeathPanel() 
    {
        if (playerHealth.playerIsDead)
        {
            deathPanel.SetActive(true);
            interfacePanel.SetActive(false);
        }
    }
    private void BloodScreen()
    {
        float maxHealth = playerHealth.maxPlayerHealth;

        alphaColor = playerHealth.currentPlayerHealth < playerHealth.maxPlayerHealth / 2f ?
                maxHealth / maxHealth - (playerHealth.currentPlayerHealth / maxHealth) : 0;

        bloodImage.color = new Color(180, 180, 180, alphaColor);
    }
}
