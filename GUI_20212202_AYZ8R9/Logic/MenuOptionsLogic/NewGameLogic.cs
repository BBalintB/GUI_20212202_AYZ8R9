using GUI_20212202_AYZ8R9.Helper;
using GUI_20212202_AYZ8R9.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GUI_20212202_AYZ8R9.Logic.MenuOptionsLogic
{
    public class NewGameLogic : INewGameLogic
    {
        Game game;

        public void SetupHero(Game game)
        {
            this.game = game;
            this.game.FileLastSaveDate = DateTime.Now.ToString();
        }
        public void SetUpNewGame()
        {
            if (game.FileName != null)
            {
                this.game.FileLastSaveDate = DateTime.Now.ToString();
                Save.WriteOutJSON(game, Path.Combine("Games", game.FileName + ".json"));
            }
        }
        public void SetHeroType(HeroTypes type)
        {
            game.Hero.HeroType = type;
            
        }
    }
}
