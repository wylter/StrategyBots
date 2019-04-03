using UnityEngine.SceneManagement;
using UnityEngine;

public class GameUtilitiesManager : MonoBehaviour{

    [SerializeField]
    private LevelLoader _loader = null;
    public LevelLoader levelLoader { get { return _loader; } }
    [SerializeField]
    private SoundController _sound = null;
    public SoundController soundController { get { return _sound; } }

    void Start(){
        Debug.Assert(_loader != null, "LevlLoader is null");
        Debug.Assert(_sound != null, "SoundController is null");

        DontDestroyOnLoad(this);

        SceneManager.LoadScene(1);
    }
}
