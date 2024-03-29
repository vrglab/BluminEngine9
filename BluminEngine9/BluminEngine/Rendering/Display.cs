﻿
using Google.Api;
using OpenTK.Core.Exceptions;
using OpenTK.Graphics.OpenGL;
using OpenTK.Windowing.GraphicsLibraryFramework;
using System.Text.Json.Serialization;


namespace BluminEngine9.Rendering
{
    public unsafe class Display
    {
        
        public Window* window { get; private set; }
        public WindowStyle Style { get; private set; }
        public Resolution currentResolution { get; set; }

        private OpenTK.Windowing.GraphicsLibraryFramework.Monitor* curentMonitor { get; set; }

        public unsafe Display(Resolution res, string name, WindowStyle style)
        {
            Style = style;

            switch (style)
            {
                case WindowStyle.Normal:
                        GLFW.DefaultWindowHints();
                    break;
                case WindowStyle.Locked:
                        GLFW.WindowHint(WindowHintBool.Resizable, false);
                    break;
                case WindowStyle.Fullscreen:
                        GLFW.WindowHint(WindowHintBool.Resizable, false);
                        GLFW.WindowHint(WindowHintBool.Maximized, true);
                        GLFW.WindowHint(WindowHintBool.Decorated, false);
                    break;
            }
            
            window = GLFW.CreateWindow(res.Width, res.Heigth, name, curentMonitor, window);
            Application.init(window);
            SetWindowResolution(res);
            GLFW.MakeContextCurrent(window);
            GLFW.FocusWindow(window);
        }

        public void SetWindowResolution(Resolution res)
        {
            currentResolution = res;
            GLFW.SetWindowSize(window, res.Width, res.Heigth);
            GL.Viewport(0, 0, res.Width, res.Heigth);
        }
    }

    public struct Resolution
    {
        public int Width { get; private set; }
        public int Heigth { get; private set; }

        public static Resolution DefaultDisplayResolution { get => new Resolution(1920,1080 );}

        public Resolution(int width, int heigth)
        {
            Width = width;
            Heigth = heigth;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public override string? ToString()
        {
            return Width.ToString() + ":" + Heigth.ToString();
        }
    }

    public enum WindowStyle
    {
        Normal,
        Locked,
        Fullscreen
    }
}
