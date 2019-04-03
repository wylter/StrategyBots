using UnityEngine.SceneManagement;
using UnityEngine;

public class LevelLoader : MonoBehaviour{

    private Animator _animator;
    private int levelToLoad;

    private void Start() {
        _animator = GetComponent<Animator>();
        SceneManager.activeSceneChanged += OnSceneChanged;
    }

    public void FadeToLevel(int levelIndex) {
        levelToLoad = levelIndex;
        _animator.SetTrigger("FadeOut");
    }

    public void OnFadeComplete() {
        SceneManager.LoadScene(levelToLoad);
    }

    public void OnSceneChanged(Scene current, Scene next) {
        _animator.SetTrigger("FadeIn");
    }
}
