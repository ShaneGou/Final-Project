using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class GoldPickup : MonoBehaviour
{
    public int value;
    public AudioClip coinSound;
    public GameObject pickupEffect;

    private void OnTriggerEnter(Collider other)
    {
         if (other.tag == "Player")
        {
            FindObjectOfType<GameManager>().AddGold(value);
            
            Instantiate(pickupEffect, transform.position, transform.rotation);
            AudioSource.PlayClipAtPoint(coinSound, transform.position);

            Destroy(gameObject);
        }
    }
}
