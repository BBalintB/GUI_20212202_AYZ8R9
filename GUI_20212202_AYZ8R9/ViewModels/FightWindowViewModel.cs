using GUI_20212202_AYZ8R9.Logic;
using GUI_20212202_AYZ8R9.Models;
using GUI_20212202_AYZ8R9.Models.NPC;
using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.DependencyInjection;
using Microsoft.Toolkit.Mvvm.Input;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace GUI_20212202_AYZ8R9.ViewModels
{
    public class FightWindowViewModel : ObservableRecipient, IWhoWin
    {
        #region Properties

        public ObservableCollection<Models.ICharacter> AvailableHeroes { get; set; }
        public ObservableCollection<Models.ICharacter> Heroes { get; set; }
        public ObservableCollection<Models.ICharacter> Villians { get; set; }

        private Visibility fightWindowVisibility;

        public Visibility FightWindowVisibility
        {
            get { return fightWindowVisibility; }
            set {
                SetProperty(ref fightWindowVisibility, value);
            }
        }

        private Visibility selectorWindowVisibility;

        public Visibility SlectorWindowVisibility
        {
            get { return selectorWindowVisibility; }
            set
            {
                SetProperty(ref selectorWindowVisibility, value);
            }
        }

        private WindowState windowState;

        public WindowState WindowState
        {
            get { return windowState; }
            set {
                SetProperty(ref windowState, value);
            }
        }



        public Action CloseHeroWin { get; set; }
        public Action CloseVillianWin { get; set; }

        private Models.ICharacter selectedNPC;

        public Models.ICharacter SelectedNPC
        {
            get { return selectedNPC; }
            set
            {
                SetProperty(ref selectedNPC, value);
                (AddToTeamCommand as RelayCommand).NotifyCanExecuteChanged();
            }
        }

        private Models.ICharacter selectedHero;

        public Models.ICharacter SelectedHero
        {
            get { return selectedHero; }
            set
            {
                SetProperty(ref selectedHero, value);
                (SpecialCommand as RelayCommand).NotifyCanExecuteChanged();
                (RemoveFromTeamCommand as RelayCommand).NotifyCanExecuteChanged();
            }
        }

        private Models.ICharacter selectedEnemy;

        public Models.ICharacter SelectedEnemy
        {
            get { return selectedEnemy; }
            set
            {
                SetProperty(ref selectedEnemy, value);
                (AttackCommand as RelayCommand).NotifyCanExecuteChanged();
                (SpecialCommand as RelayCommand).NotifyCanExecuteChanged();
            }
        }

        private Models.ICharacter currentPlayer;

        public Models.ICharacter CurrentPlayer
        {
            get { return currentPlayer; }
            set
            {
                SetProperty(ref currentPlayer, value);
            }
        }

        private string team;

        public string Team
        {
            get { return team; }
            set {
                SetProperty(ref team, value);
            }
        }

        public int Round { 
            get { 
                return logic.Rounds;
            }
        }

        public ICommand AttackCommand { get; set; }
        public ICommand HealCommand { get; set; }
        public ICommand SpecialCommand { get; set; }
        public ICommand AddToTeamCommand { get; set; }
        public ICommand RemoveFromTeamCommand { get; set; }
        public ICommand GetBoosterCommand { get; set; }
        public ICommand StartCommand { get; set; }

        public Game Game { get; set; }

        public static bool IsInDesignMode
        {
            get
            {
                var prop = DesignerProperties.IsInDesignModeProperty;
                return (bool)DependencyPropertyDescriptor.FromProperty(prop, typeof(FrameworkElement)).Metadata.DefaultValue;
            }
        }

        #endregion

        #region Values
        int heroRound = 0;
        public bool Clicked { get { return logic.Clicked; } }
        Hero hero;
        Game backupGame;
        IFightWindowLogic logic;
        #endregion

        #region Constructors
        public FightWindowViewModel() : this(IsInDesignMode ? null : Ioc.Default.GetService<IFightWindowLogic>())
        {
            FightWindowVisibility = Visibility.Collapsed;
            WindowState = WindowState.Normal;
        }

        public FightWindowViewModel(IFightWindowLogic logic)
        {
            Heroes = new ObservableCollection<Models.ICharacter>();
            AvailableHeroes = new ObservableCollection<Models.ICharacter>();
            Villians = new ObservableCollection<Models.ICharacter>();
            this.logic = logic;
            Villians.Add(new NPC_Character()
            {
                Name = "bandit1",
                HP = 100,
                Attack = 20,
                HeroType = HeroTypes.Bandit
            });
            Villians.Add(new NPC_Character()
            {
                Name = "bandit2",
                HP = 100,
                Attack = 20,
                HeroType = HeroTypes.Bandit
            });
            Villians.Add(new NPC_Character()
            {
                Name = "bandit3",
                HP = 100,
                Attack = 20,
                HeroType = HeroTypes.Bandit
            });

            #region SelectorWindowCommands
            AddToTeamCommand = new RelayCommand(
                () => {
                    logic.AddToTeam(SelectedNPC);
                },
                ()=>SelectedNPC != null
                );

            RemoveFromTeamCommand = new RelayCommand(
                () => {
                    logic.RemoveFromTeam(SelectedHero);
                },
                ()=>SelectedHero != null
                );

            GetBoosterCommand = new RelayCommand(
                () => {
                    logic.GetBooster();
                }
                );
            StartCommand = new RelayCommand(
                () => {
                    WindowState = WindowState.Maximized;
                    SlectorWindowVisibility = Visibility.Collapsed;
                    FightWindowVisibility = Visibility.Visible;
                }
                );
            #endregion

            #region PlayerCommands
            AttackCommand = new RelayCommand(
                () =>
                {
                    logic.Attack(SelectedHero, SelectedEnemy);
                    if (EndGame())
                    {
                        BotRound();
                        heroRound = (Heroes.Count - 1) <= heroRound ? 0 : heroRound + 1;
                        SelectedHero = Heroes.ElementAt(heroRound);
                        CurrentPlayer = SelectedHero;
                    }

                }, 
                () => SelectedEnemy != null
            ) ;

            HealCommand = new RelayCommand(() =>
            {
                logic.Heal(SelectedHero);
                BotRound();
                heroRound = (Heroes.Count - 1) <= heroRound ? 0 : heroRound + 1;
                SelectedHero = Heroes.ElementAt(heroRound);
            }
            );

            SpecialCommand = new RelayCommand(
                () => 
                    {
                        logic.Special(SelectedHero,SelectedEnemy);
                        if (EndGame())
                        {
                            BotRound();
                            heroRound = (Heroes.Count - 1) <= heroRound ? 0 : heroRound + 1;
                            SelectedHero = Heroes.ElementAt(heroRound);
                            CurrentPlayer = SelectedHero;
                        }
                    }
                ,
                () => SelectedHero!= null ? (SelectedHero.SpecialAttackCounter > 1 ? true: false) : false && SelectedEnemy != null
                );

            #endregion

            Messenger.Register<FightWindowViewModel, string, string>(this, "RoundInfo", (recipient, msg) =>
            {
                OnPropertyChanged("Round");
            });

            Messenger.Register<FightWindowViewModel, string, string>(this, "ClickInfo", (recipient, msg) =>
            {
                OnPropertyChanged("Clicked");
            });

            Team = "Heroes";
        }


        #endregion

        #region SetupFight
        public void Setup(Game game)
        {
            this.Game = game;
            backupGame = JsonConvert.DeserializeObject<Game>(File.ReadAllText(Path.Combine("Games", game.FileName + ".json")));
            this.hero = game.Hero;
            Heroes.Add(hero);
            logic.SetupCollections(Heroes, Villians, AvailableHeroes);
            SelectedHero = Heroes.ElementAt(heroRound);
            CurrentPlayer = SelectedHero;
            AvailableHeroes.Add(new NPC_Character()
            {
                Name = "Pista",
                HP = 100,
                Attack = 35,
                SpecialAttackCounter = 0,
                HeroType = HeroTypes.Assault
            });
        }

        #endregion

        #region ExtraMethods
        bool EndGame()
        {
            
            if (Heroes.Count == 0)
            {
                CloseVillianWin?.Invoke();
                Game.Hero = backupGame.Hero;
                return false;
            }
            else if (Villians.Count == 0)
            {
                CloseHeroWin?.Invoke();
                Game.Hero = backupGame.Hero;
                return false;
            }
            else {
                return true;
            }
        }

        void BotRound() {
            Team = "Bad guys";
            if (heroRound == Heroes.Count() - 1)
            {
                foreach (var item in Villians)
                {
                    if (EndGame())
                    {
                        CurrentPlayer = item;
                        logic.BotRound(item);
                    }
                }
            }
            Team = "Heroes";
        }
        #endregion
    }
}
