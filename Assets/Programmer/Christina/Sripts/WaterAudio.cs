using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterAudio : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        SoundManager.Instance.PlayBackgroundSFX(SoundManager.effectsAudio.waterEffect);
    }
}
