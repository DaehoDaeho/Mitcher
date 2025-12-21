using UnityEngine;
using UnityEngine.UI;

public class EnemyHPUI : MonoBehaviour
{
    [SerializeField]
    private Image imageHP;

    [SerializeField]
    private EnemyHealth enemyHealth;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if(enemyHealth != null)
        {
            HandleChangedHP(enemyHealth.GetCurrentHealth(), enemyHealth.GetMaxHealth());
        }
    }

    private void OnEnable()
    {
        if(enemyHealth != null)
        {
            enemyHealth.OnChangedHP += HandleChangedHP;
        }
    }

    private void OnDisable()
    {
        if (enemyHealth != null)
        {
            enemyHealth.OnChangedHP -= HandleChangedHP;
        }
    }

    void HandleChangedHP(int currentHealth, int maxHealth)
    {
        if(currentHealth <= 0)
        {
            imageHP.fillAmount = 0.0f;
            return;
        }

        imageHP.fillAmount = (float)currentHealth / (float)maxHealth;
    }
}
