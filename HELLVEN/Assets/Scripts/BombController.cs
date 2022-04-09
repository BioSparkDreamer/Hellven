using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombController : MonoBehaviour
{
    [Header("Explode Variables")]
    public float timeToExplode = 0.5f;
    public GameObject explodeEffect;

    [Header("Destructible Variables")]
    public float blastRange;
    public LayerMask whatIsDestructible;

    void Update()
    {
        //Make time to explode count down
        timeToExplode -= Time.deltaTime;

        if (timeToExplode <= 0)
        {
            if (explodeEffect != null)
                Instantiate(explodeEffect, transform.position, transform.rotation);

            Destroy(gameObject);

            //Create an array of collider2D game object using Physics2D.OverlapCircle to make the array
            Collider2D[] objectsToDestroy = Physics2D.OverlapCircleAll(transform.position, blastRange, whatIsDestructible);

            if (objectsToDestroy.Length > 0)
            {
                //Use a for each loop to detect how many Collider2D game objects are in the list and destroy them
                foreach (Collider2D col in objectsToDestroy)
                    Destroy(col.gameObject);
            }
        }
    }
}
