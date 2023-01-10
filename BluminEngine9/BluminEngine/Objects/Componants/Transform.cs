using BluminEngine9.BluminEngine.Utilities.Debuging;
using BluminEngine9.BluminEngine.Utilities.Mathmatics.Vectors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BluminEngine9.BluminEngine.Objects.Componants
{
    public class Transform : Componant
    {
        public Vector3 Position { get; private set; }
        public Vector3 Rotation { get; private set; }
        public Vector3 Scale { get; private set; }

        public override void Awake()
        {
        }

        public override void OnDestroy()
        {
        }

        public override void OnRender()
        {
        }

        public override void Start()
        {
        }

        public override void Update()
        {
        }
    }
}
