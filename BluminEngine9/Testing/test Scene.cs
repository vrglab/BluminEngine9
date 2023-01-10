using BluminEngine9.BluminEngine;
using BluminEngine9.BluminEngine.Rendering.Shading;
using BluminEngine9.BluminEngine.SceneMannagment;
using BluminEngine9.BluminEngine.Utilities.AssetsManegment;
using BluminEngine9.BluminEngine.Utilities.Debuging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BluminEngine9.Testing
{
    public class test_Scene : Scene
    {
        testGameobject obj;
        public override void Awake()
        {
            obj = RegisterGameObject(new testGameobject());
        }

        public override void Start()
        {
        }

        public override void Update()
        {
        }
    }
}
