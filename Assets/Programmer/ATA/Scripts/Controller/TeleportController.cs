using UnityEngine;

    public class TeleportController : MonoBehaviour
    {
        [SerializeField] private TeleportManager manager;

        private void OnTriggerEnter(Collider other)
        {

            if (other.CompareTag("Player"))
            {
                if (manager != null)
                {
                    manager.TeleportToNextPoint();

                }
            }
        }
    }