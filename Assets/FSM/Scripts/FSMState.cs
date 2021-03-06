﻿using UnityEngine;

namespace Himeki.FSM
{
    public abstract class FSMState
    {

        protected FSMState(FSM fsm, GameObject owner)
        {
            stateMachine = fsm;
            ownerAgent = owner;
        }

        abstract public StateId GetId();
        abstract public void OnStateEnter();
        abstract public void OnStateExit();
        abstract public void OnStateUpdate();

        protected GameObject ownerAgent;
        protected FSM stateMachine;
    }
}