using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GUI_20212202_AYZ8R9.Logic
{
    internal class MainCharecterLogic
    {
        Size size;
        public void Resize(Size size)
        {
            this.size = size;
        }

        public int Counter = 0;
        public int Counter2 = 0;

        public double Character_Pozition = 0;

        public double Last_Character_Pozition = 0;

        public bool jump = false;
    }
}
