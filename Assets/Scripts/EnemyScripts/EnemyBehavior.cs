using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyBehavior : MonoBehaviour
{
    [SerializeField] private GameObject player, wayPointParent = null;
    [SerializeField] private float speed, distanceToPlayer;
    [SerializeField] Animator anim = null;
    [SerializeField] private NavMeshAgent agent = null;
    [SerializeField] private List<Vector3> wayPoints = null;
    // Start is called before the first frame update
    void Start()
    {
        SetWayPoints();
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

    public Vector3 GetRandomWayPoint()
    {
        int temp = (int) Random.Range(0, wayPoints.Count);
        return wayPoints[temp];
    }
    #endregion

    #region Mutator Functions
    private void SetWayPoints()
    {
        for(int i = 0; i < wayPointParent.transform.childCount; i ++)
        {
            Debug.Log("Got One");
            wayPoints.Add(wayPointParent.transform.GetChild(i).position);
        }
    }

    public void SetEnemyDestination(Vector3 destination)
    {
        agent.SetDestination(destination);
    }
    #endregion
}
