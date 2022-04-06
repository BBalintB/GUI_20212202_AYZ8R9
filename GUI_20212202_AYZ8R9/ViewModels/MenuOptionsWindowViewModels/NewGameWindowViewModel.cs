using GUI_20212202_AYZ8R9.Logic.MenuOptionsLogic;
using GUI_20212202_AYZ8R9.Models;
using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Input;
using Microsoft.VisualStudio.PlatformUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace GUI_20212202_AYZ8R9.ViewModels.MenuOptionsWindowViewModels
{
    public class NewGameWindowViewModel:ObservableRecipient,IConncetion
    {
        public Action Close { get; set; } // Action for window closing
        public Action FreshInfo { get; set; } // action for refreshing the data in the object newGmae

        private Game newGame;

        public Game NewGame
        {
            get { return newGame; }
            set {
                SetProperty(ref newGame, value);
            }
        }

        INewGameLogic logic;
        
        public ICommand ChangeGridCommand{ get; set; }
        public ICommand CreateHeroCommand{ get; set; }
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

        #region Methods
        public void Setup(Game game) {
            
            NewGame = game; // Set the new game
            logic.SetupHero(NewGame);
        }

        public void CloseWindow()
        {
            Close?.Invoke();
        }

        public void Refresh()
        {
            FreshInfo?.Invoke();
        }
        #endregion

        public NewGameWindowViewModel(INewGameLogic logic)
        {
            this.logic = logic;
            GridOpenerHeroCreate = Visibility.Visible;
            GridOpenerSaveDetails = Visibility.Collapsed;
            ChangeGridCommand = new RelayCommand(() =>
            {
                FreshInfo();
                GridOpenerHeroCreate = GridOpenerHeroCreate == Visibility.Collapsed ? Visibility.Visible: Visibility.Collapsed;
                GridOpenerSaveDetails = GridOpenerSaveDetails == Visibility.Collapsed ? Visibility.Visible: Visibility.Collapsed;
            }
            );

            CreateHeroCommand = new RelayCommand(
                () =>
                {
                    Close();
                    logic.SetUpNewGame();
                }
                );

            Hero1Command = new RelayCommand(
                () => {
                    logic.SetHeroType(HeroTypes.archer);
                }
           );
            Hero2Command = new RelayCommand(
                () => {
                    logic.SetHeroType(HeroTypes.assault);
                }
           );
            Hero3Command = new RelayCommand(
                () => {
                    logic.SetHeroType(HeroTypes.support);
                }
           );
        }

    }

}
