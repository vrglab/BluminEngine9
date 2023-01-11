using BluminEngine9.Utilities.EventSystem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BluminEngine9.Objects
{
    public abstract class Componant : Object, IEngineActor
    {
        public GameObject? gameobject { get; init; }

        public abstract void Awake();
        public abstract void Start();
        public abstract void Update();
        public abstract void OnRender();
        public abstract void OnDestroy();
    }
}
