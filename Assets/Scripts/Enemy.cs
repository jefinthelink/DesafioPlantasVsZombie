using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{

    [Header("tipo de inimigo")]
    [SerializeField] private ModesOfEnemy modes;
    [Header("valor em moedas")]
     public int coinValue = 1;
    [Header("Vida")]
    [SerializeField] private Slider lifeBar;
    [SerializeField]private int life = 10;
    [Header("Mordida")]
    [SerializeField] private int normalDamage = 2;
    [SerializeField] private int damageExplode = 5;
    [SerializeField] private int damageFire = 1;
    [SerializeField] private float delayOfBite = 1.0f;
    [HideInInspector]public bool toBeBite = false;
    [HideInInspector]public Tower towerToBite;
    
    private EnemyMoviment enemyMoviment;
    private float enemySpeedWithIce;
    private int maxLife;
    private float delayOfBiteAux;
    private Material material;
    private Color normalcolor;
    private bool isFire = false, slow = false;
    private float delayFire = 1.0f, timeFire = 5.0f, timeFireAux = 5.0f, delayIce = 1.0f, timeIce = 5.0f, timeIceAux = 5.0f;

    private void Start()
    {
        SetValues();
    }
    private void Update()
    {

        TakeFire();
        takeIce();
        delayOfBite -= Time.deltaTime;
        if (toBeBite && delayOfBite <= 0.0f)
        {
            delayOfBite = delayOfBiteAux;
            Bite(towerToBite);
        }
    }
    private void SetValues()
    {
        enemyMoviment = transform.GetComponent<EnemyMoviment>();
        enemySpeedWithIce = enemyMoviment.speed / 2;
        delayOfBiteAux = delayOfBite;
        maxLife = life;
        lifeBar.maxValue = maxLife;
        lifeBar.minValue = 0;
        lifeBar.value = life;
        lifeBar.gameObject.SetActive(false);
        material = transform.GetComponent<MeshRenderer>().material;
        normalcolor = material.color;
    }
    public void Bite(Tower tower_)
    {
        if (towerToBite == null)
        {
            transform.GetComponent<EnemyMoviment>().toBeMoviment = true;
            toBeBite = false;
        }
        else
        {
            //efeito de ataque
            if (modes == ModesOfEnemy.normal)
            {
                tower_.TakeDAmege(normalDamage);
                Debug.Log("dando a mordida");
            }
            if (modes == ModesOfEnemy.explode)
            {
                tower_.TakeDAmege(damageExplode);
                Debug.Log("explodindo");
                Death();
            }
            if (modes == ModesOfEnemy.tank)
            {
                tower_.TakeDAmege(normalDamage);
                Debug.Log("dando a mordida tanque");
            }
            if (modes == ModesOfEnemy.takeFire)
            {
                tower_.TakeDAmege(damageFire);
                tower_.SetFire();

                Debug.Log("dando a mordida de fogo");
            }
        }
    }
    public void TakeDamage(int damage)
    {
        if (damage >= life)
        {
            Death();
        }
        else
        {
             if (life == maxLife)
            {
            lifeBar.gameObject.SetActive(true);
            }
            if (modes == ModesOfEnemy.tank)
            {
                life -= damage / 2;
                lifeBar.value = life;
            }
            else
            {
            life -= damage;
            lifeBar.value = life;
            }
        }
    }
    private void Death()
    {
        GameManager.instance.coins += coinValue;
        //colocar efeito de morte
        Destroy(this.gameObject);
    }
    public void SetFire()
    {
        isFire = true;
        timeFire = timeFireAux;
        material.color = Color.red;
        //startar particula de fogo
    }
    public void SetIce()
    {
        slow = true;
        timeIce = timeIceAux;
        enemyMoviment.speed = enemySpeedWithIce;
        material.color = Color.blue;
        //setar particula de gelo
    }
    private void endFire()
    {
        //parar oarticula de fogo
        material.color = normalcolor;
    }
    private void TakeFire()
    {
        if (isFire)
        {
            timeFire -= Time.deltaTime;
            if (timeFire > 0.0f)
            {
                delayFire -= Time.deltaTime;
                if (delayFire <= 0.0f)
                {
                    TakeDamage(1);
                    delayFire = 1.0f;
                }

            }
            else
            {
                isFire = false;
                timeFire = timeFireAux;
                endFire();
            }

        }
    }
    private void takeIce()
    {
        if (slow)
        {
            timeIce -= Time.deltaTime;
            if (timeIce < 0.0f)
            {
                slow = false;
                timeIce = timeIceAux;
                endIce();
            }
        }
    }

    private void endIce()
    {
        enemyMoviment.speed = enemyMoviment.speedAux;
        material.color = normalcolor;
    //parar particula de gelo
    }

    
}

public enum ModesOfEnemy
{
normal,
explode,
tank,
takeFire
}