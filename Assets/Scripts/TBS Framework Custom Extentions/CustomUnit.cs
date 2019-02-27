using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class CustomUnit : Unit{

    [SerializeField]
    private LayerMask _obstacleLayerMask;
    [SerializeField]
    private GameObject _unitBody;

    [SerializeField]
    private Slider _healthUI;

    private RaycastHit2D[] linecastCache;

    private void Start() {
        linecastCache = new RaycastHit2D[1];
        _healthUI.maxValue = _healthUI.value = HitPoints;
    }

    public override void Initialize(){
        base.Initialize();
        transform.localPosition = new Vector3(transform.localPosition.x, 0.5f, transform.localPosition.z);
    }

    protected override void OnMouseEnter() {

    }
    protected override void OnMouseExit() {

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

    public override void Move(Cell destinationCell, List<Cell> path) {
        base.Move(destinationCell, path);
        MovementPoints = 0;
    }

    protected override IEnumerator MovementAnimation(List<Cell> path) {
        isMoving = true;
        path.Reverse();
        foreach (var cell in path) {
            Vector3 destination_pos = new Vector3(cell.transform.localPosition.x, transform.localPosition.y, cell.transform.localPosition.z);
 
            _unitBody.transform.localRotation = Quaternion.LookRotation(Vector3.forward, destination_pos - transform.localPosition);

            while (transform.localPosition != destination_pos) {
                transform.localPosition = Vector3.MoveTowards(transform.localPosition, destination_pos, Time.deltaTime * MovementSpeed);
                yield return 0;
            }
        }
        isMoving = false;
    }

    public List<Cell> GetAvailableAttackableCells(List<Cell> cells) {

        List<Cell> attackableCells = new List<Cell>(cells);

        attackableCells = attackableCells.Where( otherCell => Cell.GetDistance(otherCell) <= AttackRange && IsCellInVision(otherCell)).ToList();


        return attackableCells;
    }

    public bool IsCellInVision(Cell otherCell) {
        return Physics2D.LinecastNonAlloc(Cell.transform.position, otherCell.transform.position, linecastCache, _obstacleLayerMask) == 0;
    }

    protected override void Defend(Unit other, int damage) {
        base.Defend(other, damage);

        _healthUI.value = HitPoints;
    }
}
