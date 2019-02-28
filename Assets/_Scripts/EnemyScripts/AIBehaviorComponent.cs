using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AIBehaviorComponent : MonoBehaviour
{
    public enum AIStates
    {
        IdleState,   //0
        ChaseState,  //2
        AttackState  //3
    }

    public AIStates CurrentState = AIStates.IdleState;

    public GameObject Target;

    public GunComponent Gun;
    private Rigidbody RB;
    private NavMeshAgent Agent;

    public bool AlwaysChaseTarget = true;
    public float AgentAttackDistance = 5.0f;
    public float AgentAttackTurnSpeed = 2;
	// Use this for initialization
	void Start ()
    {
        RB = GetComponent<Rigidbody>();
        Agent = GetComponent<NavMeshAgent>();
	}


    public void DoIdle()
    {

        if (AlwaysChaseTarget == false)
        {
            Agent.isStopped = true;
        }
        else
        {
            CurrentState = AIStates.ChaseState;
        }
    }
   
    public void DoChase()
    {
        if (Agent.isStopped == true)
        {
            Agent.isStopped = false;
        }

        Agent.SetDestination(Target.transform.position);
       
        if(Agent.remainingDistance <= AgentAttackDistance)
        {
            CurrentState = AIStates.AttackState;
        }

    }

    public void DoAttack()
    {
        if (Agent.isStopped == false)
        {
            Agent.isStopped = true;
        }

        Agent.SetDestination(Target.transform.position);

        

        if (Agent.remainingDistance > AgentAttackDistance)
        {
            CurrentState = AIStates.ChaseState;
        }
        else
        {
            Vector3 Dir = (transform.position - Target.transform.position).normalized;


            transform.forward = Vector3.Slerp(transform.forward, -Dir, AgentAttackTurnSpeed * Time.deltaTime);

            Gun.Fire();
        }
        

    }
    //will update the Agents target destination to move towards and do a 
    //distance check to switch states
    public void UpdateState()
    {
        //State Machines
        switch(CurrentState)
        {
            default:
                CurrentState = AIStates.IdleState;
            break;

                //State1
            case AIStates.IdleState:
                DoIdle();
                break;
                // State2
            case AIStates.ChaseState:
                DoChase();
                break;
                //State3
            case AIStates.AttackState:
                DoAttack();
                break;
        }
        

    }

	// Update is called once per frame
	void Update ()
    {
        UpdateState();
	}
}
