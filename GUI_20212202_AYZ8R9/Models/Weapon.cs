using Microsoft.Toolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GUI_20212202_AYZ8R9.Models
{
    public class Weapon:ObservableObject
    {
        private string name;

        public string Name
        {
            get { return name; }
            set {
                SetProperty(ref name, value);

            }
        }
        private int damage;

        public int Damage
        {
            get { return damage; }
            set {
                SetProperty(ref damage,value);
            }
        }

        private int hpBoost;

        public int HPBoost
        {
            get { return hpBoost; }
            set {
                SetProperty(ref hpBoost,value);
            }
        }

    }
}
