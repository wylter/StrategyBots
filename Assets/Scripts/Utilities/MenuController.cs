using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuController : MonoBehaviour
{
    [SerializeField]
    private Animator pickAnimator = null;

    public void Start() {
        Debug.Assert(pickAnimator != null, "PickAnimator is null");
    }

    public void TogglePickMenu(bool enabled) {
        pickAnimator.SetBool("MenuOn", enabled);
    }
}
