using GUI_20212202_AYZ8R9.Models;
using System;

namespace GUI_20212202_AYZ8R9.ViewModels
{
    public interface IWhoWin
    {
        Action CloseHeroWin { get; set; }
        Action CloseVillianWin { get; set; }
    }
}