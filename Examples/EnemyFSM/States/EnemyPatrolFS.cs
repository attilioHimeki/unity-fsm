using UnityEngine;
using System.Collections;
using AIEngine;

public class EnemyPatrolFS : FSMState {

    private NavMeshAgent mEnemyNavAgent;
    private Animation mEnemyAnimation;

    private bool isWaiting;
    private float waitCounter;

    private GameObject[] waypoints;
    private GameObject currentWaypoint;

    private float viewFieldRadius = 80.0f;
    private float viewRange = 6.0f;
    private float sphereRadius = 4.0f;

    private float maxWaypointDistance = 20.0f;

    private PlayerHealthController playerHealth;

    public EnemyPatrolFS(FSM fsm, GameObject enemy, GameObject player)
        : base(fsm, enemy, player)
    {
        mEnemyNavAgent = enemy.GetComponent<NavMeshAgent>();
        mEnemyAnimation = enemy.GetComponent<Animation>();

        playerHealth = player.GetComponent<PlayerHealthController>();

        waypoints = GameObject.FindGameObjectsWithTag("EnemyWaypoint");
    }

    override public void OnStateEnter()
    {
        isWaiting = false;
        waitCounter = 0.0f;

        SetNewWaypoint();
    }

    override public void OnStateExit()
    {

    }

    override public void Update()
    {
        if(isTargetVisible())
        {
            mFSM.MoveToState(EnemyStateId.Chase);
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
        return mEnemyNavAgent.velocity.sqrMagnitude > 0.0f;
    }

    void SetNewWaypoint()
    {
        GameObject newWaypoint = FindCloseWaypoint();
        if(newWaypoint == null)
        {
            newWaypoint = FindRandomWaypoint();
        }

        currentWaypoint = newWaypoint;

        mEnemyAnimation.Play("walk");

        mEnemyNavAgent.SetDestination(currentWaypoint.transform.position);
    }

    GameObject FindCloseWaypoint()
    {
        Collider[] colliders = Physics.OverlapSphere(mAgent.transform.position, maxWaypointDistance);

        foreach(Collider c in colliders)
        {
            if(c.tag == "EnemyWaypoint")
            {
                return c.gameObject;
            }
        }

        return null;
    }

    GameObject FindRandomWaypoint()
    {
        GameObject newWaypoint;
        do
        {
            newWaypoint = waypoints[Random.Range(0, waypoints.Length - 1)];
        }
        while (currentWaypoint == newWaypoint);

        return newWaypoint;
    }

    bool isTargetVisible()
    {
        return !playerHealth.IsDead() && (CanSeePlayer() || IsNearbyPlayer());
    }

    bool CanSeePlayer()
    {
        Vector3 direction = mPlayer.transform.position - mAgent.transform.position;
        float angle = Vector3.Angle(direction, mAgent.transform.forward);
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

    bool IsNearbyPlayer()
    {
        Collider[] colliders = Physics.OverlapSphere(mAgent.transform.position, sphereRadius);

        foreach (Collider c in colliders)
        {
            if (c.tag == "Player")
            {
                return true;
            }
        }
        return false;
    }

    public override void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(mAgent.transform.position, sphereRadius);
        Gizmos.DrawRay(mAgent.transform.position, mAgent.transform.forward * viewRange);
    }


}
