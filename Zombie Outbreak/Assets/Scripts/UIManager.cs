using TMPro;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance { get; private set; }

    [Header("Counters")]
    [SerializeField] private TextMeshProUGUI healthCounterText;
    [SerializeField] private TextMeshProUGUI scoreCounterText;
    [SerializeField] private TextMeshProUGUI ammoCounterText;

    [Header("End Screens")]
    [SerializeField] private GameObject endGamePanel;
    [SerializeField] private TextMeshProUGUI resultText;

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

    public void ShowEndScreen(bool victory, int finalScore)
    {
        endGamePanel.SetActive(true);

        Time.timeScale = 0f;

        Cursor.lockState = CursorLockMode.None;

        Cursor.visible = true;

        string message;
        if (victory)
        {
            message = "¡VICTORIA! \nSobreviviste al ataque de zombies.";
            resultText.color = Color.green;
        }
        else
        {
            message = "GAME OVER \nFuiste devorado por los zombies.";
             resultText.color = Color.red;
        }

        resultText.text = message + $"\n\nPuntaje Final: {finalScore}";

         healthCounterText.gameObject.SetActive(false);
         scoreCounterText.gameObject.SetActive(false);
    }

    public void UpdateHealthCounter(int currentHealth)
    {
        healthCounterText.text = currentHealth.ToString();
    }

    public void UpdateScoreCounter(int score)
    {
        scoreCounterText.text = score.ToString();
    }

    public void UpdateAmmoCounter(int currAmmo, int maxAmmo)
    {
        ammoCounterText.text = currAmmo.ToString() + " / " + maxAmmo.ToString();
    }

    public void BackToMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
