﻿using GUI_20212202_AYZ8R9.Logic.MenuOptionsLogic;
using GUI_20212202_AYZ8R9.Models;
using GUI_20212202_AYZ8R9.ViewModels.MenuOptionsWindowViewModels;
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
using System.Windows.Shapes;

namespace GUI_20212202_AYZ8R9.MenuOptionsWindows
{
    /// <summary>
    /// Interaction logic for LoadGameWindow.xaml
    /// </summary>
    public partial class LoadGameWindow : Window
    {
        public LoadGameWindow(ILoadGameLogic logic, Game game)
        {
            InitializeComponent();
            this.DataContext = new LoadGameWindowViewModel(logic);
            (this.DataContext as LoadGameWindowViewModel).Setup(game);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            foreach (var item in stack_editor.Children)
            {
                if (item is TextBox t)
                {
                    t.GetBindingExpression(TextBox.TextProperty).UpdateSource();
                }
            }
            this.DialogResult = true;
        }
    }
}
