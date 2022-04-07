using GUI_20212202_AYZ8R9.Helper;
using GUI_20212202_AYZ8R9.Renderer;
using System;
using System.Collections.Generic;
using System.ComponentModel;

using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace GUI_20212202_AYZ8R9.Logic
{
    internal class MainCharacterLogic : ICharacter
    {
        public MainCharacterLogic()
        {

        }

        public event EventHandler Changed;

        public char[,] Map { get; set; }//<--------Full screen MAP matrix

        public Position left_corner { get; set; }

        public Position right_corner { get; set; }

        public string MainPath { get; set; }

        public string DoingPath { get; set; }

        System.Windows.Size size;
        public int Animation_Counter { get; set; }

        public int Idle_Animation_Counter { get; set; }

        public int Jump_Counter { get; set; }

        private bool Task_Run { get; set; }
        private bool Turn_Right { get; set; }

        BackgroundWorker BWJUMP;

        public void SetupSizes(System.Windows.Size area)
        {
            this.size = area;
            left_corner = new Position();
            right_corner = new Position();
            Animation_Counter = 1;

            //Set starter position()----------------
            left_corner.Horizontal = 300;
            left_corner.Vertical = 300;
            Right_Corner_Set();
            //--------------------------------------

            this.BWJUMP = new BackgroundWorker();
            this.BWJUMP.DoWork += (obj, ea) => this.JUMP();
            this.BWJUMP.WorkerReportsProgress = true;
            this.BWJUMP.ProgressChanged += Bw_ProgressChanged;

            Map = new char[(int)area.Width, (int)area.Height];
        }


        public enum Controls
        {
            Left, Right, Jump, Stop
        }

        public void Control(Controls control)
        {
            switch (control)
            {
                case Controls.Left:
                    Task_Run = false;
                    this.Turn_Right = false;
                    Run_Set(Controls.Left);
                    Changed?.Invoke(this, null);
                    break;
                case Controls.Right:
                    Task_Run = false;
                    this.Turn_Right = true;
                    Run_Set(Controls.Right);
                    Changed?.Invoke(this, null);
                    break;
                case Controls.Jump:
                    Task_Run = false;
                    BWJUMP.RunWorkerAsync();
                    //Method2();
                    break;
                case Controls.Stop:
                    Task_Run = true;
                    Method1();
                    break;
                default:
                    break;
            }
            //Changed?.Invoke(this, null);
        }


        public void Run_Set(Controls controls)
        {
            MainPath = "Run";
            if (controls == Controls.Left)
            {
                left_corner.Horizontal -= 10;
                right_corner.Horizontal -= 10;
                DoingPath = "Back_Run"; //Animation Image
            }
            else
            {
                left_corner.Horizontal += 10;
                right_corner.Horizontal += 10;
                DoingPath = "Run"; //Animation Image
            }
            if (Animation_Counter >= 6)
            {
                Animation_Counter = 0;
            }
            Animation_Counter++;
            Thread.Sleep(50);
        }

        public void Right_Corner_Set()
        { 
            this.right_corner.Horizontal = this.left_corner.Horizontal + 136;
            this.right_corner.Vertical = this.left_corner.Vertical + 167;
        }


        public async Task Method1()
        {
            Task.Run(() =>
            {
                while (Task_Run)
                {
                    MainPath = "Idle";
                    if (Turn_Right)
                    {
                        DoingPath = "Idle";
                    }
                    else
                    {
                        DoingPath = "Back_Idle";
                    }
                    if (Animation_Counter < 4)
                    {
                        Animation_Counter++;
                    }
                    else
                    {
                        Animation_Counter = 1;
                    }
                    Thread.Sleep(50);
                    //Changed?.Invoke(this, null);
                }
            });
        }

        public async Task Method2()
        {
            Task.Run(() =>
            {
                for (int i = 0; i < 7; i++)
                {
                    if (Jump_Counter >= 7)
                    {
                        Jump_Counter = 1;
                    }
                    left_corner.Horizontal += 10;
                    right_corner.Horizontal += 10;
                    DoingPath = "Run";
                    if (Jump_Counter < 4)
                    {
                        left_corner.Vertical = left_corner.Vertical - 10;
                        right_corner.Vertical = right_corner.Vertical - 10;
                    }
                    else
                    {
                        left_corner.Vertical = left_corner.Vertical + 10;
                        right_corner.Vertical = right_corner.Vertical + 10;
                    }
                    Jump_Counter++;
                    Thread.Sleep(100);
                    //Changed?.Invoke(this, null);
                }          
            });
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
                if (Jump_Counter >= 7)
                {
                    Jump_Counter = 1;
                }
                left_corner.Horizontal += 10;
                right_corner.Horizontal += 10;
                DoingPath = "Run";
                if (Jump_Counter < 4)
                {
                    left_corner.Vertical = left_corner.Vertical - 10;
                    right_corner.Vertical = right_corner.Vertical - 10;
                ;
                }
                else
                {
                    left_corner.Vertical = left_corner.Vertical + 10;
                    right_corner.Vertical = right_corner.Vertical + 10;
                ;
                }
                Jump_Counter++;
            Thread.Sleep(1000);
            Changed?.Invoke(this, null);
            //Changed?.Invoke(this, null);
        }
        #endregion
    }
}
