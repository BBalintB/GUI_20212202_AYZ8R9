using System;

namespace GUI_20212202_AYZ8R9.ViewModels.MenuOptionsWindowViewModels
{
    internal interface IConncetion
    {
        Action Close { get; set; }
        Action FreshInfo { get; set; }
        void CloseWindow();
        void Refresh();
    }
}