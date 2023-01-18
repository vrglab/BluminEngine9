using BluminEngine9.Rendering;
using BluminEngine9.SceneMannagment;
using BluminEngine9.Utilities;
using BluminEngine9.Utilities.AssetsManegment;
using BluminEngine9.Utilities.Debuging;
using BluminEngine9.Utilities.EventSystem;
using OpenTK;
using OpenTK.Graphics.OpenGL;
using OpenTK.Windowing.Desktop;
using OpenTK.Windowing.GraphicsLibraryFramework;
using System.Reflection;

namespace BluminEngine9
{
    public class Application
    {
        static RenderMannager rm = new RenderMannager();

        public static BluminGlobalEvent OnCloseEventCallback { get; } = new BluminGlobalEvent();
        public static Display display { get; private set; }

        public static string AppName { get; private set; }
        public static SceneMannager sceneMannager { get; private set; }

        public unsafe static void StartEngine(Resolution res, string name, WindowStyle style, Scene srtartingScene)
        {
            OnCloseEventCallback.addListner(() =>
            {
                Debug.OnClose();
            });
            AppName = name;
            GLFW.Init();
            try
            {
                if (!Directory.Exists(AppInfo.AssetsPath))
                {
                    Directory.CreateDirectory(AppInfo.AssetsPath);
                }

                Debug.Log("Loading Assets");
                ResourceMannager.LoadAllDataFromAssetsPath();
                Debug.Log("Deleting duplicate assets");
                ResourceMannager.DeleteDuplicates();

                display = new Display(res, name, style);

                init(display.window);

                //set the viewport
                GL.Viewport(0, 0, display.currentResolution.Width, display.currentResolution.Heigth);

                //set the viewport callback
                GLFW.SetWindowSizeCallback(display.window, (Window* win, int h,int w) =>
                {
                    display.currentResolution = new Resolution(w, h);
                    GL.Viewport(0, 0, display.currentResolution.Width, display.currentResolution.Heigth);
                });

                Debug.Log("Batching Assets");
                ResourceMannager.BatchLoadedFiles();

                sceneMannager = new SceneMannager(srtartingScene);

                GC.Collect();
                GL.ClearColor(System.Drawing.Color.DarkCyan);
                while (!GLFW.WindowShouldClose(display.window))
                {
                    GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);
                    sceneMannager.CurentScene.UpdateEventCallback.Invoke();
                    rm.Render();
                    GLFW.SwapBuffers(display.window);
                    GLFW.PollEvents();
                }
                OnCloseEventCallback.Invoke();
                GC.Collect();
            }
            catch (Exception e)
            {
                GC.Collect();
                Debug.LogException(e);
                OnCloseEventCallback.Invoke();
            }
        }


        public static void InitializeGlBindings()
        {
            Assembly assembly;
            try
            {
                assembly = Assembly.Load("OpenTK.Graphics");
            }
            catch (Exception e)
            {
                Debug.LogException(e);
                return;
            }

            var provider = new GLFWBindingsContext();

            void LoadBindings(string typeNamespace)
            {
                var type = assembly.GetType($"OpenTK.Graphics.{typeNamespace}.GL");
                if (type == null)
                {
                    return;
                }

                var load = type.GetMethod("LoadBindings");
                load?.Invoke(null, new object[] { provider });
            }

            LoadBindings("ES11");
            LoadBindings("ES20");
            LoadBindings("ES30");
            LoadBindings("OpenGL");
            LoadBindings("OpenGL4");
        }

        public unsafe static void init(Window* window)
        {
            GLFW.WindowHint(WindowHintClientApi.ClientApi, ClientApi.OpenGlApi);
            GLFWGraphicsContext Context = new GLFWGraphicsContext(window);
            Context.MakeCurrent();
            Debug.Log("Loading OpenGL bindings");
            InitializeGlBindings();

            GL.Enable(EnableCap.DepthTest);
            GL.Enable(EnableCap.ScissorTest);
            GL.Enable(EnableCap.Blend);
            GL.Enable(EnableCap.TextureCubeMapSeamless);
        }
    }
}
