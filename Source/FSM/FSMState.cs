using UnityEngine;
using System.Collections;

namespace AIEngine
{
    public abstract class FSMState
    {

        protected FSMState(FSM fsm, GameObject agent, GameObject player)
        {
            mAgent = agent;
            mFSM = fsm;
            mPlayer = player;
        }

        abstract public void OnStateEnter();
        abstract public void OnStateExit();
        abstract public void Update();
        abstract public void OnDrawGizmos();

        protected GameObject mAgent;
        protected FSM mFSM;
        protected GameObject mPlayer;
    }
}