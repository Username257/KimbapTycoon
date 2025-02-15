using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class SettingCanvas : MonoBehaviour
{
    public AudioMixer mixer;

    private void Awake()
    {
        SetBGM(0.5f);
        SetSFX(0.5f);
    }

    public void SetBGM(float value)
    {
        mixer.SetFloat("BGM", Mathf.Log10(value) * 20);
    }

    public void SetSFX(float value)
    {
        mixer.SetFloat("SFX", Mathf.Log10(value) * 20);
    }

    public void SetLanguage(bool isKor)
    {
        GameManager.Setting.isKor = isKor;
        GameManager.Setting.OnLanguageChanged.Invoke();
    }
}
