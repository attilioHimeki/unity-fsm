using UnityEngine;

namespace AIEngine
{

    /**
     * Extend this class to define your own state machine.
     * */
    public abstract class FSM : MonoBehaviour
    {

        public Renderer agentView;
        public StateId initialState;
        public bool updateOnlyWhenVisible = true;

        public bool initialised { get; private set; }

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
            currentState = CreateState(initialState);
        }

        public virtual void UpdateFSM()
        {
            if (currentState != null)
            {
                if (!updateOnlyWhenVisible || agentView.isVisible)
                {
                    currentState.OnStateUpdate();
                }
            }
        }
    }

}