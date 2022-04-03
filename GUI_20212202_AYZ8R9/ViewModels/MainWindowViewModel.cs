using GUI_20212202_AYZ8R9.Logic;
using GUI_20212202_AYZ8R9.MenuOptionsWindows;
using Microsoft.Toolkit.Mvvm.DependencyInjection;
using Microsoft.Toolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace GUI_20212202_AYZ8R9.ViewModels
{
    public class MainWindowViewModel
    {
        public ICommand NewGameCommand{ get; set; }
        public ICommand LoadGameCommand{ get; set; }
        public ICommand SettingsCommand{ get; set; }
        IMenuLogic logic;
        public static bool IsInDesignMode
        {
            get
            {
                var prop = DesignerProperties.IsInDesignModeProperty;
                return (bool)DependencyPropertyDescriptor.FromProperty(prop, typeof(FrameworkElement)).Metadata.DefaultValue;
            }
        }
        public MainWindowViewModel():this(IsInDesignMode?null:Ioc.Default.GetService<IMenuLogic>())
        {

        }
        public MainWindowViewModel(IMenuLogic logic)
        {
            this.logic = logic;
            NewGameCommand = new RelayCommand(() => //This button opens the new game window
            {
                logic.CreateNewGame();
            });
            LoadGameCommand = new RelayCommand(() => //This button opens the new load game window
            {
                logic.LoadInGame();
            });
            SettingsCommand = new RelayCommand(() =>
            {
                //TODO
            });
        }
    }
}
