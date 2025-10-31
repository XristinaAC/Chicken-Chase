using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(fileName = "Unknown", menuName = "ScriptableObjects/SpawnManagerScriptableObject", order = 31)]
public class ChangeSceneScriptableObjects : ScriptableObject
{
    [SerializeField]
    public enum Direction
    {
        rigth = 0,
        left = 2,
        forward = 1,
        back = 3
    }

    [Serializable]
    public struct ChangeSceneObejct
    {
        public GameObject changeSceneArea;
        public Direction playerDirection;
    }

    [SerializeField] private LayerMask player;
    [SerializeField] List<ChangeSceneObejct> cso;
    
    public void DictatePlayerDirection()
    {
        for(int i = 0; i < cso.Count; i++)
        {
            Collider[] col = Physics.OverlapSphere(cso[i].changeSceneArea.transform.position, 0.3f, player);
            if (col.Length > 0)
            {
                col[0].GetComponent<PlayerManager>().SetDirection((int)cso[i].playerDirection);
            }
        }
        
    }
}
