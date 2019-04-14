using UnityEngine;
using UnityEngine.UI;

public class MenuController : MonoBehaviour
{
    [SerializeField]
    private Animator _pickAnimator = null;
    [SerializeField]
    private Animator _rulesAnimator = null;
    [SerializeField]
    private SoundButtonController _soundButton = null;
    [SerializeField]
    private int _maxSoundPreferences = 2;

    private SoundController _soundController;

    private int currentSoundPreference = 1;

    public void Start() {
        _soundController = GameObject.FindGameObjectWithTag("Utility")?.GetComponent<GameUtilitiesManager>()?.soundController;
        Debug.Assert(_pickAnimator != null, "PickAnimator is null");
        Debug.Assert(_soundButton != null, "SoundButton is null");
        Debug.Assert(_rulesAnimator != null, "RulesAnimator is null");


        currentSoundPreference = PlayerPrefs.GetInt("SoundON", 1);
        ChangeSoundOption(currentSoundPreference);
    }

    public void TogglePickMenu(bool enabled) {
        _pickAnimator.SetBool("MenuOn", enabled);
    }

    public void ToggleRulesMenu(bool enabled) {
        _rulesAnimator.SetBool("MenuOn", enabled);
    }

    public void ToggleSound() {
        currentSoundPreference = (currentSoundPreference + 1) % _maxSoundPreferences;
        PlayerPrefs.SetInt("SoundON", currentSoundPreference);
        ChangeSoundOption(currentSoundPreference);
    }
    public void ChangeSoundOption(int option) {
        _soundController.ToggleSound((SoundNotification)option);
        _soundButton.ToogleImage((SoundNotification)option);
    }



}
