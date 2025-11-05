using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using static UnityEditor.PlayerSettings;
using static UnityEngine.GraphicsBuffer;

public class CameraManager : MonoBehaviour
{
    [SerializeField] private GameObject player = null;

    Vector3 offset = new();
    private Vector3 refPos;

    private void Awake()
    {
        transform.position = new Vector3(player.transform.position.x + 5, transform.position.y + 0.5f, player.transform.position.z - 10);
    }

    float height;

    void Start()
    {
        offset = transform.position - player.transform.position;
    }

    void Update()
    {
        if (player.transform.position.y > transform.position.y)
        {
            Vector3 newPos = new Vector3(0, player.transform.position.y + offset.y, 0);
            transform.position = Vector3.Lerp(transform.position, newPos, 0.5f * Time.deltaTime);
            height = transform.position.y;
        }
        else if (player.transform.position.y < height - 2)
        {
            Vector3 newPos = new Vector3(0, player.transform.position.y + offset.y, 0);
            transform.position = Vector3.Lerp(transform.position, newPos, 0.5f * Time.deltaTime);
            height = 0;
        }
        transform.position = new Vector3(player.transform.position.x + offset.x + 2, transform.position.y, player.transform.position.z + offset.z);
    }
}
