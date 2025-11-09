using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;


public class CameraManager : MonoBehaviour
{
    [SerializeField] private GameObject player = null;
    [SerializeField] private Transform turnPosition;

    Vector3 offset = new();
    private Vector3 refPos;
    int zMove;
    int xMove;

    private void Awake()
    {
        transform.position = new Vector3(player.transform.position.x + 5, transform.position.y + 0.5f, player.transform.position.z - 10);
    }

    float height;

    void Start()
    {
        offset = transform.position - player.transform.position;
        zMove = 0;
        xMove = 4;
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
            //zMove 
            //xMove
            transform.Rotate(0, -90, 0);
            //move = 15;
            //transform.Rotate(0, -30, 0);

            player.GetComponent<PlayerManager2>().SetTurn();
        }
        
           transform.position = new Vector3(player.transform.position.x + offset.x + xMove, transform.position.y, player.transform.position.z + offset.z + zMove);  
    }

    bool turn;

    //public void TurnCameraZ(int xM, int zM)
    //{

    //}
    public void TurnCamera(int xM,int zM)
    {
        
            xMove = xM;
            zMove = zM;
            
       
    }
}
