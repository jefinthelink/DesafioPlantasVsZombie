using UnityEngine.Audio;
using UnityEngine;

public class TowerShot : MonoBehaviour
{
    
    [HideInInspector] public ModesOfBullet modes;
    [Header("prefabs")]
    [SerializeField] private GameObject bullet;
    [Header("cano")]
    [SerializeField] private Transform gunBarrel;
    [Header("tempos")]
    [SerializeField] private float delayShot = 1.0f;
    private float delayShotAux;
    public bool canBeShot = false;
    public GameObject enemy;
    private Tower tower;
    private AudioSource audioSource;


    private void Start()
    {
        tower = transform.GetComponentInParent<Tower>();
        delayShotAux = delayShot;
        bullet.GetComponent<Bullet>().damage = tower.damage;
        bullet.GetComponent<Bullet>().modes = modes;
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
            enemy = other.gameObject;
        }
    }
}
