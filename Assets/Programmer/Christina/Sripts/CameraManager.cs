using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class CameraManager : MonoBehaviour
{
    [SerializeField] private GameObject player = null;

    Vector3 offset = new();
    Vector3 refPos;
    Quaternion playerRotation;
    void Start()
    {
        offset = transform.position - player.transform.position;
        playerRotation = player.transform.rotation;
    }

    Quaternion rot2;
    void Update()
    {
        if (!player)
        {
            return;
        }

        //transform.position = player.transform.position + offset;
        //Vector3 newPos = new Vector3(0,0, player.transform.position.y);

        //transform.forward = player.transform.position - transform.position;
        Vector3 pos = new Vector3(player.transform.position.x + offset.x - 2, player.transform.position.y + offset.y, player.transform.position.z + offset.z - 5);
        transform.position = Vector3.SmoothDamp(pos + offset, player.transform.position, ref refPos, 1);
    }

    bool canRotate = false;

    void Rotate()
    {
        Quaternion rot2 = new Quaternion(0, player.transform.rotation.y, 0, 0);
        transform.rotation = Quaternion.Slerp(transform.rotation, player.transform.rotation, 5f * Time.deltaTime);
        transform.rotation = Quaternion.Slerp(transform.rotation, rot2, 2f * Time.deltaTime);
    }

    void LateUpdate()
    {
        if(canRotate)
        {
            Rotate();
        }
    }

    public void SetCanRotate()
    {
        canRotate = true;
    }
}
