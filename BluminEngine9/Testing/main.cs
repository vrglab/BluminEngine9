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
        Application.StartEngine(new Resolution(1234, 1234), "Testing windows", new test_Scene());
    }
}