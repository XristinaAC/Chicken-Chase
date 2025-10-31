using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeSceneManager : MonoBehaviour
{
    [SerializeField] ChangeSceneScriptableObjects ChangeSceneSO;
    // Update is called once per frame

    void Update()
    {
        ChangeSceneSO.DictatePlayerDirection();
    }
}
