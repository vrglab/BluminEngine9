using BluminEngine9.Utilities.AssetsManegment;
using BluminEngine9.Utilities.Debuging;
using BluminEngine9.Utilities.Mathmatics;
using BluminEngine9.Utilities.Mathmatics.Vectors;
using OpenTK.Graphics.OpenGL;
using OpenTK.Mathematics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml.Linq;
using Vector2 = BluminEngine9.Utilities.Mathmatics.Vectors.Vector2;
using Vector3 = BluminEngine9.Utilities.Mathmatics.Vectors.Vector3;

namespace BluminEngine9.Rendering.Shading
{
    public interface IShader : IDisposable
    {
        int GetUniformLocation(string name);
        int GetUniformLocation(string name, string type, int arraypos);
        void SetUniform(string name, int data);
        void SetUniform(string name, float data);
        void SetUniform(string name, bool data);
        void SetUniform(string name, Vector3 data);
        void SetUniform(string name, Vector2 data);
        void SetUniform(string name, Matrix data);
        void Run();
        void Stop();
    }

    public class Shader : IShader
    {

        private int VertexShader, Fragmentshader;

        public int ProgramID { get; private set; } 

        public Shader(FileData fd)
        {
            string file = Encoding.UTF8.GetString(fd.Bytes);
            string[] f = file.Split(new string[] { "@Vertex@", "@Fragment@", "@Metadata@", "@name@:" }, StringSplitOptions.RemoveEmptyEntries);

            if(f.Length <= 1)
            {
                Debug.LogError("Shader failed to load");
            }
            else
            {
                string vertex = f[0];
                string fragment = f[1];

                ProgramID = GL.CreateProgram();
                VertexShader = GL.CreateShader(ShaderType.VertexShader);
                setShaderData(vertex, VertexShader);
                GL.AttachShader(ProgramID, VertexShader);

                Fragmentshader = GL.CreateShader(ShaderType.FragmentShader);
                setShaderData(fragment, Fragmentshader);
                GL.AttachShader(ProgramID, Fragmentshader);
                GL.LinkProgram(ProgramID);
            }
        }

        private string Source(int shader, String data)
        {
            GL.ShaderSource(shader, data);
            GL.CompileShader(shader);
            return data;
        }

        private string Compile(int shader, string data)
        {
            bool Compiled = GL.GetShaderInfoLog(shader) != string.Empty;
            int index = 0;
            var tempog = data;
            var Fulldata = tempog;
            bool FullyFailed = false;

            while (!Compiled && FullyFailed == false)
            {
                switch (index)
                {
                    case 0:
                        Fulldata = "#version 110 core \n" + tempog;
                        break;
                    case 1:
                        Fulldata = "#version 120 core \n" + tempog;
                        break;
                    case 2:
                        Fulldata = "#version 130 core \n" + tempog;
                        break;
                    case 3:
                        Fulldata = "#version 140 core \n" + tempog;
                        break;
                    case 4:
                        Fulldata = "#version 150 core \n" + tempog;
                        break;
                    case 5:
                        Fulldata = "#version 330 core \n" + tempog;
                        break;
                    case 6:
                        Fulldata = "#version 400 core \n" + tempog;
                        break;
                    case 7:
                        Fulldata = "#version 410 core \n" + tempog;
                        break;
                    case 8:
                        Fulldata = "#version 420 core \n" + tempog;
                        break;
                    case 9:
                        Fulldata = "#version 430 core \n" + tempog;
                        break;
                    case 10:
                        Fulldata = "#version 440 core \n" + tempog;
                        break;
                    case 11:
                        Fulldata = "#version 450 core \n" + tempog;
                        break;
                    case 12:
                        Fulldata = "#version 460 core \n" + tempog;
                        break;
                    case 13:
                        Fulldata = tempog;
                        break;

                }
                Source(shader, Fulldata);
                if (GL.GetShaderInfoLog(shader) != string.Empty)
                {
                    Compiled = true;
                }
                else
                {
                    if (index >= 13)
                    {
                        FullyFailed = true;
                    }
                }
                index++;
            }
            if (FullyFailed)
            {
                Debug.LogError(GL.GetShaderInfoLog(shader));
                return "null";
            }
            return Fulldata;
        }

        public string setShaderData(string data, int id)
        {
            Source(id, data);
            return Compile(id, data);
        }

        public void Run()
        {
            GL.UseProgram(ProgramID);
        }

        public void Stop()
        {
            GL.UseProgram(0);
        }

        public int GetUniformLocation(string name)
        {
            return GL.GetUniformLocation(ProgramID, name);
        }

        public int GetUniformLocation(string name, string type, int arraypos)
        {
            string n = name + "[" + arraypos + "]" + "." + type;
            return GetUniformLocation(n);
        }

        public void SetUniform(string name, int data)
        {

            GL.Uniform1(GetUniformLocation(name), data);
        }

        public void SetUniform(string name, float data)
        {
            GL.Uniform1(GetUniformLocation(name), data);
        }

        public void SetUniform(string name, bool data)
        {
            GL.Uniform1(GetUniformLocation(name), data ? 0 : 1);
        }

        public void SetUniform(string name, Vector3 data)
        {
            GL.Uniform3(GetUniformLocation(name), data.x, data.y, data.z);
        }

        public void SetUniform(string name, Vector2 data)
        {
            GL.Uniform2(GetUniformLocation(name), data.x, data.y);
        }

        public void SetUniform(string name, Matrix data)
        {
            Matrix4 mat = data;
            GL.UniformMatrix4(GetUniformLocation(name), true, ref mat);
        }

        public void Dispose()
        {
            GL.DeleteShader(VertexShader);
            GL.DeleteShader(Fragmentshader);
            GL.DeleteProgram(ProgramID);
        }
    }
}
