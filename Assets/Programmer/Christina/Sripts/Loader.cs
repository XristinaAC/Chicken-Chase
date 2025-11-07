using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Loader : MonoBehaviour
{
    [SerializeField] GameObject player;
    [SerializeField] Transform levelPosition;

    // Start is called before the first frame update
    void Awake()
    {
        //Instantiate(player, levelPosition);
        if (PlayerManager2.Instance == null)
        {
            //Instantiate(player, levelPosition);
        }
    }
}
