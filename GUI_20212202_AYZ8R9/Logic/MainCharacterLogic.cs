using GUI_20212202_AYZ8R9.Helper;
using GUI_20212202_AYZ8R9.MenuOptionsWindows;
using GUI_20212202_AYZ8R9.Models;
using GUI_20212202_AYZ8R9.Renderer;
using GUI_20212202_AYZ8R9.ViewModels;
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
        private bool Task_Idle { get; set; }

        private bool JumpIsBusy { get; set; }

        public bool Turn_Right { get; set; }

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
            dt.Tick += Dt_Idle;
            dt.Start();

            LoadFirstMap();

            this.RunSpeed = 50;

            Task_Run = true;
            MethodDown();
            MethodFix();
            //MethodIdle();
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
                left_corner.Horizontal = right_corner.Horizontal - this.Charactersize.Width;
                left_corner.Vertical = right_corner.Vertical - this.Charactersize.Height;
            }
            switch (control)
            {
                case Controls.Left:
                    this.Task_Idle = false;
                    Run_Set(Controls.Left);
                    break;
                case Controls.Right:
                    this.Task_Idle = false;
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
                    Task_Run = true;
                    Task_Idle = true;
                    MethodDown();
                    dt.Start();
                    break;
                case Controls.Down:
                    Task_Idle = false;
                    Ladder_Climbing(Controls.Down);
                    break;
                case Controls.Up:
                    Task_Idle = false;
                    Ladder_Climbing(Controls.Up);
                    break;
                case Controls.Run:
                    this.RunSpeed = 10;
                    break;
                case Controls.RunStop:
                    this.RunSpeed = 50;
                    //if (!Task_Run)
                    //{
                    //    Task_Run = true;
                    //    Task_Idle = true;
                    //    //MethodIdle();
                    //    MethodDown();
                    //}
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
                        if (RunCanExecute(i))
                        {
                            if (blocks[i].BlockType == Element.NE)
                            {
                                if (this.game.Hero.Battles[this.MapLogic.ActualMapNumber] == true)
                                {
                                    LoadNextMap();
                                }
                            }
                            if (blocks[i].BlockType == Element.PRE)
                            {
                                LoadPreviousMap();
                            }
                            if (blocks[i].BlockType == Element.EN)
                            {

                                if (this.game.Hero.Battles[this.MapLogic.ActualMapNumber] == false)
                                {
                                    FightWindow a = new FightWindow(this.game);
                                    bool tmp = (bool)a.ShowDialog();
                                    //(a.DataContext as FightWindowViewModel).
                                    this.game.Hero.Battles[this.MapLogic.ActualMapNumber] = tmp;
                                    this.game.FileLastSaveDate = DateTime.Now.ToString();
                                    Save.WriteOutJSON(game, Path.Combine("Games", game.FileName + ".json"));
                                }
                            }
                            if (blocks[i].BlockType == Element.CH || blocks[i].BlockType == Element.CH1)
                            {
                                if (game.Hero.Chests[this.MapLogic.ActualMapNumber] ==false )
                                {
                                    game.Hero.Inventory.Add(RandomUtil.GetARandomWeapon());
                                    game.Hero.Chests[this.MapLogic.ActualMapNumber]= true;
                                }
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
                    Rect player = new Rect(right_corner.Horizontal, right_corner.Vertical - 1, 1 /*right_corner.Horizontal - left_corner.Horizontal*/, 1 /*right_corner.Vertical - left_corner.Vertical*/);
                    if (player.IntersectsWith(blocks[i].Positon))
                    {
                        if (RunCanExecute(i))
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
                            if (blocks[i].BlockType == Element.EN && !this.game.Hero.Battles[this.MapLogic.ActualMapNumber])
                            {

                                if (this.game.Hero.Battles[this.MapLogic.ActualMapNumber] == false)
                                {
                                    bool tmp = (bool)new FightWindow(this.game).ShowDialog();
                                    this.game.Hero.Battles[this.MapLogic.ActualMapNumber] = tmp;
                                    this.game.FileLastSaveDate = DateTime.Now.ToString();
                                    Save.WriteOutJSON(game, Path.Combine("Games", game.FileName + ".json"));
                                }
                            }
                            if (blocks[i].BlockType == Element.CH || blocks[i].BlockType == Element.CH1)
                            {
                                if (game.Hero.Chests[this.MapLogic.ActualMapNumber] == false)
                                {
                                    game.Hero.Inventory.Add(RandomUtil.GetARandomWeapon());
                                    game.Hero.Chests[this.MapLogic.ActualMapNumber] = true;
                                }
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
            Thread.Sleep(10);
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
                        if ((blocks[i].BlockType != Element.X
                            || blocks[i].BlockType != Element.R
                            || blocks[i].BlockType != Element.W
                            || blocks[i].BlockType != Element.S
                            || blocks[i].BlockType != Element.H
                            || blocks[i].BlockType != Element.U
                            || blocks[i].BlockType != Element.V)
                            && blocks[i].BlockType == Element.L)
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
                    Rect player = new Rect(left_corner.Horizontal + (right_corner.Horizontal - left_corner.Horizontal) / 2, left_corner.Vertical, 1, right_corner.Vertical - left_corner.Vertical);
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

        #region MapMethod
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
                    left_corner.Horizontal = blocks[i].Positon.X + 30;
                    left_corner.Vertical = blocks[i].Positon.Y - 2;
                    right_corner.Horizontal = blocks[i].Positon.X + blocks[i].Positon.Width + 30;
                    right_corner.Vertical = blocks[i].Positon.Y + blocks[i].Positon.Height - 2;
                    this.Charactersize = new Size(right_corner.Horizontal - left_corner.Horizontal, right_corner.Vertical - left_corner.Vertical);
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
                    left_corner.Horizontal = blocks[i].Positon.X - 30;
                    left_corner.Vertical = blocks[i].Positon.Y - 2;
                    right_corner.Horizontal = blocks[i].Positon.X + blocks[i].Positon.Width - 30;
                    right_corner.Vertical = blocks[i].Positon.Y + blocks[i].Positon.Height - 2;
                    this.Charactersize = new Size(right_corner.Horizontal - left_corner.Horizontal, right_corner.Vertical - left_corner.Vertical);
                }
            }
            this.game.Hero.MapPosition = this.MapLogic.ActualMapNumber;
            Changed2?.Invoke(this, null);
        }

        #endregion

        #region ChestMethod
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
        #endregion

        #region Tasks
        public async Task MethodIdle()
        {
            Task.Run(() =>
            {
                while (Task_Idle)
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
                    Monitor.Enter(Changed);
                    Changed2?.Invoke(this, null);
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
                Task_Idle = false;
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
                Task_Idle = true;
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
                    bool inx = false;
                    Rect player = new Rect(left_corner.Horizontal + 6, left_corner.Vertical + 3, right_corner.Horizontal - left_corner.Horizontal - 12, right_corner.Vertical - left_corner.Vertical);
                    for (int i = 0; i < blocks.Count; i++)
                    {
                        if (player.IntersectsWith(blocks[i].Positon)
                        && blocks[i].BlockType != Element.W
                        && blocks[i].BlockType != Element.CH
                        && blocks[i].BlockType != Element.PRE
                        && blocks[i].BlockType != Element.NE
                        && blocks[i].BlockType != Element.CH1
                        && blocks[i].BlockType != Element.Z
                        && blocks[i].BlockType != Element.PLAYER
                        && blocks[i].BlockType != Element.HOMEB
                        && blocks[i].BlockType != Element.EN)
                        {
                            down = false;
                        }
                        else
                        {
                            inx = true;
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
                        Thread.Sleep(5);
                        Changed2?.Invoke(this, null);
                    }
                    else
                    {
                        this.JumpIsBusy = false;
                    }
                    Thread.Sleep(5);
                }
            });
        }

        public async Task MethodFix()
        {
            Task.Run(() =>
            {
                while (true)
                {
                    bool up = true;
                    bool inx = false;
                    Rect player = new Rect(left_corner.Horizontal + 6, left_corner.Vertical, right_corner.Horizontal - left_corner.Horizontal - 12, 1);
                    for (int i = 0; i < blocks.Count; i++)
                    {
                        if (player.IntersectsWith(blocks[i].Positon)
                        || blocks[i].BlockType == Element.W
                        || blocks[i].BlockType == Element.CH
                        || blocks[i].BlockType == Element.PRE
                        || blocks[i].BlockType == Element.NE
                        || blocks[i].BlockType == Element.CH1
                        || blocks[i].BlockType == Element.Z
                        || blocks[i].BlockType == Element.PLAYER
                        || blocks[i].BlockType == Element.HOMEB
                        || blocks[i].BlockType == Element.P2
                        || blocks[i].BlockType == Element.P1
                        || blocks[i].BlockType == Element.P3
                        || blocks[i].BlockType == Element.P4)
                        {
                            up = false;
                        }
                        else
                        {
                            inx = true;
                        }
                    }
                    if (up || !inx)
                    {
                        right_corner.Vertical -= 1;
                        left_corner.Vertical -= 1;
                        MainPath = "Idle";
                        if (Turn_Right)
                        {
                            DoingPath = "Jump";
                        }
                        else
                        {
                            DoingPath = "Back_Jump";
                        }
                        Thread.Sleep(5);
                        this.JumpIsBusy = false;
                        Changed2?.Invoke(this, null);
                    }
                    Thread.Sleep(1);
                }
            });
        }
        #endregion

        #region Bools

        public bool RunCanExecute(int i)
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
                            || blocks[i].BlockType == Element.P2
                            || blocks[i].BlockType == Element.P1
                            || blocks[i].BlockType == Element.P3
                            || blocks[i].BlockType == Element.P4
                            )
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool DownCanExecute(int i)
        {
            return true;
        }
        #endregion
        private void Dt_Idle(object? sender, EventArgs e)
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
    }
}
