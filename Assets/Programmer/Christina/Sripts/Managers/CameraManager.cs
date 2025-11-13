
using UnityEngine;



public class CameraManager : MonoBehaviour
{
    private Transform player = null;
    private Vector3 offset = new();
    private Vector3 refPos;
    private int zMove;
    private int xMove;

    float height;

    void Start()
    {
        if (!player)
        {
            player = GameObject.FindGameObjectWithTag("Player").transform;
        }
        transform.position = new Vector3(player.transform.position.x + 5, transform.position.y + 0.5f, player.transform.position.z - 10);
        offset = transform.position - player.transform.position;
        zMove = 0;
        xMove = 4;
    }

    void Update()
    {
        if (!player)
        {
            player = GameObject.FindGameObjectWithTag("Player").transform;
        }

        if (player.transform.position.y > transform.position.y + 3)
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
            transform.Rotate(0, -90, 0);
            player.GetComponent<PlayerManager2>().SetTurn();
        }
        
        transform.position = new Vector3(player.transform.position.x + offset.x + xMove, transform.position.y, player.transform.position.z + offset.z + zMove);  
    }

    public void TurnCamera(int xM,int zM)
    {
        xMove = xM;
        zMove = zM;
    }
}