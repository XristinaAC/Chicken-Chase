using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Xml.Serialization;
using TMPro;
using Unity.Burst.CompilerServices;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEditor.ShaderGraph.Internal;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerManager : MonoBehaviour
{
    [SerializeField] private float playerSpeed = 0;
    [SerializeField] private float jumpingSpeed = 50;
    [SerializeField] private float gravity = 0;
    [SerializeField] private LayerMask mask;
    [SerializeField] private Transform basePosition;
    [SerializeField] private GameObject mainCamera;
    [SerializeField] private float _jumpHeight = 1;

    private Vector3 _runningVelocity = Vector3.zero;
    private bool _obstacleHit = false;
    private bool _isHoldingSpace = false;
    private float _rbDrag = 0;
    private float _glidingDrag = 15;
    private float _glidingTimer = 5;
    private float _glidingTime = 0;
    private Vector3 _jumpHeightV;

    private bool _isJumping = false;
    private Vector3 _direction;

    private void Awake()
    {
        _runningVelocity = new Vector3(playerSpeed * Time.deltaTime, 0, 0);
        _runningVelocity = _runningVelocity.normalized;
    }

    private void Start()
    {
        _rbDrag = this.GetComponent<Rigidbody>().drag;
        _jumpHeightV = new Vector3(0, _jumpHeight, 0);
        SetDirection(0);
    }

    public void SetDirection(int direction)
    {
        if(direction == 0)
        {
            Debug.Log("hey");
            _direction = new Vector3(_runningVelocity.x * Time.deltaTime, 0, 0);
        }
        else if(direction == 1)
        {
            _direction = new Vector3(0, 0, _runningVelocity.x * Time.deltaTime * playerSpeed);
        }
    }

    private void Update()
    {
        if (!_obstacleHit)
        {
            transform.position += _direction;
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            _isHoldingSpace = true;
            _isJumping = true;
            _glidingTime = 0;
        }

        RaycastHit hit;
        Physics.Raycast(transform.position, Vector3.down, out hit,mask);
        Debug.Log(Vector3.Distance(hit.point, transform.position));

        if (Vector3.Distance(hit.point, transform.position) > 2)
        {
            midAir = true;
            canGlide = true;
        }

        if (canGlide && (this.GetComponent<Rigidbody>().velocity.y > 0.5 || this.GetComponent<Rigidbody>().velocity.y < 0.5) && _glidingTime < _glidingTimer && _isHoldingSpace)
        {
            Debug.Log("glide");
            this.GetComponent<Rigidbody>().drag = _glidingDrag;
            //this.GetComponent<Rigidbody>().velocity += new Vector3(0, gravity, 0);
            _glidingTime += Time.fixedDeltaTime;
        }

        if (Input.GetKeyUp(KeyCode.Space))
        {
            _isHoldingSpace = false;
            _glidingTime = 0;
            canGlide = false;
            this.GetComponent<Rigidbody>().drag = _rbDrag;
        }
    }

    bool midAir = false;
    bool canGlide = false;

    private void FixedUpdate()
    {
        if (Physics.CheckSphere(basePosition.transform.position, 0.1f, mask))
        {
            if (_isJumping && !_isHoldingSpace)
            {
                this.GetComponent<Rigidbody>().AddForce(_jumpHeightV * jumpingSpeed, ForceMode.Impulse);
                //midAir = true;
            }
        }
        else
        {
            midAir = false;
            _glidingTime = 0;
            _isJumping = false;
        }
    }

    Quaternion rotation;
    private void LateUpdate()
    {
        if(rotateCamera)
        {
            mainCamera.transform.rotation = Quaternion.Slerp(mainCamera.transform.rotation, rotation, 0.5f);
        }
    }

    private float timeCount = 0.0f;
    bool rotateCamera = false;
    void ChangeDirection()
    {
        _direction = new Vector3(0, 0, _runningVelocity.x * Time.deltaTime);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            //midAir = false;
        }

        if (collision.gameObject.tag == "obstacle")
        {
            this.gameObject.SetActive(false);
        }

        if (collision.gameObject.tag == "change scene")
        {
            transform.Rotate(0, -45, 0);
            //ChangeDirection();
            //rotateCamera = true;
            //rotation = new Quaternion();
            ////rotation.y = -45;
            //mainCamera.transform.Rotate(0, -45, 0);
            ////var targetRotation = Quaternion.LookRotation(mainCamera.transform.position - this.transform.position);
            ////Quaternion.RotateTowards(mainCamera.transform.rotation, targetRotation, -45);
        }
    }
}
