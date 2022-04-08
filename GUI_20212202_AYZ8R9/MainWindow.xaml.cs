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
        MainCharacterLogic logic;

        public MainWindow()
        {
            InitializeComponent();
            MapLogic logic = new MapLogic();
            display.SetupModel(logic);
            //display.SetupModel();
            this.BWJUMP = new BackgroundWorker();
            this.BWJUMP.DoWork += (obj, ea) => this.JUMP();
            this.BWJUMP.WorkerReportsProgress = true;
            this.BWJUMP.ProgressChanged += Bw_ProgressChanged;
        }

        BackgroundWorker BWJUMP;

        private void Window_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            //display.Resize(new Size(grid.ActualWidth, grid.ActualHeight));
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            
            display.Resize(new Size(grid.ActualWidth, grid.ActualHeight));
            logic = new MainCharacterLogic();
            logic.SetupSizes(new System.Windows.Size((int)grid.ActualWidth, (int)grid.ActualHeight));
            logic.DoingPath = "Run";
            display.SetupModel(logic);
            display.InvalidateVisual();          
            //display.Resize(new Size(grid.ActualWidth, grid.ActualHeight));
            //display.InvalidateVisual();
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Left)
            {
                logic.Control(MainCharacterLogic.Controls.Left);
            }
            else if (e.Key == Key.Right)
            {
                logic.Control(MainCharacterLogic.Controls.Right);
            }
            else if (e.Key == Key.A)
            {
                logic.Control(MainCharacterLogic.Controls.Left);
            }
            else if (e.Key == Key.D)
            {
                logic.Control(MainCharacterLogic.Controls.Right);
            }
            else if (e.Key == Key.Space)
            {
                logic.Control(MainCharacterLogic.Controls.Jump);
            }
        }

        private void Window_KeyUp(object sender, KeyEventArgs e)
        {

            if (e.Key == Key.Left)
            {
                logic.Control(MainCharacterLogic.Controls.Stop);
            }
            else if (e.Key == Key.Right)
            {
                logic.Control(MainCharacterLogic.Controls.Stop);
            }
            else if (e.Key == Key.A)
            {
                logic.Control(MainCharacterLogic.Controls.Stop);
            }
            else if (e.Key == Key.D)
            {
                logic.Control(MainCharacterLogic.Controls.Stop);
            }
            else if (e.Key == Key.Space)
            {
                logic.Control(MainCharacterLogic.Controls.Stop);
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
            display.InvalidateVisual();
        }
        #endregion

    }
}
