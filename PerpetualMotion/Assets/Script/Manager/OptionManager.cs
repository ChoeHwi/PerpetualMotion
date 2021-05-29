using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OptionManager : MonoBehaviour
{
    /// <summary>セーブマネージャーを格納する場所</summary>
    SaveManager saveManager;
    /// <summary>オーディオマネージャーを格納する場所</summary>
    AudioManager audioManager;
    [SerializeField] Slider sliderBGM;
    [SerializeField] Slider sliderSE;


    void Start()
    {
        if (GameObject.FindObjectOfType<SaveManager>())
        {
            saveManager = GameObject.FindObjectOfType<SaveManager>().GetComponent<SaveManager>();
            Debug.Log(saveManager.saveData.m_BGMVolume);
            Debug.Log(saveManager.saveData.m_SEVolume);
            sliderBGM.value = saveManager.saveData.m_BGMVolume;
            sliderSE.value = saveManager.saveData.m_SEVolume;
        }
        if (GameObject.FindObjectOfType<AudioManager>())
        {
            audioManager = GameObject.FindObjectOfType<AudioManager>().GetComponent<AudioManager>();
        }
    }

    public void VolumeValueChanged(bool isBGM)
    {
        if (isBGM)
        {
            if (audioManager)
            {
                audioManager.VolumeChangeBGM(sliderBGM.value);
            }
            if (saveManager)
            {
                saveManager.saveData.m_BGMVolume = sliderBGM.value;
            }
        }
        else
        {
            if (audioManager)
            {
                audioManager.VolumeChangeSE(sliderSE.value);
            }
            if (saveManager)
            {
                saveManager.saveData.m_SEVolume = sliderSE.value;
            }
        }
    }
}
