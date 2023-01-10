using BluminEngine9.BluminEngine.Rendering.Shading;
using BluminEngine9.BluminEngine.Utilities.AssetsManegment.Types;
using BluminEngine9.BluminEngine.Utilities.Buffering;
using BluminEngine9.BluminEngine.Utilities.Mathmatics;
using BluminEngine9.BluminEngine.Utilities.Mathmatics.Vectors;
using OpenTK.Graphics.OpenGL;

namespace BluminEngine9.BluminEngine.Objects.Componants
{
    public unsafe class ImageRenderer : Componant
    {
        public Image image { get; set; }
        public IShader shader { get; set; }

        VertexArrayBuffer<int> ObjectBuffer { get; set; }

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

            GL.ActiveTexture(TextureUnit.Texture0);
            GL.BindTexture(TextureTarget.Texture2D, image.TextureId);
            shader.SetUniform("guiTexture", 0);

            shader.SetUniform("transformationMatrix", Matrix.transform(gameobject.transform));

            GL.BindVertexArray(ObjectBuffer.BufferID);
            GL.DrawArrays(PrimitiveType.TriangleStrip, 0, ObjectBuffer.Length);
        }

        public override void Start()
        {
            var arrayData = new Vector2[] {
                new Vector2(-1f, 1f),
                new Vector2(-1f, -1f) ,
                new Vector2(1f, 1f) ,
                new Vector2(1f, -1f) };

            int[] positionData = new int[arrayData.Length * 3];

            for (int i = 0; i < arrayData.Length; ++i)
            {
                positionData[i * 3] = (int)arrayData[i].x;
                positionData[i * 3 + 1] = (int)arrayData[i].y;
            }

            ObjectBuffer = new VertexArrayBuffer<int>(positionData, BufferTarget.ArrayBuffer, BufferUsageHint.StreamDraw);
        }

        public override void Update()
        {
        }
    }
}
