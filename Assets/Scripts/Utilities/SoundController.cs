using UnityEngine;
using UnityEngine.Audio;
using System.Collections;

public class SoundController : MonoBehaviour {

    [SerializeField]
    private AudioMixer audioMixer;                  //Reference to the audio mixer of the project
    [SerializeField]
    private AudioSource musicSource;                 //Drag a reference to the audio source which will play the music.

    public void SetVolume(float volume) {
        audioMixer.SetFloat("Volume", volume);
    }

    public float GetVolume() {
        float volume = 0;
        audioMixer.GetFloat("Volume", out volume);
        return volume;
    }

    //Changes the songs between levels (Only if the next level has a different music)
    public void ChangeSong(AudioClip clip) {
        if (musicSource.clip != clip) {
            musicSource.clip = clip;

            //Play the clip.
            musicSource.Play();
        }
    }
}
