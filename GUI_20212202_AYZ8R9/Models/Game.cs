using Microsoft.Toolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

        private Hero hero;

        public Hero Hero
        {
            get { return hero; }
            set {
                SetProperty(ref hero, value);
            }
        }


        private string fileLastSaveDate;

        public string FileLastSaveDate
        {
            get { return fileLastSaveDate; }
            set { SetProperty(ref fileLastSaveDate, value); }
        }

        public ObservableCollection<Weapon> BunkerWeapons{ get; set; }

        public Game()
        {
            BunkerWeapons = new ObservableCollection<Weapon>();
            Hero = new Hero();
        }


        //Coming soon
    }
}
