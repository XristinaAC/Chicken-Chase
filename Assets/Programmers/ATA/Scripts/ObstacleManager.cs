using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleManager : MonoBehaviour
{
    [SerializeField] private List<GameObject> obstaclesPrefabs = new List<GameObject>();
    [SerializeField] private List<Transform> spawnPositions = new List<Transform>();
    [SerializeField] private int maxObstacles;

    void OnEnable()
    {
        if(LevelManager.Instance != null)
            LevelManager.Instance.OnLevelLoadComplete +=  ObstacleSpawn;
        
        ObstacleSpawn();
    }

    void OnDisable()
    {
        if(LevelManager.Instance != null)
            LevelManager.Instance.OnLevelLoadComplete -=  ObstacleSpawn;
    }
   

    void ObstacleSpawn()
    {
        if (obstaclesPrefabs.Count == 0 || spawnPositions.Count == 0) return;
        int count = Mathf.Min(maxObstacles, spawnPositions.Count);

     
        List<Transform> spawnPos = new List<Transform>(spawnPositions);
        
        for (int i = 0; i < count; i++)
        {
            int randomPrefabs = Random.Range(0, obstaclesPrefabs.Count);
            int randomIndex = Random.Range(0,spawnPos.Count);
            Transform newPos = spawnPos[randomIndex];
            spawnPos.RemoveAt(randomIndex);
            
            Instantiate(obstaclesPrefabs[randomPrefabs], newPos.position, Quaternion.identity);
        }
    }
    
}
