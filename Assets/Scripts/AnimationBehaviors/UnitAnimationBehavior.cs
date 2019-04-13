using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitAnimationBehavior : StateMachineBehaviour
{
    private CustomUnit _unit;
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex){
        if (_unit == null) {
            _unit = animator.gameObject.GetComponent<CustomUnit>();
            Debug.Assert(_unit != null, "Custom unit not found");
        }

        if (_unit) {
            _unit.isAnimating = true;
        }
    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex){
        if (_unit) {
            _unit.isAnimating = false;
        }
    }
}
