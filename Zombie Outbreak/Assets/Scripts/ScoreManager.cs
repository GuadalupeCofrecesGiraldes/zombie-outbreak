using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance { get; private set; }
    private int score = 0;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void AddScore(int points)
    {
        score += points;

        if (UIManager.Instance != null)
        {
            UIManager.Instance.UpdateScoreCounter(score);
        }
    }

    void Start()
    {
        // Inicializar la UI con el puntaje 0
        if (UIManager.Instance != null)
        {
            UIManager.Instance.UpdateScoreCounter(score);
        }
    }
}