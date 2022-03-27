using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [Header("Camera Variables")]
    private PlayerController thePlayer;
    public Collider2D boundsBox;
    private float halfHeight, halfWidth;

    void Start()
    {
        //Get a reference to the player controller script
        thePlayer = FindObjectOfType<PlayerController>();

        //Get the half width and height of the camera
        halfHeight = Camera.main.orthographicSize;
        halfWidth = halfHeight * Camera.main.aspect;
    }

    void LateUpdate()
    {
        if (thePlayer != null)
        {
            transform.position = new Vector3(
            Mathf.Clamp(thePlayer.transform.position.x, boundsBox.bounds.min.x + halfWidth, boundsBox.bounds.max.x - halfWidth),
            Mathf.Clamp(thePlayer.transform.position.y, boundsBox.bounds.min.y + halfHeight, boundsBox.bounds.max.y - halfHeight),
            transform.position.z);
        }
    }
}
