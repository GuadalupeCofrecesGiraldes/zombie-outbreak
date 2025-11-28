using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[System.Serializable]
public class Wave
{
    public string waveName;
    public float timeUntilNextWave = 30f;

    public List<EnemyGroup> enemyGroups;
}

[System.Serializable]
public class EnemyGroup
{
    public GameObject enemyPrefab;
    public int count;              
}

[System.Serializable]
public class PickupSpawnData
{
    public GameObject medkitPrefab;
    public GameObject ammoPrefab;

    [Range(0, 1)] public float medkitChance = 0.3f; 
    public int maxMedkits = 2;                     

    [Range(0, 1)] public float ammoChance = 0.5f;
    public int maxAmmoPacks = 4;
}

public class WaveManager : MonoBehaviour
{
    public static WaveManager Instance;

    [Header("Game Config")]
    [SerializeField] private float gameDuration = 180f;
    [SerializeField] private TextMeshProUGUI timerText;

    [Header("Waves")]
    [SerializeField] private List<Wave> waves;

    [Header("Spawn Points")]
    [SerializeField] private Transform[] spawnPoints;

    [Header("Pickups")]
    [SerializeField] private PickupSpawnData pickupData;

    private int currentWaveIndex = 0;
    private float waveCountdown;
    private float gameTimer;
    private bool isWaveActive = false;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        gameTimer = gameDuration;
        if (waves.Count > 0)
        {
            isWaveActive = true;
            StartNextWave();
        }
    }

    void Update()
    {
        if (gameTimer <= 0)
        {
            EndGame();
            return;
        }

        gameTimer -= Time.deltaTime;
        UpdateTimerUI();

        if (isWaveActive)
        {
            waveCountdown -= Time.deltaTime;

            if (waveCountdown <= 0)
            {
                StartNextWave();
            }
        }
    }

    private void UpdateTimerUI()
    {
        if (timerText != null)
        {
            int minutes = Mathf.FloorToInt(gameTimer / 60f);
            int seconds = Mathf.FloorToInt(gameTimer % 60f);
            timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
        }
    }

    private void StartNextWave()
    {
        if (currentWaveIndex < waves.Count)
        {
            Debug.Log("Iniciando Oleada " + waves[currentWaveIndex].waveName);
            StartCoroutine(SpawnWaveEnemies(waves[currentWaveIndex]));
            SpawnPickups();

            float duration = waves[currentWaveIndex].timeUntilNextWave;

            currentWaveIndex++;

            waveCountdown = duration;
        }
        else
        {
            Debug.Log("Todas las oleadas completadas.");
            isWaveActive = false;
        }
    }

    private IEnumerator SpawnWaveEnemies(Wave wave)
    {
        isWaveActive = true;

        foreach (var group in wave.enemyGroups)
        {
            for (int i = 0; i < group.count; i++)
            {
                Transform spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];

                Instantiate(group.enemyPrefab, spawnPoint.position, spawnPoint.rotation);

                yield return new WaitForSeconds(0.5f);
            }
        }

    }

    private void EndGame()
    {
        Debug.Log("¡Tiempo terminado! Fin del Juego.");
        this.enabled = false;
    }

    private void SpawnPickups()
    {
        if (spawnPoints.Length == 0)
        {
            Debug.LogWarning("No hay puntos de spawn para los pickups.");
            return;
        }

        for (int i = 0; i < pickupData.maxMedkits; i++)
        {
            if (Random.value < pickupData.medkitChance)
            {
                Transform spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];
                Instantiate(pickupData.medkitPrefab, spawnPoint.position, Quaternion.identity);
                Debug.Log("Spawned Medkit.");
            }
        }

        for (int i = 0; i < pickupData.maxAmmoPacks; i++)
        {
            if (Random.value < pickupData.ammoChance)
            {
                Transform spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];
                Instantiate(pickupData.ammoPrefab, spawnPoint.position, Quaternion.identity);
                Debug.Log("Spawned Ammo.");
            }
        }
    }
}