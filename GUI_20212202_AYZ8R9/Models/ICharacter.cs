using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GUI_20212202_AYZ8R9.Models
{
    public interface ICharacter
    {
        public string Name { get; set; }
        public int HP { get; set; }
        public int Attack { get; set; }
        public HeroTypes HeroType { get; set; }

        public int SpecialAttackCounter { get; set; }
        public RoundPosition Position { get; set; }
        public Weapon Weapon { get; set; }
    }
}
