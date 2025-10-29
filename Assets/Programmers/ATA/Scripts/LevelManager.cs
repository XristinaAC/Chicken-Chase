using System;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance { get; private set; }
    
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
    
    public async Task LoadLevelAsync(string sceneName)
    {
        OnLevelLoadStart?.Invoke();
        
        var asyncLoad = SceneManager.LoadSceneAsync(sceneName);
        asyncLoad.allowSceneActivation = false;
        
        while (asyncLoad.progress < 0.9f)
            await Task.Yield();

        asyncLoad.allowSceneActivation = true;

        OnLevelLoadComplete?.Invoke();
        await Task.Delay(500);
    }

    public async void LoadNextLevelAsync()
    {

        int nextIndex = SceneManager.GetActiveScene().buildIndex + 1;


        if (nextIndex < SceneManager.sceneCountInBuildSettings)
        {
            string scenePath = SceneUtility.GetScenePathByBuildIndex(nextIndex);
            
            string sceneName = System.IO.Path.GetFileNameWithoutExtension(scenePath);

            await LoadLevelAsync(sceneName);
        }
    }
}