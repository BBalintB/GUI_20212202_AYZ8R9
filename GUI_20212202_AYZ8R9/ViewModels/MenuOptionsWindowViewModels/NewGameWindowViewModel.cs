using GUI_20212202_AYZ8R9.Logic.MenuOptionsLogic;
using GUI_20212202_AYZ8R9.Models;
using Microsoft.Toolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace GUI_20212202_AYZ8R9.ViewModels.MenuOptionsWindowViewModels
{
    public class NewGameWindowViewModel
    {
        public Game NewGame { get; set; }

        public void Setup(Game game) {
            
            NewGame = game; // Set the new game   
            ;
        }
        public NewGameWindowViewModel()
        {
            
        }

    }

}
