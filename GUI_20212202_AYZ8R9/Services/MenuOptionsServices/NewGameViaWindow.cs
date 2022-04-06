using GUI_20212202_AYZ8R9.Logic.MenuOptionsLogic;
using GUI_20212202_AYZ8R9.MenuOptionsWindows;
using GUI_20212202_AYZ8R9.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GUI_20212202_AYZ8R9.Services.MenuOptionsServices
{
    public class NewGameViaWindow : INewGameViaWindow
    {
        INewGameLogic logic;
        public NewGameViaWindow(INewGameLogic logic)
        {
            this.logic = logic;
        }
        public Game NewGame()
        {
            Game tmp = new Game(); // set the save datetime for today
            new NewGameWindow(tmp,logic).ShowDialog();
            if (tmp.FileName != null)
            {
                return tmp;
            }
            return null;
        }
    }
}
