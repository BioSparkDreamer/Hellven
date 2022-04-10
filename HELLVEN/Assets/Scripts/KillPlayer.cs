using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillPlayer : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            PlayerHealthController.instance.TakeDamage(15);
            PlayerHealthController.instance.ShowDeathEffect();

            if (PlayerHealthController.instance.currentHealth > 0)
            {
                GameManager.instance.Respawn();
            }
        }
    }
}
