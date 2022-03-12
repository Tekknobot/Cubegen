using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    public Slider musicVolumeSlider;
    public AudioSource musicAudioSource;

    public AudioClip[] song;

    // Start is called before the first frame update
    void Awake() {
        musicAudioSource.clip = song[Random.Range(0, song.Length)];
        musicAudioSource.Play();
    }

    // Update is called once per frame
    void Update() {
        //StartCoroutine(FadeAudioSource.StartFade(musicAudioSource, 16, 1f));
        musicVolumeSlider.value = musicAudioSource.volume;
    }

    public void AdjustMusicVolume() {
        musicAudioSource.volume = musicVolumeSlider.value;
    }
}
