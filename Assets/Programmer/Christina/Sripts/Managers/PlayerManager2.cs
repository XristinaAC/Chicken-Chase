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
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class PlayerManager2 : MonoBehaviour
{
    [SerializeField] private float playerSpeed = 0;
    [SerializeField] private float jumpingSpeed = 0;
    [SerializeField] private float _glidingDrag = 10;
    [SerializeField] private LayerMask mask;
    [SerializeField] private Transform basePosition;
    [SerializeField] private GameObject mainCamera;
    [SerializeField] private float _jumpHeight = 1;
    [SerializeField] GameObject replayButton;

    private Vector3 _runningVelocity = Vector3.zero;
    private bool _obstacleHit = false;
    private bool _isHoldingSpace = false;
    private float _rbDrag = 1;
    private float _glidingTime = 0;
    private Vector3 _jumpHeightV;
    bool canGlide = false;
    private bool _isJumping = false;
    private Vector3 _direction;

    private void Awake()
    {
        replayButton.SetActive(false);
    }

    private void Start()
    {
        _runningVelocity = new Vector3(playerSpeed * 0.016f, 0, 0);
        _rbDrag = this.GetComponent<Rigidbody>().drag;
        _jumpHeightV = new Vector3(0, _jumpHeight, 0);
        SetDirection(0);
    }

    public void SetDirection(int direction)
    {
        if(direction == 0)
        {
            _direction = new Vector3(_runningVelocity.x, 0, 0);
        }
        else if(direction == 1)
        {
            _direction = new Vector3(0, 0, _runningVelocity.x);
        }
    }

    float counter = 0;
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
            heldSpaceDuration = 0;
           counter = Time.time;
            //counter = 0;
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
    float distance;
    void CheckingGroundDistance()
    {
        RaycastHit hit;
        Physics.Raycast(transform.position, Vector3.down, out hit, 100, mask);
        distance = Vector3.Distance(hit.point, transform.position);
        //Debug.Log(Vector3.Distance(hit.point, transform.position));
        if (Vector3.Distance(hit.point, transform.position) > _jumpHeight/1.5)
        {
            canGlide = true;
        }
        else if (_glidingTime > 0.091 && Vector3.Distance(hit.point, transform.position) < 2)
        {
            canGlide = false;
            this.GetComponent<Rigidbody>().drag = _rbDrag;
        }
    }

    void Gliding()
    {
        if (canGlide && (this.GetComponent<Rigidbody>().velocity.y > 0.5 || this.GetComponent<Rigidbody>().velocity.y < 0.5) && _glidingTime < 1 && _isHoldingSpace)
        {
            this.GetComponent<Rigidbody>().drag = _glidingDrag;
            //this.GetComponent<Rigidbody>().velocity += new Vector3(0, gravity, 0);
            _glidingTime += Time.deltaTime;
        }
    }

    float heldSpaceDuration = 0;
    void EndingGliding()
    {
        //When the player stops pressing the space button
        if (Input.GetKeyUp(KeyCode.Space))
        {
            if(_isHoldingSpace)
            {
                heldSpaceDuration = Time.time - counter;
                //Debug.Log(heldSpaceDuration);
            }
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
            if (_isJumping && distance <= 0.5f && heldSpaceDuration < 9 && !_isHoldingSpace)
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
            replayButton.SetActive(true);
        }

        if (collision.gameObject.tag == "change scene")
        {
            transform.Rotate(0, -45, 0);
        }
    }

    public void Replay()
    {
        SceneManager.LoadScene("Garg_lvl");
    }
}
