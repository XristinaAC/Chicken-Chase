using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerSpawnManager : MonoBehaviour
{
    [SerializeField] private GameObject playerPrefab;
    private GameObject currentPlayer;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        var oldPlayer = GameObject.FindWithTag("Player");
        if (oldPlayer != null)
            Destroy(oldPlayer);
        
        var spawnPoint = GameObject.FindWithTag("SpawnPoint");
        Vector3 spawnPos = Vector3.zero;
        Quaternion spawnRot = Quaternion.identity;

        if (spawnPoint != null)
        {
            spawnPos = spawnPoint.transform.position;
            spawnRot = spawnPoint.transform.rotation;
        }
        else
        {
            Debug.LogWarning("SpawnPoint bulunamadı!");
        }
        
        currentPlayer = Instantiate(playerPrefab, spawnPos, spawnRot);
    }
}