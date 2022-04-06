using GUI_20212202_AYZ8R9.Models;
using System.Collections.Generic;
using System.Windows;

namespace GUI_20212202_AYZ8R9.Logic
{
    public interface IMenuLogic
    {
        void DeleteFile(Game game);
        void SetupCollection(IList<Game> games);
        void CreateNewGame();
        void LoadInGame(Game game);
        void SetUpVisibility(Visibility menu, Visibility game);
    }
}