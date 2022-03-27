using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyJumper : MonoBehaviour
{
    [Header("Object Variables")]
    public Rigidbody2D theRB;

    [Header("Movement Variables")]
    public float moveSpeed;
    private int currentPoint;
    public Transform[] patrolPoints;
    public float waitAtPoint, jumpForce;
    private float waitCounter;

    void Start()
    {
        //Get Component
        theRB = GetComponent<Rigidbody2D>();

        waitCounter = waitAtPoint;

        foreach (Transform pPoint in patrolPoints)
        {
            pPoint.SetParent(null);
        }
    }

    void Update()
    {
        if (Mathf.Abs(transform.position.x - patrolPoints[currentPoint].position.x) > .2f)
        {
            if (transform.position.x < patrolPoints[currentPoint].position.x)
            {
                theRB.velocity = new Vector2(moveSpeed, theRB.velocity.y);
                transform.localScale = new Vector3(-1f, 1f, 1f);
            }
            else
            {
                theRB.velocity = new Vector2(-moveSpeed, theRB.velocity.y);
                transform.localScale = Vector3.one;
            }

            if (transform.position.y < patrolPoints[currentPoint].position.y - .5f && theRB.velocity.y < .1f)
            {
                theRB.velocity = new Vector2(theRB.velocity.x, jumpForce);
            }
        }
        else
        {
            theRB.velocity = new Vector2(0, theRB.velocity.y);
            waitCounter -= Time.deltaTime;

            if (waitCounter <= 0)
            {
                waitCounter = waitAtPoint;
                currentPoint++;

                if (currentPoint >= patrolPoints.Length)
                {
                    currentPoint = 0;
                }
            }
        }
    }
}
