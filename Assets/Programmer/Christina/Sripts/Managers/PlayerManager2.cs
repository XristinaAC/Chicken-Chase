using Unity.VisualScripting;
using UnityEngine;

public class PlayerManager2 : MonoBehaviour
{
    public static PlayerManager2 Instance = null;

    [SerializeField] private float playerSpeed = 0;
    private float jumpingSpeed = 0;
    [SerializeField] private float _glidingDrag = 10;
    [SerializeField] private LayerMask mask;
    [SerializeField] private Transform basePosition;
    [SerializeField] private GameObject mainCamera;
    [SerializeField] private float _jumpHeight = 1;
    [SerializeField] private Transform spawnPosition;
    [SerializeField] private float chickenHeight;
    [SerializeField] private float _gravity = -2;

    private bool _jumpHighPeak = false;
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
        if (Instance == null)
        {
            Instance = this;
        }
        else if (Instance != this)
        {
            //Destroy(this.gameObject);
        }

        //DontDestroyOnLoad(this.gameObject);
    }

    private void Start()
    {
        _rbDrag = this.GetComponent<Rigidbody>().drag;
        _jumpHeightV = new Vector3(0, Mathf.Sqrt(1 * -2 * (Physics.gravity.y * 1)), 0);
        chickenHeight = transform.position.y;
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

    float height;
    private float counter = 0;
    private void Update()
    {
        if (GameManager.Instance.CurrentState != GameManager.GameState.Playing) return;
        PlayerMovement();
        PressingJumpButton();
        GlidingActions();
        EndingGliding();
    }

    void PlayerMovement()
    {
        if (!_obstacleHit)
        {
            transform.position -= transform.forward * (playerSpeed * Time.deltaTime) ;
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
        

        if (Physics.CheckSphere(transform.position, 0.5f, mask))
        {
            isGrounded = true;
            chickenHeight = transform.position.y;
            canGlide = false;
            _jumpHighPeak = false;
        }
        else
        {
            Physics.Raycast(transform.position, Vector3.down, out hit, 200, mask);
            distance = Vector3.Distance(hit.point, transform.position);
            isGrounded = false;
            this.GetComponent<Rigidbody>().AddForce(new Vector3(0, _gravity, 0), ForceMode.Acceleration);
        }

        if (this.GetComponent<Rigidbody>().velocity.y < 0 && !_jumpHighPeak && !isGrounded && _isHoldingSpace)
        {
            Debug.Log("Glide" + transform.position.y);
            canGlide = true;
            _jumpHighPeak = true;
        }
    }

    void Gliding()
    {
        if (canGlide)
        {
            this.GetComponent<Rigidbody>().drag = _glidingDrag;
        }
    }

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
    void Jumping()
    {
        if (_isJumping && isGrounded == true)// && distance <= 1.0f)
        {
            jumpingSpeed = Mathf.Sqrt(2 * _jumpHeight * Mathf.Abs(_gravity));
            this.GetComponent<Rigidbody>().AddForce(_jumpHeightV * jumpingSpeed, ForceMode.Impulse);

            isGrounded = false;
        }
        _isJumping = false;
    }
    private void FixedUpdate()
    {
        Jumping();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            canGlide = false;
        }

        if (collision.gameObject.tag == "obstacle")
        {
            Die();
        }

        if (collision.gameObject.tag == "change scene")
        {
            transform.Rotate(0, -45, 0);
        }
    }

    private void Die()
    {
        
        if (GameManager.Instance != null)
            GameManager.Instance.ChangeState(GameManager.GameState.GameOver);
            
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
}
