using Microsoft.Toolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GUI_20212202_AYZ8R9.ViewModels
{
    internal class MainWindowViewModel: ObservableRecipient
    {
        private double height;

        public double Height
        {
            get { return height; }
            set 
            {
                SetProperty(ref height, value);

            }
        }

        private double width;

        public double Width
        {
            get { return width; }
            set
            {
                SetProperty(ref width, value);
            }
        }

        public MainWindowViewModel()
        {
            this.Height = 300;
            this.Width = 300;
        }
    }
}
