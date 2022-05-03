using GUI_20212202_AYZ8R9.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GUI_20212202_AYZ8R9.Helper
{
    static class RandomUtil
    {
        public static Random rnd = new Random();

        public static Weapon GetARandomWeapon() {
            string direction = Directory.GetCurrentDirectory();
            string[] weapons = Save.ReadFromTxt(Path.Combine(direction, "Weapons", "Weapon.txt"));
            List<Weapon> wp = new List<Weapon>();
            for (int i = 0; i < weapons.Length; i++)
            {
                string[] tmp = weapons[i].Split(':');
                Enum.TryParse(tmp[3], out WeaponType xy);
                wp.Add(new Weapon()
                {
                    Name = tmp[0],
                    Damage = int.Parse(tmp[1]),
                    HPBoost = int.Parse(tmp[2]),
                    Type = xy,
                    ImageURL = Path.Combine(Directory.GetCurrentDirectory(), "Images", "Gun", tmp[0] + ".png")
                }) ;
            }
            var random = rnd.Next(0, 101);
            Weapon newGun = new Weapon();
            if (random < 30)
            {
                newGun = wp.ElementAt(rnd.Next(0,10));
            }
            else if (random < 50)
            {
                newGun = wp.ElementAt(rnd.Next(10,18));
            }
            else if (random < 70)
            {
                newGun = wp.ElementAt(rnd.Next(19,24));
            }
            else if (random < 90)
            {
                newGun = wp.ElementAt(rnd.Next(25,28));
            }
            else {
                newGun = wp.ElementAt(rnd.Next(28,30));
            }
            return newGun;
        }
    }
}
