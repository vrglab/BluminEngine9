using BluminEngine9.BluminEngine.Rendering;
using BluminEngine9.BluminEngine.SceneMannagment;
using BluminEngine9.BluminEngine.Utilities;
using BluminEngine9.BluminEngine.Utilities.AssetsManegment;
using BluminEngine9.BluminEngine.Utilities.Debuging;
using BluminEngine9.BluminEngine.Utilities.EventSystem;
using OpenTK;
using OpenTK.Graphics.OpenGL;
using OpenTK.Windowing.Desktop;
using OpenTK.Windowing.GraphicsLibraryFramework;
using System.Reflection;

namespace BluminEngine9.BluminEngine
{
    public class Application
    {
        static RenderMannager rm = new RenderMannager();

        public static BluminGlobalEvent OnCloseEventCallback { get; } = new BluminGlobalEvent();


        public static string AppName { get; private set; }
        public static SceneMannager sceneMannager { get; private set; }

        public unsafe static void StartEngine(Resolution res, string name, Scene srtartingScene)
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
                Debug.Log("Deleting Duplicate Assets");
                ResourceMannager.DeleteDuplicates();
               
                Display display = new Display(res, name);

                init(display.window);

                Debug.Log("Batching Assets");
                ResourceMannager.BatchLoadedFiles();

                sceneMannager = new SceneMannager(srtartingScene);

                GL.ClearColor(System.Drawing.Color.DarkCyan);
                GC.Collect();
                while (!GLFW.WindowShouldClose(display.window))
                {
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


        private static void InitializeGlBindings()
        {
            // We don't put a hard dependency on OpenTK.Graphics here.
            // So we need to use reflection to initialize the GL bindings, so users don't have to.

            // Try to load OpenTK.Graphics assembly.
            Assembly assembly;
            try
            {
                assembly = Assembly.Load("OpenTK.Graphics");
            }
            catch
            {
                // Failed to load graphics, oh well.
                // Up to the user I guess?
                // TODO: Should we expose this load failure to the user better?
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

        unsafe static void init(Window* window)
        {
            GLFW.WindowHint(WindowHintClientApi.ClientApi, ClientApi.OpenGlApi);
            GLFWGraphicsContext Context = new GLFWGraphicsContext(window);
            Context.MakeCurrent();
            Debug.Log("Loading OpenGL bindings");
            InitializeGlBindings();

            GL.Enable(EnableCap.DepthTest);
            /*GL.Enable(EnableCap.ScissorTest);
            GL.Enable(EnableCap.Blend);
            GL.Enable(EnableCap.TextureCubeMapSeamless);*/
        }
    }
}
