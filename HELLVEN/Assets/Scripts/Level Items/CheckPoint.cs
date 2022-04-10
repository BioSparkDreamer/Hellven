using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoint : MonoBehaviour
{
    [Header("CheckPoint Variables")]
    public SpriteRenderer theSR;
    public Sprite cpOn, cpOff;
    public Transform spawnPoint;

    //Function to turn the sprite back to the off sprite
    public void ResetCheckPoint()
    {
        theSR.sprite = cpOff;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            //Deactivate all checkpoints, set the new spawn point and turn the sprite to on
            GameManager.instance.DeactivateCheckPoints();
            GameManager.instance.SetSpawnPoint(spawnPoint.position);
            theSR.sprite = cpOn;
        }
    }
}
