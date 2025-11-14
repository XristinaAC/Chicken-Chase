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

    GameObject _oldPlayer;
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
<<<<<<< HEAD
        // 1) Menü sahnesinde Player spawn ETME
        if (scene.buildIndex == 0)
            return;
=======
        var oldPlayer = GameObject.FindWithTag("Player");
        _oldPlayer = GameObject.FindWithTag("Player");
        if (_oldPlayer != null)
            currentPlayer = _oldPlayer;
            //Destroy(oldPlayer);

        var spawnPoint = GameObject.FindWithTag("SpawnPoint");
        Vector3 spawnPos = Vector3.zero;
        Quaternion spawnRot = Quaternion.identity;
>>>>>>> 267401acb0b4badab5498e75df317feddadfbabc

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