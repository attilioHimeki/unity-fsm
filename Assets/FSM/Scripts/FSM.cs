using System.Collections.Generic;

namespace Himeki.FSM
{
    public class FSM
    {
        public bool running { get; private set; }
        private Dictionary<StateId, FSMState> states;
        private StateId initialState;
        private StateId previousState;
        private FSMState currentState;

        public FSM()
        {
            states = new Dictionary<StateId, FSMState>();
        }

        public void AddState(FSMState state)
        {
            states[state.GetId()] = state;
        }

        public void SetInitialState(StateId id)
        {
            initialState = id;
        }

        public void MoveToState(StateId id)
        {
            currentState.OnStateExit();

            previousState = currentState.GetId();

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

                previousState = StateId.None;

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