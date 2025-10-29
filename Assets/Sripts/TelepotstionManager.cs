using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TelepotstionManager : MonoBehaviour
{
    [SerializeField] GameObject Player = null; 
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject == Player)
        {
            Player.GetComponent<Rigidbody>().AddForce(0,200, 0);
            Player.GetComponent<Rigidbody>().useGravity = false;
        }
    }
}
