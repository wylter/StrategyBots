using UnityEngine;
using System.Collections;

[CreateAssetMenu(fileName = "Data", menuName = "TBSDataObjects/MusicSettings", order = 1)]
public class MusicSettings : ScriptableObject {
    public AudioClip[] _levelSongs;
}
