using Himeki.FSM;

public class EnemyFSMAgent : FSMAgent 
{

    protected override void InitialiseFSM()
    {
        base.InitialiseFSM();

        stateMachine.AddState(new EnemyAttackFS(stateMachine, gameObject));
        stateMachine.AddState(new EnemyChaseFS(stateMachine, gameObject));
        stateMachine.AddState(new EnemyPatrolFS(stateMachine, gameObject));

        stateMachine.SetInitialState(EnemyStateId.Patrol);
    }

}
