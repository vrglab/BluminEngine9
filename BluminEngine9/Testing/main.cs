using BluminEngine9;
using BluminEngine9.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

internal class main
{
    public static void Main(string[] args)
    {
        Application.StartEngine(Resolution.DefaultDisplayResolution, "Testing windows", WindowStyle.Normal, new test_Scene());
    }
}