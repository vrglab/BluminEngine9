using BluminEngine9.BluminEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BluminEngine9.Testing
{
    internal class main
    {
        public static void Main(string[] args)
        {
            Application.StartEngine(new BluminEngine.Rendering.Resolution(1234, 1234), "Testing windows", new test_Scene());
        }
    }
}
