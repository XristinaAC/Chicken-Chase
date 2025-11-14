using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerSpawnManager : MonoBehaviour
{
    public static PlayerSpawnManager Instance;

    [SerializeField] private GameObject playerPrefab;
    private GameObject playerInstance;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
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
        if (scene.buildIndex == 0)
            return; 

        SpawnOrMovePlayer();
    }

    private void SpawnOrMovePlayer()
    {
        Transform spawnPoint = GameObject.FindWithTag("SpawnPoint")?.transform;

        if (spawnPoint == null)
        {
            Debug.LogWarning("SpawnPoint bulunamadı!");
            return;
        }
        
        if (playerInstance == null)
        {
            playerInstance = Instantiate(playerPrefab, spawnPoint.position, spawnPoint.rotation);
            return;
        }
        
        playerInstance.transform.SetPositionAndRotation(spawnPoint.position, spawnPoint.rotation);

        if (playerInstance.TryGetComponent<Rigidbody>(out var rb))
            rb.velocity = Vector3.zero;
    }
}