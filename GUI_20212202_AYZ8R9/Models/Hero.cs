using Microsoft.Toolkit.Mvvm.ComponentModel;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

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
        private BitmapImage image;
        [JsonIgnore]
        public BitmapImage Image
        {
            get { return image; }
            set
            {
                SetProperty(ref image, value);
            }
        }

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

        private int mapPosition;

        public int MapPosition
        {
            get { return mapPosition; }
            set {
                SetProperty(ref mapPosition, value);
            }
        }

        private Weapon weapon;

        public Weapon Weapon
        {
            get { return weapon; }
            set {
                if (Weapon != null)
                {
                    this.Attack -= Weapon.Damage;
                    this.HP -= Weapon.HPBoost;
                    this.Attack += value.Damage;
                    this.HP += value.HPBoost;
                }
                
                SetProperty(ref weapon, value);
            }
        }

        private int[] chests;

        public int[] Chests
        {
            get { return chests; }
            set { chests = value; }
        }

        private bool[] battles;

        public bool[] Battles
        {
            get { return battles; }
            set { battles = value; }
        }


        public ObservableCollection<Weapon> Inventory { get; set; }

        public Hero()
        {
            //HeroType = HeroTypes.Neutral;
            Position = RoundPosition.Neutral;
            Weapon = new Weapon();
            Inventory = new ObservableCollection<Weapon>();
            Chests = new int[7];
            Battles = new bool[7];
        }

        void SetStats(HeroTypes type) {
            switch (type)
            {
                case HeroTypes.Archer:
                    HP = 100;
                    Attack = 75;
                    Image = new BitmapImage(new Uri(Path.Combine(Directory.GetCurrentDirectory(), "Images", "heads", "arch_head.png"), UriKind.RelativeOrAbsolute));

                    break;
                case HeroTypes.Assault:
                    HP = 100;
                    Attack = 60;
                    Image = new BitmapImage(new Uri(Path.Combine(Directory.GetCurrentDirectory(), "Images", "heads", "soilder_head.png"), UriKind.RelativeOrAbsolute));

                    break;
                case HeroTypes.Support:
                    HP = 100;
                    Attack = 50;
                    Image = new BitmapImage(new Uri(Path.Combine(Directory.GetCurrentDirectory(), "Images", "heads", "supp_head.png"), UriKind.RelativeOrAbsolute));

                    break;
                default:
                    HP = 0;
                    Attack = 0;
                    break;
            }
            if (Chests != null)
            {
                Chests[0] = 0;
                Chests[1] = 2;
                Chests[2] = 3;
                Chests[3] = 0;
                Chests[4] = 0;
                Chests[5] = 0;
                Chests[6] = 0;
            }
            if (Battles != null)
            {
                Battles[0] = true;
                Battles[1] = true;
                Battles[2] = false;
                Battles[3] = false;
                Battles[4] = false;
                Battles[5] = false;
                Battles[6] = false;
            }

        }
    }
}
