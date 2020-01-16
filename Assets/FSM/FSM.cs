using System.Collections.Generic;

namespace AIEngine
{
    public class FSM
    {
        public bool running { get; private set; }
        private Dictionary<StateId, FSMState> states;
        private StateId initialState;
        private FSMState currentState;

        public FSM()
        {
            states = new Dictionary<StateId, FSMState>();
        }

        public void AddState(StateId id, FSMState state)
        {
            states[id] = state;
        }

        public void SetInitialState(StateId id)
        {
            initialState = id;
        }

        public void MoveToState(StateId id)
        {
            currentState.OnStateExit();

            states.TryGetValue(id, out currentState);

            if (currentState != null)
            {
                currentState.OnStateEnter();
            }
            else
            {
                // show error
            }
        }

        public void StartFSM()
        {
            if (initialState != null && states.Count > 0)
            {
                states.TryGetValue(initialState, out currentState);

                if (currentState != null)
                {
                    currentState.OnStateEnter();
                }

                running = true;
            }
            else
            {
                // Show error
            }
        }

        public void StopFSM()
        {
            currentState = null;
            running = false;
        }

        public virtual void UpdateFSM()
        {
            if (currentState != null)
            {
                currentState.OnStateUpdate();
            }
            else
            {
                // show error
            }
        }
    }

}