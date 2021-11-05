using UnityEngine.SceneManagement;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    [SerializeField] private GameObject gameOverPanel, gameplayPanel;
    public int coins;

    private void Awake()
    {
        if (!instance)
            instance = this;
        else
            Destroy(gameObject);

        //DontDestroyOnLoad(gameObject);
    }
    public void PauseGame()
    {
        Time.timeScale = 0;
    }
    public void unpauseGame()
    {
        Time.timeScale = 1;
    }
    public void GameOver()
    {
        gameOverPanel.SetActive(true);
        gameplayPanel.SetActive(false);
    }

    public void NewGame()
    {

        SceneManager.LoadScene("GamePlay");
        unpauseGame();
    }
}
