using UnityEngine.UI;
using UnityEngine;

public class Tower : MonoBehaviour
{
    [SerializeField] private ModesOfBullet modes;
    public int id;
    [SerializeField] private Slider lifeBar;
    public int life = 10;
    private int maxLife;
    public int damage = 2;
    private bool isFire = false;
    private float delayFire = 1.0f, timeFire = 5.0f, timeFireAux = 5.0f;
    [SerializeField] private TowerShot towerShot;
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
    }
    private void Update()
    {
        TakeFire();
    }
    public void TakeDAmege(int damage)
    {
        Debug.Log("dando dano na torre " + damage);
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
        //colocar efeito de morte
        Destroy(this.gameObject);
    }
    public void SetFire()
    {
        isFire = true;
        timeFire = timeFireAux;
        //startar particula de fogo
    }
    private void endFire()
    {
    //parar oarticula de fogo
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
