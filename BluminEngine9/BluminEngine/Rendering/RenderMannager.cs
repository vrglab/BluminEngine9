using BluminEngine9.Utilities.EventSystem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BluminEngine9.Rendering
{
    public class RenderMannager
    {

        public void Render()
        {
            Application.sceneMannager.CurentScene.OnRenderEventCallback.Invoke();
        }
    }
}
