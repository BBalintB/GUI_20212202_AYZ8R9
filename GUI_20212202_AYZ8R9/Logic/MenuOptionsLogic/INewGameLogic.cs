using GUI_20212202_AYZ8R9.Models;
using System;

namespace GUI_20212202_AYZ8R9.Logic.MenuOptionsLogic
{
    public interface INewGameLogic
    {
        void SetHeroType(HeroTypes type);
        void SetupHero(Game game);
        void SetUpNewGame();
       
    }
}