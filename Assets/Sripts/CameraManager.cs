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
        //Vector3 newPos = new Vector3(0,0, player.transform.position.y);
        //transform.LookAt(player.transform.right);
        //transform.forward = player.transform.position - transform.position;
        
    }
    Vector3 _currentRotation;

    void LateUpdate()
    {
    }
}
