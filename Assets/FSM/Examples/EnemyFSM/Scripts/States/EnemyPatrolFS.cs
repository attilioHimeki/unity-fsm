using UnityEngine;
using Himeki.FSM;
public class EnemyPatrolFS : FSMState 
{

    private UnityEngine.AI.NavMeshAgent ownerNavAgent;
    private float waitCounter;

    private Transform[] waypoints;
    private Transform currentWaypoint;

    private float viewFieldRadius = 120.0f;
    private float viewRange = 6.0f;

    private PlayerController player;

    public EnemyPatrolFS(FSM fsm, GameObject owner)
        : base(fsm, owner)
    {
        ownerNavAgent = owner.GetComponent<UnityEngine.AI.NavMeshAgent>();

        player = ExampleWorld.instance.getPlayerAgent();

        waypoints = ExampleWorld.instance.getWaypoints();
    }

    public override StateId GetId()
    {
        return EnemyStateId.Patrol;
    }

    override public void OnStateEnter()
    {
        waitCounter = 0.0f;

        SetNewWaypoint();
    }

    override public void OnStateExit()
    {
        ownerNavAgent.isStopped = true;
    }

    override public void OnStateUpdate()
    {
        if(isTargetVisible())
        {
            stateMachine.MoveToState(EnemyStateId.Chase);
        }
        else if(!IsMoving())
        {
            waitCounter += Time.deltaTime;

            if(waitCounter > 2.0f)
            {
                SetNewWaypoint();
                waitCounter = 0.0f;
            }
        }
    }

    bool IsMoving()
    {
        return ownerNavAgent.velocity.sqrMagnitude > 0.0f;
    }

    void SetNewWaypoint()
    {
        currentWaypoint = FindRandomWaypoint();

        if(currentWaypoint)
        {
            ownerNavAgent.SetDestination(currentWaypoint.transform.position);
        }
    }
    Transform FindRandomWaypoint()
    {
        if(waypoints.Length > 0)
        {
            Transform newWaypoint;
            do
            {
                newWaypoint = waypoints[Random.Range(0, waypoints.Length - 1)];
            }
            while (currentWaypoint == newWaypoint);

            return newWaypoint;
        }

        return null;
    }

    bool isTargetVisible()
    {
        return !player.IsDead() && CanSeePlayer();
    }

    bool CanSeePlayer()
    {
        Vector3 direction = player.transform.position - ownerAgent.transform.position;
        float angle = Vector3.Angle(direction, ownerAgent.transform.forward);
        float distance = direction.magnitude;

        if (distance < viewRange && angle < viewFieldRadius)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

}
