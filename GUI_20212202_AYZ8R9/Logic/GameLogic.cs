using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GUI_20212202_AYZ8R9.Logic
{
    public class GameLogic : IGameModel
    {
        public enum Element
        {
            A,B,C,D,E,F,X
                
        }

        public string[] maps;

        public Element[,] GameMatrix { get; set; }

        public GameLogic()
        {
            maps = new string[5];
            var allmap = Directory.GetFiles(Path.Combine(Directory.GetCurrentDirectory(), "Map"),
               "*.txt");
            int db = 0;
            foreach (var item in allmap)
            {
                maps[db] = item;
            }
            LoadNext(maps[0]);
        }

        private void LoadNext(string path)
        {           

            string[] lines = File.ReadAllLines(path);
            GameMatrix = new Element[33, 60];
            for (int i = 0; i < GameMatrix.GetLength(0); i++)
            {
                for (int j = 0; j < GameMatrix.GetLength(1); j++)
                {
                    GameMatrix[i, j] = ConvertToEnum(lines[i][j]);
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
                default:
                    return Element.X;
            }
        }

    }
}
