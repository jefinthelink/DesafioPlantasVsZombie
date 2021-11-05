using UnityEngine.SceneManagement;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    [SerializeField] private GameObject gameOverPanel, gameplayPanel;

    private void Awake()
    {
        if (!instance)
            instance = this;
        else
            Destroy(gameObject);

        //DontDestroyOnLoad(gameObject);
    }
    private void Start()
    {
        Debug.Log("timescale = " + Time.time);
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

        unpauseGame();
        SceneManager.LoadScene("GamePlay");
    }
}
