using UnityEngine;

public class Weapon : MonoBehaviour
{
    [Header("Estadísticas del Arma")]
    [SerializeField] private float damage = 25f;
    [SerializeField] private float range = 50f;
    [SerializeField] private int maxAmmo = 30;
    private int currentAmmo;

    [SerializeField] private float fireRate = 0.5f;
    private float nextTimeToFire = 0f;

    void Start()
    {
        currentAmmo = maxAmmo;
         UIManager.Instance.UpdateAmmoCounter(currentAmmo, maxAmmo);
    }

    void Update()
    {
        // Ejemplo de detección de disparo (debería estar en un PlayerInput Manager)
        if (Input.GetButton("Fire1") && Time.time >= nextTimeToFire)
        {
            Shoot();
        }
    }

    void Shoot()
    {
        if (currentAmmo <= 0)
        {
            return;
        }

        nextTimeToFire = Time.time + fireRate;
        currentAmmo--;

        UIManager.Instance.UpdateAmmoCounter(currentAmmo, maxAmmo);

        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit, range))
        {
            Debug.Log("Hit: " + hit.transform.name);

            Health targetHealth = hit.transform.GetComponent<Health>();

            if (targetHealth != null)
            {
                targetHealth.TakeDamage((int)damage);
            }

        }
    }
    public void Reload(int amount)
    {
        currentAmmo += amount;
        if (currentAmmo > maxAmmo)
        {
            currentAmmo = maxAmmo;
        }
         UIManager.Instance.UpdateAmmoCounter(currentAmmo, maxAmmo);
    }
}