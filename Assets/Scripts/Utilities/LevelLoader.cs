using UnityEngine.SceneManagement;
using UnityEngine;

public class LevelLoader : MonoBehaviour{

    [SerializeField]
    private SoundController _sound = null;

    private Animator _animator;
    private int levelToLoad;

    private void Start() {
        _animator = GetComponent<Animator>();
        SceneManager.activeSceneChanged += OnSceneChanged;

        Debug.Assert(_animator != null, "Animator not found");
        Debug.Assert(_sound != null, "SoundController is null");
    }

    public void FadeToLevel(int levelIndex) {
        levelToLoad = levelIndex;
        _animator.SetTrigger("FadeOut");
        _sound.ChangeSong(levelIndex);
    }

    public void OnFadeComplete() {
        SceneManager.LoadScene(levelToLoad);
    }

    public void OnSceneChanged(Scene current, Scene next) {
        _animator.SetTrigger("FadeIn");
    }
}
