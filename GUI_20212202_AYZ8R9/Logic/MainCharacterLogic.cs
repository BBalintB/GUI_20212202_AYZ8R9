using GUI_20212202_AYZ8R9.Helper;
using GUI_20212202_AYZ8R9.Models;
using GUI_20212202_AYZ8R9.Renderer;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
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
        public event EventHandler Changed2;
        public Position left_corner { get; set; }

        public Position right_corner { get; set; }

        public string MainPath { get; set; }

        public string DoingPath { get; set; }

        System.Windows.Size size;

        public int Animation_Counter { get; set; }

        public int Jump_Counter { get; set; }

        private bool Task_Run { get; set; }

        private bool JumpIsBusy { get; set; }

        private bool Turn_Right { get; set; }

        public List<Block> blocks { get; set; }

        DispatcherTimer dt = new DispatcherTimer();

        public DispatcherTimer dt2 = new DispatcherTimer();

        public DispatcherTimer dt3 = new DispatcherTimer();

        BackgroundWorker BWJUMP;

        public void SetupSizes(System.Windows.Size area, MapLogic mapLogic)
        {
            MainPath = "Idle";
            this.size = area;
            left_corner = new Position();
            right_corner = new Position();
            Animation_Counter = 1;
            Turn_Right = true;

            this.blocks = new List<Block>();
            this.blocks = mapLogic.Blocks;

            
            dt.Interval = TimeSpan.FromMilliseconds(50);
            dt.Tick += Dt_Tick;
            dt.Start();


            dt3.Interval = TimeSpan.FromMilliseconds(50);
            dt3.Tick += Dt_Jump;

            this.BWJUMP = new BackgroundWorker();
            this.BWJUMP.DoWork += (obj, ea) => this.JUMP();
            this.BWJUMP.WorkerReportsProgress = true;
            this.BWJUMP.ProgressChanged += Bw_ProgressChanged;


            //dt2.Interval = TimeSpan.FromMilliseconds(50);
            dt2.Tick += Dt_Tick2;

            int bigmapwidthcount = 0;
            int bigmapheightcount = 0;

            bool first_time = true;

            //Set starter position()----------------
            for (int i = 0; i < blocks.Count; i++)
            {
                if (blocks[i].BlockType == Element.PRE)
                {
                    left_corner.Horizontal = blocks[i].Positon.X;
                    left_corner.Vertical = blocks[i].Positon.Y;
                    right_corner.Horizontal = blocks[i].Positon.X + blocks[i].Positon.Width;
                    right_corner.Vertical = blocks[i].Positon.Y + blocks[i].Positon.Height;
                }
            }

            Task_Run = true;
            MethodDown();

        }



        public enum Controls
        {
            Left, Right, Down, Up, Jump, Stop
        }

        public void Control(Controls control)
        {
            switch (control)
            {
                case Controls.Left:
                    Run_Set(Controls.Left);
                    break;
                case Controls.Right:
                    Run_Set(Controls.Right);
                    break;
                case Controls.Jump:
                    if (!JumpIsBusy)
                    {
                        MethodJump();
                    }
                    break;
                case Controls.Stop:
                    Animation_Counter = 1;
                    break;
                case Controls.Down:
                    Ladder_Climbing(Controls.Down);
                    break;
                case Controls.Up:
                    Ladder_Climbing(Controls.Up);
                    break;
                default:
                    break;
            }
            Changed?.Invoke(this, null);
        }


        public void Run_Set(Controls controls)
        {
            MainPath = "Run";
            if (controls == Controls.Left)
            {
                bool run = false;
                for (int i = 0; i < blocks.Count; i++)
                {
                    Rect player = new Rect(left_corner.Horizontal, right_corner.Vertical, 0, 0);
                    if (player.IntersectsWith(blocks[i].Positon))
                    {
                        if (blocks[i].BlockType == Element.X 
                            || blocks[i].BlockType == Element.PRE
                            || blocks[i].BlockType == Element.PLAYER
                            || blocks[i].BlockType == Element.W 
                            || blocks[i].BlockType == Element.CH1 
                            || blocks[i].BlockType == Element.CH
                            || blocks[i].BlockType == Element.L)
                        {
                            run = true;
                        }
                        else
                        {
                            run = false;
                            //break;
                        }
                    }
                }
                if (run)
                {
                    this.Turn_Right = false;
                    left_corner.Horizontal -= 5/*30.2*/;
                    right_corner.Horizontal -= 5/*30.2*/;
                    DoingPath = "Back_Run"; //Animation Image

                    if (Animation_Counter >= 6)
                    {
                        Animation_Counter = 0;
                    }
                    Animation_Counter++;
                }
            }
            else
            {
                bool run = false;
                for (int i = 0; i < blocks.Count; i++)
                {
                    Rect player = new Rect(left_corner.Horizontal, left_corner.Vertical, right_corner.Horizontal - left_corner.Horizontal, right_corner.Vertical - left_corner.Vertical);
                    if (player.IntersectsWith(blocks[i].Positon))
                    {
                        if (blocks[i].BlockType == Element.X 
                            || blocks[i].BlockType == Element.PRE 
                            || blocks[i].BlockType == Element.PLAYER 
                            || blocks[i].BlockType == Element.W 
                            || blocks[i].BlockType == Element.CH1 
                            || blocks[i].BlockType == Element.CH
                            || blocks[i].BlockType == Element.L)
                        {
                            run = true;
                        }
                        else
                        {
                            run = false;
                            //break;
                        }
                    }
                }
                if (run)
                {
                    this.Turn_Right = true;
                    left_corner.Horizontal += 5;
                    right_corner.Horizontal += 5;
                    DoingPath = "Run"; //Animation Image

                    if (Animation_Counter >= 6)
                    {
                        Animation_Counter = 0;
                    }
                    Animation_Counter++;
                }
            }
            Thread.Sleep(50);
        }

        public void Right_Corner_Set(int Height, int Width)
        {
            int startHorizontal = 0;
            int startVertical = 0;
            //this.right_corner.Horizontal = startHorizontal;
            //this.right_corner.Vertical = startVertical;
            this.right_corner.Horizontal = this.left_corner.Horizontal + Width;
            this.right_corner.Vertical = this.left_corner.Vertical + Height * 1.3;
        }

        private void Ladder_Climbing(Controls controls)
        {
            dt.Stop();
            if (controls == Controls.Down)
            {
                bool down = false;
                for (int i = 0; i < blocks.Count; i++)
                {
                    Rect player = new Rect(left_corner.Horizontal, right_corner.Vertical, 5, 5);
                    if (player.IntersectsWith(blocks[i].Positon))
                    {
                        if (/*blocks[i].BlockType == Element.X*/
                        //    || blocks[i].BlockType == Element.PRE
                        //    || blocks[i].BlockType == Element.W
                        //    || blocks[i].BlockType == Element.CH1
                        //    || blocks[i].BlockType == Element.CH
                            /*||*/ blocks[i].BlockType == Element.L)
                        {
                            MainPath = "Climb";
                            down = true;
                        }
                        else
                        {
                            down = false;
                            //break;
                        }
                    }
                }
                if (down)
                {
                    this.Turn_Right = false;
                    left_corner.Vertical += 5/*30.2*/;
                    right_corner.Vertical += 5/*30.2*/;
                    DoingPath = "Climb"; //Animation Image

                    if (Animation_Counter >= 6)
                    {
                        Animation_Counter = 0;
                    }
                    Animation_Counter++;
                }
            }
            else
            {
                bool up = false;
                for (int i = 0; i < blocks.Count; i++)
                {
                    Rect player = new Rect(left_corner.Horizontal, left_corner.Vertical, right_corner.Horizontal - left_corner.Horizontal, right_corner.Vertical - left_corner.Vertical);
                    if (player.IntersectsWith(blocks[i].Positon))
                    {
                        if (//blocks[i].BlockType == Element.X 
                        //    || blocks[i].BlockType == Element.PRE 
                        //    || blocks[i].BlockType == Element.PLAYER 
                        //    || blocks[i].BlockType == Element.W 
                        //    || blocks[i].BlockType == Element.CH1 
                        //    || blocks[i].BlockType == Element.CH 
                            /*||*/ blocks[i].BlockType == Element.L)
                        {
                            MainPath = "Climb";
                            up = true;
                        }
                        else
                        {
                            up = false;
                            //break;
                        }
                    }
                }
                if (up)
                {
                    left_corner.Vertical -= 5;
                    right_corner.Vertical -= 5;
                    DoingPath = "Climb"; //Animation Image

                    if (Animation_Counter >= 6)
                    {
                        Animation_Counter = 0;
                    }
                    Animation_Counter++;
                }
            }
            dt.Start();
            Thread.Sleep(50);
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
                    //Thread.Sleep(50);
                    Monitor.Enter(Changed);
                    Changed?.Invoke(this, null);
                    Monitor.Exit(Changed);
                }
            });
        }

        public async Task MethodJump()
        {
            this.JumpIsBusy = true;
            Task.Run(() =>
            {
                Task_Run = false;
                for (int i = 0; i < 4; i++)
                {
                    if (Jump_Counter > 4)
                    {
                        Jump_Counter = 1;

                    }
                        if (right_corner.Vertical - 10 > 0)
                        {
                            right_corner.Vertical -= 15;
                            left_corner.Vertical -= 15;
                            MainPath = "Jump";
                            if (Turn_Right)
                            {
                                DoingPath = "Jump";
                            }
                            else
                            {
                                DoingPath = "Back_Jump";
                            }
                            if (Animation_Counter < 4)
                            {
                                Animation_Counter++;
                            }
                            else
                            {
                                Animation_Counter = 1;
                            }
                            Changed2?.Invoke(this, null);
                            Jump_Counter++;
                    }
                    Thread.Sleep(100);
                }
                Task_Run = true;

            });
            Thread.Sleep(100);
            this.JumpIsBusy = false;
        }

        public async Task MethodDown()
        {
            Task.Run(() =>
            {
                while (true)
                {
                    bool down = true;
                    Rect player = new Rect(left_corner.Horizontal+4, left_corner.Vertical + 1, right_corner.Horizontal - left_corner.Horizontal-8, right_corner.Vertical - left_corner.Vertical);
                    for (int i = 0; i < blocks.Count; i++)
                    {
                        if (player.IntersectsWith(blocks[i].Positon) 
                        && blocks[i].BlockType != Element.X 
                        && blocks[i].BlockType != Element.W
                        && blocks[i].BlockType != Element.CH
                        && blocks[i].BlockType != Element.CH1)
                        {
                            down = false;
                        }
                    }
                    if (down)
                    {
                        right_corner.Vertical += 1;
                        left_corner.Vertical += 1;
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
                        Thread.Sleep(5);
                        //Monitor.Enter(Changed);
                        Changed2?.Invoke(this, null);
                        //Monitor.Exit(Changed);

                    }

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


        private void Dt_Tick2(object? sender, EventArgs e)
        {
            bool down = true;
            Rect player = new Rect(left_corner.Horizontal, left_corner.Vertical+1, right_corner.Horizontal - left_corner.Horizontal, right_corner.Vertical - left_corner.Vertical);
            for (int i = 0; i < blocks.Count; i++)
            {
                if (player.IntersectsWith(blocks[i].Positon) && blocks[i].BlockType != Element.X)
                {
                    down = false;
                }
            }
            if (down)
            {
                right_corner.Vertical += 1;
                left_corner.Vertical += 1;
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
                Thread.Sleep(5);
                //Monitor.Enter(Changed);
                Changed2?.Invoke(this, null);
                //Monitor.Exit(Changed);

            }
        }

        private void Dt_Jump(object? sender, EventArgs e)
        {
            if (Jump_Counter > 4)
            {
                //dt2.Start();
                Jump_Counter = 1;
                dt3.Stop();
            }
            else
            {
                if (right_corner.Vertical - 5 > 0)
                {
                    dt2.Stop();
                    right_corner.Vertical -= 5;
                    left_corner.Vertical -= 5;
                    MainPath = "Jump";
                    if (Turn_Right)
                    {
                        DoingPath = "Jump";
                    }
                    else
                    {
                        DoingPath = "Back_Jump";
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
                    Changed?.Invoke(this, null);
                    Jump_Counter++;
                }
            }
        }
    }
}
