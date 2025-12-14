using UnityEngine;

public class PotionPickup : MonoBehaviour
{
    [SerializeField]
    private int potionAmount = 1;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerStats playerStats = collision.GetComponent<PlayerStats>();

        if(playerStats != null)
        {
            playerStats.AddPotion(potionAmount);

            Destroy(gameObject);
        }
    }
}
