using AIEngine;

public class EnemyStateId : StateId
{
    public static readonly EnemyStateId Patrol = new EnemyStateId("EnemyPatrol");
    public static readonly EnemyStateId Chase = new EnemyStateId("EnemyChase");
    public static readonly EnemyStateId Attack = new EnemyStateId("EnemyAttack");
    public static readonly EnemyStateId Flee = new EnemyStateId("EnemyFlee");

    public EnemyStateId(string id)
        : base(id)
    {
    }

}