using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEditor.ShaderGraph.Internal;
using UnityEngine;
using TMPro;

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

    private Vector3 _runningVelocity = Vector3.zero;
    private Vector3 _jumpingVelocity = Vector3.zero;
    private bool _obstacleHit = false;
    private bool _isHoldingSpace = false;
    private float _rbDrag = 0;
    private float _glidingDrag = 10;
    private float _glidingTimer = 5;
    private float _glidingTime = 0;

    private bool _isJumping = false;

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
        _runningVelocity = Vector3.right * playerSpeed;
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
           transform.position += new Vector3(_runningVelocity.x * Time.deltaTime,0,0);
        }

        //if (_isJumping == false)
        //{
            if (Input.GetKeyDown(KeyCode.Space))
            {
                _isJumping = true;
                _isHoldingSpace = true;
                _jumpingVelocity.y = jumpingSpeed;
                //_glidingTime += Time.deltaTime;
            }
           
        //}

        if (Input.GetKeyUp(KeyCode.Space))
        {
            _isJumping = false;
            _isHoldingSpace = false;
            this.GetComponent<Rigidbody>().drag = _rbDrag;
        }
    }

    private void FixedUpdate()
    {
        if (Physics.CheckSphere(basePosition.transform.position, 0.1f, mask))
        {
            if (_isJumping)
            {
                this.GetComponent<Rigidbody>().AddForce(Vector3.up * jumpingSpeed, ForceMode.Impulse);
                Debug.Log("hi");
                if (_isHoldingSpace && _glidingTime < _glidingTimer)
                {
                    this.GetComponent<Rigidbody>().drag = _glidingDrag;
                    _glidingTime += Time.deltaTime;
                }
            }
            else
            {
                if (_isHoldingSpace && _glidingTime < _glidingTimer)
                {
                    this.GetComponent<Rigidbody>().drag = _glidingDrag;
                    _glidingTime += Time.deltaTime;
                }
                Debug.Log("hry");
                this.GetComponent<Rigidbody>().velocity = Vector3.zero;
            }
            _isJumping = false;
        }
      
    }

    private void DetectCollision()
    {
        if(Physics.CheckSphere(basePosition.transform.position, 0.1f, mask))
        {
            Debug.Log("hi");
            _isJumping = false;
        }
        RaycastHit hi;
        //if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.up),out hi, 0.1f))
        //{
        //    Debug.Log("hi");
        //    _isJumping = false;
        //}
    }

    //Turn that into raycasts
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            _glidingTime = 0;
            //_isJumping = false;
        }
    }
}
