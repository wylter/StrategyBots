using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomUnit : Unit{

    [SerializeField]
    private float rotationSpeed = 90f;

    public override void Initialize(){
        base.Initialize();
        transform.localPosition = new Vector3(transform.localPosition.x, 0.5f, transform.localPosition.z);
    }

    public override void MarkAsAttacking(Unit other) {

    }

    public override void MarkAsDefending(Unit other) {

    }

    public override void MarkAsDestroyed() {

    }

    public override void MarkAsFinished() {

    }

    public override void MarkAsFriendly() {

    }

    public override void MarkAsReachableEnemy() {

    }

    public override void MarkAsSelected() {

    }

    public override void UnMark() {

    }

    protected override IEnumerator MovementAnimation(List<Cell> path) {
        isMoving = true;
        path.Reverse();
        foreach (var cell in path) {
            Vector3 destination_pos = new Vector3(cell.transform.localPosition.x, transform.localPosition.y, cell.transform.localPosition.z);

            transform.localRotation = Quaternion.Euler(transform.localRotation.eulerAngles.x, Quaternion.LookRotation(destination_pos - transform.localPosition).eulerAngles.y, transform.localRotation.eulerAngles.z);

            while (transform.localPosition != destination_pos) {
                transform.localPosition = Vector3.MoveTowards(transform.localPosition, destination_pos, Time.deltaTime * MovementSpeed);
                yield return 0;
            }
        }
        isMoving = false;
    }
}
