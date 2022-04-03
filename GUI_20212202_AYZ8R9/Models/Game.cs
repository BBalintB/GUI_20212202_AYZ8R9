using Microsoft.Toolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GUI_20212202_AYZ8R9.Models
{
    public class Game:ObservableObject
    {
        private string fileName; // Contains the name of the save

        public string FileName
        {
            get { return fileName; }
            set { SetProperty(ref fileName,value); }
        }

        private string heroName; // Name of the hero in that save

        public string HeroName
        {
            get { return heroName; }
            set { SetProperty(ref heroName, value); }
        }

        private DateTime fileLastSaveDate;

        public DateTime FileLastSaveDate
        {
            get { return fileLastSaveDate; }
            set { SetProperty(ref fileLastSaveDate, value); }
        }



        //Coming soon
    }
}
