using Microsoft.Toolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GUI_20212202_AYZ8R9.Models
{
    public enum HeroTypes { 
        neutral,
        archer,
        assault,
        support
    }
    public class Hero:ObservableObject
    {
        private string heroName; // Name of the hero in that save

        public string HeroName
        {
            get { return heroName; }
            set { SetProperty(ref heroName, value); }
        }

        private HeroTypes heroType;

        public HeroTypes HeroType
        {
            get { return heroType; }
            set {
                SetProperty(ref heroType, value);
            }
        }

        public Hero()
        {
            HeroType = HeroTypes.neutral;
        }


    }
}
