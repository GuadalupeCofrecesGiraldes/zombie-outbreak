using UnityEngine;

public class Pickup : MonoBehaviour
{
    public enum PickupType { Health, Ammo }
    public PickupType type;
    [SerializeField] private int value = 25; // Cantidad de vida o balas a restaurar

    private void OnTriggerEnter(Collider other)
    {
        // Asumiendo que el Jugador tiene el tag "Player"
        if (other.CompareTag("Player"))
        {
            ApplyEffect(other.gameObject);
            Destroy(gameObject); // El item desaparece al ser recogido
        }
    }

    void ApplyEffect(GameObject player)
    {
        switch (type)
        {
            case PickupType.Health:
                Health playerHealth = player.GetComponent<Health>();
                if (playerHealth != null)
                {
                    // Necesitas un método publico para curar en Health.cs
                    // playerHealth.Heal(value); 
                }
                break;

            case PickupType.Ammo:
                Weapon playerWeapon = player.GetComponentInChildren<Weapon>();
                if (playerWeapon != null)
                {
                    playerWeapon.Reload(value);
                }
                break;
        }
    }
}