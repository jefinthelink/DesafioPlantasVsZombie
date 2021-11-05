using TMPro;
using UnityEngine;

public class Timer : MonoBehaviour
{
    [Header("text")]
    [SerializeField] private TMP_Text text;
    public static Timer instance;
    [Header("tempo de partida")]
    [SerializeField]private float timeMatch = 40.0f;
    private float timeMatchAux;
    private bool pauseGame = false;

    private void Start()
    {
        SetValues();
    }
    private void Update()
    {
        CountTime();
    }
    private void CountTime()
    {
        if (!pauseGame)
        {
            timeMatch -= Time.deltaTime;
            text.text = timeMatch.ToString("f");
            if (timeMatch <= 0.0f)
            {
                pauseGame = true;
                Debug.Log("Fim");
                GameManager.instance.PauseGame();
                GameManager.instance.GameOver();
            }
        }
    }
    private void SetValues()
    {
        timeMatchAux = timeMatch;
    }
    public float GetTimeMatch()
    {
        return timeMatch;
    }
}
