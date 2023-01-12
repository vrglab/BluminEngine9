using BluminEngine9.Objects;
using BluminEngine9.Objects.Componants;
using BluminEngine9.Utilities.AssetsManegment;
using BluminEngine9.Utilities.Debuging;
using BluminEngine9.Utilities.Mathmatics.Vectors;
using Microsoft.Extensions.DependencyModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


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
        transform.Position = new Vector3(transform.Position.x - 0.5f, transform.Position.y, transform.Position.z);
    }
}
