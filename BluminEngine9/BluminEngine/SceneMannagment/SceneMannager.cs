using BluminEngine9.BluminEngine.Objects;
using BluminEngine9.BluminEngine.Utilities.EventSystem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BluminEngine9.BluminEngine.SceneMannagment
{
    public class SceneMannager
    {
        public Scene CurentScene { get; private set; }

        public SceneMannager(Scene curentScene)
        {
            SetScene(curentScene);
        }

        public void SetScene(Scene scene)
        {
            if(CurentScene != null)
            {
                CurentScene.UnloadEventCallback.Invoke();
                CurentScene = scene;
                CurentScene.AwakeEventCallback.Invoke();
                CurentScene.StartEventCallback.Invoke();
            }
            else
            {
                CurentScene = scene;
                CurentScene.AwakeEventCallback.Invoke();
                CurentScene.StartEventCallback.Invoke();
            }
        }
    }

    public abstract class Scene : Instancable, IEngineActor
    {
        protected Dictionary<Guid, GameObject> RegisteredGameObjects = new Dictionary<Guid, GameObject>();


        public BluminGlobalEvent AwakeEventCallback { get; } = new BluminGlobalEvent();
        public BluminGlobalEvent StartEventCallback { get; } = new BluminGlobalEvent();
        public BluminGlobalEvent UpdateEventCallback { get; } = new BluminGlobalEvent();
        public BluminGlobalEvent UnloadEventCallback { get; } = new BluminGlobalEvent();
        public BluminGlobalEvent OnRenderEventCallback { get; } = new BluminGlobalEvent();

        protected Scene()
        {

            UpdateEventCallback.addListner(() =>
            {
                foreach (var item in RegisteredGameObjects)
                {
                    item.Value.UpdateEvent.Invoke();
                }
                Update();
            });
            AwakeEventCallback.addListner(() =>
            {
                Awake();
            });
            StartEventCallback.addListner(() =>
            {
                Start();
            });
            OnRenderEventCallback.addListner(() =>
            {
                foreach (var item in RegisteredGameObjects)
                {
                    item.Value.OnRenderEvent.Invoke();
                }
            });
            UnloadEventCallback.addListner(() =>
            {
                foreach (var item in RegisteredGameObjects)
                {
                    item.Value.OnDestroyEvent.Invoke();
                }
            });
        }

        public t RegisterGameObject<t>(t objToRegister) where t : GameObject
        {
            if(!RegisteredGameObjects.ContainsKey(objToRegister.GetInstanceId()))
            {
                objToRegister.AwakeEvent.Invoke();
                RegisteredGameObjects.Add(GetInstanceId(), objToRegister);
                objToRegister.StartEvent.Invoke();
            }
            return objToRegister;
        }

        public abstract void Awake();
        public abstract void Start();
        public abstract void Update();
    }
}
