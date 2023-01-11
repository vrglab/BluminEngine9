using BluminEngine9.Utilities.Debuging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using OpenTK.Graphics.OpenGL;

namespace BluminEngine9.Utilities.Buffering
{
    public unsafe class ObjectBuffer<t> : Buffer where t : unmanaged
    {


        public ObjectBuffer(t[] data, BufferTarget Target, BufferUsageHint usage)
        {
            target = Target;
            BufferID = GL.GenBuffer();
            Length = data.Length;   
            GL.BindBuffer(target, BufferID);
            GL.BufferData<t>(Target, data.Length * sizeof(t), data, usage);
            GL.BindBuffer(target, 0);

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
            GL.BindVertexArray(BufferID);
            GL.BindBuffer(Target, BufferID);
            GL.BufferData(Target, data.Length * sizeof(t), data, usage);
            GL.VertexAttribIPointer(0, 2, VertexAttribIntegerType.UnsignedInt, 0, IntPtr.Zero);
            GL.BindBuffer(Target, 0);
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
