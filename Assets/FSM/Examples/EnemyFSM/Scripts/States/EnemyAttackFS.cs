using UnityEngine;
using Himeki.FSM;

public class EnemyAttackFS : FSMState 
{
    private PlayerController player;
    private float mAttackTimeCounter;
    private float maxDistanceFromTarget = 5.0f;
    private float attackIntervalSecs = 3.0f;

    public EnemyAttackFS(FSM fsm, GameObject owner)
        : base(fsm, owner)
    {
        player = ExampleWorld.instance.getPlayerAgent();
    }

     public override StateId GetId()
    {
        return EnemyStateId.Attack;
    }

    override public void OnStateEnter()
    {
        mAttackTimeCounter = 0.0f;
    }

    override public void OnStateExit()
    {
        
    }

    override public void OnStateUpdate()
    {
        float distance = Vector3.Distance(player.transform.position, ownerAgent.transform.position);

        if (distance > maxDistanceFromTarget)
        {
            stateMachine.MoveToState(EnemyStateId.Chase);
        }
        else if(player.IsDead())
        {
            stateMachine.MoveToState(EnemyStateId.Patrol);
        }
        else
        {
            Vector3 point = player.transform.position;
            point.y = ownerAgent.transform.position.y;
            ownerAgent.transform.LookAt(point);

            mAttackTimeCounter += Time.deltaTime;

            if (mAttackTimeCounter >= attackIntervalSecs)
            {
                Attack();
                mAttackTimeCounter = 0.0f;
            }
        }
        
    }

    private void Attack()
    {
        player.LoseHealth(20);
    }

}
