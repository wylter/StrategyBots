
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class SoundButtonController : MonoBehaviour {

    [SerializeField]
    private Sprite _spriteON = null;
    [SerializeField]
    private Sprite _spriteOFF = null;

    private Image _image;

    private void Start() {
        _image = GetComponent<Image>();
        Debug.Assert(_image != null, "The object has no image attached");
        Debug.Assert(_spriteON != null, "SpriteON is null");
        Debug.Assert(_spriteOFF != null, "SpriteOFF is null");
    }

    public void ToogleImage(SoundNotification option) {
        switch (option){
            case SoundNotification.ON:
                _image.sprite = _spriteON;
                break;
            case SoundNotification.OFF:
                _image.sprite = _spriteOFF;
                break;
            default:
                Debug.Assert(false, "Not implemented option detected");
                break;
        }
    }

}