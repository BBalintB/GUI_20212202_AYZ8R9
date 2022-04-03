using GUI_20212202_AYZ8R9.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GUI_20212202_AYZ8R9.Logic.MenuOptionsLogic
{
    public class LoadGameLogic : ILoadGameLogic
    {
        IList<Game> games;
        public void SetupCollection(IList<Game> games)
        {
            this.games = games;
        }
        public LoadGameLogic()
        {

        }

        public void RemoveSave(Game game)
        {
            games.Remove(game);
            File.Delete("Games/" + game.FileName+".json");
        }
    }
}
