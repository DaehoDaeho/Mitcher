using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerStatsUI : MonoBehaviour
{
    [SerializeField]
    private TMP_Text hpText;

    [SerializeField]
    private TMP_Text potionText;

    [SerializeField]
    private Image hpImage;

    [SerializeField]
    private PlayerStats playerStats;

    private void Start()
    {
        if (playerStats != null)
        {
            UpdateHealthUI(playerStats.GetCurrentHealth(), playerStats.GetMaxHealth());
            UpdatePotionUI(playerStats.GetPotionCount());
        }
    }

    private void OnEnable()
    {
        if(playerStats != null)
        {
            playerStats.OnChangedHealth += UpdateHealthUI;
            playerStats.OnChangedPotionCount += UpdatePotionUI;
        }
    }

    private void OnDisable()
    {
        if (playerStats != null)
        {
            playerStats.OnChangedHealth -= UpdateHealthUI;
            playerStats.OnChangedPotionCount -= UpdatePotionUI;
        }
    }

    void UpdateHealthUI(int currentHealth, int maxHealth)
    {
        if (hpText != null)
        {
            hpText.text = currentHealth + "/" + maxHealth;
        }

        if (hpImage != null)
        {
            if (currentHealth <= 0)
            {
                hpImage.fillAmount = 0;
                return;
            }

            float ratio = (float)currentHealth / (float)maxHealth;
            hpImage.fillAmount = ratio;
        }
    }

    void UpdatePotionUI(int potionCount)
    {
        if (potionText != null)
        {
            potionText.text = "Potion: x" + potionCount;
        }
    }
}
