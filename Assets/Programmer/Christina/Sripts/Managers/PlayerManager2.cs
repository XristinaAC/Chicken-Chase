using Unity.VisualScripting;
using UnityEngine;

public class PlayerManager2 : MonoBehaviour
{
    public static PlayerManager2 Instance = null;

    [SerializeField] private float playerSpeed = 0;
    [SerializeField] private float _glidingDrag = 10;
    [SerializeField] private LayerMask mask;
    [SerializeField] private Transform basePosition;
    [SerializeField] private float _jumpHeight = 1;
    [SerializeField] private float _gravity = -2;

    private bool _jumpHighPeak = false;
    private bool _obstacleHit = false;
    private bool _isHoldingSpace = false;
    private float _rbDrag = 1;
    private Vector3 _jumpHeightV;
    private bool canGlide = false;
    private bool _isJumping = false;
    private float jumpingSpeed = 0;
    private bool _attack = false;
    private bool _turn;
    bool isGrounded = false;
    bool itWasInTheAir = false;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    private void Start()
    {
        _rbDrag = this.GetComponent<Rigidbody>().drag;
        _jumpHeightV = new Vector3(0, Mathf.Sqrt(1 * -2 * (Physics.gravity.y * 1)), 0);
    }

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
        }
    }

    void GlidingActions()
    {
        CheckingGroundDistance();
        Gliding();
    }

    void CheckingGroundDistance()
    {
        if (Physics.CheckSphere(transform.position, 0.5f, mask))
        {
            isGrounded = true;
            canGlide = false;
            _jumpHighPeak = false;
        }
        else
        {
            isGrounded = false;
            this.GetComponent<Rigidbody>().AddForce(new Vector3(0, _gravity, 0), ForceMode.Acceleration);
        }

        if (this.GetComponent<Rigidbody>().velocity.y < 0 && !_jumpHighPeak && !isGrounded && _isHoldingSpace)
        {
            SoundManager.Instance.PlaySFX(SoundManager.effectsAudio.glidingAudioEffect);
            canGlide = true;
            _jumpHighPeak = true;
        }
    }

    void Gliding()
    {
        if (canGlide)
        {
            itWasInTheAir = true;
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

   
    void Jumping()
    {
        if (_isJumping && isGrounded == true)
        {
            SoundManager.Instance.PlaySFX(SoundManager.effectsAudio.jumpingAudioEffect);

            jumpingSpeed = Mathf.Sqrt(2 * _jumpHeight * Mathf.Abs(_gravity));
            this.GetComponent<Rigidbody>().AddForce(_jumpHeightV * jumpingSpeed, ForceMode.Impulse);

            itWasInTheAir = true;
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
            if(itWasInTheAir)
            {
                SoundManager.Instance.PlaySFX(SoundManager.effectsAudio.landAudioEffect);
                itWasInTheAir = false;
            }
            canGlide = false;
            _attack = false;
        }

        if (collision.gameObject.tag == "obstacle")
        {
            Die();
        }

        if (collision.gameObject.tag == "change scene")
        {
            _turn = true;
        }

        if (collision.gameObject.tag == "Projectile")
        {
            _attack = true;
        }
    }

    private void Die()
    {
        SoundManager.Instance.PlaySFX(SoundManager.effectsAudio.deathAudioEffect);
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

    public void SetTurn()
    {
        _turn = false;
    }

    public bool GetTurn()
    {
        return _turn;
    }

    public bool GetAttack()
    {
        return _attack;
    }
}
