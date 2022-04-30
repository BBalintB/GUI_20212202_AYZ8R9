using GUI_20212202_AYZ8R9.Helper;
using GUI_20212202_AYZ8R9.Logic.MenuOptionsLogic;
using GUI_20212202_AYZ8R9.Models;
using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.IO;
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

        private string heroImagePath;

        public string HeroImagePath
        {
            get { return heroImagePath; }
            set
            {
                SetProperty(ref heroImagePath, value);
            }
        }

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


        /// <summary>
        /// logic.SetHeroType(HeroTypes.archer);
        /// this.HeroImagePath = @"Images\Archer\Idle_1";
        /// </summary>
        public void SetUpHero1()
        {
            logic.SetHeroType(HeroTypes.Archer);
            string path = Directory.GetCurrentDirectory();
            this.HeroImagePath = (Path.Combine(path, "Images", "Archer", "Idle_1.png"));
        }

        /// <summary>
        /// logic.SetHeroType(HeroTypes.assault);
        /// this.HeroImagePath = @"Images\Archer\Idle_1";
        /// </summary>
        public void SetUpHero2()
        {
            logic.SetHeroType(HeroTypes.Assault);
            string path = Directory.GetCurrentDirectory();
            this.HeroImagePath = (Path.Combine(path ,"Images", "Assault", "Idle_1.png"));
        }

        /// <summary>
        /// logic.SetHeroType(HeroTypes.support);
        /// this.HeroImagePath = @"Images\Support\Idle_1";
        /// </summary>
        public void SetUpHero3()
        {
            logic.SetHeroType(HeroTypes.Support);
            string path = Directory.GetCurrentDirectory();
            this.HeroImagePath = (Path.Combine(path, "Images", "Support", "Idle_1.png"));
        }

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
                    NewGame.Hero.Weapon = RandomUtil.GetARandomWeapon();
                    NewGame.Hero.Inventory.Add(NewGame.Hero.Weapon);
                }
                );

            Hero1Command = new RelayCommand(
                () => {
                    SetUpHero1();
                }
           );
            Hero2Command = new RelayCommand(
                () => {
                    SetUpHero2();
                }
           );
            Hero3Command = new RelayCommand(
                () => {
                    SetUpHero3();
                }
           );
        }

    }

}
