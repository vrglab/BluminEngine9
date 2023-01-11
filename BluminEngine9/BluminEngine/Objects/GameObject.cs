using BluminEngine9.Objects.Componants;
using BluminEngine9.Utilities.EventSystem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BluminEngine9.Objects
{
    public abstract class GameObject : Object, IEngineActor
    {
        public string Name { get; set; } = string.Empty;
        public Transform transform { get; private set; }

        private Dictionary<Guid, Componant> RegisteredComponants = new Dictionary<Guid, Componant>();

        internal BluminEvent UpdateEvent { get;  } = new BluminEvent();
        internal BluminEvent StartEvent { get; } = new BluminEvent();
        internal BluminEvent AwakeEvent { get; } = new BluminEvent();
        internal BluminEvent OnRenderEvent { get; } = new BluminEvent();
        internal BluminEvent OnDestroyEvent { get; } = new BluminEvent();

        protected GameObject() : base()
        {
            transform = AddComponant<Transform>();
            UpdateEvent.addListner(this, () =>
            {
                foreach (var item in RegisteredComponants)
                {
                    item.Value.Update();
                }
                Update();
            });
            StartEvent.addListner(this, () =>
            {
                foreach (var item in RegisteredComponants)
                {
                    item.Value.Start();
                }
                Start();
            });
            AwakeEvent.addListner(this, () =>
            {
                foreach (var item in RegisteredComponants)
                {
                    item.Value.Awake();
                }
                Awake();
            });
            OnRenderEvent.addListner(this, () =>
            {
                foreach (var item in RegisteredComponants)
                {
                    item.Value.OnRender();
                }
            });
            OnDestroyEvent.addListner(this, () =>
            {
                foreach (var item in RegisteredComponants)
                {
                    item.Value.OnDestroy();
                }
            });
        }

        public t AddComponant<t>() where t : Componant, new()
        {
            var Refrenace = new t
            {
                gameobject = this
            };
            Refrenace.Awake();
            RegisteredComponants.Add(Refrenace.GetInstanceId(), Refrenace);
            Refrenace.Start();
            return Refrenace;
        }

        public t AddComponant<t>(t Refrenace) where t : Componant, new()
        {
            Refrenace.Awake();
            RegisteredComponants.Add(Refrenace.GetInstanceId(), Refrenace);
            Refrenace.Start();
            return Refrenace;
        }

        public t? GetComponant<t>() where t : Componant
        {
            foreach (var item in RegisteredComponants)
            {
                if(item is t)
                {
                    return item as t;
                }
            }
            return default;
        }

        public t[] GetComponants<t>() where t : Componant
        {
            List<t> list = new List<t>();
            foreach (var item in RegisteredComponants)
            {
                if (item is t)
                {
                    list.Add(item as t);
                }
            }
            return list.ToArray();
        }

        public void RemoveComponant<t>() where t : Componant
        {
            Guid removed = Guid.Empty;
            foreach (var item in RegisteredComponants)
            {
                if(item is t)
                {
                    removed = item.Key;
                    item.Value.OnDestroy();
                }
            }
            RegisteredComponants.Remove(removed);
        }

        public abstract void Awake();
        public abstract void Start();
        public abstract void Update();
    }
}
