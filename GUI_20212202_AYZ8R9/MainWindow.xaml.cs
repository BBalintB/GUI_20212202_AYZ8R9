using GUI_20212202_AYZ8R9.Helper;
using GUI_20212202_AYZ8R9.Logic;
using GUI_20212202_AYZ8R9.MenuOptionsWindows;
using GUI_20212202_AYZ8R9.Models;
using GUI_20212202_AYZ8R9.ViewModels;
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

        public bool run { get; set; }

        private Game game;

        public Game Game
        {
            get { return game; }
            set { game = value; }
        }


        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            if (DataContext is IMainActions vm)
            {
                vm.LoadAction += async () =>
                {
                    run = true;
                    Method1();
                    this.Game = (this.DataContext as MainWindowViewModel).SelectedGame;
                    display.Resize(new Size(grid.ActualWidth, grid.ActualHeight));
                    Maplogic = new MapLogic(new System.Windows.Size((int)grid.ActualWidth, (int)grid.ActualHeight));
                    display.SetupModel(Maplogic);// Load map                     
                    display.InvalidateVisual();

                    CharacterDisplay.Resize(new Size(grid.ActualWidth, grid.ActualHeight));
                    Characterlogic = new MainCharacterLogic();
                    Characterlogic.SetupSizes(new System.Windows.Size((int)grid.ActualWidth, (int)grid.ActualHeight), Maplogic ,game);
                    Characterlogic.DoingPath = "Run";
                    CharacterDisplay.SetupModel(Characterlogic);
                    CharacterDisplay.InvalidateVisual();
                    display.InvalidateVisual();
                };
            //Game game = (this.DataContext as MainWindowViewModel).SelectedGame;

                vm.CloseWindow += () =>
                {
                    run = false;
                    this.Close();
                };
            }
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
            {
               
                if ((DataContext as MainWindowViewModel).GameVisibility == Visibility.Visible)
                {
                    (DataContext as MainWindowViewModel).GameVisibility = Visibility.Collapsed;
                    (DataContext as MainWindowViewModel).MenuVisibility = Visibility.Visible;
                }
                else
                {
                    (DataContext as MainWindowViewModel).GameVisibility = Visibility.Visible;
                    (DataContext as MainWindowViewModel).MenuVisibility = Visibility.Collapsed;
                }
            }
            else if(e.Key == Key.L)
            {
                new Inventory(this.Game).ShowDialog();
                this.Game.FileLastSaveDate = DateTime.Now.ToString();
                Save.WriteOutJSON(this.Game, System.IO.Path.Combine("Games", game.FileName + ".json"));
            }
            //else if (e.Key == Key.Right)
            //{
            //    Characterlogic.Control(MainCharacterLogic.Controls.Right);
            //}
            //else if (e.Key == Key.Down)
            //{
            //    Characterlogic.Control(MainCharacterLogic.Controls.Down);
            //}
            //else if (e.Key == Key.Up)
            //{
            //    Characterlogic.Control(MainCharacterLogic.Controls.Up);
            //}
            //else if (e.Key == Key.A)
            //{
            //    Characterlogic.Control(MainCharacterLogic.Controls.Left);
            //}
            //else if (e.Key == Key.D)
            //{
            //    Characterlogic.Control(MainCharacterLogic.Controls.Right);
            //}
            //else if (e.Key == Key.S)
            //{
            //    Characterlogic.Control(MainCharacterLogic.Controls.Down);
            //}
            //else if (e.Key == Key.W)
            //{
            //    Characterlogic.Control(MainCharacterLogic.Controls.Up);
            //}
            //else if (e.Key == Key.Space)
            //{
            //    Characterlogic.Control(MainCharacterLogic.Controls.Jump);
            //}
            //if (Keyboard.IsKeyDown(Key.Right))
            //{
            //    Characterlogic.Control(MainCharacterLogic.Controls.Right);
            //}
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
            else if (e.Key == Key.Up)
            {
                Characterlogic.Control(MainCharacterLogic.Controls.Stop);
            }
            else if (e.Key == Key.Down)
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
            else if (e.Key == Key.W)
            {
                Characterlogic.Control(MainCharacterLogic.Controls.Stop);
            }
            else if (e.Key == Key.S)
            {
                Characterlogic.Control(MainCharacterLogic.Controls.Stop);
            }
            else if (e.Key == Key.Space)
            {
                Characterlogic.Control(MainCharacterLogic.Controls.Stop);
            }
            else if (e.Key == Key.LeftShift)
            {
                Characterlogic.Control(MainCharacterLogic.Controls.RunStop);
                Characterlogic.Control(MainCharacterLogic.Controls.Stop);
            }
        }

        private async Task Method1()
        {
            Task.Run(() =>
            {
                try
                {
                    while (run)
                    {
                        if (Dispatcher != null)
                        {
                        Application.Current.Dispatcher?.Invoke((Action)delegate
                        {
                            if (Keyboard.IsKeyDown(Key.Left))
                            {
                                Characterlogic.Control(MainCharacterLogic.Controls.Left);
                                if (Keyboard.IsKeyDown(Key.LeftShift))
                                {
                                    Characterlogic.Control(MainCharacterLogic.Controls.Run);
                                }
                            }
                        });
                        Application.Current.Dispatcher?.Invoke((Action)delegate
                        {
                            if (Keyboard.IsKeyDown(Key.Right))
                            {
                                Characterlogic.Control(MainCharacterLogic.Controls.Right);
                                if (Keyboard.IsKeyDown(Key.LeftShift))
                                {
                                    Characterlogic.Control(MainCharacterLogic.Controls.Run);
                                }
                            }
                        });
                        Application.Current.Dispatcher?.Invoke((Action)delegate
                        {
                            if (Keyboard.IsKeyDown(Key.Down))
                            {
                                Characterlogic.Control(MainCharacterLogic.Controls.Down);
                            }
                        });
                        Application.Current.Dispatcher?.Invoke((Action)delegate
                        {
                            if (Keyboard.IsKeyDown(Key.Up))
                            {
                                Characterlogic.Control(MainCharacterLogic.Controls.Up);
                            }
                        });

                        Application.Current.Dispatcher?.Invoke((Action)delegate
                        {
                            if (Keyboard.IsKeyDown(Key.A))
                            {
                                Characterlogic.Control(MainCharacterLogic.Controls.Left);
                                if (Keyboard.IsKeyDown(Key.LeftShift))
                                {
                                    Characterlogic.Control(MainCharacterLogic.Controls.Run);
                                }
                            }
                        });
                        Application.Current.Dispatcher?.Invoke((Action)delegate
                        {
                            if (Keyboard.IsKeyDown(Key.D))
                            {
                                Characterlogic.Control(MainCharacterLogic.Controls.Right);
                                if (Keyboard.IsKeyDown(Key.LeftShift))
                                {
                                    Characterlogic.Control(MainCharacterLogic.Controls.Run);
                                }
                            }
                        });
                        Application.Current.Dispatcher?.Invoke((Action)delegate
                        {
                            if (Keyboard.IsKeyDown(Key.S))
                            {
                                Characterlogic.Control(MainCharacterLogic.Controls.Down);
                            }
                        });
                        Application.Current.Dispatcher?.Invoke((Action)delegate
                        {
                            if (Keyboard.IsKeyDown(Key.W))
                            {
                                Characterlogic.Control(MainCharacterLogic.Controls.Up);
                            }
                        });
                        Application.Current.Dispatcher?.Invoke((Action)delegate
                        {
                            if (Keyboard.IsKeyDown(Key.Space))
                            {
                                Characterlogic.Control(MainCharacterLogic.Controls.Jump);
                            }
                        });
                        Application.Current.Dispatcher?.Invoke((Action)delegate
                        {
                            if (Keyboard.IsKeyDown(Key.Right))
                            {
                                Characterlogic.Control(MainCharacterLogic.Controls.Right);
                            }
                        });
                        }
                        Thread.Sleep(30);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }

            });
        }

        private void Bw_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            //display.Character_Pozition += e.ProgressPercentage;
            CharacterDisplay.InvalidateVisual();
        }

        private void grid_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            CharacterDisplay.Resize(new Size(grid.ActualWidth, grid.ActualHeight));
        }

        private void Window_Closing(object sender, CancelEventArgs e)
        {
            run = false;
        }
    }
}
