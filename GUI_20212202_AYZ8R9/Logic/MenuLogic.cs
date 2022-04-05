using GUI_20212202_AYZ8R9.Models;
using GUI_20212202_AYZ8R9.Services.MenuOptionsServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GUI_20212202_AYZ8R9.Logic
{
    public class MenuLogic : IMenuLogic
    {
        public Game Game { get; set; }
        public INewGameViaWindow NewGame { get; set; }
        public ILoadGameViaWindow LoadGame { get; set; }
        public MenuLogic(INewGameViaWindow newGame, ILoadGameViaWindow loadGame)
        {
            Game = new Game();
            this.NewGame = newGame;
            this.LoadGame = loadGame;
        }

        public void CreateNewGame()
        {
            Game tmp = NewGame.NewGame();
            ;
            if (tmp.FileName != null)
            {
                Game = tmp;
            }
        }

        public void LoadInGame() {
            Game tmp = LoadGame.LoadGame();
            Game = tmp;
            //TODO starts the game with the selected game object
        }
    }
}
