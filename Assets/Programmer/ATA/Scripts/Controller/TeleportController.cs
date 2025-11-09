using UnityEngine;

    public class TeleportController : MonoBehaviour
    {
        [SerializeField] private TeleportManager manager;
        [SerializeField] private Camera mainCamera;
        [SerializeField] private Camera secondCamera;
        [SerializeField] private Camera thirdCamera;
        [SerializeField] private Camera fourthCamera;
    [SerializeField] private Transform ChangeCameraPosition;

    private void Awake()
    {
        //mainCamera.enabled = true;
        secondCamera.enabled = false;
        thirdCamera.enabled = false;
        fourthCamera.enabled = false;
    }

    private void OnTriggerEnter(Collider other)
        {

            if (other.CompareTag("Player"))
            {
                if (manager != null)
                {
                // mainCamera.enabled = false;

                //secondCamera.enabled = true;
                   //mainCamera.GetComponent<CameraManager>().TurnCamera();
                    manager.TeleportToNextPoint();

                }
            }
        }
    }