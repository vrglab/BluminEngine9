using BluminEngine9.BluminEngine.Objects;
using BluminEngine9.BluminEngine.Objects.Componants;
using BluminEngine9.BluminEngine.Utilities.AssetsManegment;
using BluminEngine9.BluminEngine.Utilities.Debuging;
using Microsoft.Extensions.DependencyModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BluminEngine9.Testing
{
    public class testGameobject : GameObject
    {
        public override void Awake()
        {
            
        }

        public override void Start()
        {
            ImageRenderer a = AddComponant<ImageRenderer>();
            a.image = ResourceMannager.getImage("Images/Cover Image.jpg");
            a.shader = ResourceMannager.getShader("Default.shad");
        }

        public override void Update()
        {

        }
    }
}
