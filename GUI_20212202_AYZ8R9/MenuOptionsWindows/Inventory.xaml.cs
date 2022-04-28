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
    /// Interaction logic for Inventory.xaml
    /// </summary>
    public partial class Inventory : Window
    {
        public Inventory(Game game)
        {
            InitializeComponent();
            this.DataContext = new InventoryWindowViewModel();
            (this.DataContext as InventoryWindowViewModel).Setup(game);
        }
    }
}
