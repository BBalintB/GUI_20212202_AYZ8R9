﻿using Microsoft.Toolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace GUI_20212202_AYZ8R9.Models.NPC
{
    public class NPC_Character:ObservableObject,ICharacter
    {


        private string color;
        public string Color
        {
            get { return color; }
            set
            {
                SetProperty(ref color, value);
            }
        }

        private BitmapImage image;
        public BitmapImage Image
        {
            get { return image; }
            set
            {
                SetProperty(ref image, value);
            }
        }


        private string name;

        public string Name
        {
            get { return name; }
            set {
                SetProperty(ref name, value);
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

        private HeroTypes type;

        public HeroTypes HeroType
        {
            get { return type; }
            set {
                SetProperty(ref type, value);
            }
        }

        private int specialAttackCounter;

        public int SpecialAttackCounter
        {
            get { return specialAttackCounter; }
            set {
                SetProperty(ref specialAttackCounter, value);
            }
        }

        private RoundPosition position;

        public RoundPosition Position
        {
            get { return position; }
            set
            {
                SetProperty(ref position, value);
            }
        }

        private Weapon weapon;

        public Weapon Weapon
        {
            get { return weapon; }
            set
            {
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

        public NPC_Character()
        {
            Position = RoundPosition.Neutral;
            Weapon = new Weapon();            
        }


    }
}
