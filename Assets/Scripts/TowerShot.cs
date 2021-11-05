using UnityEngine.Audio;
using UnityEngine;

public class TowerShot : MonoBehaviour
{
    private AudioSource audioSource;
     public ModesOfBullet modes;
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
        Debug.Log("modo da torres = " + modes.ToString());
        Debug.Log("modo da bala = " + bullet.GetComponent<Bullet>().modes);
        audioSource = transform.GetComponent<AudioSource>();
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
            audioSource.Play();
            Instantiate(bullet, gunBarrel.position, bullet.transform.rotation);
           
            
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




}
