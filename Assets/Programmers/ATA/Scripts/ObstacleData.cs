using UnityEngine;

namespace Programmers.ATA.Scripts
{
    [CreateAssetMenu(fileName = "ObstacleData", menuName = "Obstacles/Obstacle Data", order = 0)]
    public class ObstacleData : ScriptableObject
    {
        public GameObject prefab;          
        [Range(0f, 1f)] public float spawnChance = 1f;
    }
}