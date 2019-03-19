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

    private RaycastHit2D[] _linecastCache;

    public bool isActing;

    private Animator animator;

    private AbilityBehavior _ability;
    public AbilityBehavior ability { get { return _ability; } }

    private void Start() {
        _linecastCache = new RaycastHit2D[1];
        _healthUI.maxValue = _healthUI.value = HitPoints;
        animator = GetComponent<Animator>();
        _ability = GetComponent<AbilityBehavior>();
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
        
        isMoving = isActing = true;
        path.Reverse();
        foreach (var cell in path) {
            Vector3 destination_pos = new Vector3(cell.transform.localPosition.x, transform.localPosition.y, cell.transform.localPosition.z);
 
            _unitBody.transform.localRotation = Quaternion.LookRotation(Vector3.forward, destination_pos - transform.localPosition);

            while (transform.localPosition != destination_pos) {
                transform.localPosition = Vector3.MoveTowards(transform.localPosition, destination_pos, Time.deltaTime * MovementSpeed);
                yield return 0;
            }
        }
        isMoving = isActing = false;
    }

    public List<Cell> GetAvailableAttackableCells(List<Cell> cells) {
        return GetAvailableAttackableCells(cells, AttackRange);
    }

    public List<Cell> GetAvailableAttackableCells(List<Cell> cells, int range) {

        List<Cell> attackableCells = new List<Cell>(cells);

        attackableCells = attackableCells.Where(otherCell => Cell.GetDistance(otherCell) <= range && IsCellInVision(otherCell)).ToList();


        return attackableCells;
    }

    public bool IsCellInVision(Cell otherCell) {
        return Physics2D.LinecastNonAlloc(Cell.transform.position, otherCell.transform.position, _linecastCache, _obstacleLayerMask) == 0;
    }

    public override void Defend(Unit other, int damage) {
        base.Defend(other, damage);

        _healthUI.value = HitPoints;
    }

    public override void DealDamage(Unit other) {
        if (isMoving)
            return;
        if (ActionPoints == 0)
            return;
        if (!IsUnitAttackable(other, Cell))
            return;

        isActing = true;
        _unitBody.transform.localRotation = Quaternion.LookRotation(Vector3.forward, other.transform.position - transform.localPosition);
        animator.SetTrigger("Attack");

        StartCoroutine(ResolveAttack(other));
    }

    private IEnumerator ResolveAttack(Unit other) {
        while (isActing) {
            yield return null;
        }

        MarkAsAttacking(other);
        ActionPoints--;
        other.Defend(this, AttackFactor);

        if (ActionPoints == 0) {
            SetState(new UnitStateMarkedAsFinished(this));
            MovementPoints = 0;
        }
    }
}
