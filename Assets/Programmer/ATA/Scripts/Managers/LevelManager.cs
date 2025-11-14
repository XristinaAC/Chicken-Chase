using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance { get; private set; }
    
    [Header("Level Settings")]
    [SerializeField] private List<string> endlessLevelNames = new List<string>();
    
    private string _lastLoadedScene; // Prevent to loading same scene
    
    
    public event Action OnLevelLoadStart;
    public event Action OnLevelLoadComplete;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else Destroy(gameObject);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.N))
        {
            LoadNextLevelAsync();
        }
    }

    public async Task LoadLevelAsync(string sceneName)
    {
        OnLevelLoadStart?.Invoke();
        
        var asyncLoad = SceneManager.LoadSceneAsync(sceneName);
        asyncLoad.allowSceneActivation = false;
        
        while (asyncLoad.progress < 0.9f)
            await Task.Yield();

        asyncLoad.allowSceneActivation = true;

        OnLevelLoadComplete?.Invoke();
        Time.timeScale = 1f;
    }

    public async Task LoadNextLevelAsync()
    {

        int nextIndex = SceneManager.GetActiveScene().buildIndex + 1;


        if (nextIndex < SceneManager.sceneCountInBuildSettings)
        {
            string scenePath = SceneUtility.GetScenePathByBuildIndex(nextIndex);
            
            string sceneName = System.IO.Path.GetFileNameWithoutExtension(scenePath);

            await LoadLevelAsync(sceneName);
        }
    }

    public async Task LoadRandomLevelAsync()
    {

        if (endlessLevelNames == null || endlessLevelNames.Count == 0) return;
        
        
        string nextScene;
        string currentScene = SceneManager.GetActiveScene().name;
        do
        {
            nextScene = endlessLevelNames[UnityEngine.Random.Range(0, endlessLevelNames.Count)];
        }
        while ((nextScene == _lastLoadedScene || nextScene == currentScene) && endlessLevelNames.Count > 1);


        _lastLoadedScene = nextScene;

        await LoadLevelAsync(nextScene);
    }
}