using UnityEngine;
using AIEngine;

public class EnemyChaseFS : FSMState 
{

    private UnityEngine.AI.NavMeshAgent enemyNavAgent;
    private PlayerController player;

    private float maxAttackDistance = 3.0f;
    private float maxChaseDistance = 6.0f;

    public EnemyChaseFS(FSM fsm, GameObject owner)
        : base(fsm, owner)
    {
        enemyNavAgent = owner.GetComponent<UnityEngine.AI.NavMeshAgent>();

        player = ExampleWorld.instance.getPlayerAgent();
    }

    public override StateId GetId()
    {
        return EnemyStateId.Chase;
    }

    override public void OnStateEnter()
    {
        enemyNavAgent.SetDestination(player.transform.position);
    }

    override public void OnStateExit()
    {
        enemyNavAgent.isStopped = true;
        enemyNavAgent.ResetPath();
    }

    override public void OnStateUpdate()
    {
        float distance = Vector3.Distance(ownerAgent.transform.position, player.transform.position);

        if (distance < maxAttackDistance)
        {
            stateMachine.MoveToState(EnemyStateId.Attack);
        } 
        else
        {
            if(distance < maxChaseDistance)
            {
                enemyNavAgent.SetDestination(player.transform.position);
            }
            else
            {
                stateMachine.MoveToState(EnemyStateId.Patrol);
            }
        }
    }

}
