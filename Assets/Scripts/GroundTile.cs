using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundTile : MonoBehaviour
{
    private GroundSpawner groundspawner;

    public GameObject CoinPrefab;
    public GameObject[] obstaclePrefabs;
    public Transform[] spawnpoints;

    private void Awake()
    {
        groundspawner = GameObject.FindObjectOfType<GroundSpawner>();
    }

    // Start is called before the first frame update
    void Start()
    {
        SpawnObs();
        SpawnCoin();
    }

    private void OnTriggerExit(Collider other)
    {
        groundspawner.spawnTile();

        Destroy(gameObject, 5f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SpawnObs()
    {
        int ChooseSpawnObsPoint = Random.Range(0, spawnpoints.Length);
        int SpawnPrefab = Random.Range(0, obstaclePrefabs.Length);

        Instantiate(obstaclePrefabs[SpawnPrefab], spawnpoints[ChooseSpawnObsPoint].transform.position, Quaternion.identity, transform);
    }

    public void SpawnCoin()
    {
        int spawnAmount = 2;

        for (int i=0; i<spawnAmount; i++)
        {
            int ChooseSpawnCoinPoint = Random.Range(0, spawnpoints.Length);
            GameObject tempCoin = Instantiate(CoinPrefab);
            tempCoin.transform.position = SpawnRandomPoint(spawnpoints[ChooseSpawnCoinPoint]);
        }
    }

    Vector3 SpawnRandomPoint(Transform lanePosition)
    {
        Vector3 point = new Vector3(lanePosition.position.x, lanePosition.position.y, Random.Range(lanePosition.position.z - 13f, lanePosition.position.z -2f));

        point.y = 1;
        return point;
    }
}
