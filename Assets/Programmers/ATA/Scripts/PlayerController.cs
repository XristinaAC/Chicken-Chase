using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    private static PlayerController Instance;

    
    [SerializeField] private float speed = 10f;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            SceneManager.sceneLoaded += OnSceneLoaded;
        }
        else 
            Destroy(gameObject);
    }

    private void Start()
    {
        if (GameManager.Instance != null)
            GameManager.Instance.OnGameStateChanged += OnGameGoing;
    }
    private void Update()
    {
        transform.Translate(Vector3.right * (Time.deltaTime * speed));
    }

    private void OnDisable()
    {
        GameManager.Instance.OnGameStateChanged -= OnGameGoing;
    }

    private void OnDestroy()
    {
      
        SceneManager.sceneLoaded -= OnSceneLoaded;
    
    }

    private void OnGameGoing(GameManager.GameState state)
    {
        enabled = (state == GameManager.GameState.Playing);
    }


    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        GameObject spawnPosition = GameObject.FindGameObjectWithTag("PlayerSpawn");
        
        // if there is no spawn position in the scene, player position will 0,0,0 
        if(spawnPosition != null)
            transform.position = spawnPosition.transform.position;
        else 
            transform.position = Vector3.zero; 
    }
  


}