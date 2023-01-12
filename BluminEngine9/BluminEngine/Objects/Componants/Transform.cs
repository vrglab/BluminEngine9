using BluminEngine9.Utilities.Mathmatics.Vectors;

namespace BluminEngine9.Objects.Componants
{
    public class Transform : Componant
    {
        public Vector3 Position { get; set; }
        public Vector3 Rotation { get; set; }
        public Vector3 Scale { get; set; }

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
