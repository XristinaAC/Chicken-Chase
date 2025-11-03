
    using System.Threading.Tasks;
    using UnityEngine;

    public class RandomFinishTrigger : MonoBehaviour
    {
        private bool _triggered = false;

        private async void OnTriggerEnter(Collider other)
        {
            if (_triggered || !other.CompareTag("Player"))
                return;

            _triggered = true;

            if (LevelManager.Instance != null)
            {
                await Task.Delay(800);
                await LevelManager.Instance.LoadRandomLevelAsync();
            }
        }
    }
