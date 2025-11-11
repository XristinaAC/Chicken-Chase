using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UiPanelOpener : MonoBehaviour
{  
    //Open chosen panel
    public void ShowPanel(GameObject panel)
    {
        SoundManager.Instance.PlaySFX(SoundManager.Instance.buttonEffect);
        panel.SetActive(true);
    }

    public void HidePanel(GameObject panel)
    {
        SoundManager.Instance.PlaySFX(SoundManager.Instance.buttonEffect);
        panel.SetActive(false);
    }

    public void TogglePanel(GameObject panel)
    {
        SoundManager.Instance.PlaySFX(SoundManager.Instance.buttonEffect);
        panel.SetActive(!panel.activeSelf);

    }
}
