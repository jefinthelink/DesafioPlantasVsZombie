using UnityEngine;

public class Bullet : MonoBehaviour
{   [Header("efeito da bala")]
    [SerializeField] private TrailRenderer trial;
    [Header("força de lançamento")]
    [SerializeField] private float force = 10.0f;
    [HideInInspector] public ModesOfBullet modes;
    [HideInInspector] public int damage;
    private Rigidbody rb;

    private void Start()
    {
        SetValues();
    }
    private void SetValues() 
    {
        
        rb = transform.GetComponent<Rigidbody>();
        rb.AddForce(new Vector3(-force, 0.0f, 0.0f), ForceMode.Force);
        Destroy(this.gameObject, 5);
        if (modes == ModesOfBullet.fire)
        {
            trial.startColor = Color.red;
            trial.endColor = Color.clear;
        }

        if (modes == ModesOfBullet.ice)
        {
            trial.startColor = Color.blue;
            trial.endColor = Color.clear;
        }


        if (modes == ModesOfBullet.normal)
        {
            trial.startColor = Color.black;
            trial.endColor = Color.clear;
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
           
            if (modes == ModesOfBullet.normal)
            {  
                collision.gameObject.GetComponent<Enemy>().TakeDamage(damage);
                Destroy(this.gameObject);
            }
            if (modes == ModesOfBullet.fire)
            {
                collision.gameObject.GetComponent<Enemy>().TakeDamage(damage);
                collision.gameObject.GetComponent<Enemy>().SetFire();   
                Destroy(this.gameObject);
            }
            if (modes == ModesOfBullet.ice)
            {
                collision.gameObject.GetComponent<Enemy>().TakeDamage(damage);
                collision.gameObject.GetComponent<Enemy>().SetIce();
                Destroy(this.gameObject);
            }
        }
        if (collision.gameObject.tag == "Terrain")
        {
            Destroy(this.gameObject);
        }
    }
    private void OnBecameInvisible()
    {
        Destroy(this.gameObject);
    }
}

public enum ModesOfBullet
{
normal,
fire,
ice
}
