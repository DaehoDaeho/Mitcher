using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;

public class PlayerStats : MonoBehaviour
{
    [SerializeField]
    private int maxHealth = 100;

    [SerializeField]
    private int currentHealth = 0;

    [SerializeField]
    private int potionHealAmount = 25;

    [SerializeField]
    private int potionCount = 0;

    [SerializeField]
    private int testDamageAmount = 10;

    public event Action<int, int> OnChangedHealth;
    public event Action<int> OnChangedPotionCount;
    public event Action OnPlayerDeath;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        currentHealth = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Alpha1) == true)
        {
            UsePotion();
        }

        if(Input.GetKeyDown(KeyCode.H) == true)
        {
            TakeDamage(testDamageAmount);
        }
    }

    void UsePotion()
    {
        if(potionCount <= 0)
        {
            Debug.Log("사용할 포션이 없습니다.");
            return;
        }

        if(currentHealth >= maxHealth)
        {
            Debug.Log("이미 체력이 가득 찼습니다.");
            return;
        }

        Heal(potionHealAmount);
        --potionCount;

        if (OnChangedPotionCount != null)
        {
            OnChangedPotionCount.Invoke(potionCount);
        }
    }

    public void Heal(int amount)
    {
        if(currentHealth <= 0)
        {
            Debug.Log("이미 사망 상태입니다.");
            return;
        }

        currentHealth += amount;

        if(currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }

        if (OnChangedHealth != null)
        {
            OnChangedHealth.Invoke(currentHealth, maxHealth);
        }
    }

    public void AddPotion(int amount)
    {
        potionCount += amount;

        if(OnChangedPotionCount != null)
        {
            OnChangedPotionCount.Invoke(potionCount);
        }
    }

    public void TakeDamage(int amount)
    {
        if(currentHealth <= 0)
        {
            Debug.Log("이미 사망 상태입니다.");
            return;
        }

        currentHealth -= amount;

        if(currentHealth < 0)
        {
            currentHealth = 0;
        }

        if(OnChangedHealth != null)
        {
            OnChangedHealth.Invoke(currentHealth, maxHealth);
        }

        if(currentHealth == 0)
        {
            if(OnPlayerDeath != null)
            {
                OnPlayerDeath.Invoke();
            }
        }
    }

    public int GetCurrentHealth()
    {
        return currentHealth;
    }

    public int GetMaxHealth()
    {
        return maxHealth;
    }

    public int GetPotionCount()
    {
        return potionCount;
    }
}
