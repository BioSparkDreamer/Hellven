using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickups : MonoBehaviour
{
    [Header("Pickup Variables")]
    public bool isCollected;
    public bool isAmmo, isHealth, isBomb;
    public int pickupAmount;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player" && !isCollected)
        {
            if (isAmmo)
            {
                PlayerController.instance.currentAmmo += pickupAmount;
                UIController.instance.UpdateAmmoUI();
                DestroyPickup();

            }

            if (isHealth && PlayerHealthController.instance.currentHealth < PlayerHealthController.instance.maxHealth)
            {
                PlayerHealthController.instance.AddHealth(pickupAmount);
                DestroyPickup();

            }

            if (isBomb)
            {
                PlayerController.instance.currentBombAmmo += pickupAmount;
                UIController.instance.UpdateBombUI();
                DestroyPickup();
            }
        }
    }

    public void DestroyPickup()
    {
        isCollected = true;
        AudioManager.instance.PlaySFXAdjusted(6);
        Destroy(gameObject);
    }
}
