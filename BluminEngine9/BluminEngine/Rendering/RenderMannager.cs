using BluminEngine9.BluminEngine.Utilities.EventSystem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BluminEngine9.BluminEngine.Rendering
{
    public class RenderMannager
    {

        public void Render()
        {
            Application.sceneMannager.CurentScene.OnRenderEventCallback.Invoke();
        }
    }
}
