using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class CameraManager : MonoBehaviour
{
    [SerializeField] private GameObject player = null;

    Vector3 offset = new();

    void Start()
    {
        offset = new Vector3(transform.position.x - player.transform.position.x, transform.position.y - player.transform.position.y, transform.position.z - player.transform.position.z) ;
        transform.position = new Vector3(player.transform.position.x + offset.x, transform.position.y, player.transform.position.z + offset.z);
    }

    void Update()
    {
        transform.position = new Vector3(player.transform.position.x + offset.x,transform.position.y, player.transform.position.z + offset.z);
        //Vector3 newPos = new Vector3(0,0, player.transform.position.y);
     
        //transform.forward = player.transform.position - transform.position;

        if(player.transform.position.y > transform.position.y)
        {
            transform.position = new Vector3(player.transform.position.x + offset.x, transform.position.y, player.transform.position.z + offset.z);
        } 
    }
}
