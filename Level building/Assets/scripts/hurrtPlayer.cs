using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hurrtPlayer : MonoBehaviour
{
    public int damageToGive = 1;
    public AudioClip hurtSound;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            Vector3 hitDirection = other.transform.position - transform.position;
            hitDirection = hitDirection.normalized;

            FindObjectOfType<HealthManager>().HurtPlayer(damageToGive, hitDirection);
            AudioSource.PlayClipAtPoint(hurtSound, transform.position);
        }
    }
}
