using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private TrailRenderer trial;
    [HideInInspector] public ModesOfBullet modes;
    [SerializeField] private float force = 10.0f;
    private Rigidbody rb;
    [HideInInspector] public int damage;

    private void Start()
    {
        SetValues();
    }
    private void SetValues() 
    {
        Debug.Log("modo da bala na bala " + modes.ToString());
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

    private void Update()
    {
        Debug.Log("modo da bala = " + modes.ToString());
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
           
            if (modes == ModesOfBullet.normal)
            {
                //efeito de dano no inimigo
               
                collision.gameObject.GetComponent<Enemy>().TakeDamage(damage);
                Destroy(this.gameObject);
            }
            if (modes == ModesOfBullet.fire)
            {
                //efeito de dano no inimigo
               
                collision.gameObject.GetComponent<Enemy>().TakeDamage(damage);
                collision.gameObject.GetComponent<Enemy>().SetFire();   
                Destroy(this.gameObject);
            }
            if (modes == ModesOfBullet.ice)
            {
                //efeito de dano no inimigo
               
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
