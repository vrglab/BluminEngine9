using BluminEngine9.Rendering.Shading;
using BluminEngine9.Utilities.AssetsManegment.Types;
using BluminEngine9.Utilities.Buffering;
using BluminEngine9.Utilities.Debuging;
using BluminEngine9.Utilities.Mathmatics;
using BluminEngine9.Utilities.Mathmatics.Vectors;
using OpenTK.Graphics.OpenGL;

namespace BluminEngine9.Objects.Componants
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
            GL.EnableVertexAttribArray(0);
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
                new Vector2(-1f, -1f),
                new Vector2(1f, 1f),
                new Vector2(1f, -1f)
            };

            float[] positionData = new float[arrayData.Length * 2];

            for (int i = 0; i < arrayData.Length; ++i)
            {
                positionData[i * 2] = arrayData[i].x;
                positionData[i * 2 + 1] = arrayData[i].y;
            }

            ObjectBuffer = new VertexArrayBuffer<float>(positionData, BufferTarget.ArrayBuffer, BufferUsageHint.StreamDraw);

            Debug.LogBuffer("Vertex array", ObjectBuffer);
        }

        public override void Update()
        {
        }
    }
}
