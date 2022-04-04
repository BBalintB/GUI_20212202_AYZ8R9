﻿using GUI_20212202_AYZ8R9.Helper;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GUI_20212202_AYZ8R9.Logic
{
    public class MapLogic : IGameModel
    {
        int ActualMapNumber;

        public enum Element
        {
            A,B,C,D,E,R,S,V,U,W,X
                
        }

        ExcelDataReader r;

        public string[] maps;

        public Element[,] GameMatrix { get; set; }

        public MapLogic()
        {
            ActualMapNumber = 0;
            r = new ExcelDataReader(Path.Combine(Directory.GetCurrentDirectory(), "Maps","map.xlsx"));
            LoadNext(ActualMapNumber+1);
        }

        private void LoadNext(int number)
        {
            ActualMapNumber++;
            GameMatrix = new Element[30, 54];
            string[,] map = r.GetMap(number);
            ;
            for (int i = 0; i < map.GetLength(0); i++)
            {
                for (int j = 0; j < map.GetLength(1); j++)
                {
                    GameMatrix[i, j] = ConvertToEnum(Convert.ToChar(map[i,j]));
                }
            }          
        }

        private Element ConvertToEnum(char v)
        {
            switch (v)
            {
                case 'A': return Element.A;
                case 'B': return Element.B;
                case 'C': return Element.C;
                case 'D': return Element.D;
                case 'E': return Element.E;
                case 'R': return Element.R;
                case 'V': return Element.V;
                case 'S': return Element.S;
                case 'U': return Element.U;
                case 'W': return Element.W;
                default:
                    return Element.X;
            }
        }

    }
}
