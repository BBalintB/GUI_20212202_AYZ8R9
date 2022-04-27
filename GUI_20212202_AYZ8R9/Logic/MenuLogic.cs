using GUI_20212202_AYZ8R9.MenuOptionsWindows;
using GUI_20212202_AYZ8R9.Models;
using GUI_20212202_AYZ8R9.Services.MenuOptionsServices;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace GUI_20212202_AYZ8R9.Logic
{
    public class MenuLogic : IMenuLogic
    {
        IList<Game> games;
        Visibility menu;
        Visibility game;

        public Game Game { get; set; }
        public INewGameViaWindow NewGame { get; set; }
        public MenuLogic(INewGameViaWindow newGame)
        {
            this.NewGame = newGame;
        }

        public void SetupCollection(IList<Game> games)
        {
            this.games = games;
            string[] files = Directory.GetFiles("Games", "*.json"); //Reads out the files from the Games map
            for (int i = 0; i < files.Length; i++)
            {
                if (File.Exists(files[i]))
                {
                    var hq = JsonConvert.DeserializeObject<Game>(File.ReadAllText(files[i])); //It fills up the Games list with the content of the save 
                    games.Add(hq);
                }
            }
        }

        public void SetUpVisibility(Visibility menu, Visibility game)
        {
            this.menu = menu;
            this.game = game;
        }

        public void CreateNewGame(Game game)
        {
            game = NewGame.NewGame();
            if (game.FileName != null)
            {
                games.Add(game); // Add the new game to the games collcetion
            }
        }

        public void DeleteFile(Game game)
        {
            if (MessageBox.Show("Delete File?", "Question", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes) //Brings up a window to ask you really want to delete a file
            {
                File.Delete(Path.Combine("Games", game.FileName + ".json")); //Search for the .json file and delete it
                games.Remove(game); // delete the game from the games collection
            }
        }

        public void LoadInGame(Game game)
        {
            //TODO starts the game with the selected game object
            //new FightWindow(game).ShowDialog();

        }
    }
}