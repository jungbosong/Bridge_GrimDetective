using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    private static AudioManager instance;
    private AudioSource BGMaudioSource;
    private AudioSource EffectSoundaudioSource;
    public AudioMixer audioMixer;
    public Slider BGMaudioSlider;
    public Slider EffectSoundaudioSlider;
    
    public static AudioManager Instance {
        get{
            if(instance == null){
                var obj = FindObjectOfType<AudioManager>();
                if(obj != null) {
                    instance = obj;
                } else {
                    var newObj = new GameObject().AddComponent<AudioManager>();
                    instance = newObj;
                }
            }
            return instance;
        }
    }
    private void Awake() 
    {
        var objs = FindObjectsOfType<AudioManager>();
        if(objs.Length != 1) {
            Destroy(gameObject);
            return;
        }
        DontDestroyOnLoad(gameObject);
        BGMaudioSource = GetComponent<AudioSource>();
        BGMaudioSlider.value = PlayerPrefs.GetFloat("BGM", 0.75f);
        EffectSoundaudioSource = GetComponent<AudioSource>();
        EffectSoundaudioSlider.value = PlayerPrefs.GetFloat("EffectSound", 0.75f);
    }

    public void BGMAudioControl()
    {
        float sound = BGMaudioSlider.value;
        if(sound == 0) audioMixer.SetFloat("BGM", -80f);
        else {
            audioMixer.SetFloat("BGM", Mathf.Log10(sound)*20);
            PlayerPrefs.SetFloat("BGM", sound);
        }
    }
    public void EffectSoundAudioControl()
    {
        float sound = EffectSoundaudioSlider.value;
        if(sound == 0) audioMixer.SetFloat("EffectSound", -80f);
        else {
            audioMixer.SetFloat("EffectSound", Mathf.Log10(sound)*20);
            PlayerPrefs.SetFloat("EffectSound", sound);
        }
    }


    public void ShowSliderValue()
    {
        Debug.Log(BGMaudioSlider.value);
    }
}
