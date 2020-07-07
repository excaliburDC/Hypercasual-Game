using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GetSavedVolume : MonoBehaviour
{
    public Slider bgmSlider;
    public Slider sfxSlider;

    private void OnEnable()
    {
        GetVolume();
    }
    public void GetVolume()
    {
        bgmSlider.value = AudioManager.Instance.bgmFloat;
        sfxSlider.value = AudioManager.Instance.sfxFloat;
    }
}
