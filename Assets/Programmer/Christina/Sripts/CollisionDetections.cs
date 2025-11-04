using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class CollisionDetections : MonoBehaviour
{
    [SerializeField] private GameObject player;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            //player.transform.position = Vector3.zero;
        }
    }
}
