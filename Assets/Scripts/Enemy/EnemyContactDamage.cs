using UnityEngine;

public class EnemyContactDamage : MonoBehaviour
{
    [SerializeField]
    private int damageAmount = 5;

    [SerializeField]
    private float damageInterval = 1.0f;

    [SerializeField]
    private Animator animator;

    private float timeSinceLastDamage = 0.0f;

    // Update is called once per frame
    void Update()
    {
        timeSinceLastDamage += Time.deltaTime;
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (timeSinceLastDamage < damageInterval)
        {
            return;
        }

        if (collision.collider.CompareTag("Player") == true)
        {
            PlayerStats playerStats = collision.gameObject.GetComponent<PlayerStats>();
            if(playerStats != null)
            {
                if(animator != null)
                {
                    animator.SetTrigger("Attack");
                }

                playerStats.TakeDamage(damageAmount);
                timeSinceLastDamage = 0.0f;
            }
        }
    }
}
