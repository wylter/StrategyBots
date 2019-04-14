
using UnityEngine;

public class PageSelector : MonoBehaviour{

    [SerializeField]
    private Animator[] _pageAnimators = null;
    [SerializeField]
    private int _startDownPosition = -1300;

    private int _currentPage = 0;


    private void Start() {
        Debug.Assert(_pageAnimators != null, "_pageAnimators is null");
        Debug.Assert(_pageAnimators.Length != 0, "_pageAnimators doesnt have elements");

        _pageAnimators[0].SetTrigger("Init");
        for (int i = 1; i < _pageAnimators.Length; i++) {
            RectTransform pageTransform = _pageAnimators[i].GetComponent<RectTransform>();
            if (pageTransform) {
                pageTransform.anchoredPosition = new Vector2(pageTransform.anchoredPosition.x, _startDownPosition);
            }

        }
    }

    public void GoUp() {
        if (_currentPage > 0) {
            _pageAnimators[_currentPage].SetTrigger("GoDown");
            _currentPage--;
            _pageAnimators[_currentPage].SetTrigger("GoDown");
        }
    }

    public void GoDown() {
        if (_currentPage < (_pageAnimators.Length - 1)) {
            _pageAnimators[_currentPage].SetTrigger("GoUp");
            _currentPage++;
            _pageAnimators[_currentPage].SetTrigger("GoUp");
        }
    }
}
