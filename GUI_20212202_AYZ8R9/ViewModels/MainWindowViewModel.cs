using GUI_20212202_AYZ8R9.Logic;
using GUI_20212202_AYZ8R9.MenuOptionsWindows;
using GUI_20212202_AYZ8R9.Models;
using GUI_20212202_AYZ8R9.Renderer;
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
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace GUI_20212202_AYZ8R9.ViewModels
{
    public class MainWindowViewModel : ObservableRecipient
    {
        public ObservableCollection<Game> Games { get; set; }
        private Game selectedGame;

        public Game SelectedGame
        {
            get { return selectedGame; }
            set 
            {
                SetProperty(ref selectedGame, value);
                (LoadGameCommand as RelayCommand).NotifyCanExecuteChanged();
                (DeleteFileCommand as RelayCommand).NotifyCanExecuteChanged();
            }
        }

        private Visibility menuVisibility;

        public Visibility MenuVisibility
        {
            get { return menuVisibility; }
            set
            {
                SetProperty(ref menuVisibility, value);
            }
        }

        private Visibility gameVisibility;

        public Visibility GameVisibility
        {
            get { return gameVisibility; }
            set
            {
                SetProperty(ref gameVisibility, value);
            }
        }


        public ICommand NewGameCommand { get; set; }
        public ICommand LoadGameCommand { get; set; }
        public ICommand SettingsCommand { get; set; }
        public ICommand DeleteFileCommand { get; set; }
        IMenuLogic logic;
        IGameModel model;
        public static bool IsInDesignMode
        {
            get
            {
                var prop = DesignerProperties.IsInDesignModeProperty;
                return (bool)DependencyPropertyDescriptor.FromProperty(prop, typeof(FrameworkElement)).Metadata.DefaultValue;
            }
        }
        public MainWindowViewModel() : this(IsInDesignMode ? null : Ioc.Default.GetService<IMenuLogic>(),Ioc.Default.GetService<IGameModel>())
        {

        }
        public MainWindowViewModel(IMenuLogic logic, IGameModel model)
        {
            Display display = new Display();
            Games = new ObservableCollection<Game>();
            this.logic = logic;
            logic.SetupCollection(Games);
            GameVisibility = Visibility.Collapsed;
            logic.SetUpVisibility(MenuVisibility, GameVisibility);
            NewGameCommand = new RelayCommand(() => //This button opens the new game window
            {
                MenuVisibility = Visibility.Collapsed;
                logic.CreateNewGame();
                MenuVisibility = Visibility.Visible;
            });
            LoadGameCommand = new RelayCommand(
            () => //This button opens the new load game window
            {
                MenuVisibility = Visibility.Collapsed;
                GameVisibility = Visibility.Visible;
                logic.LoadInGame(SelectedGame);

            },
            () => SelectedGame != null);
            DeleteFileCommand = new RelayCommand(
            () => //This button opens the game
            {
                logic.DeleteFile(selectedGame);
            },
            () => SelectedGame != null);
            SettingsCommand = new RelayCommand(() =>
        {
                //TODO
            });
        }
    }
}
