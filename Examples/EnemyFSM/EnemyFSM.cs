using UnityEngine;
using System.Collections;
using AIEngine;

public class EnemyFSM : FSM 
{

    private GameObject mPlayer;

    public override void InitFSM()
    {
        mPlayer = GameObject.Find("Player");

        base.InitFSM();
    }


    public override void UpdateFSM()
    {
        base.UpdateFSM();
    }

    protected override FSMState CreateState(StateId state)
    {
        switch (state.GetId())
        {
            case "EnemyPatrol":
                return new EnemyPatrolFS(this, gameObject, mPlayer);
            case "EnemyChase":
                return new EnemyChaseFS(this, gameObject, mPlayer);
            case "EnemyAttack":
                return new EnemyAttackFS(this, gameObject, mPlayer);
            default:
                return null;
        }
    }
    

    public override void StopFSM()
    {
        animation.Play("death");
        Destroy(gameObject, 1.5f);

        base.StopFSM();
    }

    void OnDrawGizmos()
    {
        if(currentState != null)
        {
            currentState.OnDrawGizmos();
        }
    }
}
