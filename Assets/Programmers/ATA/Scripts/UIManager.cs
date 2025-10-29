using UnityEngine;
using System.Collections;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    [Header("Fade Settings")]
    [SerializeField] private CanvasGroup fadeCanvas;
    [SerializeField] private float fadeDuration = 1f;
    public float FadeDuration => fadeDuration; 

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {

        FadeIn(); 
    }

    public void FadeOut()
    {
        StartCoroutine(FadeRoutine(1f));
    }

    public void FadeIn()
    {
        StartCoroutine(FadeRoutine(0f));
    }

    private IEnumerator FadeRoutine(float target)
    {
        float start = fadeCanvas.alpha;
        float time = 0f;

        while (time < fadeDuration)
        {
            //continue fade effect while timescale = 0;
            time += Time.unscaledDeltaTime; 
            fadeCanvas.alpha = Mathf.Lerp(start, target, time / fadeDuration);
            yield return null;
        }

        fadeCanvas.alpha = target;
    }
}