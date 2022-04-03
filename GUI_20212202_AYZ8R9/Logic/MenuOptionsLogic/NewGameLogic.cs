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
        public void SetUpNewGame(Game game)
        {
            string newGame = JsonConvert.SerializeObject(game); //Serialize the incoming game object
            File.WriteAllText("Games/" + game.FileName+".json", newGame); //It save it into a file named after the object file name prop
            //TODO hero creator window
        }
    }
}
