using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovementComponent : MonoBehaviour {

    public GameObject Target;

    private Vector3 Offset;

    public float MoveSpeed = 1.0f;

	// Use this for initialization
	void Start () {
        Offset = transform.position - Target.transform.position;
	}
	
	// Update is called once per frame
	void LateUpdate () {
        Vector3 DesiredPosition = Target.transform.position + Offset;

        transform.position = Vector3.Lerp(transform.position, DesiredPosition, MoveSpeed * Time.deltaTime);
	}
}
