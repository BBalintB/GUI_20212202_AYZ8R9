using GUI_20212202_AYZ8R9.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static GUI_20212202_AYZ8R9.Logic.MainCharacterLogic;

namespace GUI_20212202_AYZ8R9.Logic
{
    internal interface ICharacter
    {
        Position left_corner { get; set; }

        Position right_corner { get; set; }

        char[,] Map { get; set; }

        event EventHandler Changed;

        string MainPath { get; set; }

        string DoingPath { get; set; }

        int Animation_Counter { get; set; }

        Task Method1();
    }
}
