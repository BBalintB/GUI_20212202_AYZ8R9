using GUI_20212202_AYZ8R9.Logic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace GUI_20212202_AYZ8R9.Renderer
{
    public class Display : FrameworkElement
    {
        IGameModel model;

        public void SetupModel(IGameModel model)
        {
            this.model = model;
        }


    }
}
