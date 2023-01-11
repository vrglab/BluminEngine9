using BluminEngine9.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BluminEngine9.Utilities.AssetsManegment.Types
{
    public class LoadedFileTypes : Instancable
    {
        public string Name { get; private set; }
        public FileData File { get; private set; }

        public LoadedFileTypes(FileData file)
        {
            Name = file.Name;
            File = file;
        }
    }
}
