using GUI_20212202_AYZ8R9.Models;
using System;
using System.Collections.Generic;
using static GUI_20212202_AYZ8R9.Logic.MapLogic;

namespace GUI_20212202_AYZ8R9.Logic
{
    public interface IGameModel
    {
        Element[,] ActualMap { get; set; }
        Element[][,] AllMap { get; set; }
        List<Block> Blocks { get; set; }
        event EventHandler Changed;
        int ActualBGNumber { get; set; }
        int ActualMapNumber { get; set; }

    }
}