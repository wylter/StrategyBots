using UnityEngine;
using UnityEngine.UI;

public class UnitOverlayController : MonoBehaviour{

    [SerializeField]
    private Image[] _toggleImages = null;
    [SerializeField]
    private Color _selectedColor = Color.white;
    private Color _defalutColor;

    private int _currentSpot = -1;
    public int currentSpot { get { return _currentSpot; } }

    void Start(){
        Debug.Assert(_toggleImages.Length == PickMenuController.maxUnits, "TooglesImages not set");

        _defalutColor = _toggleImages[0].color;
        Debug.Assert(_toggleImages[1].color == _defalutColor && _toggleImages[2].color == _defalutColor, "Toggle Images dont have all the same color, might result in inconsistency in the graphic");
    }

    public void UpdateOverlay(int spot) {
        _currentSpot = spot;
        for (int i = 0; i < _toggleImages.Length; i++) {
            _toggleImages[i].color = (i == currentSpot) ? _selectedColor : _defalutColor;
        }
    }

}
