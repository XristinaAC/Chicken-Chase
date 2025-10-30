using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndGame : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("End");
        SaveManager._saveInstance.Set_Score(9);
        SaveManager._saveInstance.Save_Data();
    }
}
