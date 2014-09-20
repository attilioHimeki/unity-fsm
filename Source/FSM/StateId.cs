namespace AIEngine
{

    /**
     * Extend this class to define different states for your own state machine.
     * */
    public class StateId
    {

        private readonly string mId;

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