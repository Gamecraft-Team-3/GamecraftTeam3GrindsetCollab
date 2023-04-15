using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyBehavior : MonoBehaviour
{
    [SerializeField] private GameObject player = null;
    [SerializeField] private float speed, distanceToPlayer;
    [SerializeField] Animator anim = null;
    [SerializeField] private NavMeshAgent agent = null;
    [SerializeField] private List<Vector3> wayPoints = null;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }
    #region Getter Functions
    //Returns the distacne from the player
    public float GetPlayerDistance()
    {
        return distanceToPlayer;
    }

    //Returns the way points the enemy will be patroling
    public List<Vector3> GetWayPoints()
    {
        return wayPoints;
    }

    //Returns the Nav Mesh Agent
    public NavMeshAgent GetAgent()
    {
        return agent;
    }
    #endregion
}
