using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Rendering;

public class VolumeControls : MonoBehaviour
{
    public AudioMixer masterVolumeMixer;
    
    public void SetMasterVolume(float sliderValue)
    {
        //masterVolumeMixer.SetFloat("MasterVolume", sliderValue);
        masterVolumeMixer.SetFloat("ExposedMasterVolume", Mathf.Log10(sliderValue) * 20);
    }

    public void SetBGMVolume(float sliderValue)
    {
        //masterVolumeMixer.SetFloat("MasterVolume", sliderValue);
        masterVolumeMixer.SetFloat("ExposedBGMVolume", Mathf.Log10(sliderValue) * 20);
    }

    public void SetSFXVolume(float sliderValue)
    {
        //masterVolumeMixer.SetFloat("MasterVolume", sliderValue);
        masterVolumeMixer.SetFloat("ExposedSFXVolume", Mathf.Log10(sliderValue) * 20);
    }
}
