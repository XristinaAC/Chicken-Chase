using System;
using UnityEngine;
using TMPro;

public class TimerManager : MonoBehaviour
{
    public static TimerManager Instance { get; private set; }

    private float _elapsedTime = 0f;
    private bool _isRunning = false;
    private TextMeshProUGUI _timerText;

    public float ElapsedTime => _elapsedTime;

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

    private void Update()
    {
        if (!_isRunning) return;

        _elapsedTime += Time.deltaTime;
        UpdateTimerUI();
    }

    private void UpdateTimerUI()
    {
        if (_timerText == null) return;

        TimeSpan t = TimeSpan.FromSeconds(_elapsedTime);
        _timerText.text = $"{t.Minutes:D2}:{t.Seconds:D2}";
    }

    public void SetTimerText(TextMeshProUGUI text)
    {
        _timerText = text;
        UpdateTimerUI();
    }

    public void StartTimer() => _isRunning = true;
    public void StopTimer() => _isRunning = false;
    public void ResetTimer()
    {
        _elapsedTime = 0f;
        UpdateTimerUI();
    }
}