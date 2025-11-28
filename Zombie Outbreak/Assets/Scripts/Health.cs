using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] private int maxHealth = 100;
    private int currentHealth;

    private ZombieAi zombieAI;

    void Start()
    {
        currentHealth = maxHealth;
        zombieAI = GetComponent<ZombieAi>();

        if(UIManager.Instance != null)
        {
            UIManager.Instance.UpdateHealthCounter(currentHealth);
        }
    }

    public void TakeDamage(int damageAmount)
    {
        if (currentHealth <= 0) return;

        Debug.Log($"Current health: {currentHealth}");
        if(damageAmount >= currentHealth)
        {
            currentHealth = 0;
        } else
        {
            currentHealth -= damageAmount;
        }

        if (UIManager.Instance != null)
        {
            UIManager.Instance.UpdateHealthCounter(currentHealth);
        }

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        if (zombieAI != null && ScoreManager.Instance != null)
        {
            ScoreManager.Instance.AddScore(10);
            zombieAI.DieLogic();
        }

        Destroy(gameObject, 1f);
    }
}