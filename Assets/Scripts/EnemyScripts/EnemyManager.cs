using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private List<Vector3> enemySpawnPoints;
    [SerializeField] private GameObject spawnPointParent, enemyObject;
    [SerializeField] private float enemySpawningTimer = 0;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        SetSpawnPoints();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void SpawnWave(int enemyCount)
    {
        StartCoroutine(EnemySeperationTimer(enemyCount));
    }

    private void SpawnEnemy()
    {
        int pos = Random.Range(0, enemySpawnPoints.Count - 1);
        Instantiate(enemyObject, enemySpawnPoints[pos], enemyObject.transform.rotation);
    }

    private IEnumerator EnemySeperationTimer(int enemyCount)
    {
        for(int i = 0; i < enemyCount; i ++)
        {
            yield return new WaitForSeconds(enemySpawningTimer);
            SpawnEnemy();
        }
    }

    #region Getter Functions
    public GameObject GetPlayer()
    {
        return player;
    }

    public Vector3 GetSpawnPoint()
    {
        Vector3 temp = enemySpawnPoints[Random.Range(0, enemySpawnPoints.Count - 1)];
        return temp;
    }
    #endregion
    #region Mutator Functions
    public void SetSpawnPoints()
    {
        for (int i = 0; i < spawnPointParent.transform.childCount; i++)
        {
            enemySpawnPoints.Add(spawnPointParent.transform.GetChild(i).position);
        }
    }
    #endregion
}
