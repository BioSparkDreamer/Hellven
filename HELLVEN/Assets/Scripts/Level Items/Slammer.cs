using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slammer : MonoBehaviour
{
    [Header("Slammer Variables")]
    private Vector3 startPoint;
    public Transform theSlammer, slamTarget;
    public float slamSpeed, resetSpeed, waitAfterSlam;
    private float waitCounter;
    private bool isSlamming, isResetting;

    void Start()
    {
        //Get the starting point of the slammer
        startPoint = theSlammer.position;
    }

    void Update()
    {
        //If slammer is detecting where the player is by using the distance between the slam point and the player
        if (!isResetting && !isSlamming)
        {
            if (Vector3.Distance(slamTarget.position, PlayerController.instance.transform.position) < 2f)
            {
                isSlamming = true;
                waitCounter = waitAfterSlam;
            }
        }

        //If slammer is slamming then do this code
        if (isSlamming)
        {
            theSlammer.position = Vector3.MoveTowards(theSlammer.position, slamTarget.position, slamSpeed * Time.deltaTime);

            if (theSlammer.position == slamTarget.position)
            {
                waitCounter -= Time.deltaTime;

                if (waitCounter <= 0)
                {
                    isSlamming = false;
                    isResetting = true;
                }
            }
        }

        //If slammer is resetting then do this code
        if (isResetting)
        {
            theSlammer.position = Vector3.MoveTowards(theSlammer.position, startPoint, resetSpeed * Time.deltaTime);

            if (theSlammer.position == startPoint)
                isResetting = false;
        }
    }
}
