using System.Collections;
using System.Collections.Generic;
using UnityEngine;
             
public class CharacterMovementComponent : MonoBehaviour
{
    //How much force we add each frame
    public float Acceleration = 100.0f;

    private float CurrentMaxVelocity;
    public float MaxWalkVelocity = 5;
    public float MaxSprintVelocity = 10;

    private bool IsSprinting;
    //Current RB on main object
    private Rigidbody RB;

    public GunComponent Gun;

    //Player Input
    Vector3 InputVector;
    // Use this for initialization
    void Start ()
    {
        RB = GetComponent<Rigidbody>();
	}

    //Dynamic function able to move and RB based off of Horizontal and Vertical Input
    public void Movement(Rigidbody rb, Vector3 IPVector, float Accel, float MaxVel)
    {
        //accelerates rb
        rb.AddForce(IPVector * Accel * Time.deltaTime);
                      // set velocity to the clamp we created
        rb.velocity = VectorClamp(rb.velocity, -MaxVel, MaxVel, true, false, true);
    }

    // Takes in one vector and stops all axis from going out of bounds of a set parameter
    /*
    void VectorClamp(out Vector3 CurrentVector, float Min, float Max)
    {
        CurrentVector = Vector3.zero;

        // return Vector3.zero;
    }
    */
    // Takes in one vector and stops all axis from going out of bounds of a set parameter
    public Vector3 VectorClamp(Vector3 CurrentVector, float Min, float Max)
    {
        Vector3 Result = CurrentVector;

        Result.x = Mathf.Clamp(CurrentVector.x, Min, Max);

        Result.y = Mathf.Clamp(CurrentVector.y, Min, Max);

        Result.z = Mathf.Clamp(CurrentVector.z, Min, Max);

        return Result;
    }
    // Same name as VectorClamp but will do something else if you sepcify the bools at the end
    public Vector3 VectorClamp(Vector3 CurrentVector, float Min, float Max, bool ClampX = true, bool ClampY = true, bool ClampZ = true)
    {      //Condition

        Vector3 Result = CurrentVector;

        if (ClampX == true)
        {
            Result.x = Mathf.Clamp(CurrentVector.x, Min, Max);
        }

        if(ClampY == true)
        {
            Result.y = Mathf.Clamp(CurrentVector.y, Min, Max);
        }

        if (ClampZ == true)
        {
            Result.z = Mathf.Clamp(CurrentVector.z, Min, Max);
        }
        return Result;
    }
    
    
    void InputUpdate()
    {
        InputVector.x = Input.GetAxisRaw("Horizontal");
        InputVector.z = Input.GetAxisRaw("Vertical");

        IsSprinting = Input.GetKey(KeyCode.LeftShift);

        if (Input.GetMouseButton(0))
        {
            Gun.Fire();
        }

        if (IsSprinting == true)
        {
            CurrentMaxVelocity = MaxSprintVelocity;
        }
        else
        {
            CurrentMaxVelocity = MaxWalkVelocity;
        }
    }

    // Update is called once per frame
    void Update ()
    {
        InputUpdate();


        Movement(RB, InputVector, Acceleration, CurrentMaxVelocity);
        
    }
}
