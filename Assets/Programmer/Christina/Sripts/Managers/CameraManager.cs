using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;


public class CameraManager : MonoBehaviour
{
    [SerializeField] private GameObject player = null;
    [SerializeField] private Transform turnPosition;

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
        move = 0;
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
            Vector3 newPos = new Vector3(0, player.transform.position.y - offset.y, 0);
            transform.position = Vector3.Lerp(transform.position, newPos, 10 * Time.deltaTime);
            height = 0;
        }
        if(player.GetComponent<PlayerManager2>().GetTurn())
        {
            //transform.position = new Vector3(player.transform.position.x + 20, transform.position.y, player.transform.position.z + offset.z);

            //transform.RotateAround(player.transform.position, Vector3.up, -90);
            //Vector3 rotPos = new Vector3(0, player.transform.position.y - transform.position.y, 0);
            //transform.rotation = Quaternion.LookRotation(player.transform.position - transform.position, Vector3.up);
            
            //transform.rotation = new Quaternion(0, -transform.rotation.y, 0,0);
            transform.Rotate(0, -90, 0);
            move = 15;
            //transform.Rotate(0, -30, 0);

            player.GetComponent<PlayerManager2>().SetTurn();
        }
        else
        {
            transform.position = new Vector3(player.transform.position.x + offset.x + 4, transform.position.y, player.transform.position.z + offset.z + move);
        }
            
    }

    bool turn;
    int move;
    public void TurnCamera()
    {
        turn = true;
        transform.Rotate(0, -90, 0);
    }
}
