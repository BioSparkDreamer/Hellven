using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFlyer : MonoBehaviour
{
    [Header("Movement Variables")]
    public float moveSpeed;
    public int currentPoint;
    public Transform[] points;

    [Header("Attacking Variables")]
    private Vector3 attackTarget;
    public float distanceToAttack, chaseSpeed, waitAfterAttack;
    private float attackCounter;


    void Start()
    {
        for (int i = 0; i < points.Length; i++)
            points[i].SetParent(null);
    }

    void Update()
    {
        if (attackCounter > 0)
        {
            attackCounter -= Time.deltaTime;
        }
        else
        {
            MovingBetweenPoints();
        }
    }

    void MovingBetweenPoints()
    {
        //If player is out of range then have the enemy navigate between points
        if (Vector3.Distance(transform.position, PlayerController.instance.transform.position) > distanceToAttack)
        {
            //Reset the attack target when the player moves away
            attackTarget = Vector3.zero;

            //Move the transform position of the enemy towards the current point
            transform.position = Vector3.MoveTowards(transform.position, points[currentPoint].position, moveSpeed * Time.deltaTime);

            //If the enemy reaches the current point then increment the current point by 1
            if (Vector3.Distance(transform.position, points[currentPoint].position) < .05f)
            {
                currentPoint++;

                //If the current point gets bigger than our array length than reset it back to 0
                if (currentPoint >= points.Length)
                    currentPoint = 0;
            }

            //Moving Right and Left depending on the current points x position
            if (transform.position.x < points[currentPoint].position.x)
            {
                transform.localScale = new Vector3(-1f, 1f, 1f);
            }
            else if (transform.position.x > points[currentPoint].position.x)
            {
                transform.localScale = Vector3.one;
            }
        }
        else
        {
            AttackingPlayer();
        }
    }

    void AttackingPlayer()
    {
        if (attackTarget == Vector3.zero)
            attackTarget = PlayerController.instance.transform.position;

        //Move the transform position of the enemy towards the attack target
        transform.position = Vector3.MoveTowards(transform.position, attackTarget, chaseSpeed * Time.deltaTime);

        if (transform.position.x < PlayerController.instance.transform.position.x)
            transform.localScale = new Vector3(-1f, 1f, 1f);
        else if (transform.position.x > PlayerController.instance.transform.position.x)
            transform.localScale = Vector3.one;

        //If the enemy has reached the attack target then do this code
        if (Vector3.Distance(transform.position, attackTarget) <= .1f)
        {
            attackCounter = waitAfterAttack;
            attackTarget = Vector3.zero;
        }
    }
}
