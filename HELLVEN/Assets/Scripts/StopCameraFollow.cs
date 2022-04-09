using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StopCameraFollow : MonoBehaviour
{
    void Start()
    {

    }

    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            CameraController.instance.stopFollow = true;
        }
    }
}
