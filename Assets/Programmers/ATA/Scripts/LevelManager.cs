using System;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance { get; private set; }

    // to control obstacles or powerup in future.
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
    
    // First load the scene, control fade effect and task delay.
    // in the future manage to sound or shader when load the scene.
    public async Task LoadLevelAsync(string sceneName)
    {
        OnLevelLoadStart?.Invoke();
        UIManager.Instance.FadeOut();
        
        //waiting fade animation finish then users can see the level.
        await Task.Delay((int)(UIManager.Instance.FadeDuration * 1000));

        var asyncLoad = SceneManager.LoadSceneAsync(sceneName);
        asyncLoad.allowSceneActivation = false;

        // control loading scene while playing game
        while (asyncLoad.progress < 0.9f)
            await Task.Yield();

        asyncLoad.allowSceneActivation = true;

        // level loaded, event invoked and fade animation started 0.5 seconds 
        OnLevelLoadComplete?.Invoke();
        await Task.Delay(500);
        UIManager.Instance.FadeIn();
    }

    public async void LoadNextLevelAsync()
    {
        // find the next scene
        int nextIndex = SceneManager.GetActiveScene().buildIndex + 1;

        // find scene in build settings.
        if (nextIndex < SceneManager.sceneCountInBuildSettings)
        {
            string scenePath = SceneUtility.GetScenePathByBuildIndex(nextIndex);
            
            string sceneName = System.IO.Path.GetFileNameWithoutExtension(scenePath);

            await LoadLevelAsync(sceneName);
        }
    }
}