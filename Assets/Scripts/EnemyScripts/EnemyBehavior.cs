using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyBehavior : MonoBehaviour
{
    [SerializeField] private int numWayPointsToPatrol = 0;
    [SerializeField] private Animator anim = null;
    [SerializeField] private NavMeshAgent agent = null;
    [SerializeField] public GameObject wayPointParent = null;
    private List<Vector3> wayPoints = new List<Vector3>();
    [SerializeField] private EnemyManager enemyManager = null;
    private GameObject player = null;
    private float distanceToPlayer = 100;
    // Start is called before the first frame update
    void Start()
    {
        GetInitialStuff();
        SetWayPoints();

        Debug.Log("I am here");
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void GetInitialStuff()
    {
        enemyManager = FindAnyObjectByType<EnemyManager>();
        wayPointParent = GameObject.Find("WayPointList");
        player = enemyManager.GetPlayer();
    }
    #region Getter Functions
    //Returns the distacne from the player
    public float GetPlayerDistance()
    {
        distanceToPlayer = Vector3.Distance(player.transform.position, this.transform.position);
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
        if(wayPoints.Count == 0)
        {
            Debug.Log("I have no Waypoints");
            return new Vector3(0, 0, 0);
        }
        int temp = Random.Range(0, wayPoints.Count);
        return wayPoints[temp];
    }

    public GameObject GetPlayer()
    {
        return player;
    }
    #endregion

    #region Mutator Functions
    private void SetWayPoints()
    {
        for(int i = 0; i < numWayPointsToPatrol; i ++)
        {
            wayPoints.Add(wayPointParent.transform.GetChild(Random.Range(0, wayPointParent.transform.childCount - 1)).position);
        }
    }

    public void SetEnemyDestination(Vector3 destination)
    {
        agent.SetDestination(destination);
    }

    public void ClearEnemyDestination()
    {
        agent.ResetPath();
    }

    public void DestroySelf()
    {
        enemyManager.DestroyEnemy(this.gameObject);
    }
    #endregion
}
