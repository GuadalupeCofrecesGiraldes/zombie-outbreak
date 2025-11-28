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
    }

    public void TakeDamage(int damageAmount)
    {
        if (currentHealth <= 0) return;

        Debug.Log($"Current health: {currentHealth}");
        currentHealth -= damageAmount;

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        if (zombieAI != null)
        {
            zombieAI.DieLogic();
        }

        Destroy(gameObject, 5f);
    }
}