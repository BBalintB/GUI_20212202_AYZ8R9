using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GUI_20212202_AYZ8R9.ViewModels
{
    interface IMainActions
    {
        Action CloseWindow { get; set; }
        Action LoadAction { get; set; }
    }
}
