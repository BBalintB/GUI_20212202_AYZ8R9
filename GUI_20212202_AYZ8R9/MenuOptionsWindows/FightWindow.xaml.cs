using GUI_20212202_AYZ8R9.Models;
using GUI_20212202_AYZ8R9.ViewModels;
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
    /// Interaction logic for FightWindow.xaml
    /// </summary>
    public partial class FightWindow : Window
    {
        public FightWindow(Game game)
        {
            InitializeComponent();
            this.DataContext =new FightWindowViewModel();
            (this.DataContext as FightWindowViewModel).Setup(game);
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            if (DataContext is IWhoWin vm)
            {
                vm.CloseHeroWin += () =>
                {
                    this.DialogResult = true;
                    this.Close();
                };

                vm.CloseVillianWin += () => 
                {
                    this.Close();
                };
            }
        }
    }
}
