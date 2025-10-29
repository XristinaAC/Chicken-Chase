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
    [SerializeField] private LayerMask mask;

    private Vector3 _runningVelocity = Vector3.zero;
    private Vector3 _jumpingVelocity = Vector3.zero;
    private bool _obstacleHit = false;
    private bool _isHoldingSpace = false;

    private bool _isJumping = false;

    private void Start()
    {
        
        _runningVelocity = Vector3.right * playerSpeed;
    }

    private void Update()
    {
        if (!_obstacleHit)
        {
            transform.position += _runningVelocity * Time.deltaTime;
        }
        
        if(_isJumping == false)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                _isJumping = true;
                _isHoldingSpace = true;
                _jumpingVelocity.y = jumpingSpeed;
            }
        }

        if(Input.GetKeyUp(KeyCode.Space))
        {
            _isHoldingSpace = false;
        }
    }

    private void FixedUpdate()
    {
        if(_isJumping)
        {
            this.GetComponent<Rigidbody>().AddForce(0, _jumpingVelocity.y, 0);

            if (_isHoldingSpace == false)
            {
                _jumpingVelocity.y += gravity * Time.fixedDeltaTime;
            }
            else
            {
                this.GetComponent<Rigidbody>().AddForce(20, 0, 0);
            }
            DetectCollision();
        }
    }

    private void DetectCollision()
    {
        RaycastHit hi;
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.up),out hi, 10.0f))
        {
            Debug.Log("hi");
            _isJumping = false;
        }
    }

    //Turn that into raycasts
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Ground")
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
