using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerSpawnManager : MonoBehaviour
{
    public static PlayerSpawnManager Instance;
    [SerializeField] private GameObject playerPrefab;
    private GameObject currentPlayer;

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
        // 1) Menü sahnesinde Player spawn ETME
        if (scene.buildIndex == 0)
            return;

        // 2) Eğer zaten player varsa tekrar spawn etme
        if (currentPlayer != null)
        {
            MovePlayerToSpawnPoint();
            return;
        }

        // 3) Sahne ilk kez açılıyorsa yeni player oluştur
        SpawnNewPlayer();
    }

    private void SpawnNewPlayer()
    {
        Transform spawn = GameObject.FindWithTag("SpawnPoint")?.transform;

        if (spawn == null)
        {
            Debug.LogWarning("SpawnPoint bulunamadı!");
            spawn = new GameObject("SpawnPoint").transform;
        }

        currentPlayer = Instantiate(playerPrefab, spawn.position, spawn.rotation);
    }

    private void MovePlayerToSpawnPoint()
    {
        Transform spawn = GameObject.FindWithTag("SpawnPoint")?.transform;
        if (spawn == null) return;

        currentPlayer.transform.SetPositionAndRotation(spawn.position, spawn.rotation);

        // rigidbody varsa sıfırla
        if (currentPlayer.TryGetComponent<Rigidbody>(out var rb))
            rb.velocity = Vector3.zero;
    }
}