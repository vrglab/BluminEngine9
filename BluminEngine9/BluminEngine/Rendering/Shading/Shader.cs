using BluminEngine9.BluminEngine.Utilities.AssetsManegment;
using BluminEngine9.BluminEngine.Utilities.Debuging;
using BluminEngine9.BluminEngine.Utilities.Mathmatics.Vectors;
using OpenTK.Graphics.OpenGL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml.Linq;
using Vector2 = BluminEngine9.BluminEngine.Utilities.Mathmatics.Vectors.Vector2;
using Vector3 = BluminEngine9.BluminEngine.Utilities.Mathmatics.Vectors.Vector3;

namespace BluminEngine9.BluminEngine.Rendering.Shading
{
    public interface IShader
    {
        int GetUniformLocation(string name);
        int GetUniformLocation(string name, string type, int arraypos);
        void SetUniform(String name, int data);
        void SetUniform(String name, float data);
        void SetUniform(String name, bool data);
        void SetUniform(String name, Vector3 data);
        void SetUniform(String name, Vector2 data);
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

        public int GetUniformLocation(string name)
        {
            throw new NotImplementedException();
        }

        public int GetUniformLocation(string name, string type, int arraypos)
        {
            throw new NotImplementedException();
        }

        public void SetUniform(string name, int data)
        {
            throw new NotImplementedException();
        }

        public void SetUniform(string name, float data)
        {
            throw new NotImplementedException();
        }

        public void SetUniform(string name, bool data)
        {
            throw new NotImplementedException();
        }

        public void SetUniform(string name, Vector3 data)
        {
            throw new NotImplementedException();
        }

        public void SetUniform(string name, Vector2 data)
        {
            throw new NotImplementedException();
        }

        public void Run()
        {
            GL.UseProgram(ProgramID);
        }

        public void Stop()
        {
            GL.UseProgram(0);
        }
    }
}
