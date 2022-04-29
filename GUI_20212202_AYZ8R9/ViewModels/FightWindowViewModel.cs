using GUI_20212202_AYZ8R9.Helper;
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
        public ObservableCollection<string> Log { get; set; }

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

        private Visibility endWindowVisibility;

        public Visibility EndWindowVisibility
        {
            get { return endWindowVisibility; }
            set
            {
                SetProperty(ref endWindowVisibility, value);
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

        bool EndGame
        {
            get
            {
                if (Heroes.Count == 0)
                {
                    (ContinueCommand as RelayCommand).NotifyCanExecuteChanged();
                    FightWindowVisibility = Visibility.Collapsed;
                    EndWindowVisibility = Visibility.Visible;
                    WindowState = WindowState.Normal;
                    WhoWon = "You lost";
                    return false;
                }
                else if (Villians.Count == 0)
                {
                    (ContinueCommand as RelayCommand).NotifyCanExecuteChanged();
                    FightWindowVisibility = Visibility.Collapsed;
                    EndWindowVisibility = Visibility.Visible;
                    WindowState = WindowState.Normal;
                    WhoWon = "You won";
                    return false;
                }
                else
                {
                    return true;
                }
            }

        }

        private string whoWon;

        public string WhoWon
        {
            get { return whoWon; }
            set {
                SetProperty(ref whoWon, value);
            }
        }


        public ICommand AttackCommand { get; set; }
        public ICommand HealCommand { get; set; }
        public ICommand SpecialCommand { get; set; }
        public ICommand AddToTeamCommand { get; set; }
        public ICommand RemoveFromTeamCommand { get; set; }
        public ICommand GetBoosterCommand { get; set; }
        public ICommand StartCommand { get; set; }
        public ICommand ContinueCommand { get; set; }
        public ICommand RestartCommand { get; set; }
        public ICommand ExitCommand { get; set; }


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
        
        Hero hero;
        Game backupGame;
        IFightWindowLogic logic;
        #endregion

        #region Constructors
        public FightWindowViewModel() : this(IsInDesignMode ? null : Ioc.Default.GetService<IFightWindowLogic>())
        {
            
            
        }

        public FightWindowViewModel(IFightWindowLogic logic)
        {
            SetupCollections();
            this.logic = logic;

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
            int tmp = RandomUtil.rnd.Next(0, 101);
            GetBoosterCommand = new RelayCommand(
                () => {
                    logic.GetBooster();
                },
                ()=> tmp>10
                );
            StartCommand = new RelayCommand(
                () => {
                    WindowState = WindowState.Maximized;
                    SlectorWindowVisibility = Visibility.Collapsed;
                    FightWindowVisibility = Visibility.Visible;
                    SelectedHero = Heroes.ElementAt(heroRound);
                    CurrentPlayer = SelectedHero;
                }
                );
            #endregion

            #region PlayerCommands
            AttackCommand = new RelayCommand(
                () =>
                {
                    logic.Attack(SelectedHero, SelectedEnemy);
                    if (EndGame)
                    {
                        BotRound();
                        
                    }

                }, 
                () => SelectedEnemy != null
            ) ;

            HealCommand = new RelayCommand(() =>
            {
                logic.Heal(SelectedHero);
                BotRound();
            }
            );

            SpecialCommand = new RelayCommand(
                () => 
                    {
                        logic.Special(SelectedHero,SelectedEnemy);
                        if (EndGame)
                        {
                            BotRound();
                        }
                    }
                ,
                () => SelectedHero!= null ? (SelectedHero.SpecialAttackCounter > 1 ? true: false) : false && SelectedEnemy != null
                );

            #endregion

            #region EndWindowCommands
            ContinueCommand = new RelayCommand(
                () => {
                    this.Game.FileLastSaveDate = DateTime.Now.ToString();
                    Save.WriteOutJSON(Game, Path.Combine("Games", Game.FileName + ".json"));
                    CloseHeroWin?.Invoke();
                },
                ()=> Villians.Count==0
                );
            RestartCommand = new RelayCommand(
                () => {
                    ResetCollections();
                    Setup(Game);
                }
                );
            ExitCommand = new RelayCommand(
                () => {
                    backupGame = JsonConvert.DeserializeObject<Game>(File.ReadAllText(Path.Combine("Games", Game.FileName + ".json")));
                    this.Game = backupGame;
                    if (Villians.Count != 0)
                    {
                        int i = 0;
                        while (Game.Hero.Inventory.Count>1)
                        {
                            var xy = Game.Hero.Inventory[i].Name != this.hero.Weapon.Name;
                            if (xy)
                            {
                                Game.Hero.Inventory.Remove(Game.Hero.Inventory[i]);
                            }
                            else {
                                i++;
                            }
                        }
                    }
                    this.Game.FileLastSaveDate = DateTime.Now.ToString();
                    Save.WriteOutJSON(Game, Path.Combine("Games", Game.FileName + ".json"));
                    CloseVillianWin?.Invoke();
                }
                );
            #endregion

            Messenger.Register<FightWindowViewModel, string, string>(this, "RoundInfo", (recipient, msg) =>
            {
                OnPropertyChanged("Round");
                
            });
            Team = "Heroes";
        }


        #endregion

        #region SetupFight

        public void SetupCollections() {
            
            Heroes = new ObservableCollection<Models.ICharacter>();
            AvailableHeroes = new ObservableCollection<Models.ICharacter>();
            Villians = new ObservableCollection<Models.ICharacter>();
            Log = new ObservableCollection<string>();
            
        }

        public void Setup(Game game)
        {
            this.Game = game;
            backupGame = JsonConvert.DeserializeObject<Game>(File.ReadAllText(Path.Combine("Games", game.FileName + ".json")));
            this.hero = backupGame.Hero;
            Heroes.Add(hero);
            logic.SetupCollections(Heroes, Villians, AvailableHeroes,Log, Game);
            FightWindowVisibility = Visibility.Collapsed;
            EndWindowVisibility = Visibility.Collapsed;
            SlectorWindowVisibility = Visibility.Visible;
            WindowState = WindowState.Normal;
            
        }

        #endregion

        #region ExtraMethods
        


        void BotRound() {
            Team = "Bad guys";
            bool tmp = true;
            if (heroRound == Heroes.Count() - 1)
            {
                tmp = EndGame;
                foreach (var item in Villians)
                {
                    
                    if (tmp)
                    {
                        ;
                        CurrentPlayer = item;
                        logic.BotRound(item);
                    }
                    else
                    {
                        break;
                    }
                    tmp = EndGame;

                }
                
            }
            if (tmp)
            {
                heroRound = (Heroes.Count - 1) <= heroRound ? 0 : heroRound + 1;
                SelectedHero = Heroes.ElementAt(heroRound);
                CurrentPlayer = SelectedHero;
                Team = "Heroes";

            }

            
            
        }

        void ResetCollections()
        {
            ;
            while (Heroes.Count != 0)
            {
                Heroes.Remove(Heroes.ElementAt(0));
            }
            while (Villians.Count != 0)
            {
                Villians.Remove(Villians.ElementAt(0));
            }
            while (Log.Count != 0)
            {
                Log.Remove(Log.ElementAt(0));
            }
            while (AvailableHeroes.Count != 0)
            {
                AvailableHeroes.Remove(AvailableHeroes.ElementAt(0));
            }
            heroRound = 0;
            
        }
        #endregion
    }
}
