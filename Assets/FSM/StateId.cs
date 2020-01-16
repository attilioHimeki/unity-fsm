using UnityEngine;

namespace AIEngine
{

    [System.Serializable]
    public class StateId
    {
        [SerializeField]
        private string mId;

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