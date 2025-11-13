using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerSpawnManager : MonoBehaviour
{
    [SerializeField] private GameObject playerPrefab; // 💡 Chicken prefab
    private GameObject currentPlayer;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject); // Kalıcı olsun, her sahnede çalışsın
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
        // Eski player varsa sahneden temizle
        //var oldPlayer = GameObject.FindWithTag("Player");
        _oldPlayer = GameObject.FindWithTag("Player");
        if (_oldPlayer != null)
        {
            currentPlayer = _oldPlayer;
            //Destroy(oldPlayer);
            return;
        }

        // SpawnPoint bul
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
            Debug.LogWarning("SpawnPoint bulunamadı, (0,0,0) pozisyonuna spawn ediliyor!");
        }

        // Yeni player oluştur
        currentPlayer = Instantiate(playerPrefab, spawnPos, spawnRot);
    }
}