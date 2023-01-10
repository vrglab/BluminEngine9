using BluminEngine9.BluminEngine.Utilities.Debuging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using OpenTK.Graphics.OpenGL;

namespace BluminEngine9.BluminEngine.Utilities.Buffering
{
    public unsafe class ObjectBuffer<t> : Buffer where t : unmanaged
    {


        public ObjectBuffer(t[] data, BufferTarget Target, BufferUsageHint usage)
        {
            target = Target;
            BufferID = GL.GenBuffer();
            Length = data.Length;   
            GL.BindBuffer(target, BufferID);
            GL.BufferData<t>(Target, data.Length, data, usage);
            
        }

        public override void Dispose()
        {
            GL.DeleteBuffer(BufferID);
            GC.SuppressFinalize(this);
        }
    }

    public unsafe class VertexArrayBuffer<t> : Buffer where t : unmanaged
    {

        public VertexArrayBuffer(t[] data, BufferTarget Target, BufferUsageHint usage)
        {
            BufferID = GL.GenVertexArray();
            Length = data.Length;
            GL.BindBuffer(BufferTarget.ElementArrayBuffer, BufferID);
            GL.BufferData(Target, data.Length, data, usage);
        }

        public override void Dispose()
        {
            GL.DeleteBuffer(BufferID);
            GC.SuppressFinalize(this);
        }
    }

    public unsafe abstract class Buffer : IDisposable
    {
        public int BufferID { get; protected set; }
        public int Length { get; protected set; }

        protected BufferTarget target;

        public abstract void Dispose();
    }

}
