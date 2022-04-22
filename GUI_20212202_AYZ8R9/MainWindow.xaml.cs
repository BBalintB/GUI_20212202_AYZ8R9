using GUI_20212202_AYZ8R9.Logic;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
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
using static GUI_20212202_AYZ8R9.Logic.MainCharacterLogic;

namespace GUI_20212202_AYZ8R9
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        MainCharacterLogic Characterlogic;
        MapLogic Maplogic;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            display.Resize(new Size(grid.ActualWidth, grid.ActualHeight));
            Maplogic = new MapLogic(new System.Windows.Size((int)grid.ActualWidth, (int)grid.ActualHeight));
            display.SetupModel(Maplogic);// Load map           
            display.InvalidateVisual();

            CharacterDisplay.Resize(new Size(grid.ActualWidth, grid.ActualHeight));
            Characterlogic = new MainCharacterLogic();
            Characterlogic.SetupSizes(new System.Windows.Size((int)grid.ActualWidth, (int)grid.ActualHeight), Maplogic);
            Characterlogic.DoingPath = "Idle";
            CharacterDisplay.SetupModel(Characterlogic);
            CharacterDisplay.InvalidateVisual();

            //CharacterDisplay2.Resize(new Size(grid.ActualWidth, grid.ActualHeight));
            //Characterlogic = new MainCharacterLogic();
            //Characterlogic.SetupSizes(new System.Windows.Size((int)grid.ActualWidth, (int)grid.ActualHeight), Maplogic.Blocks);
            //Characterlogic.DoingPath = "Idle";
            //CharacterDisplay2.SetupModel(Characterlogic);
            //CharacterDisplay2.InvalidateVisual();
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Left)
            {
                Characterlogic.Control(MainCharacterLogic.Controls.Left);
            }
            else if (e.Key == Key.Right)
            {
                Characterlogic.Control(MainCharacterLogic.Controls.Right);
            }
            else if (e.Key == Key.Down)
            {
                Characterlogic.Control(MainCharacterLogic.Controls.Down);
            }
            else if (e.Key == Key.Up)
            {
                Characterlogic.Control(MainCharacterLogic.Controls.Up);
            }
            else if (e.Key == Key.A)
            {
                Characterlogic.Control(MainCharacterLogic.Controls.Left);
            }
            else if (e.Key == Key.D)
            {
                Characterlogic.Control(MainCharacterLogic.Controls.Right);
            }
            else if (e.Key == Key.S)
            {
                Characterlogic.Control(MainCharacterLogic.Controls.Down);
            }
            else if (e.Key == Key.W)
            {
                Characterlogic.Control(MainCharacterLogic.Controls.Up);
            }
            else if (e.Key == Key.Space)
            {
                Characterlogic.Control(MainCharacterLogic.Controls.Jump);
            }
            if (Keyboard.IsKeyDown(Key.Right))
            {
                Characterlogic.Control(MainCharacterLogic.Controls.Right);
            }
        }
        private void Window_KeyUp(object sender, KeyEventArgs e)
        {

            if (e.Key == Key.Left)
            {
                Characterlogic.Control(MainCharacterLogic.Controls.Stop);
            }
            else if (e.Key == Key.Right)
            {
                Characterlogic.Control(MainCharacterLogic.Controls.Stop);
            }
            else if (e.Key == Key.A)
            {
                Characterlogic.Control(MainCharacterLogic.Controls.Stop);
            }
            else if (e.Key == Key.D)
            {
                Characterlogic.Control(MainCharacterLogic.Controls.Stop);
            }
            //else if (e.Key == Key.Space)
            //{
            //    Characterlogic.Control(MainCharacterLogic.Controls.Stop);
            //}
        }

        private void Bw_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            //display.Character_Pozition += e.ProgressPercentage;
            CharacterDisplay.InvalidateVisual();
        }

        private void Grid_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {

        }
    }
}
