using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private int maxHp;
    public int hp { get; private set; }

    void Start()
    {
        hp = maxHp;
    }
    
    public void LoseHealth(int hpLost)
    {
        hp -= hpLost;
    }
    public bool IsDead()
    {
        return hp <= 0;
    }
}
