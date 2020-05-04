using UnityEngine;

namespace Himeki.FSM
{
    public class FSMAgent : MonoBehaviour
    {
        public bool updateOnlyWhenVisible = false;
        protected FSM stateMachine;
        private Renderer agentView;

        public void Awake()
        {
            agentView = GetComponent<Renderer>();

            InitialiseFSM();
        }

        public void Start()
        {
            stateMachine.StartFSM();
        }

        protected virtual void InitialiseFSM()
        {
            stateMachine = new FSM();
        }

        void Update()
        {
            if(stateMachine != null && stateMachine.running)
            {
                if (!updateOnlyWhenVisible || agentView.isVisible)
                {
                    stateMachine.UpdateFSM();
                }
            }
        }
    }
}