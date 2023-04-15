using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyBaseFSM : StateMachineBehaviour
{
    protected float distance = 0;
    protected EnemyBehavior enemyBehavior = null;
    protected List<Vector3> wayPoints = null;
    protected NavMeshAgent agent = null;
    protected GameObject player = null;
    
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        distance = animator.GetFloat("DistanceFromPlayer");
        Debug.Log(distance);
        enemyBehavior = animator.gameObject.GetComponent<EnemyBehavior>();
        wayPoints = enemyBehavior.GetWayPoints();
        agent = enemyBehavior.GetAgent();
        player = enemyBehavior.GetPlayer();
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)

    {
        distance = enemyBehavior.GetPlayerDistance();
        animator.SetFloat("DistanceFromPlayer", distance);
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        
    }

}