using UnityEngine;

public class HealthController : MonoBehaviour
{
    public int hp { get; private set; }
    
    public void LoseHealth(int hpLost)
    {
        hp -= hpLost;
    }
    public bool IsDead()
    {
        return hp <= 0;
    }
}
