using UnityEngine;

namespace Himeki.FSM
{

    [System.Serializable]
    public class StateId
    {
        public static readonly StateId None = new StateId("None");
        [SerializeField] private string mId;

        public StateId(string id)
        {
            mId = id;
        }

        public string GetId()
        {
            return mId;
        }

    }
}