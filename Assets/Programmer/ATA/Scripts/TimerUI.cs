using TMPro;
using UnityEngine;

    public class TimerUI : MonoBehaviour
    {
        private void Start()
        {
            if (TimerManager.Instance != null)
            {
                TextMeshProUGUI text = GetComponentInChildren<TextMeshProUGUI>();
                if (text != null)
                    TimerManager.Instance.SetTimerText(text);
            }
        }
    }
