using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthComponent : MonoBehaviour {

    public float MaxHealth = 100;
    public float CurrentHealth;


    public void Death()
    {
        Destroy(gameObject);
    }

    public void TakeDamage(float DamageAmount)
    {
        CurrentHealth -= DamageAmount;

        if(CurrentHealth <= 0)
        {
            Death();
        }
    }


	// Use this for initialization
	void Start () {
        CurrentHealth = MaxHealth;// <----- DO THIS
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
