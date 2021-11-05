using TMPro;
using UnityEngine;

public class Timer : MonoBehaviour
{
    [SerializeField] private TMP_Text text;
    public static Timer instance;
    [SerializeField]private float timeMatch = 40.0f;
    private float timeMatchAux;

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
        timeMatch -= Time.deltaTime;
        text.text = timeMatch.ToString();
        if (timeMatch <= 0.0f)
        {
            GameManager.instance.PauseGame();
            GameManager.instance.GameOver();
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
