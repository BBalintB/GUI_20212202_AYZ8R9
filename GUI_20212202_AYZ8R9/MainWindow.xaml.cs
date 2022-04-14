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

            this.BWJUMP = new BackgroundWorker();
            this.BWJUMP.DoWork += (obj, ea) => this.JUMP();
            this.BWJUMP.WorkerReportsProgress = true;
            this.BWJUMP.ProgressChanged += Bw_ProgressChanged;
        }

        BackgroundWorker BWJUMP;

        private void Window_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            CharacterDisplay.Resize(new Size(grid.ActualWidth, grid.ActualHeight));
            display.Resize(new Size(grid.ActualWidth, grid.ActualHeight));
            display.InvalidateVisual();

        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            display.Resize(new Size(grid.ActualWidth, grid.ActualHeight));
            this.Maplogic = new MapLogic();
            display.SetupModel(Maplogic);// Load map           
            display.InvalidateVisual();

            CharacterDisplay.Resize(new Size(grid.ActualWidth, grid.ActualHeight));
            Characterlogic = new MainCharacterLogic();
            Characterlogic.SetupSizes(new System.Windows.Size((int)grid.ActualWidth, (int)grid.ActualHeight), Maplogic.GameMatrix /*I need the game matrix*/);
            Characterlogic.DoingPath = "Run";
            CharacterDisplay.SetupModel(Characterlogic);
            CharacterDisplay.InvalidateVisual();          
            display.InvalidateVisual();
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
            else if (e.Key == Key.A)
            {
                Characterlogic.Control(MainCharacterLogic.Controls.Left);
            }
            else if (e.Key == Key.D)
            {
                Characterlogic.Control(MainCharacterLogic.Controls.Right);
            }
            else if (e.Key == Key.Space)
            {
                Characterlogic.Control(MainCharacterLogic.Controls.Jump);
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
            else if (e.Key == Key.Space)
            {
                Characterlogic.Control(MainCharacterLogic.Controls.Stop);
            }
        }

        #region BW Tasks

        public void JUMP()
        {
            for (int i = 0; i < 7; i++)
            {
                //display.jump = true;
                this.BWJUMP.ReportProgress(10);
                Thread.Sleep(50);
            }
        }

        private void Bw_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            //display.Character_Pozition += e.ProgressPercentage;
            CharacterDisplay.InvalidateVisual();
        }
        #endregion

        private void grid_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            CharacterDisplay.Resize(new Size(grid.ActualWidth, grid.ActualHeight));
        }
    }
}
