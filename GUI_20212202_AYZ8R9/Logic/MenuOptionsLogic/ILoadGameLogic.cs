using GUI_20212202_AYZ8R9.Models;
using System.Collections.Generic;

namespace GUI_20212202_AYZ8R9.Logic.MenuOptionsLogic
{
    public interface ILoadGameLogic
    {
        void RemoveSave(Game game);
        void SetupCollection(IList<Game> games);
    }
}