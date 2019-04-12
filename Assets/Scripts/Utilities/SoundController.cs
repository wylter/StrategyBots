using UnityEngine;
using UnityEngine.Audio;
using System.Collections;

public class SoundController : MonoBehaviour {

    private static float MUTEDVOLUME = -80f;
    private float _defaultVolume = 0f;

    [SerializeField]
    private AudioMixer audioMixer = null;                  //Reference to the audio mixer of the project
    [SerializeField]
    private MusicSettings _musicSettings = null;

    [SerializeField]
    private AudioSource _source0 = null;
    [SerializeField]
    private AudioSource _source1 = null;

    private bool _isSource0Playing = true;

    private bool _isMuted = false;
    public bool isMuted { get { return _isMuted; } }

    private Coroutine _zerothSourceFadeRoutine = null;
    private Coroutine _firstSourceFadeRoutine = null;

    private void Start() {
        Debug.Assert(audioMixer != null, "AudioMixer is null");
        Debug.Assert(_musicSettings != null, "MusicSettings is null");
        Debug.Assert(_source0 != null, "Source0 is null");
        Debug.Assert(_source1 != null, "Source1 is null");

        Debug.Assert(_source0.clip != null, "No starting song inserted");
        _source0.enabled = true;
        _source1.enabled = false;

        audioMixer.GetFloat("MasterVolume", out _defaultVolume);
        Debug.Assert(audioMixer.GetFloat("MasterVolume", out _defaultVolume), "MasterVolume exposed value not found in the audio mixer");
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

        if (songIsValid) {
            CrossFade(_musicSettings._levelSongs[i], 1f, 2f);
        }
    }

    public void CrossFade(AudioClip clipToPlay, float maxVolume, float fadingTime) {

        AudioSource firstSource;
        AudioSource secondSource;

        if (_isSource0Playing) {
            firstSource = _source0;
            secondSource = _source1;
        } else {
            firstSource = _source1;
            secondSource = _source0;
        }

        secondSource.enabled = true;

        //If the clip requested is the same, no reason to crossfade
        if (clipToPlay == firstSource.clip) {
            return;
        }

        secondSource.clip = clipToPlay;
        secondSource.Play();
        secondSource.volume = 0;

        if (_firstSourceFadeRoutine != null) {
            StopCoroutine(_firstSourceFadeRoutine);
        }
        _firstSourceFadeRoutine = StartCoroutine(fadeSource(secondSource, secondSource.volume, maxVolume, fadingTime));

        if (_zerothSourceFadeRoutine != null) {
            StopCoroutine(_zerothSourceFadeRoutine);
        }
        _zerothSourceFadeRoutine = StartCoroutine(fadeSource(firstSource, firstSource.volume, 0, fadingTime));


        _isSource0Playing = !_isSource0Playing;

        return;
    }



    IEnumerator fadeSource(AudioSource sourceToFade, float startVolume, float endVolume, float duration) {
        float startTime = Time.time;

        while (sourceToFade.volume != endVolume) {

            if (duration == 0) {
                sourceToFade.volume = endVolume;
                break;
            }
            float elapsed = Time.time - startTime;

            sourceToFade.volume = Mathf.Clamp01(Mathf.Lerp(startVolume, endVolume, elapsed / duration));

            yield return null;
        }

        if (endVolume == 0) {
            sourceToFade.enabled = false;
            sourceToFade.clip = null;
        }
    }

    public void ToggleSound() {
        _isMuted = !_isMuted;
        audioMixer.SetFloat("MasterVolume", _isMuted ? MUTEDVOLUME : _defaultVolume);
    }
}
