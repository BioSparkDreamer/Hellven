using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    [Header("Object Variables")]
    public Rigidbody2D theRB;

    [Header("Movement Variables")]
    public float moveSpeed;
    public Vector2 moveDir;

    [Header("Extra Variables")]
    public int damageToDeal;
    public GameObject impactEffect;

    void Start()
    {
        //Get Components
        theRB = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        //Move the velocity of the bullet depending on the move direction & move speed
        theRB.velocity = (moveDir * moveSpeed);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Enemy")
        {
            other.GetComponent<EnemyHealthController>().TakeDamage(damageToDeal);
        }

        //Instantiate the Impact Effect
        if (impactEffect != null)
            Instantiate(impactEffect, transform.position, transform.rotation);

        //Destroy the bullet
        Destroy(gameObject);
    }

    private void OnBecameInvisible()
    {
        //Destroy the gameobject if it leaves the editor view or screen
        Destroy(gameObject);
    }
}
