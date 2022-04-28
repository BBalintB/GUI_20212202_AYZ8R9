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

        public void SetupCollections(IList<Weapon> bunker, IList<Weapon> inventory)
        {
            this.bunker = bunker;
            this.inventory = inventory;
        }

        public void AddToBunker(Weapon selected)
        {
            inventory.Remove(selected);
            bunker.Add(selected);
        }

        public void AddToInventory(Weapon selected)
        {
            bunker.Remove(selected);
            inventory.Add(selected);
        }
    }
}
