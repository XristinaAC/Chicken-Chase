using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;
using static UnityEngine.GraphicsBuffer;

public class CameraManager : MonoBehaviour
{
    [SerializeField] private GameObject player = null;
    [SerializeField] private GameObject col = null;

    Vector3 offset = new();
    Vector3 refPos;
    Quaternion playerRotation;
    Vector3 pDirection;
    Vector3 direction3D;
    Vector3 direction3D2;
    void Start()
    {
        offset = transform.position - player.transform.position;
        offset = new Vector3(offset.x - 5 , 0, 0);
        pDirection = transform.position;
        transform.rotation = player.transform.rotation;
        Quaternion rotation = Quaternion.Euler(0, -45, 0);
        Quaternion rotation2 = Quaternion.Euler(0, -45, 0);

        // A starting direction (e.g., "forward")
        Vector3 startingDirection = Vector3.right;

        // Multiply the rotation by the starting direction to get the new direction
        direction3D = rotation * startingDirection;
        direction3D2 = rotation * direction3D;
    }

    Quaternion rot2;
    void Update()
    {
        if (!player)
        {
            return;
        }

        Vector3 startingDirection = player.transform.position;
        Quaternion rotation = Quaternion.Euler(0, -22, 0);
        direction3D = rotation * startingDirection;
  
        //transform.position = player.transform.position + offset;
        Vector3 pos = new Vector3(player.transform.position.x + offset.x, player.transform.position.y + offset.y, player.transform.position.z + offset.z - 10);
        transform.position = Vector3.SmoothDamp(pos, player.transform.position, ref refPos, 1);

        if (canRotate)
        {
            //transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.FromToRotation(Vector3.right, direction3D), 0.5f * Time.deltaTime);
            //transform.rotation = Quaternion.FromToRotation(Vector3.right, direction3D);
            
            transform.position = new Vector3(player.transform.position.x + 10, player.transform.position.y, player.transform.position.z - 10);
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.FromToRotation(Vector3.right, direction3D2), 2f * Time.deltaTime);
        }

        //transform.LookAt(player.transform);
        //Vector3 newPos = new Vector3(0,0, player.transform.position.y);
        //transform.forward = player.transform.position - transform.position;
        //Vector3 pos = new Vector3(player.transform.position.x + offset.x - 2, player.transform.position.y + offset.y, player.transform.position.z + offset.z - 5);
        //transform.position = Vector3.SmoothDamp(pos + offset, player.transform.position, ref refPos, 1);
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
            //Rotate();
        }
    }

    public void SetCanRotate(bool can)
    {
        canRotate = can;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "change scene")
        {
            Debug.Log("Hey");
        }
    }
}
