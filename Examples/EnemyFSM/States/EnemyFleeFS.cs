using UnityEngine;
using System.Collections;
using AIEngine;

public class EnemyFleeFS : FSMState
{

    private NavMeshAgent mEnemyNavAgent;
    private Animation mEnemyAnimation;


    private float viewFieldRadius = 90.0f;
    private float viewRange = 7.0f;
    private float sphereRadius = 4.0f;

    public EnemyFleeFS(FSM fsm, GameObject enemy, GameObject player)
        : base(fsm, enemy, player)
    {
        mEnemyNavAgent = enemy.GetComponent<NavMeshAgent>();
        mEnemyAnimation = enemy.GetComponent<Animation>();
    }

    override public void OnStateEnter()
    {
        mEnemyNavAgent.SetDestination(mPlayer.transform.position);
        mEnemyAnimation.Play("walk");
    }

    override public void OnStateExit()
    {
        mEnemyNavAgent.Stop();
        mEnemyNavAgent.ResetPath();
        mEnemyAnimation.Play("idle");
    }

    override public void Update()
    {
        float distance = Vector3.Distance(mPlayer.transform.position, mAgent.transform.position);

        if (distance < 3.0f)
        {
            mFSM.MoveToState(EnemyStateId.Attack);
        }
        else if (mEnemyNavAgent.remainingDistance < 2.0f)
        {
            if (distance < 10.0f)
            {
                mEnemyNavAgent.SetDestination(mAgent.transform.position);
            }
            else
            {
                mFSM.MoveToState(EnemyStateId.Patrol);
            }
        }
    }

    public override void OnDrawGizmos()
    {
        Gizmos.DrawLine(mAgent.transform.position, mAgent.transform.forward);
    }

}
