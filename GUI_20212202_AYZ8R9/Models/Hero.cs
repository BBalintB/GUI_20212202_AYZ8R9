using Microsoft.Toolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GUI_20212202_AYZ8R9.Models
{
    public enum HeroTypes { 
        Neutral,
        Archer,
        Assault,
        Support,
        Medic,
        Heavy,
        Sniper,
        Specialist,
        Bandit,
        Soldier,
        Robot,
        Mercenary
    }

    public enum RoundPosition { 
        Neutral,
        Attack,
        Heal,
        Special
    }

    public class Hero:ObservableObject,ICharacter
    {
        private string heroName; // Name of the hero in that save

        public string Name
        {
            get { return heroName; }
            set { SetProperty(ref heroName, value); }
        }

        private HeroTypes heroType;

        public HeroTypes HeroType
        {
            get { return heroType; }
            set {
                SetStats(value);
                SetProperty(ref heroType, value);
            }
        }
        private int hP;

        public int HP
        {
            get { return hP; }
            set {
                SetProperty(ref hP, value);
            }
        }

        private int attack;

        public int Attack
        {
            get { return attack; }
            set {
                SetProperty(ref attack, value);
            }
        }

        private int specialAttackCounter;

        public int SpecialAttackCounter
        {
            get { return specialAttackCounter; }
            set
            {
                SetProperty(ref specialAttackCounter, value);
            }
        }

        private RoundPosition position;

        public RoundPosition Position
        {
            get { return position; }
            set {
                SetProperty(ref position, value);
            }
        }



        public Hero()
        {
            HeroType = HeroTypes.Neutral;
            Position = RoundPosition.Neutral;
        }



        void SetStats(HeroTypes type) {
            switch (type)
            {
                case HeroTypes.Archer:
                    HP = 100;
                    Attack = 75;
                    break;
                case HeroTypes.Assault:
                    HP = 100;
                    Attack = 60;
                    break;
                case HeroTypes.Support:
                    HP = 100;
                    Attack = 50;
                    break;
                default:
                    HP = 0;
                    Attack = 0;
                    break;
            }
        }


    }
}
