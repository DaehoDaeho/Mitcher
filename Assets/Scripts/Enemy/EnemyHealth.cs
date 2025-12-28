using System;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField]
    private int maxHealth = 50;

    [SerializeField]
    private int currentHealth = 0;

    public event Action<int, int> OnChangedHP;

    [SerializeField] private bool isSlimeQuestTarget = false;   // 이 몬스터가 슬라임 퀘스트 대상인지 여부.
    [SerializeField] private QuestManager questManager;

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
        // 만약 슬라임 퇴치 퀘스트의 대상이면.
        if(isSlimeQuestTarget == true)
        {
            if(questManager != null)
            {
                // 퀘스트 매니저에 사망을 통보한다.
                questManager.ReportSlimeKilled();
            }
        }

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
