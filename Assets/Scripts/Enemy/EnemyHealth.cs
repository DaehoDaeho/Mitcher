using System;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField]
    private int maxHealth = 50;

    [SerializeField]
    private int currentHealth = 0;

    public event Action<int, int> OnChangedHP;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        currentHealth = maxHealth;
    }

    /// <summary>
    /// 대미지 적용 및 사망 체크.
    /// </summary>
    /// <param name="amount">대미지 양</param>
    public void TakeDamage(int amount)
    {
        if(currentHealth <= 0)
        {
            return;
        }

        currentHealth -= amount;

        if(OnChangedHP != null)
        {
            OnChangedHP.Invoke(currentHealth, maxHealth);
        }

        if(currentHealth <= 0)
        {
            currentHealth = 0;
            Die();
        }
    }

    /// <summary>
    /// 사망 처리.
    /// </summary>
    private void Die()
    {
        Destroy(gameObject);
    }

    public int GetMaxHealth()
    {
        return maxHealth;
    }

    public int GetCurrentHealth()
    {
        return currentHealth;
    }
}
