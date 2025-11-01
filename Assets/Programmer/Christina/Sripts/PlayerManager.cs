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
    private float _glidingTimer = 0.5f;
    private float _glidingTime = 0;
    private Vector3 _jumpHeightV;
    bool canGlide = false;
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
            _direction = new Vector3(_runningVelocity.x * Time.deltaTime, 0, 0);
        }
        else if(direction == 1)
        {
            _direction = new Vector3(0, 0, _runningVelocity.x * Time.deltaTime * playerSpeed);
        }
    }

    private void Update()
    {
        PlayerMovement();
        PressingJumpButton();
        GlidingActions();
        EndingGliding();
    }

    void PlayerMovement()
    {
        if (!_obstacleHit)
        {
            transform.position += _direction;
        }
    }

    void PressingJumpButton()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            _isJumping = true;
            _isHoldingSpace = true;
            _glidingTime = 0;
        }
    }

    void GlidingActions()
    {
        CheckingGroundDistance();
        Gliding();
    }

    void CheckingGroundDistance()
    {
        RaycastHit hit;
        Physics.Raycast(transform.position, Vector3.down, out hit, mask);
        if (Vector3.Distance(hit.point, transform.position) > 3)
        {
            canGlide = true;
        }
        else if (_glidingTime > 0.091 && Vector3.Distance(hit.point, transform.position) < 1.5)
        {
            canGlide = false;
            this.GetComponent<Rigidbody>().drag = _rbDrag;
        }
    }

    void Gliding()
    {
        if (canGlide && (this.GetComponent<Rigidbody>().velocity.y > 0.5 || this.GetComponent<Rigidbody>().velocity.y < _glidingTimer) && _glidingTime < 1 && _isHoldingSpace)
        {
            this.GetComponent<Rigidbody>().drag = _glidingDrag;
            //this.GetComponent<Rigidbody>().velocity += new Vector3(0, gravity, 0);
            _glidingTime += Time.deltaTime;
            Debug.Log("in");
        }
        Debug.Log(_glidingTime);
    }
    

    void EndingGliding()
    {
        //When the player stops pressing the space button
        if (Input.GetKeyUp(KeyCode.Space))
        {
            _isHoldingSpace = false;
            _glidingTime = 0;
            canGlide = false;
            this.GetComponent<Rigidbody>().drag = _rbDrag;
        }
    }

    private void FixedUpdate()
    {
        if (Physics.CheckSphere(basePosition.transform.position, 0.1f, mask))
        {
            if (_isJumping && !_isHoldingSpace)
            {
                this.GetComponent<Rigidbody>().AddForce(_jumpHeightV * jumpingSpeed, ForceMode.Impulse);
            }
        }
        else
        {
            _isJumping = false;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "obstacle")
        {
            this.gameObject.SetActive(false);
        }

        if (collision.gameObject.tag == "change scene")
        {
            transform.Rotate(0, -45, 0);
            mainCamera.GetComponent<CameraManager>().SetCanRotate(true);
        }
        else
        {
            mainCamera.GetComponent<CameraManager>().SetCanRotate(false);
        }
    }
}
