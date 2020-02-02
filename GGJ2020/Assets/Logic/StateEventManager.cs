namespace Logic
{
    public class StateEventManager
    {
        public delegate void OnStatePause();

        public static event OnStatePause StatePauseEvent;

        public delegate void OnStatePlay();

        public static event OnStatePlay StatePlayEvent;

        public static void SendStatePause()
        {
            StatePauseEvent?.Invoke();
        }

        public static void SendStatePlay()
        {
            StatePlayEvent?.Invoke();
        }
    }
}