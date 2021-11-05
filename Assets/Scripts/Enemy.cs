using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;
public class Enemy : MonoBehaviour
{
    [Header("tipo de inimigo")]
    [SerializeField] private ModesOfEnemy modes;
    [Header("efeitos de particulas")]
    [SerializeField] private GameObject fire;
    [SerializeField] private GameObject ice;
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
    
    private AudioSource audioSource;
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
        audioSource = transform.GetComponent<AudioSource>();
        fire.SetActive(false);
        ice.SetActive(false);
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
            audioSource.Play();
            if (modes == ModesOfEnemy.normal)
            {
                tower_.TakeDAmege(normalDamage);
               
            }
            if (modes == ModesOfEnemy.explode)
            {
                tower_.TakeDAmege(damageExplode);
                
                Death();
            }
            if (modes == ModesOfEnemy.tank)
            {
                tower_.TakeDAmege(normalDamage);
               
            }
            if (modes == ModesOfEnemy.takeFire)
            {
                tower_.TakeDAmege(damageFire);
                tower_.SetFire();

                
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
        Destroy(this.gameObject);
    }
    public void SetFire()
    {
        isFire = true;
        timeFire = timeFireAux;
        material.color = Color.red;
        fire.SetActive(true);
    }
    public void SetIce()
    {
        slow = true;
        timeIce = timeIceAux;
        enemyMoviment.speed = enemySpeedWithIce;
        material.color = Color.blue;
        ice.SetActive(true);
    }
    private void endFire()
    {
        fire.SetActive(false);
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
        ice.SetActive(true);
    }
    private void OnBecameInvisible()
    {
        Destroy(this.gameObject);
    }
}

public enum ModesOfEnemy
{
normal,
explode,
tank,
takeFire
}