using GUI_20212202_AYZ8R9.Logic.MenuOptionsLogic;
using GUI_20212202_AYZ8R9.Models;
using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace GUI_20212202_AYZ8R9.ViewModels.MenuOptionsWindowViewModels
{
    public class NewGameWindowViewModel:ObservableRecipient
    {
        private Game newGame;

        public Game NewGame
        {
            get { return newGame; }
            set {
                SetProperty(ref newGame, value);
                (ChangeGridCommand as RelayCommand).NotifyCanExecuteChanged();
            }
        }


        public ICommand ChangeGridCommand{ get; set; }
        public ICommand Hero1Command{ get; set; }
        public ICommand Hero2Command { get; set; }
        public ICommand Hero3Command { get; set; }

        private Visibility gridOpenerHC;

        public Visibility GridOpenerHeroCreate
        {
            get { return gridOpenerHC; }
            set {
                SetProperty(ref gridOpenerHC, value);
            }
        }

        private Visibility gridOpenerSD;

        public Visibility GridOpenerSaveDetails
        {
            get { return gridOpenerSD; }
            set
            {
                SetProperty(ref gridOpenerSD, value);
            }
        }

        public void Setup(Game game) {
            
            NewGame = game; // Set the new game   
        }
        public NewGameWindowViewModel()
        {
            GridOpenerHeroCreate = Visibility.Visible;
            GridOpenerSaveDetails = Visibility.Collapsed;
            ChangeGridCommand = new RelayCommand(() =>
            {
                GridOpenerHeroCreate = GridOpenerHeroCreate == Visibility.Collapsed ? Visibility.Visible: Visibility.Collapsed;
                GridOpenerSaveDetails = GridOpenerSaveDetails == Visibility.Collapsed ? Visibility.Visible: Visibility.Collapsed;
            }
            //},
            //() => NewGame.FileName != null
            );

            Hero1Command = new RelayCommand(
                () => {
                    NewGame.Hero.HeroType = HeroTypes.archer;
                }
           );
            Hero2Command = new RelayCommand(
                () => {
                    NewGame.Hero.HeroType = HeroTypes.assault;
                }
           );
            Hero3Command = new RelayCommand(
                () => {
                    NewGame.Hero.HeroType = HeroTypes.support;
                }
           );
        }

    }

}
