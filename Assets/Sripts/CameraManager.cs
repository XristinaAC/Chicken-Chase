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
        offset = transform.position - player.transform.position;

    }

    void Update()
    {
        transform.position = player.transform.position + offset;
    
        transform.LookAt(player.transform);
        //transform.forward = player.transform.position - transform.position;
        //transform.position.
    }

    void LateUpdate()
    {

    }
}
