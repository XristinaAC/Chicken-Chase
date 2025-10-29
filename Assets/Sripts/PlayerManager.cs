using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEditor.ShaderGraph.Internal;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float playerSpeed = 0;
    [SerializeField] private float jumpingSpeed = 50;
    [SerializeField] private float gravity = 0;

    private Vector3 _runningVelocity = Vector3.zero;
    private Vector3 _jumpingVelocity = Vector3.zero;
    private bool _obstacleHit = false;

    private bool _isJumping = false;

    private void Start()
    {
        
        _runningVelocity = Vector3.right * playerSpeed;
    }

    private void Update()
    {
        //For the player to move continiously we need a speed and a direction
       
        if (!_obstacleHit)
        {
            transform.position += _runningVelocity * Time.deltaTime;
            //DetectCollision();
        }
        
        if(_isJumping == false)
        {
            if (Input.GetKeyDown(KeyCode.W))
            {
                if (!this.GetComponent<Rigidbody>().useGravity)
                {
                    this.GetComponent<Rigidbody>().useGravity = true;
                    this.GetComponent<Rigidbody>().AddForce(0, 50, 0);
                }
                else
                {
                    _isJumping = true;
                    _jumpingVelocity.y = jumpingSpeed;
                } 
            }
        }
    }

    private void Gliding()
    { 
        if(Input.GetKeyDown(KeyCode.Space))
        {
            //Gliding code here
        }
    }

    private void FixedUpdate()
    {
        Vector3 pos = transform.position;
        if(_isJumping)
        {
            this.GetComponent<Rigidbody>().AddForce(_jumpingVelocity.x, _jumpingVelocity.y, 0);
            _jumpingVelocity.y += gravity * Time.fixedDeltaTime;
            //if(_obstacleHit) { _obstacleHit = false; }
                
            //DetectCollision();
        }
    }

    private void DetectCollision()
    {
        RaycastHit hit;
        Vector3 rayOrigin = new Vector3(0, transform.position.y, 0);
        Vector3 rayfDirection = Vector3.up;
        float rayDistance = _jumpingVelocity.y * Time.fixedDeltaTime;
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.down), out hit, 10f))
        {
            //_isJumping = false;
            Debug.Log("down");
        }
    }

    //Turn that into raycasts
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Ground")
        {
            _isJumping = false;
        }

        if (collision.gameObject.tag == "obstacle")
        {
            this.gameObject.SetActive(false);
            //_obstacleHit = true;
        }
    }
}
