using GUI_20212202_AYZ8R9.Helper;
using GUI_20212202_AYZ8R9.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static GUI_20212202_AYZ8R9.Logic.MainCharacterLogic;
using static GUI_20212202_AYZ8R9.Logic.MapLogic;

namespace GUI_20212202_AYZ8R9.Logic
{
    public interface ICharacter
    {
        Position left_corner { get; set; }

        Position right_corner { get; set; }

        //Element[,] DisplayMap { get; set; }

        event EventHandler Changed;
        event EventHandler Changed2;

        string MainPath { get; set; }

        string DoingPath { get; set; }

        int Animation_Counter { get; set; }

        List<Block> blocks { get; set; }

        Task Method1();
    }
}
