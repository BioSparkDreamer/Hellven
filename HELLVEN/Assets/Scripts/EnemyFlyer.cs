using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFlyer : MonoBehaviour
{
    [Header("Object Variables")]
    public Rigidbody2D theRB;

    [Header("Movement Variables")]
    public float moveSpeed;
    public Transform upPoint, downPoint;
    public bool movingUp;

    void Start()
    {
        //Get Component
        theRB = GetComponent<Rigidbody2D>();

        //Detach the transform points on the enemy
        upPoint.parent = null;
        downPoint.parent = null;
        movingUp = true;
    }

    void Update()
    {
        if (movingUp)
        {
            if (transform.position.y > upPoint.position.y)
            {
                movingUp = false;
            }
        }
        else
        {
            if (transform.position.y < downPoint.position.y)
            {
                movingUp = true;
            }
        }
    }

    void FixedUpdate()
    {
        if (movingUp)
        {
            theRB.velocity = new Vector2(theRB.velocity.x, moveSpeed);
        }
        else if (!movingUp)
        {
            theRB.velocity = new Vector2(theRB.velocity.x, -moveSpeed);
        }
    }
}
