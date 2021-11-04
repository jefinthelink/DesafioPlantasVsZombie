using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [HideInInspector] public ModesOfBullet modes;
    [SerializeField] private float force = 10.0f;
    private Rigidbody rb;
    [HideInInspector] public int damage;

    private void Start()
    {
        rb = transform.GetComponent<Rigidbody>();
        rb.AddForce(new Vector3(-force,0.0f,0.0f),ForceMode.Force);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            if (modes == ModesOfBullet.normal)
            {
                //efeito de dano no inimigo
                Debug.Log("dano da bala " + damage);
                collision.gameObject.GetComponent<Enemy>().TakeDamage(damage);
                Destroy(this.gameObject);
            }
            if (modes == ModesOfBullet.fire)
            {
                //efeito de dano no inimigo
                Debug.Log("dano da bala " + damage);
                collision.gameObject.GetComponent<Enemy>().TakeDamage(damage);
                collision.gameObject.GetComponent<Enemy>().SetFire(); ;
                Destroy(this.gameObject);
            }
            if (modes == ModesOfBullet.ice)
            {
                //efeito de dano no inimigo
                Debug.Log("dano da bala " + damage);
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

    private void OnBecameVisible()
    {
       // Destroy(this.gameObject);
    }

}

public enum ModesOfBullet
{
normal,
fire,
ice
}
