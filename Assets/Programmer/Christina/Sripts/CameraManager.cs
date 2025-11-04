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
        transform.position = new Vector3(player.transform.position.x + offset.x, transform.position.y, player.transform.position.z + offset.z);
    }
}
