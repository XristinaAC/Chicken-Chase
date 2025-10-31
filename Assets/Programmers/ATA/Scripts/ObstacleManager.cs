using System.Collections.Generic;
using Programmers.ATA.Scripts;
using UnityEngine;

public class ObstacleManager : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private ObstacleSet obstacleSet;
    [SerializeField] private List<Transform> spawnPositions = new List<Transform>();
    [SerializeField] private int maxObstacles = 5;

    private void OnEnable()
    {
        if (LevelManager.Instance != null)
            LevelManager.Instance.OnLevelLoadComplete += ObstacleSpawn;

        ObstacleSpawn();
    }

    private void OnDisable()
    {
        if (LevelManager.Instance != null)
            LevelManager.Instance.OnLevelLoadComplete -= ObstacleSpawn;
    }

    private void ObstacleSpawn()
    {
        if (obstacleSet == null || obstacleSet.obstacles.Count == 0 || spawnPositions.Count == 0)
            return;

        int count = Mathf.Min(maxObstacles, spawnPositions.Count);
        List<Transform> availableSpawns = new List<Transform>(spawnPositions);

        for (int i = 0; i < count; i++)
        {
            // Random obstacles
            ObstacleData selected = GetRandomObstacle();
            if (selected == null || selected.prefab == null) continue;

            // Random Spawn points
            int randomIndex = Random.Range(0, availableSpawns.Count);
            Transform spawnPoint = availableSpawns[randomIndex];
            availableSpawns.RemoveAt(randomIndex);

            Instantiate(selected.prefab, spawnPoint.position, Quaternion.identity);
        }
    }

    private ObstacleData GetRandomObstacle()
    {
        if (obstacleSet == null || obstacleSet.obstacles.Count == 0)
            return null;

        float totalWeight = 0f;
        foreach (var obstacle in obstacleSet.obstacles)
            totalWeight += obstacle.spawnChance;

        float randomPoint = Random.Range(0f, totalWeight);
        float current = 0f;

        foreach (var obstacle in obstacleSet.obstacles)
        {
            current += obstacle.spawnChance;
            if (randomPoint <= current)
                return obstacle;
        }

        return obstacleSet.obstacles[obstacleSet.obstacles.Count - 1];
    }
}