using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPatroller : MonoBehaviour
{
    [Header("Object Variables")]
    public Rigidbody2D theRB;
    public Animator anim;

    [Header("Movement Variables")]
    public float moveSpeed;
    public Transform leftPoint, rightPoint;
    public bool movingRight;
    public float waitAtPoint;
    private float waitCounter;

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
        if (waitCounter > 0)
        {
            waitCounter -= Time.deltaTime;
            anim.SetBool("isMoving", false);
            theRB.velocity = Vector3.zero;
        }
        else
        {
            if (movingRight)
            {
                transform.localScale = new Vector3(-1f, 1f, 1f);
                theRB.velocity = new Vector2(moveSpeed, theRB.velocity.y);

                if (transform.position.x > rightPoint.position.x)
                {
                    movingRight = false;
                    waitCounter = waitAtPoint;
                }
            }
            else
            {
                transform.localScale = Vector3.one;
                theRB.velocity = new Vector2(-moveSpeed, theRB.velocity.y);

                if (transform.position.x < leftPoint.position.x)
                {
                    movingRight = true;
                    waitCounter = waitAtPoint;
                }
            }

            anim.SetBool("isMoving", true);
        }
    }
}
