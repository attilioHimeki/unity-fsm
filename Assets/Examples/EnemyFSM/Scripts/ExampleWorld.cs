using UnityEngine;

// A rather simple implementation of a World context to showcase the state machine usage.
public class ExampleWorld : MonoBehaviour
{
    public static ExampleWorld instance;
    public Transform[] waypoints;
    public PlayerController player;

    void Awake()
    {
        instance = this;
    }

    public Transform[] getWaypoints()
    {
        return waypoints;
    }

    public PlayerController getPlayerAgent()
    {
        return player;
    }

}
