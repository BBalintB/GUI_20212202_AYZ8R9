using GUI_20212202_AYZ8R9.Helper;
using GUI_20212202_AYZ8R9.Renderer;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Threading;
using static GUI_20212202_AYZ8R9.Logic.MapLogic;

namespace GUI_20212202_AYZ8R9.Logic
{
    public class MainCharacterLogic : ICharacter
    {
        public MainCharacterLogic()
        {

        }

        public event EventHandler Changed;

        public Element[,] Map { get; set; }//<--------Full screen MAP matrix

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

        public void SetupSizes(System.Windows.Size area, Element[,] map)
        {
            this.size = area;
            left_corner = new Position();
            right_corner = new Position();
            Animation_Counter = 1;

            //Set starter position()----------------
            left_corner.Horizontal = 250;
            left_corner.Vertical = 250;
            Right_Corner_Set();
            //--------------------------------------

            DispatcherTimer dt = new DispatcherTimer();
            dt.Interval = TimeSpan.FromMilliseconds(50);
            dt.Tick += Dt_Tick;
            dt.Start();

            this.BWJUMP = new BackgroundWorker();
            this.BWJUMP.DoWork += (obj, ea) => this.JUMP();
            this.BWJUMP.WorkerReportsProgress = true;
            this.BWJUMP.ProgressChanged += Bw_ProgressChanged;

            //DispatcherTimer dt = new DispatcherTimer();
            //dt.Interval = TimeSpan.FromMilliseconds(50);
            //dt.Tick += Dt_Tick;
            //dt.Start();



            //------------------------------------------------------------
            Map = new Element[(int)area.Height, (int)area.Width];

            int Height = (int)(area.Height / map.GetLength(0));
            int Width = (int)(area.Width / map.GetLength(1));

            int bigmapwidthcount = 0;
            int bigmapheightcount = 0;
            
            bool first_time = true;

            using (TextWriter tw = new StreamWriter("matrixsmall.txt"))
            {
                for (int j = 0; j < map.GetLength(0); j++)
                {
                    for (int i = 0; i < map.GetLength(1); i++)
                    {
                        tw.Write(map[j, i] + " ");
                    }
                    tw.WriteLine();
                }
            }

            ;
            for (int i = 0; i < map.GetLength(0); i++)
            {
                
                for (int j = 0; j < map.GetLength(1); j++)
                {
                    
                    if (first_time)
                    {
                        Set(map[i, j],0,1*Height,0,1*Width);
                        first_time = false;
                        ;
                    }
                    else
                    {
                        Set(map[i, j], i*Height, (i+1)*Height, j*Width, (j+1)*Width);
                        ;
                    }
                    
                }
            }
            ;

            using (TextWriter tw = new StreamWriter("matrixbig.txt"))
            {
                for (int j = 0; j < Map.GetLength(0); j++)
                {
                    for (int i = 0; i < Map.GetLength(1); i++)
                    {
                        tw.Write(Map[j, i] + " ");
                    }
                    tw.WriteLine();
                }
            }
            //-------------------------------------------------------------------


            //Method1();
        }

        public void Set(Element element, int lineStart, int lineStop, int columStart, int columStop)
        {
            for (int i = lineStart; i < lineStop; i++)
            {
                for (int j = columStart; j < columStop; j++)
                {
                    this.Map[i, j] = element;
                }
            }
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
                    this.Turn_Right = false;
                    Run_Set(Controls.Left);
                    Changed?.Invoke(this, null);
                    break;
                case Controls.Right:
                    this.Turn_Right = true;
                    Run_Set(Controls.Right);
                    Changed?.Invoke(this, null);
                    break;
                case Controls.Jump:
                    Monitor.Exit(Changed);
                    //if (!BWJUMP.IsBusy)
                    //{
                    //    BWJUMP.RunWorkerAsync();
                    //}
                    Method2();
                    break;
                case Controls.Stop:
                    //Method1();
                    //Method3();
                    break;
                default:
                    break;
            }
            Monitor.Enter(Changed);
            Changed?.Invoke(this, null);
        }


        public void Run_Set(Controls controls)
        {
            MainPath = "Run";
            if (controls == Controls.Left)
            {
                left_corner.Horizontal -= 10/*30.2*/;
                right_corner.Horizontal -= 10/*30.2*/;
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
            this.right_corner.Horizontal = this.left_corner.Horizontal + 56;
            this.right_corner.Vertical = this.left_corner.Vertical + 87;
        }


        public async Task Method1()
        {
            Task.Run(() =>
            {
                while (true)
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
                    //Thread.Sleep(50);
                    Monitor.Enter(Changed);
                    Changed?.Invoke(this, null);
                    Monitor.Exit(Changed);
                }
            });
        }

        public void Method3()
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
            //Thread.Sleep(50);
            //Monitor.Enter(Changed);
            Changed?.Invoke(this, null);
            //Monitor.Exit(Changed);
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
                    Monitor.Enter(Changed);
                    Changed?.Invoke(this, null);
                }
                
            });
            Monitor.Exit(Changed);
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
                
                }
                else
                {
                    left_corner.Vertical = left_corner.Vertical + 10;
                    right_corner.Vertical = right_corner.Vertical + 10;
                
                }
                Jump_Counter++;
            Thread.Sleep(100);
            Monitor.Enter(Changed);
            Changed?.Invoke(this, null);
            Monitor.Exit(Changed);
        }
        #endregion

        private void Dt_Tick(object? sender, EventArgs e)
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
            //Monitor.Enter(Changed);
            Changed?.Invoke(this, null);
            //Monitor.Exit(Changed);
        }

    }
}
