using UnityEngine;

public class EnemyMoviment : MonoBehaviour
{
    public bool toBeMoviment = true;
    private Enemy enemy;
    public float speed = 10.0f;
    [HideInInspector]public float speedAux;
    

    private void Start()
    {
        SetValues();
    }
    private void Update()
    {
        Move();
        CollidingWithEnemy();
    }
    private void CollidingWithEnemy()
    {
        if (!enemy.toBeBite)
        {
            Ray ray = new Ray(transform.position, transform.right);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, 1))
            {
               
                if (hit.transform.CompareTag("Enemy"))
                {
                    toBeMoviment = false;
                }
            }
            else
            {
                toBeMoviment = true;
            }
        }
    }
    private void Move()
    {
        if (toBeMoviment)
        {
        transform.Translate(new Vector3(speed, 0.0f,0.0f)*Time.deltaTime);
        }
    }
    private void SetValues()
    {
        enemy = transform.GetComponent<Enemy>();
        speedAux = speed;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Tower")
        {
            toBeMoviment = false;
            enemy.towerToBite = other.gameObject.GetComponent<Tower>();
            enemy.toBeBite = true;
        }
        if (other.gameObject.tag == "End")
        {
            GameManager.instance.PauseGame();
            GameManager.instance.GameOver();
        }

    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Tower")
        {
            toBeMoviment = true;
            enemy.towerToBite = null;
            enemy.toBeBite = false;
        }

    }
    
}
