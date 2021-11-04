using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private GameObject[] enemys;
    [SerializeField] private float delayToSpawnEnemy = 2.0f;
    private float delayToSpawnEnemyAux;

    private void Start()
    {
        SetValues();
    }

    private void Update()
    {
        delayToSpawnEnemy -= Time.deltaTime;
        if (delayToSpawnEnemy <= 0.0f)
        {
            delayToSpawnEnemy = delayToSpawnEnemyAux;
            Spawn();
        }
    }

    private void SetValues()
    {
        delayToSpawnEnemyAux = delayToSpawnEnemy;
    }
    private void Spawn()
    {
        int index = Random.Range(0, enemys.Length);
        Instantiate(enemys[index],transform.position,enemys[index].transform.rotation);
    }


}
