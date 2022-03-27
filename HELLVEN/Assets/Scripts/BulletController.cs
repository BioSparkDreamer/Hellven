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

    void Start()
    {
        //Get Components
        theRB = GetComponent<Rigidbody2D>();
        AudioManager.instance.PlaySFXAdjusted(4);
    }

    void FixedUpdate()
    {
        //Move the velocity of the bullet
        theRB.velocity = (moveDir * moveSpeed);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Enemy")
        {
            other.GetComponent<EnemyHealthController>().TakeDamage(damageToDeal);
        }

        Destroy(gameObject);
    }

    private void OnBecameInvisible()
    {
        //Destroy the gameobject if it leaves the editor view or screen
        Destroy(gameObject);
    }
}
