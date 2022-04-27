using GUI_20212202_AYZ8R9.Models;
using System.Collections.Generic;

namespace GUI_20212202_AYZ8R9.Logic
{
    interface IInventoryLogic
    {
        void AddToBunker(Weapon selected);
        void AddToInventory(Weapon selected);
        void SetupCollections(IList<Weapon> bunker, IList<Weapon> inventory);
    }
}