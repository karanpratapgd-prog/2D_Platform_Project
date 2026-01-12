using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [Header("Score Settings")]
    public int coinCount = 0;
    public int winScore = 45;
    public Text coinText;          

    [Header("Win Screen")]
    public GameObject winCanvas;   

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        UpdateCoinUI();

        if (winCanvas != null)
            winCanvas.SetActive(false);

        Time.timeScale = 1f; 
    }

    
    public void AddCoin()
    {
        coinCount++;
        UpdateCoinUI();

        if (coinCount >= winScore)
        {
            WinGame();
        }
    }

   
    void UpdateCoinUI()
    {
        if (coinText != null)
        {
            coinText.text = "Coins : " + coinCount;
        }
        else
        {
            Debug.LogWarning("CoinText not assigned in GameManager");
        }
    }

    
    void WinGame()
    {
        Time.timeScale = 0f;       
        if (winCanvas != null)
            winCanvas.SetActive(true);
    }

    
    public void ReplayGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    
    public void QuitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}
