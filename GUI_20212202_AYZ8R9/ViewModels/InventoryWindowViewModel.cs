using GUI_20212202_AYZ8R9.Logic;
using GUI_20212202_AYZ8R9.Models;
using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace GUI_20212202_AYZ8R9.ViewModels
{
    class InventoryWindowViewModel:ObservableRecipient
    {
        public ObservableCollection<Weapon> Bunker { get; set; }
        public ObservableCollection<Weapon> HeroInventory { get; set; }

        IInventoryLogic logic;

        private Hero hero;

        public Hero Hero
        {
            get { return hero; }
            set {
                SetProperty(ref hero, value);
            }
        }

        private Weapon selectedFromBunker;

        public Weapon SelectedFromBunker
        {
            get { return selectedFromBunker; }
            set {
                SetProperty(ref selectedFromBunker, value);
            }
        }

        private Weapon selectedFromInventory;

        public Weapon SelectedFromInventory
        {
            get { return selectedFromInventory; }
            set
            {
                SetProperty(ref selectedFromInventory, value);
            }
        }


        public ICommand AddToBunker { get; set; }
        public ICommand AddToInventory{ get; set; }
        public ICommand AddToHero{ get; set; }


        public InventoryWindowViewModel(Game game, IInventoryLogic logic)
        {
            Bunker = new ObservableCollection<Weapon>();
            HeroInventory = new ObservableCollection<Weapon>();
            this.Hero = game.Hero;
            this.logic = logic;
            logic.SetupCollections(Bunker, HeroInventory);
            AddToBunker = new RelayCommand(()=> { 
                
            },
            ()=> Hero.MapPosition == 4 && SelectedFromInventory != null
            );

            AddToInventory = new RelayCommand(() => {

            },
            () => Hero.MapPosition == 4 && SelectedFromBunker != null
            );

            AddToHero = new RelayCommand(() => {

            },
            ()=> SelectedFromInventory != null
            );
        }

    }
}
