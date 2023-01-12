using BluminEngine9.Objects;
using BluminEngine9.Rendering;
using BluminEngine9.Utilities.Mathmatics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BluminEngine9.BluminEngine.Objects.Componants
{
    public class Camera : Componant
    {
        public Matrix OrthoMatrix { get; private set; }
        public Matrix ViewMatrix { get; private set; }
        public Matrix ProjectionMatrix { get; private set; }
        public float FOV { get; set; } = 90f;
        public float FarPlane { get; set; } = 1000f;
        public float NearPlane { get; set; } = 0.5f;

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
            ProjectionMatrix = Matrix.projection(FOV,(Application.display.currentResolution.Width/ Application.display.currentResolution.Heigth) + 0.7f,FarPlane, NearPlane);
            OrthoMatrix = Matrix.OrthoMatrix(-1, 1, -1, 1, FarPlane, NearPlane);
            ViewMatrix = Matrix.view(gameobject.transform.Position, gameobject.transform.Rotation);
        }
    }
}
