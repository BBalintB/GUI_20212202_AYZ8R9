using GUI_20212202_AYZ8R9.Logic.MenuOptionsLogic;
using GUI_20212202_AYZ8R9.Models;
using GUI_20212202_AYZ8R9.ViewModels.MenuOptionsWindowViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace GUI_20212202_AYZ8R9.MenuOptionsWindows
{
    /// <summary>
    /// Interaction logic for NewGameWindow.xaml
    /// </summary>
    public partial class NewGameWindow : Window
    {
        public NewGameWindow(Game game, INewGameLogic logic)
        {
            InitializeComponent();
            this.DataContext = new NewGameWindowViewModel(logic);
            (this.DataContext as NewGameWindowViewModel).Setup(game);
            
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            if (DataContext is IConncetion vm)
            {
                vm.FreshInfo += () =>
                {
                    foreach (var item in stack_editor.Children)
                    {
                        if (item is TextBox t)
                        {
                            t.GetBindingExpression(TextBox.TextProperty).UpdateSource();
                        }
                    }
                };
                vm.Close += () =>
                {
                    this.DialogResult = true;
                };
            }
        }
    }
}
