using UnityEngine;


public class CameraManager : MonoBehaviour
{
    [SerializeField] private GameObject player = null;

    Vector3 offset = new();
    private Vector3 refPos;

    private void Awake()
    {
        transform.position = new Vector3(player.transform.position.x + 5, transform.position.y + 0.5f, player.transform.position.z - 10);
    }

    float height;

    void Start()
    {
        offset = transform.position - player.transform.position;
        //height = transform.position.y;
    }

    void Update()
    {
        if(player.transform.position.y > transform.position.y + 3)
        {
            Vector3 newPos = new Vector3(0, player.transform.position.y + offset.y, 0);
            transform.position = Vector3.Lerp(transform.position, newPos, 0.5f * Time.deltaTime);
            height = transform.position.y;
        }
        else if(player.transform.position.y < transform.position.y - 5)
        {
            Vector3 newPos = new Vector3(0, player.transform.position.y + offset.y, 0);
            transform.position = Vector3.Lerp(transform.position, newPos, 10 * Time.deltaTime);
            height = 0;
        }
        transform.position = new Vector3(player.transform.position.x + offset.x + 2, transform.position.y, player.transform.position.z + offset.z);
    }
}
