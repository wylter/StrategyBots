using UnityEngine;
using UnityEngine.UI;

public class MenuController : MonoBehaviour
{
    [SerializeField]
    private Animator _pickAnimator = null;
    [SerializeField]
    private Button _soundButton = null;

    private SoundController _soundController;

    public void Start() {
        _soundController = GameObject.FindGameObjectWithTag("Utility")?.GetComponent<GameUtilitiesManager>()?.soundController;
        Debug.Assert(_pickAnimator != null, "PickAnimator is null");
        Debug.Assert(_soundButton != null, "SoundButton is null");
    }

    public void TogglePickMenu(bool enabled) {
        _pickAnimator.SetBool("MenuOn", enabled);
    }

    public void ToggleSound() {
        _soundController.ToggleSound();
    }


}
