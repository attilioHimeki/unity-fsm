using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace AIEngine
{

    /**
     * Extend this class to define your own state machine.
     *
     * */
    public abstract class FSM : MonoBehaviour
    {

        public Renderer meshRenderer;
        public StateId initialState;
        public bool updateOnlyWhenVisible = true;

        protected FSMState currentState;

        void Start()
        {
            InitFSM();
        }

        void Update()
        {
            UpdateFSM();
        }

        public void MoveToState(StateId state)
        {
            currentState.OnStateExit();

            currentState = CreateState(state);

            currentState.OnStateEnter();
        }

        protected abstract FSMState CreateState(StateId state);

        public virtual void StopFSM()
        {
            currentState = null;
        }

        public virtual void InitFSM()
        {
            currentState = CreateState(EnemyStateId.Patrol);
        }

        public virtual void UpdateFSM()
        {
            if (currentState != null)
            {
                if (updateOnlyWhenVisible)
                {
                    if (meshRenderer.isVisible)
                    {
                        currentState.Update();
                    }
                }
                else
                {
                    currentState.Update();
                }
            }
        }
    }

}