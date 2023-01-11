
using Object = BluminEngine9.Objects.Object;
using System.Drawing;
using System.Drawing.Imaging;
using BluminEngine9.Utilities.Mathmatics.Vectors;
using BluminEngine9.Objects;
using OpenTK.Graphics.OpenGL;
using BluminEngine9.Utilities.AssetsManegment.Types;

namespace BluminEngine9.Utilities.AssetsManegment.Types
{
    public class Image : LoadedFileTypes
    {
        public Vector2 Size { get; private set; }
        public int TextureId { get; private set; }

        public Image(FileData file) : base(file)
        {
            Bitmap bitmap = new(file.Fullpath);

            int tex = GL.GenTexture();

            BitmapData bd = bitmap.LockBits(new Rectangle(0, 0, bitmap.Width, bitmap.Height),
                ImageLockMode.ReadOnly, System.Drawing.Imaging.PixelFormat.Format32bppArgb);

            Size = new Vector2(bd.Width, bd.Height);

            GL.TexImage2D(TextureTarget.Texture2D, 0, PixelInternalFormat.Rgba, (int)Size.x, (int)Size.y, 0, OpenTK.Graphics.OpenGL.PixelFormat.Rgba, PixelType.Bitmap, bd.Scan0);
            
            bitmap.UnlockBits(bd);
            TextureId = tex;
        }
    }
}
