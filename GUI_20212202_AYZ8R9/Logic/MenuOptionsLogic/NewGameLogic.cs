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
            string newGame = JsonConvert.SerializeObject(game); //Serialize the incoming game object
            File.WriteAllText("Games/" + game.FileName + ".json", newGame); //It save it into a file named after the object file name prop
        }
        public void SetHeroType(HeroTypes type)
        {
            game.Hero.HeroType = type;
        }
    }
}
