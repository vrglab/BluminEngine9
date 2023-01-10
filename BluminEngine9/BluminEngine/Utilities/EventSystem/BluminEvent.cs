using BluminEngine9.BluminEngine.Objects;
using Object = BluminEngine9.BluminEngine.Objects.Object;

namespace BluminEngine9.BluminEngine.Utilities.EventSystem
{
    public class BluminEvent
    {
        public delegate void RunEvent();
        private Dictionary<Guid, RunEvent> Listeners = new Dictionary<Guid, RunEvent>();

        public void Invoke()
        {
            foreach (var listener in Listeners)
            {
                listener.Value();
            }
        }

        public void addListner(Object obj, RunEvent listener)
        {
            if (!Listeners.ContainsKey(obj.GetInstanceId()))
            {
                Listeners.Add(obj.GetInstanceId(), listener);
            }
        }

        public void removeListner(Object obj)
        {
            if (Listeners.ContainsKey(obj.GetInstanceId()))
            {
                Listeners.Remove(obj.GetInstanceId());
            }
        }
    }

    public class BluminGlobalEvent
    {
        public delegate void RunEvent();
        private List<RunEvent> Listeners = new List<RunEvent>();

        public void Invoke()
        {
            foreach (var listener in Listeners)
            {
                listener();
            }
        }

        public void addListner(RunEvent listener)
        {
           Listeners.Add(listener);
        }
    }


    public class BluminEvent<t>
    {
        public delegate void RunEvent(t arg);
        private Dictionary<Guid, RunEvent> Listeners = new Dictionary<Guid, RunEvent>();

        public void Invoke(t arg)
        {
            foreach (var listener in Listeners)
            {
                listener.Value(arg);
            }
        }

        public void addListner(Object obj, RunEvent listener)
        {
            if (!Listeners.ContainsKey(obj.GetInstanceId()))
            {
                Listeners.Add(obj.GetInstanceId(), listener);
            }
        }

        public void removeListner(Object obj)
        {
            if (Listeners.ContainsKey(obj.GetInstanceId()))
            {
                Listeners.Remove(obj.GetInstanceId());
            }
        }
    }
}
