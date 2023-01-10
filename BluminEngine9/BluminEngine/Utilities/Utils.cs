using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BluminEngine9.BluminEngine.Utilities
{
    public class Utils
    {
        
    }

    public class AppInfo
    {
        public static string AssetsPath { get => Environment.CurrentDirectory + "\\" + "Assets"; }
        public static string PersistantDatapath { get => Persistantdatapath(); }
        public static string AppName { get => Application.AppName; }

        private static string Persistantdatapath()
        {
            var dir = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\"+ "BluminEngine6" +"\\" + AppName;

            if (!Directory.Exists(dir))
            {
                Directory.CreateDirectory(dir);
            }
            return dir;
        }
    }
}
