using UnityEngine;

public class TowerShot : MonoBehaviour
{
    [HideInInspector] public ModesOfBullet modes;
    [SerializeField] private GameObject bullet;
    [SerializeField] private Transform gunBarrel;
    [SerializeField] private float delayShot = 1.0f;
    private float delayShotAux;
    public bool canBeShot = false;
    public Tower tower;
    public GameObject enemy;


    private void Start()
    {
        tower = transform.GetComponentInParent<Tower>();
        delayShotAux = delayShot;
        bullet.GetComponent<Bullet>().damage = tower.damage;
        bullet.GetComponent<Bullet>().modes = modes;
    }
    private void Update()
    {
        delayShot -= Time.deltaTime;
        if (enemy != null)
        {
            canBeShot = true;
        }
        else 
        {
            canBeShot = false;
        }
        Shot();
    }

    private void Shot()
    {
        
        if (delayShot <= 0.0f && canBeShot)
        {
            delayShot = delayShotAux;
            Instantiate(bullet, gunBarrel.position, bullet.transform.rotation);
            Debug.Log("instanciando bala");
            
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Enemy")
        {
            // canBeShot = true;
            enemy = other.gameObject;
        }
    }

    //private void OnTriggerExit(Collider other)
    //{
    //    if (other.tag == "Enemy")
    //    {
    //        canBeShot = false;
    //    }
    //}


}
