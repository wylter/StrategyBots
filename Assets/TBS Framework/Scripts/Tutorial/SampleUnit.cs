using UnityEngine;

public class SampleUnit : Unit
{

    public override void Initialize(){
        base.Initialize();
        transform.localPosition = new Vector3(transform.localPosition.x, 0.5f, transform.localPosition.z);
    }

    public override void MarkAsAttacking(Unit other)
    {      
    }

    public override void MarkAsDefending(Unit other)
    {       
    }

    public override void MarkAsDestroyed()
    {      
    }

    public override void MarkAsFinished()
    {
    }

    public override void MarkAsFriendly()
    {

    }

    public override void MarkAsReachableEnemy()
    {

    }

    public override void MarkAsSelected()
    {

    }

    public override void UnMark()
    {

    }
}
