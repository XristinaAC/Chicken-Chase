using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using TMPro;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEditor.ShaderGraph.Internal;
using UnityEngine;
using UnityEngine.UIElements;

public class Player : MonoBehaviour
{
    [SerializeField] private float playerSpeed = 0;
    [SerializeField] private float jumpingSpeed = 50;
    [SerializeField] private float gravity = 0;
    [SerializeField] private LayerMask mask;
    [SerializeField] private GameObject pNameTextParent;
    [SerializeField] private TMP_Text pNameText;
    [SerializeField] private string pName;
    [SerializeField] private int score;
    [SerializeField] private Transform basePosition;
    [SerializeField] private Camera mainCamera;

    private Vector3 _runningVelocity = Vector3.zero;
    private Vector3 _jumpingVelocity = Vector3.zero;
    private bool _obstacleHit = false;
    private bool _isHoldingSpace = false;
    private float _rbDrag = 0;
    private float _glidingDrag = 10;
    private float _glidingTimer = 5;
    private float _glidingTime = 0;

    private bool _isJumping = false;
    private Vector3 _direction;

    private void Awake()
    {
        SaveManager._saveInstance.Load_Data();
      
        if(SaveManager._saveInstance.Get_Player_Name() != null && SaveManager._saveInstance.Get_Score() != 0)
        {
            pNameTextParent.SetActive(false);
            pName = SaveManager._saveInstance.Get_Player_Name();
            score = SaveManager._saveInstance.Get_Score();
        }
    }

    private void Start()
    {
        _rbDrag = this.GetComponent<Rigidbody>().drag;
        _runningVelocity = Vector3.right * playerSpeed * Time.deltaTime;
        _direction = new Vector3(_runningVelocity.x * Time.deltaTime, 0, 0);
    }

    public void SetName()
    {
        SaveManager._saveInstance.Set_Player_Name(pNameText.GetComponent<TMP_Text>().text);
        pNameTextParent.SetActive(false);
    }

    private void Update()
    {
        if (!_obstacleHit)
        {
            transform.position += _direction;// new Vector3(_runningVelocity.x * Time.deltaTime,0,0);
        }

        if (midAir)
        {
            _glidingTime += Time.deltaTime;
        }
        
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if(!_isJumping)
            {
                _isJumping = true;
            }
           
            _isHoldingSpace = true;
            _jumpingVelocity.y = jumpingSpeed;
            _glidingTime = 0;
        }

        if (Input.GetKeyUp(KeyCode.Space))
        {
            Debug.Log("drag");
            _isHoldingSpace = false;
            _glidingTime = 0;
            this.GetComponent<Rigidbody>().drag = _rbDrag;
        }
    }

    bool midAir = false;

    private void FixedUpdate()
    {
        if (Physics.CheckSphere(basePosition.transform.position, 0.1f, mask))
        {
            if (_isJumping)
            {
                midAir = true;
                this.GetComponent<Rigidbody>().AddForce(Vector3.up * jumpingSpeed, ForceMode.Impulse);
                if (_isHoldingSpace && midAir && _glidingTime < _glidingTimer)
                {
                    this.GetComponent<Rigidbody>().drag = _glidingDrag;

                    //_glidingTime += Time.deltaTime;
                }
            }
            else
            {
                this.GetComponent<Rigidbody>().velocity = Vector3.zero;
                if (_isHoldingSpace && _glidingTime < _glidingTimer)
                {
                    Debug.Log("hi");
                    this.GetComponent<Rigidbody>().drag = _glidingDrag;
                }
            }
            if (_isHoldingSpace && midAir && _glidingTime < _glidingTimer)
            {
                this.GetComponent<Rigidbody>().drag = _glidingDrag;

                //_glidingTime += Time.deltaTime;
            }
        }
        else
        {
            midAir = false;
            _glidingTime = 0;
            _isJumping = false;
        }
    }

    void ChangeDirection()
    {
        _direction = new Vector3(0, 0, _runningVelocity.x * Time.deltaTime);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            midAir = false;
        }

        if (collision.gameObject.tag == "change scene")
        {
            Debug.Log("hi");
            ChangeDirection();
            transform.Rotate(0, -90, 0);
            //mainCamera.transform.Rotate(0,-90, 0);
        }
    }
}
