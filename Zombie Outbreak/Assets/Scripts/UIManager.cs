using TMPro;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance { get; private set; }

    [SerializeField] private TextMeshProUGUI healthCounterText;
    [SerializeField] private TextMeshProUGUI scoreCounterText;
    [SerializeField] private TextMeshProUGUI ammoCounterText;

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

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
