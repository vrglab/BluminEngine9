using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BluminEngine9.Utilities.EventSystem
{
    internal interface IEngineActor
    {
         void Awake();
         void Start();
         void Update();
    }
}
