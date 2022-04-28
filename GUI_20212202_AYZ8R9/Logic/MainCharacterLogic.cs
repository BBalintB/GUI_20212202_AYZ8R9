using GUI_20212202_AYZ8R9.Helper;
using GUI_20212202_AYZ8R9.MenuOptionsWindows;
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

        System.Windows.Size Charactersize;
        System.Windows.Size Areasize;

        public int Animation_Counter { get; set; }

        public int Jump_Counter { get; set; }

        private bool Task_Run { get; set; }

        private bool JumpIsBusy { get; set; }

        private bool Turn_Right { get; set; }

        private int RunSpeed { get; set; }

        public List<Block> blocks { get; set; }

        DispatcherTimer dt = new DispatcherTimer();

        public string CharacterType { get; set; }

        private MapLogic MapLogic { get; set; }

        private Game game { get; set; }

        public void SetupSizes(System.Windows.Size area, MapLogic mapLogic, Game game)
        {
            //CharacterType = game.Hero.HeroType.ToString();
            CharacterType = game.Hero.HeroType.ToString();
            MainPath = "Idle";
            this.game = game;
            this.Areasize = area;
            left_corner = new Position();
            right_corner = new Position();
            Animation_Counter = 1;
            Turn_Right = true;

            this.blocks = new List<Block>();
            this.blocks = mapLogic.Blocks;
            this.MapLogic = mapLogic;

            dt.Interval = TimeSpan.FromMilliseconds(50);
            dt.Tick += Dt_Tick;
            dt.Start();

            LoadFirstMap();

            this.RunSpeed = 50;

            Task_Run = true;
            MethodDown();
            //MethodLoad();
        }



        public enum Controls
        {
            Left, Right, Down, Up, Jump, Stop, Run, RunStop
        }

        public void Control(Controls control)
        {
            dt.Stop();
            if (this.Charactersize.Width != right_corner.Horizontal - left_corner.Horizontal && this.Charactersize.Height != right_corner.Vertical - left_corner.Vertical)
            {
                right_corner.Horizontal = left_corner.Horizontal + this.Charactersize.Width;
                right_corner.Vertical = left_corner.Vertical + this.Charactersize.Height;
            }
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
                        dt.Stop();
                        MethodJump();
                    }
                    break;
                case Controls.Stop:
                    Animation_Counter = 1;
                    dt.Start();
                    break;
                case Controls.Down:
                    Ladder_Climbing(Controls.Down);
                    break;
                case Controls.Up:
                    Ladder_Climbing(Controls.Up);
                    break;
                case Controls.Run:
                    this.RunSpeed = 10;
                    break;
                case Controls.RunStop:
                    this.RunSpeed = 50;
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
                bool inx = true;
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
                            || blocks[i].BlockType == Element.L
                            || blocks[i].BlockType == Element.NE
                            || blocks[i].BlockType == Element.Z
                            || blocks[i].BlockType == Element.HOME
                            || blocks[i].BlockType == Element.EN
                            || blocks[i].BlockType == Element.END
                            || blocks[i].BlockType == Element.HC
                            || blocks[i].BlockType == Element.HOMEB)
                        {
                            if (blocks[i].BlockType == Element.NE)
                            {
                                LoadNextMap();
                            }
                            if (blocks[i].BlockType == Element.PRE)
                            {
                                LoadPreviousMap();
                            }
                            if (blocks[i].BlockType == Element.EN)
                            {
                                new FightWindow(this.game).ShowDialog();
                            }
                            if (blocks[i].BlockType == Element.CH || blocks[i].BlockType == Element.CH1)
                            {
                                
                            }
                            run = true;
                            
                        }
                        else
                        {
                            run = false;
                        }
                        inx = false;
                    }
                }
                if ((run || inx) && 0 < left_corner.Horizontal - 5)
                {
                    this.Turn_Right = false;
                    left_corner.Horizontal -= 5;
                    right_corner.Horizontal -= 5;
                    DoingPath = "Back_Run";

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
                bool inx = true;
                for (int i = 0; i < blocks.Count; i++)
                {
                    Rect player = new Rect(right_corner.Horizontal, right_corner.Vertical-1,1 /*right_corner.Horizontal - left_corner.Horizontal*/,1 /*right_corner.Vertical - left_corner.Vertical*/);
                    if (player.IntersectsWith(blocks[i].Positon))
                    {
                        if (
                            blocks[i].BlockType == Element.PRE
                            || blocks[i].BlockType == Element.PLAYER
                            || blocks[i].BlockType == Element.W
                            || blocks[i].BlockType == Element.CH1
                            || blocks[i].BlockType == Element.CH
                            || blocks[i].BlockType == Element.L
                            || blocks[i].BlockType == Element.NE
                            || blocks[i].BlockType == Element.Z
                            || blocks[i].BlockType == Element.HOME 
                            || blocks[i].BlockType == Element.EN
                            || blocks[i].BlockType == Element.END
                            || blocks[i].BlockType == Element.HC
                            || blocks[i].BlockType == Element.HOMEB)
                        {
                            run = true;
                            if (blocks[i].BlockType == Element.NE)
                            {
                                LoadNextMap();
                            }
                            if (blocks[i].BlockType == Element.PRE)
                            {
                                LoadPreviousMap();
                            }
                        }
                        else
                        {
                            run = false;
                        }
                        inx = false;
                    }
                }
                if ((run || inx) && Areasize.Width > right_corner.Horizontal + 5)
                {
                    this.Turn_Right = true;
                    left_corner.Horizontal += 5;
                    right_corner.Horizontal += 5;
                    DoingPath = "Run";

                    if (Animation_Counter >= 6)
                    {
                        Animation_Counter = 0;
                    }
                    Animation_Counter++;
                }
            }
            Thread.Sleep(RunSpeed);
        }

        public void LoadFirstMap()
        {
            this.MapLogic.LoadFirstMap();
            this.blocks = MapLogic.Blocks;
            for (int i = 0; i < blocks.Count; i++)
            {
                if (blocks[i].BlockType == Element.PLAYER)
                {
                    left_corner.Horizontal = blocks[i].Positon.X;
                    left_corner.Vertical = blocks[i].Positon.Y - 2;
                    right_corner.Horizontal = blocks[i].Positon.X + blocks[i].Positon.Width;
                    right_corner.Vertical = blocks[i].Positon.Y + blocks[i].Positon.Height - 2;
                    this.Charactersize = new Size(right_corner.Horizontal - left_corner.Horizontal, right_corner.Vertical - left_corner.Vertical);
                }
            }
            this.game.Hero.MapPosition = this.MapLogic.ActualMapNumber;
            Changed2?.Invoke(this, null);
        }

        public void LoadNextMap()
        {
            this.MapLogic.LoadNextRightMap();
            this.blocks = MapLogic.Blocks;
            for (int i = 0; i < blocks.Count; i++)
            {
                if (blocks[i].BlockType == Element.PRE)
                {
                    left_corner.Horizontal = blocks[i].Positon.X+50;
                    left_corner.Vertical = blocks[i].Positon.Y-2;
                    right_corner.Horizontal = blocks[i].Positon.X + blocks[i].Positon.Width+50;
                    right_corner.Vertical = blocks[i].Positon.Y + blocks[i].Positon.Height-2;
                    this.Charactersize = new Size(right_corner.Horizontal - left_corner.Horizontal, right_corner.Vertical- left_corner.Vertical); 
                }
            }
            this.game.Hero.MapPosition = this.MapLogic.ActualMapNumber;
            Changed2?.Invoke(this, null);
        }

        public void LoadPreviousMap()
        {
            this.MapLogic.LoadNextLeftMap();
            this.blocks = MapLogic.Blocks;
            for (int i = 0; i < blocks.Count; i++)
            {
                if (blocks[i].BlockType == Element.NE)
                {
                    left_corner.Horizontal = blocks[i].Positon.X-50;
                    left_corner.Vertical = blocks[i].Positon.Y-2;
                    right_corner.Horizontal = blocks[i].Positon.X + blocks[i].Positon.Width-50;
                    right_corner.Vertical = blocks[i].Positon.Y + blocks[i].Positon.Height-2;
                    this.Charactersize = new Size(right_corner.Horizontal - left_corner.Horizontal, right_corner.Vertical - left_corner.Vertical);
                }
            }
            this.game.Hero.MapPosition = this.MapLogic.ActualMapNumber;
            Changed2?.Invoke(this, null);
        }

        public void OpenChest()
        { 
            // todo
        }

        public void OpenHomeChest()
        {
            // todo
        }

        public void OpenAttackWindow()
        {
            // todo
        }
        private void Ladder_Climbing(Controls controls)
        {
            if (controls == Controls.Down)
            {
                bool down = false;
                for (int i = 0; i < blocks.Count; i++)
                {
                    Rect player = new Rect(left_corner.Horizontal + (right_corner.Horizontal - left_corner.Horizontal) / 2, right_corner.Vertical, 10, 5);
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
                    Rect player = new Rect(left_corner.Horizontal+(right_corner.Horizontal - left_corner.Horizontal)/ 2, left_corner.Vertical, 1, right_corner.Vertical- left_corner.Vertical);
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
            Thread.Sleep(50);
        }

        //public async Task Method1()
        //{
        //    Task.Run(() =>
        //    {
        //        while (Task_Run)
        //        {
        //            MainPath = "Idle";
        //            if (Turn_Right)
        //            {
        //                DoingPath = "Idle";
        //            }
        //            else
        //            {
        //                DoingPath = "Back_Idle";
        //            }
        //            if (Animation_Counter < 4)
        //            {
        //                Animation_Counter++;
        //            }
        //            else
        //            {
        //                Animation_Counter = 1;
        //            }
        //            //Thread.Sleep(50);
        //            Monitor.Enter(Changed);
        //            Changed?.Invoke(this, null);
        //            Monitor.Exit(Changed);
        //        }
        //    });
        //}

        public async Task MethodJump()
        {
            this.JumpIsBusy = true;
            Task.Run(() =>
            {
                Task_Run = false;
                for (int i = 0; i < 4; i++)
                {
                    if (right_corner.Vertical - 10 > 0)
                    {
                        right_corner.Vertical -= 9;
                        left_corner.Vertical -= 9;
                        MainPath = "Jump";
                        if (Turn_Right)
                        {
                            DoingPath = "Jump";
                        }
                        else
                        {
                            DoingPath = "Back_Jump";
                        }
                        Animation_Counter = i + 1;
                        Changed2?.Invoke(this, null);
                    }
                    Thread.Sleep(50);
                }
                Task_Run = true;
                MethodDown();
                dt.Start();
            });
        }

        public async Task MethodDown()
        {
            Task.Run(() =>
            {
                while (Task_Run)
                {
                    bool down = true;
                    Rect player = new Rect(left_corner.Horizontal+6, left_corner.Vertical + 2, right_corner.Horizontal - left_corner.Horizontal-12, right_corner.Vertical - left_corner.Vertical);
                    for (int i = 0; i < blocks.Count; i++)
                    {
                        if (player.IntersectsWith(blocks[i].Positon)
                        && blocks[i].BlockType != Element.X
                        && blocks[i].BlockType != Element.W
                        && blocks[i].BlockType != Element.CH
                        && blocks[i].BlockType != Element.PRE
                        && blocks[i].BlockType != Element.NE
                        && blocks[i].BlockType != Element.CH1
                        && blocks[i].BlockType != Element.Z
                        && blocks[i].BlockType != Element.PLAYER
                        && blocks[i].BlockType != Element.HOMEB)
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
                        Thread.Sleep(5);
                        //Monitor.Enter(Changed);
                        Changed2?.Invoke(this, null);
                        //Monitor.Exit(Changed);
                    }
                    else
                    {
                        this.JumpIsBusy = false;
                    }
                    Thread.Sleep(1);
                }
            });
        }

        public async Task MethodLoad()
        {
            Task.Run(() =>
            {
                while (true)
                {
                    Changed2?.Invoke(this, null);
                    //Changed?.Invoke(this, null);
                }
            });
        }

        #region BW Tasks


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
                if (Animation_Counter < 4)
                {
                    Animation_Counter++;
                }
                else
                {
                    Animation_Counter = 1;
                }
                DoingPath = "Idle";
            }
            else
            {
                if (Animation_Counter < 4)
                {
                    Animation_Counter++;
                }
                else
                {
                    Animation_Counter = 1;
                }
                DoingPath = "Back_Idle";
            }
            Thread.Sleep(50);
            //Monitor.Enter(Changed);
            Changed?.Invoke(this, null);
            //Monitor.Exit(Changed);
        }


        //private void Dt_Tick2(object? sender, EventArgs e)
        //{
        //    bool down = true;
        //    Rect player = new Rect(left_corner.Horizontal, left_corner.Vertical + 1, right_corner.Horizontal - left_corner.Horizontal, right_corner.Vertical - left_corner.Vertical);
        //    for (int i = 0; i < blocks.Count; i++)
        //    {
        //        if (player.IntersectsWith(blocks[i].Positon) && blocks[i].BlockType != Element.X)
        //        {
        //            down = false;
        //        }
        //    }
        //    if (down)
        //    {
        //        right_corner.Vertical += 1;
        //        left_corner.Vertical += 1;
        //        MainPath = "Idle";
        //        if (Turn_Right)
        //        {
        //            DoingPath = "Idle";
        //        }
        //        else
        //        {
        //            DoingPath = "Back_Idle";
        //        }
        //        if (Animation_Counter < 4)
        //        {
        //            Animation_Counter++;
        //        }
        //        else
        //        {
        //            Animation_Counter = 1;
        //        }
        //        Thread.Sleep(5);
        //        //Monitor.Enter(Changed);
        //        Changed2?.Invoke(this, null);
        //        //Monitor.Exit(Changed);

        //    }
        //}
    }
}
