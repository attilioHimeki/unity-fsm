using AIEngine;

public class EnemyFSMAgent : FSMAgent 
{

    protected override void InitialiseFSM()
    {
        base.InitialiseFSM();

        stateMachine.AddState(EnemyStateId.Attack, new EnemyAttackFS(stateMachine, gameObject));
        stateMachine.AddState(EnemyStateId.Chase, new EnemyChaseFS(stateMachine, gameObject));
        stateMachine.AddState(EnemyStateId.Patrol, new EnemyPatrolFS(stateMachine, gameObject));

        stateMachine.SetInitialState(EnemyStateId.Patrol);
    }

}
