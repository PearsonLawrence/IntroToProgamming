using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour {
    
    public GameObject Owner;
    
    public float DamageValue = 10;

   public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject != Owner)
        {
            HealthComponent tempHealth = other.GetComponent<HealthComponent>();
            if(tempHealth != null)
            {
                tempHealth.TakeDamage(DamageValue);
            }
            Destroy(gameObject);
        }
    }
}
