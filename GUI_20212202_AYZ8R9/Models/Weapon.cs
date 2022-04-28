using Microsoft.Toolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GUI_20212202_AYZ8R9.Models
{
    public enum WeaponType { 
        uncommon,
        common,
        rare,
        epic,
        legendary
    }
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

        private WeaponType type;

        public WeaponType Type
        {
            get { return type; }
            set {
                SetProperty(ref type, value);
            }
        }

        public Weapon()
        {
            Damage = 0;
            HPBoost = 0;
        }

    }
}
