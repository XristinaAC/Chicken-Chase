using System.Threading.Tasks;
using UnityEngine;

public class FinishTrigger : MonoBehaviour
{
    private bool _triggered = false;

    
    // I covered async to control and safe teleport for player.
    private async void OnTriggerEnter(Collider other)
    {
        if (_triggered || !other.CompareTag("Player"))
            return;

        _triggered = true;

        if (LevelManager.Instance != null)
        {
            await Task.Yield(); 
            LevelManager.Instance.LoadNextLevelAsync();
        }
    }
}