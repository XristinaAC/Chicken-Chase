using UnityEngine;

    public class TeleportController : MonoBehaviour
    {
        [SerializeField] private TeleportManager manager;
        [SerializeField] private Camera mainCamera;
        [SerializeField] private int zMove = 0;
        [SerializeField] private int xMove = 0;

    private void OnTriggerEnter(Collider other)
        {

            if (other.CompareTag("Player"))
            {
                if (manager != null)
                {
                
                    mainCamera.GetComponent<CameraManager>().TurnCamera(xMove, zMove);
                    manager.TeleportToNextPoint();

                }
            }
        }
    }