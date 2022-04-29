using GUI_20212202_AYZ8R9.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GUI_20212202_AYZ8R9.Logic
{
    public class InventoryLogic : IInventoryLogic
    {
        IList<Weapon> bunker;
        IList<Weapon> inventory;
        Game game;

        public void SetupCollections(IList<Weapon> bunker, IList<Weapon> inventory, Game game)
        {
            this.bunker = bunker;
            this.inventory = inventory;
            this.game = game;
        }

        public void AddToBunker(Weapon selected)
        {
            if (game.Hero.Weapon.Name != selected.Name || game.Hero.Inventory.Where(x=>x.Name == selected.Name).Count()>1)
            {
                inventory.Remove(selected);
                bunker.Add(selected);
            }
            
        }

        public void AddToInventory(Weapon selected)
        {
            bunker.Remove(selected);
            inventory.Add(selected);
        }
    }
}
