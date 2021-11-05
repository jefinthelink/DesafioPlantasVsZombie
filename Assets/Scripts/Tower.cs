using UnityEngine.UI;
using UnityEngine;

public class Tower : MonoBehaviour
{
    [Header("modos")]
    public ModesOfBullet modes;
    [Header("efeitos")]
    [SerializeField] private GameObject fire;
    [Header("vida")]
    [SerializeField] private Slider lifeBar;
    public int life = 10;
    private int maxLife;
    [Header("dano")]
    public int damage = 2;
    private bool isFire = false;
    private float delayFire = 1.0f, timeFire = 5.0f, timeFireAux = 5.0f;
    [Header("outros")]
    [SerializeField] private TowerShot towerShot;
    public int id;
    private void Awake()
    {
        towerShot.modes = modes;
        if (modes == ModesOfBullet.normal)
            id = 1;

        if (modes == ModesOfBullet.ice)
            id = 2;

        if (modes == ModesOfBullet.fire)
            id = 3;
    }
    private void Start()
    {
        SetValues();
    }
    private void SetValues()
    {
        maxLife = life;
        lifeBar.maxValue = maxLife;
        lifeBar.minValue = 0;
        lifeBar.value = life;
        lifeBar.gameObject.SetActive(false);
        fire.SetActive(false);
    }
    private void Update()
    {
        TakeFire();
    }
    public void TakeDAmege(int damage)
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

            life -= damage;
            lifeBar.value = life;
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
        fire.SetActive(true);
    }
    private void endFire()
    {
        fire.SetActive(false);
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
                    TakeDAmege(1);
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
}
