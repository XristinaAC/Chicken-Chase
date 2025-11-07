
    using System.Collections.Generic;
    using Programmers.ATA.Scripts;
    using UnityEngine;

    [CreateAssetMenu(fileName = "ObstacleSet", menuName = "Obstacles/Obstacle Set", order = 1)]
    public class ObstacleSet : ScriptableObject
    {
        public List<ObstacleData> obstacles = new List<ObstacleData>();
    }
