using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    }
}
