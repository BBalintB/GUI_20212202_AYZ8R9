using GUI_20212202_AYZ8R9.Logic.MenuOptionsLogic;
using GUI_20212202_AYZ8R9.Models;
using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Input;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace GUI_20212202_AYZ8R9.ViewModels.MenuOptionsWindowViewModels
{
    public class LoadGameWindowViewModel:ObservableRecipient
    {
        public ObservableCollection<Game> Games{ get; set; }
        public ILoadGameLogic logic{ get; set; }

        public ICommand RemoveSaveCommand{ get; set; }

        private Game newGame;

        public Game SelectNewGame
        {
            get { return newGame; }
            set { 
                SetProperty(ref newGame, value);
                (RemoveSaveCommand as RelayCommand).NotifyCanExecuteChanged();
            }
        }

        public Game test{ get; set; }

        public void Setup(Game game)
        {

            test = game; // Set the load game   
            SelectNewGame = test;
        }

        public LoadGameWindowViewModel(ILoadGameLogic logic)
        {
            Games = new ObservableCollection<Game>();
            string[] files = Directory.GetFiles("Games", "*.json"); //Reads out the files from the Games map
            for (int i = 0; i < files.Length; i++)
            {
                if (File.Exists(files[i]))
                {
                    var hq = JsonConvert.DeserializeObject<Game>(File.ReadAllText(files[i])); //It fills up the Games list with the content of the save 
                    Games.Add(hq);
                }
            }
            logic.SetupCollection(Games);
            RemoveSaveCommand = new RelayCommand(
                () => logic.RemoveSave(SelectNewGame), //Remove the selected save from the list and from the Games folder
                () => SelectNewGame!=null //It is not useable until a save is selected
                ); 
        }
    }
}
