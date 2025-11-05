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

    double height;
    float counter = 0;
    private void Update()
    {
        //height = Mathf.Min(distance, jumpingSpeed);
        //Debug.Log(distance);
        PlayerMovement();
        PressingJumpButton();
        GlidingActions();
        EndingGliding();
    }

    void PlayerMovement()
    {
        if (!_obstacleHit)
        {
            transform.position += new Vector3(playerSpeed * Time.deltaTime, 0, 0); ;
        }
    }

    void PressingJumpButton()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            _isHoldingSpace = true;
            _isJumping = true;
            counter = Time.time;
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
        Physics.Raycast(transform.position, Vector3.down, out hit, 200, mask);
        distance = Vector3.Distance(hit.point, transform.position);

        height = (jumpingSpeed / _jumpHeight) - 0.6;
        if (distance >= height && !isGrounded && _isHoldingSpace)
        {
            canGlide = true;
        }
        else
        {
            canGlide = false;
        }
    }

    void Gliding()
    {
        if (canGlide)
        {
            this.GetComponent<Rigidbody>().drag = _glidingDrag;
        }
    }

    float heldSpaceDuration = 0;
    void EndingGliding()
    {
        //When the player stops pressing the space button
        if (Input.GetKeyUp(KeyCode.Space))
        {
            _isHoldingSpace = false;
            canGlide = false;
            this.GetComponent<Rigidbody>().drag = _rbDrag;
        }
    }

    bool isGrounded = false;

    private void FixedUpdate()
    {
        if (Physics.CheckSphere(basePosition.transform.position, 0.1f, mask))
        {
            if (_isJumping && distance <= 0.03 && isGrounded == true)
            {
                this.GetComponent<Rigidbody>().AddForce(_jumpHeightV * jumpingSpeed, ForceMode.Impulse);
                isGrounded = false;
            }
            _isJumping = false;
        }
        else
        {
            isGrounded = false;
            _isJumping = false;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            isGrounded = true;
            canGlide = false;
        }

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

    public bool CanGlide()
    {
        return canGlide;
    }

    public bool IsGrounded()
    {
        return isGrounded;
    }

    public bool CanJump()
    {
        return _isJumping;
    }

    public void Replay()
    {
        SceneManager.LoadScene("Garg_lvl(Kitchen)");
    }
}
