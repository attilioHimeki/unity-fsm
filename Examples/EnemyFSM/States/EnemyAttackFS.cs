using UnityEngine;
using System.Collections;
using AIEngine;

public class EnemyAttackFS : FSMState {

    private Animation mEnemyAnimation;
    private EnemyHealthController mEnemyHealth;

    private PlayerHealthController mPlayerHealth;
    private Animation mPlayerAnimation;

    private float mAttackTimeCounter;

    private float maxDistanceFromTarget = 4.0f;

    public EnemyAttackFS(FSM fsm, GameObject enemy, GameObject player)
        : base(fsm, enemy, player)
    {
        mEnemyAnimation = enemy.GetComponent<Animation>();

        mPlayerHealth = mPlayer.GetComponent<PlayerHealthController>();
        mPlayerAnimation = mPlayer.GetComponent<Animation>();

        mEnemyHealth = enemy.GetComponent<EnemyHealthController>();
    }

    override public void OnStateEnter()
    {
        Attack();
        mAttackTimeCounter = 0.0f;
    }

    override public void OnStateExit()
    {
        mEnemyAnimation.Play("idle");
    }

    override public void Update()
    {

        float distance = Vector3.Distance(mPlayer.transform.position, mAgent.transform.position);

        if (distance > maxDistanceFromTarget)
        {
            mFSM.MoveToState(EnemyStateId.Chase);
        }
        else if(mPlayerHealth.IsDead())
        {
            mFSM.MoveToState(EnemyStateId.Patrol);
        }
        else
        {
            Vector3 point = mPlayer.transform.position;
            point.y = mAgent.transform.position.y;
            mAgent.transform.LookAt(point);

            mAttackTimeCounter += Time.deltaTime;

            if (mAttackTimeCounter > 2.0f)
            {
                Attack();
                mAttackTimeCounter = 0.0f;
            }
        }
        
    }

    private void Attack()
    {

        mPlayerAnimation.CrossFade("damage");
        mPlayerAnimation.CrossFadeQueued("idle");

        mPlayerHealth.LoseHealth(10);

        mEnemyAnimation.CrossFade("attack_1");
        mEnemyAnimation.CrossFadeQueued("idle");
    }

    public override void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(mAgent.transform.position, maxDistanceFromTarget);
    }

}
