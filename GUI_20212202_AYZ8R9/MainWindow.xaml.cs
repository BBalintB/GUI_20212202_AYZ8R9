using GUI_20212202_AYZ8R9.Logic;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace GUI_20212202_AYZ8R9
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            //display.SetupModel();
        }

        private void Window_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            display.Resize(new Size(grid.ActualWidth, grid.ActualHeight));
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            display.Resize(new Size(grid.ActualWidth, grid.ActualHeight));
            display.InvalidateVisual();
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
        //    switch (e.Key)
        //    {
        //        case Key.Right:
        //            display.Character_Pozition+=10;
        //            break;
        //        case Key.Left:
        //                display.Character_Pozition-=10;
        //            break;
        //        case Key.Space:
        //            display.jump = true;
        //            display.Character_Pozition += 10;
        //            break;
        //        case Key.LeftShift:
        //            display.Character_Pozition += 10;
        //            break;
        //        default:
        //            break;
        //    }

        //    display.Resize(new Size(grid.ActualWidth, grid.ActualHeight));
        //    display.InvalidateVisual();
        }

        private void Window_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            switch (e.Key)
            {
                case Key.Right:
                    display.Character_Pozition += 10;
                    break;
                case Key.Left:
                    display.Character_Pozition -= 10;
                    break;
                case Key.Space:
                    display.jump = true;
                    display.Character_Pozition += 10;
                    break;
                case Key.LeftShift:
                    display.Character_Pozition += 10;
                    break;
                default:
                    break;
            }

            display.Resize(new Size(grid.ActualWidth, grid.ActualHeight));
            display.InvalidateVisual();
            MapLogic logic = new MapLogic();
            display.SetupModel(logic);
        }
    }
}
