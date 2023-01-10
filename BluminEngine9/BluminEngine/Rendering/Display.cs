
using OpenTK.Windowing.GraphicsLibraryFramework;
using System.Text.Json.Serialization;

namespace BluminEngine9.BluminEngine.Rendering
{
    public unsafe class Display
    {
        
        public Window* window { get; private set; }

        private OpenTK.Windowing.GraphicsLibraryFramework.Monitor* curentMonitor { get; set; }

        public unsafe Display(Resolution res, string name)
        {
            GLFW.DefaultWindowHints();
            window = GLFW.CreateWindow(res.Width, res.Heigth, name, curentMonitor, window);
            GLFW.MakeContextCurrent(window);
            GLFW.FocusWindow(window);
        }
    }

    public struct Resolution
    {
        public int Width { get; private set; }
        public int Heigth { get; private set; }

        public Resolution(int width, int heigth)
        {
            Width = width;
            Heigth = heigth;
        }
    }
}
