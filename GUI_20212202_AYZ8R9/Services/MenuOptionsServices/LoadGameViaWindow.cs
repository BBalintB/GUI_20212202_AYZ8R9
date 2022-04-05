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
    public class LoadGameViaWindow : ILoadGameViaWindow
    {
        ILoadGameLogic logic;
        public LoadGameViaWindow(ILoadGameLogic logic)
        {
            this.logic = logic;
        }
        public Game LoadGame()
        {
            //Game tmp = new Game() { FileLastSaveDate = DateTime.Now}; 
            //new LoadGameWindow(logic, tmp).ShowDialog();
            //;
            //if (tmp.FileName != "")
            //{
            //    return tmp;
            //}
            return null;
        }
    }
}
