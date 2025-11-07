using Unity.VisualScripting;
using UnityEngine;

public class PlayerManager2 : MonoBehaviour
{
    public static PlayerManager2 Instance = null;

    [SerializeField] private float playerSpeed = 0;
    [SerializeField] private float jumpingSpeed = 0;
    [SerializeField] private float _glidingDrag = 10;
    [SerializeField] private LayerMask mask;
    [SerializeField] private Transform basePosition;
    [SerializeField] private GameObject mainCamera;
    [SerializeField] private float _jumpHeight = 1;
    [SerializeField] private Transform spawnPosition;
    [SerializeField] private float chickenHeight;

    [SerializeField] private Vector3 Velocity;

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
    float counter = 0;
    private void Update()
    {
        //if (GameManager.Instance.CurrentState != GameManager.GameState.Playing) return;
        //height = Mathf.Min(distance, jumpingSpeed);
       
        //Debug.Log(distance);
        PlayerMovement();
        PressingJumpButton();
        Jumping();
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
            Velocity.y = jumpingSpeed;
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
        distance = Vector3.Distance(hit.point, basePosition.position);

        if (Physics.CheckSphere(transform.position, 0.5f, mask))
        {
            isGrounded = true;
            chickenHeight = transform.position.y;
            canGlide = false;
        }
        else
        {
            _isJumping = false;
            isGrounded = false;
            this.GetComponent<Rigidbody>().AddForce(new Vector3(0, -2, 0), ForceMode.Acceleration);
        }

        height = Mathf.Pow(jumpingSpeed, 2f) / (2f * 2);
        Debug.Log(transform.position.y);
        
        if (transform.position.y >= chickenHeight + height + (jumpingSpeed - 1.40f)  && !isGrounded && _isHoldingSpace)
        {
            Debug.Log("Glide" + transform.position.y);
            canGlide = true;
        }
        else if(distance <= 0.9f)
        {
            this.GetComponent<Rigidbody>().drag = _rbDrag;
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
    float jumpVelocity;

    void Jumping()
    {
        if (_isJumping && isGrounded == true)// && distance <= 1.0f)
        {
            this.GetComponent<Rigidbody>().AddForce(_jumpHeightV * jumpingSpeed, ForceMode.Impulse);

            //isGrounded = false;
            _isJumping = false;
        }
    }
    private void FixedUpdate()
    {
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
