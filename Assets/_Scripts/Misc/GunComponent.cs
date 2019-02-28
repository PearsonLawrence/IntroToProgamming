using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunComponent : MonoBehaviour {

    public GameObject Owner;

    //How often the bullet is fired
    public float FireFrequency;
    //ShootDelay actualy timer countdown
    private float ShootDelay;

    //how fast we fire the bullet
    public float ShootSpeed;

    //Pre created bullet that we spawn
    public GameObject BulletPrefab;

    //Empty gameobject in the scene that we spawn the bullet at (Position and rotation)
    public GameObject ShootPoint;


    public void Fire()
    {
        
        if(ShootDelay <= 0)
        {
            // Store a reference to the new bullet we spawn at shootpoints postion and rotation
            GameObject TempBullet = Instantiate(BulletPrefab, ShootPoint.transform.position, ShootPoint.transform.rotation);

            // Get component of stored prefab that we spawned and set its velocity to our shootpoints forward multiplied by guns shoot speed
            TempBullet.GetComponent<Rigidbody>().velocity = ShootPoint.transform.forward * ShootSpeed;

            TempBullet.GetComponent<BulletController>().Owner = Owner;

            Destroy(TempBullet, 5);

            ShootDelay = FireFrequency;
        }

    }

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        ShootDelay -= Time.deltaTime;
	}
}
