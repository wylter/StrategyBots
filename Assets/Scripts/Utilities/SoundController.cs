using UnityEngine;
using UnityEngine.Audio;
using System.Collections;

public class SoundController : MonoBehaviour {

    [SerializeField]
    private AudioMixer audioMixer = null;                  //Reference to the audio mixer of the project
    [SerializeField]
    private AudioSource musicSource = null;                 //Drag a reference to the audio source which will play the music.
    [SerializeField]
    private MusicSettings _musicSettings = null;

    private void Start() {
        Debug.Assert(audioMixer != null, "AudioMixer is null");
        Debug.Assert(musicSource != null, "MusicSource is null");
        Debug.Assert(_musicSettings != null, "MusicSettings is null");
    }

    public void SetVolume(float volume) {
        audioMixer.SetFloat("Volume", volume);
    }

    public float GetVolume() {
        float volume = 0;
        audioMixer.GetFloat("Volume", out volume);
        return volume;
    }

    //Changes the songs between levels (Only if the next level has a different music)
    public void ChangeSong(int i) {
        bool songIsValid = _musicSettings._levelSongs.Length > i && _musicSettings._levelSongs[i] != null;
        Debug.Assert(songIsValid, "Selected song at index " + i + " is not assigned");
        if (songIsValid && musicSource.clip != _musicSettings._levelSongs[i]) {

            musicSource.clip = _musicSettings._levelSongs[i];
            //Play the clip.
            musicSource.Play();
        }
    }
}
