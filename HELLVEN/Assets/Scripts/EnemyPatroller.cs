using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPatroller : MonoBehaviour
{
    [Header("Object Variables")]
    public Rigidbody2D theRB;

    [Header("Movement Variables")]
    public float moveSpeed;
    public Transform leftPoint, rightPoint;
    public bool movingRight;

    void Start()
    {
        //Get Components
        theRB = GetComponent<Rigidbody2D>();

        //Detach the transform points on the enemy
        leftPoint.parent = null;
        rightPoint.parent = null;
        movingRight = true;
    }

    void Update()
    {
        if (movingRight)
        {
            transform.localScale = new Vector3(-1f, 1f, 1f);

            if (transform.position.x > rightPoint.position.x)
            {
                movingRight = false;
            }
        }
        else
        {
            transform.localScale = Vector3.one;

            if (transform.position.x < leftPoint.position.x)
            {
                movingRight = true;
            }
        }
    }

    void FixedUpdate()
    {
        if (movingRight)
        {
            theRB.velocity = new Vector2(moveSpeed, theRB.velocity.y);
        }
        else if (!movingRight)
        {
            theRB.velocity = new Vector2(-moveSpeed, theRB.velocity.y);
        }
    }
}
