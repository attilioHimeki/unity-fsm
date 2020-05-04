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
            if (id != currentState.GetId())
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
                    UnityEngine.Debug.LogErrorFormat("Trying to move to State {0} but cannot be found", id);
                }
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

                    previousState = StateId.None;

                    running = true;
                }
                else
                {
                    UnityEngine.Debug.LogError("Initial state not found, FSM cannot be started");
                }
            }
            else
            {
                UnityEngine.Debug.LogError("No states defined, FSM cannot be started");
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
                UnityEngine.Debug.LogError("No current state, FSM cannot be updated");
            }
        }
    }

}