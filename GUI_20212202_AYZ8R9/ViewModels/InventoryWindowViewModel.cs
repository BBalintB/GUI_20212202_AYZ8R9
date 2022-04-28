using GUI_20212202_AYZ8R9.Logic;
using GUI_20212202_AYZ8R9.Models;
using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.DependencyInjection;
using Microsoft.Toolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace GUI_20212202_AYZ8R9.ViewModels
{
    public class InventoryWindowViewModel:ObservableRecipient
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
                (AddToInventory as RelayCommand).NotifyCanExecuteChanged();
            }
        }

        private Weapon selectedFromInventory;

        public Weapon SelectedFromInventory
        {
            get { return selectedFromInventory; }
            set
            {
                SetProperty(ref selectedFromInventory, value);
                (AddToBunker as RelayCommand).NotifyCanExecuteChanged();
                (AddToHero as RelayCommand).NotifyCanExecuteChanged();
            }
        }


        public ICommand AddToBunker { get; set; }
        public ICommand AddToInventory{ get; set; }
        public ICommand AddToHero{ get; set; }

        public static bool IsInDesignMode
        {
            get
            {
                var prop = DesignerProperties.IsInDesignModeProperty;
                return (bool)DependencyPropertyDescriptor.FromProperty(prop, typeof(FrameworkElement)).Metadata.DefaultValue;
            }
        }

        public InventoryWindowViewModel() : this(IsInDesignMode ? null : Ioc.Default.GetService<IInventoryLogic>())
        {
            
        }

        public void Setup(Game game) {
            this.Hero = game.Hero;
            //this.Bunker = game.BunkerWeapons;
            //this.HeroInventory = Hero.Inventory;
            AddToBunker = new RelayCommand(() => {
                logic.AddToBunker(selectedFromInventory);
            },
           () => Hero.MapPosition==4 && SelectedFromInventory != null
           );

            AddToInventory = new RelayCommand(() => {
                logic.AddToInventory(selectedFromBunker);
            },
            () => Hero.MapPosition == 4 && SelectedFromBunker != null
            );
        }

        public InventoryWindowViewModel(IInventoryLogic logic)
        {
            Bunker = new ObservableCollection<Weapon>();
            HeroInventory = new ObservableCollection<Weapon>();
            this.logic = logic;
            logic.SetupCollections(Bunker, HeroInventory);
            Bunker.Add(new Weapon() { 
                Name = "Brutal Axe",
                Type=WeaponType.rare,
                Damage = 15,
                HPBoost=2
            });
            Bunker.Add(new Weapon()
            {
                Name = "Extra Sniper",
                Type = WeaponType.common,
                Damage = 25,
                HPBoost = -5
            });
            Bunker.Add(new Weapon()
            {
                Name = "Legendary Pistol",
                Type = WeaponType.uncommon,
                Damage = 12,
                HPBoost = 5
            });
            Bunker.Add(new Weapon()
            {
                Name = "Shield",
                Type = WeaponType.epic,
                Damage = 10,
                HPBoost = 25
            });
            Bunker.Add(new Weapon()
            {
                Name = "Minigun",
                Type = WeaponType.legendary,
                Damage = 45,
                HPBoost = 0
            });




            AddToHero = new RelayCommand(() => {
                Hero.Weapon = selectedFromInventory;
            },
            ()=> SelectedFromInventory != null
            );
        }

    }
}
