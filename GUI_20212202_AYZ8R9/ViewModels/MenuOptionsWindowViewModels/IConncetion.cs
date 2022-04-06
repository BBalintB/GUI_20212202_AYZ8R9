using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GUI_20212202_AYZ8R9.ViewModels.MenuOptionsWindowViewModels
{
    interface IConncetion
    {
        Action Close { get; set; }
        Action FreshInfo { get; set; }
        void CloseWindow();
        void Refresh();
    }
}
