using UnityEngine;


public class CameraManager : MonoBehaviour
{  
    private GameObject _player = null;
    private PlayerManager2 _playerManager;

    Vector3 offset = new();
    private Vector3 refPos;

    private void Awake()
    {
        
        _player = GameObject.FindWithTag("Player");
        if (_player != null)
            _playerManager = _player.GetComponent<PlayerManager2>();
        
        transform.position = new Vector3(_player.transform.position.x + 5, transform.position.y + 0.5f, _player.transform.position.z - 10);
    }

    float height;

    void Start()
    {
        offset = transform.position - _player.transform.position;
        //height = transform.position.y;
    }

    void Update()
    {
        if (GameManager.Instance.CurrentState != GameManager.GameState.Playing) return;
        
        if(_player.transform.position.y > transform.position.y + 3)
        {
            Vector3 newPos = new Vector3(0, _player.transform.position.y + offset.y, 0);
            transform.position = Vector3.Lerp(transform.position, newPos, 0.5f * Time.deltaTime);
            height = transform.position.y;
        }
        else if(_player.transform.position.y < transform.position.y - 5)
        {
            Vector3 newPos = new Vector3(0, _player.transform.position.y - offset.y, 0);
            transform.position = Vector3.Lerp(transform.position, newPos, 10 * Time.deltaTime);
            height = 0;
        }
        transform.position = new Vector3(_player.transform.position.x + offset.x + 2, transform.position.y, _player.transform.position.z + offset.z);
    }
}
