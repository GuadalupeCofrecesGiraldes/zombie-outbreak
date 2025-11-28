using UnityEngine;

public class Weapon : MonoBehaviour
{
    [Header("Stats")]
    [SerializeField] private float damage = 25f;
    [SerializeField] private float range = 50f;
    [SerializeField] private float fireRate = 0.5f;
    [SerializeField] private int maxAmmo = 30;
    [SerializeField] private GameObject hitEffectPrefab;
    [SerializeField] private LayerMask whatToHit;

    private Camera playerCamera;
    private int currentAmmo;
    private float nextTimeToFire = 0f;

    void Start()
    {
        playerCamera = Camera.main;
        currentAmmo = maxAmmo;
        if (UIManager.Instance != null)
        {
            UIManager.Instance.UpdateAmmoCounter(currentAmmo, maxAmmo);
        }
    }

    public bool TryShoot()
    {
        if (Time.time < nextTimeToFire)
        {
            return false;
        }

        if (currentAmmo <= 0)
        {
            Debug.Log("Click! Sin munición.");
            return false;
        }

        nextTimeToFire = Time.time + fireRate;
        currentAmmo--;

        if (UIManager.Instance != null)
        {
            UIManager.Instance.UpdateAmmoCounter(currentAmmo, maxAmmo);
        }
        Debug.Log("Going to shoot");
        CastRayForDamage();

        return true;
    }

    private void CastRayForDamage()
    {
        if (playerCamera == null) return;

        Debug.Log("Shooting");
        RaycastHit hit;

        Vector3 origin = playerCamera.transform.position;
        Vector3 direction = playerCamera.transform.forward;

        if (Physics.Raycast(origin, direction, out hit, range, whatToHit))
        {
            Health targetHealth = hit.transform.GetComponent<Health>();

            if (targetHealth != null)
            {
                targetHealth.TakeDamage((int)damage);
            }

            if (hitEffectPrefab != null)
            {
                GameObject impact = Instantiate(hitEffectPrefab, hit.point, Quaternion.LookRotation(hit.normal));
                Destroy(impact, 2f);
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
        if (UIManager.Instance != null)
        {
            UIManager.Instance.UpdateAmmoCounter(currentAmmo, maxAmmo);
        }
    }
}