using Sirenix.OdinInspector;
using UnityEngine;

namespace Programmers.ATA.Scripts
{
    [CreateAssetMenu(fileName = "ObstacleData", menuName = "Obstacles/Obstacle Data", order = 0)]
    public class ObstacleData : ScriptableObject
    {
        [TitleGroup("Select Prefab and Set Spawn Chance")]
        public GameObject prefab;
        
        [Header("Spawn Chance")]
        [Range(0f, 1f)] public float spawnChance = 1f;
    }
}