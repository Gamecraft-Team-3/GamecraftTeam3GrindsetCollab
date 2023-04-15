using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyBehavior : MonoBehaviour
{
    [SerializeField] private GameObject wayPointParent = null;
    [SerializeField] private int numWayPointsToPatrol = 0;
    [SerializeField] private Animator anim = null;
    [SerializeField] private NavMeshAgent agent = null;
    private List<Vector3> wayPoints = null;
    private EnemyManager enemyManager = null;
    private GameObject player = null;
    private float distanceToPlayer;
    // Start is called before the first frame update
    void Start()
    {
        SetWayPoints();
        enemyManager = GameObject.Find("EnemyManager").GetComponent<EnemyManager>();
        player = enemyManager.GetPlayer();
    }

    // Update is called once per frame
    void Update()
    {

    }

    #region Getter Functions
    //Returns the distacne from the player
    public float GetPlayerDistance()
    {
        #region Calculations
        float xOne = player.transform.position.x;
        float xTwo = this.transform.position.x;
        float yOne = player.transform.position.y;
        float yTwo = player.transform.position.y;
        float a = Mathf.Abs((xOne - xTwo) * (xOne - xTwo));
        float b = Mathf.Abs((yOne - yTwo) * (yOne - yTwo));
        #endregion
        distanceToPlayer = Mathf.Sqrt(a + b);
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
        int temp = Random.Range(0, wayPoints.Count - 1);
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
            int pos = wayPointParent.transform.GetChild(Random.Range(0, wayPointParent.transform.childCount - 1)).GetSiblingIndex();
            wayPoints.Add(wayPointParent.transform.GetChild(pos).position);
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
    #endregion
}
