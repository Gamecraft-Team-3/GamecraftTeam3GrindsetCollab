using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    [SerializeField] private GameObject enemyObject, player, spawnPointParent = null;
    [SerializeField] private float enemySpawningTimer = 0;
    [SerializeField] private int maxEnemies, waveIncreaseAmount = 0;
    private List<GameObject> currentEnemies = new List<GameObject>();
    private List<Vector3> enemySpawnPoints = new List<Vector3>();
    private int waveNumber = 1;
    private int enemiesKilled = 0;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        SetSpawnPoints();

        StartSpawning();
    }

    // Update is called once per frame
    void Update()
    {
        if(currentEnemies.Count == 0 && enemiesKilled >= maxEnemies)
        {
            StartSpawning();
            enemiesKilled = 0;
            maxEnemies += waveIncreaseAmount;
            waveNumber++;
            maxEnemies = maxEnemies * waveNumber;
        }
    }

    public void DestroyEnemy(GameObject enemyToDestroy)
    {
        currentEnemies.Remove(enemyToDestroy);
        enemiesKilled++;
        Destroy(enemyToDestroy);
    }

    #region Enemy Spawning Jazz
    private void StartSpawning()
    {
        StartCoroutine(EnemySeperationTimer());
    }

    private void SpawnEnemy()
    {
        GameObject instance;
        if(enemySpawnPoints.Count <= 0)
        {
            return;
        }
        int pos = Random.Range(0, enemySpawnPoints.Count);
        instance = Instantiate(enemyObject, enemySpawnPoints[pos], enemyObject.transform.rotation);
        currentEnemies.Add(instance);
    }

    private IEnumerator EnemySeperationTimer()
    {
        for(int i = 0; i < maxEnemies; i ++)
        {
            yield return new WaitForSeconds(enemySpawningTimer);
            SpawnEnemy();
        }
        yield return new WaitForSeconds(0.5f);
        //StartCoroutine(EnemySeperationTimer());
    }
    #endregion
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
