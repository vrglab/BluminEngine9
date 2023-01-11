using BluminEngine9;
using BluminEngine9.SceneMannagment;



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

