using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer : MonoBehaviour
{
    public static Timer instance;
    [SerializeField]private float timeMatch = 40.0f;
    private float timeMatchAux;

    private void Start()
    {
        SetValues();
    }
    private void Update()
    {
        
    }
    private void CountTime()
    {
        timeMatch -= Time.deltaTime;
        if (timeMatch <= 0.0f)
        {
            GameManager.instance.PauseGame();
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
