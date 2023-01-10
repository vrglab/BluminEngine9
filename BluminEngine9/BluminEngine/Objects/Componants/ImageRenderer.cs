using BluminEngine9.BluminEngine.Rendering.Shading;
using BluminEngine9.BluminEngine.Utilities.AssetsManegment.Types;
using BluminEngine9.BluminEngine.Utilities.Buffering;
using BluminEngine9.BluminEngine.Utilities.Debuging;
using BluminEngine9.BluminEngine.Utilities.Mathmatics.Vectors;
using OpenTK.Graphics.OpenGL;

namespace BluminEngine9.BluminEngine.Objects.Componants
{
    public unsafe class ImageRenderer : Componant
    {
        public Image image { get; set; }
        public IShader shader { get; set; }

        VertexArrayBuffer<float> ObjectBuffer { get; set; }

        public override void Awake()
        {
          
        }

        public override void OnDestroy()
        {
            ObjectBuffer.Dispose();
        }

        public override void OnRender()
        {
            shader.Run();
            GL.Enable(EnableCap.Texture2D);
            GL.BindTexture(TextureTarget.Texture2D, image.TextureId);
            GL.DrawArrays(PrimitiveType.TriangleStrip, 0, ObjectBuffer.Length);
            GL.BindTexture(TextureTarget.Texture2D, 0);
            GL.Disable(EnableCap.Texture2D);
            shader.Stop();
        }

        public override void Start()
        {
            var arrayData = new Vector2[] {
                new Vector2(-1f, 1f),
                new Vector2(-1f, -1f) ,
                new Vector2(1f, 1f) ,
                new Vector2(1f, -1f) };

            float[] positionData = new float[arrayData.Length * 3];

            for (int i = 0; i < arrayData.Length; ++i)
            {
                positionData[i * 3] = arrayData[i].x;
                positionData[i * 3 + 1] = arrayData[i].y;
            }

            ObjectBuffer = new VertexArrayBuffer<float>(positionData, BufferTarget.ArrayBuffer, BufferUsageHint.StreamDraw);
        }

        public override void Update()
        {
        }
    }
}
