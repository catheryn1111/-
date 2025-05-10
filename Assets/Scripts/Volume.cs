using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class Volume : MonoBehaviour
{
    [SerializeField] AudioMixer masterMixer;
    [SerializeField] Slider musicVolumeSlider;
    [SerializeField] float musicLvl;

    private void Start()
    {
        if (PlayerPrefs.HasKey("MainMusic"))
        {
            musicVolumeSlider.value = PlayerPrefs.GetFloat("MainMusic");
            masterMixer.SetFloat("Main", PlayerPrefs.GetFloat("MainMusic"));
        }
        else
        {
            musicVolumeSlider.value = 0;
            masterMixer.SetFloat("Main", 0);
        }
    }
    public void SetMusicVolume()
    {
        musicLvl = musicVolumeSlider.value;
        masterMixer.SetFloat("Main", musicLvl);
        PlayerPrefs.SetFloat("MainMusic", musicLvl);
    }

}