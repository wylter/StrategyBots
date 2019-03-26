using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CustomUnit : Unit{

    [SerializeField]
    private int _abilityCost = 1;
    [SerializeField]
    private int _abilityCostWhenDamaged = 2;
    [Space]
    [Header("Unit Elements")]
    [SerializeField]
    private GameObject _unitBody = null;
    [SerializeField]
    private Slider _healthUI = null;
    [SerializeField]
    private TextMeshProUGUI _healthText = null;
    [Space]
    [Header("Settings")]
    [SerializeField]
    private Color _destroyedColor = Color.black;
    [SerializeField]
    private LayerMask _obstacleLayerMask = 0;
    [SerializeField]
    private Color _loseHealthColor = Color.white;
    [SerializeField]
    private Color _gainHealthColor = Color.white;

    private RaycastHit2D[] _linecastCache;

    [HideInInspector]
    public bool isActing;

    private Animator _animator;
    public Animator animator { get { return _animator; } }

    private AbilityBehavior _ability;
    public AbilityBehavior ability { get { return _ability; } }

    private bool _abilityActionUsable;
    public bool abilityActionUsable { get { return _abilityActionUsable; } set { _abilityActionUsable = value;} }

    private void Start() {
        _linecastCache = new RaycastHit2D[1];
        _healthUI.maxValue = _healthUI.value = HitPoints;
        _animator = GetComponent<Animator>();
        _ability = GetComponent<AbilityBehavior>();

        Debug.Assert(_animator != null, "Animator not found");
        Debug.Assert(_ability != null, "Ability not found");
        Debug.Assert(_unitBody != null, "UnitBody is null");
        Debug.Assert(_healthUI != null, "HealthUI is null");
        Debug.Assert(_healthText != null, "HealthText is null");
        Debug.Assert(_obstacleLayerMask != 0, "ObstacleLayerMask is not set");
    }

    public override void Initialize(){
        base.Initialize();
        transform.localPosition = new Vector3(transform.localPosition.x, 0.5f, transform.localPosition.z);

        CustomSquare cell = Cell as CustomSquare;
        cell.unit = this;
    }

    public override void OnTurnStart() {
        base.OnTurnStart();
        _abilityActionUsable = true;
    }

    protected override void OnMouseEnter() {

    }
    protected override void OnMouseExit() {

    }

    protected override void OnDestroyed() {
        CustomSquare square = Cell as CustomSquare;
        if (square) {
            square.unit = null;
        }
        MarkAsDestroyed();

        GameController gameController = FindObjectOfType<GameController>();
        gameController.DeregisterUnitFromPlayer(this);

        _unitBody.transform.SetParent(transform.parent);
        Destroy(gameObject);
    }

    public override void MarkAsAttacking(Unit other) {

    }

    public override void MarkAsDefending(Unit other) {

    }

    public override void MarkAsDestroyed() {
        _unitBody.GetComponent<SpriteRenderer>().color = _destroyedColor;
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

        CustomSquare currentSquare = Cell as CustomSquare;
        if (currentSquare) {
            currentSquare.unit = null;
        }
        Debug.Assert(currentSquare, "Cell is not custom square");

        base.Move(destinationCell, path);
        MovementPoints = 0;

        CustomSquare destSquare = destinationCell as CustomSquare;
        if (destSquare) {
            destSquare.unit = this;
        }
        Debug.Assert(destSquare, "Cell is not custom square");
    }

    protected override IEnumerator MovementAnimation(List<Cell> path) {
        
        isMoving = isActing = true;
        path.Reverse();
        foreach (var cell in path) {
            Vector3 destination_pos = new Vector3(cell.transform.position.x, cell.transform.position.y, transform.position.z);

            RotateUnitTowardPosition(destination_pos);

            while (transform.position != destination_pos) {
                transform.position = Vector3.MoveTowards(transform.position, destination_pos, Time.deltaTime * MovementSpeed);
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
        ShowHealthAnimation(-damage, _loseHealthColor);

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

        RotateUnitTowardPosition(other.transform.position);

        _animator.SetTrigger("Attack");

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

    private void ShowHealthAnimation(int amount, Color color) {
        _healthText.color = color;
        _healthText.SetText(amount.ToString());
        animator.SetTrigger("UpdateHealth");
    }

    public int GetAbilityCost() {
        return HitPoints > TotalHitPoints / 2 ? _abilityCost : _abilityCostWhenDamaged;
    }

    public void RotateUnitTowardPosition(Vector3 position) {
        Quaternion newRotation = Quaternion.LookRotation(transform.position - position, Vector3.forward);
        newRotation.x = 0;
        newRotation.y = 0;

        _unitBody.transform.rotation = newRotation;
    }
}
