using GUI_20212202_AYZ8R9.Helper;
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
        public enum Element
        {
            A, B, C, D, E, R, S, V, U, W, X, Z,L,Y,NE,PRE,HOME,PLAYER,CHEST,CHEST1
        }

        int ActualMapNumber { get; set; }
        public Element[][,] AllMap { get; set; }
        public Element[,] GameMatrix { get; set; }
        private ExcelDataReader r { get; set; }
      
        public MapLogic()
        {
            ActualMapNumber = 1;
            r = new ExcelDataReader(Path.Combine(Directory.GetCurrentDirectory(), "Maps","map.xlsx"));
            LoadFirstMap();   
            
        }

        public void LoadInAllMap()
        {
            AllMap = new Element[4][,];
            for (int i = 0; i < 4; i++)
            {
                string[,] map = r.GetMap(i);
                Element[,] matrix = MatrixConverter(map);
                AllMap[i] = matrix;                              
            }
        }

        private Element[,] MatrixConverter(string[,] map)
        {
            Element[,] matrix = new Element[30, 54];           
            ;
            for (int i = 0; i < map.GetLength(0); i++)
            {
                for (int j = 0; j < map.GetLength(1); j++)
                {
                    matrix[i, j] = ConvertToEnum(map[i, j]);
                }
            }
            return matrix;
        }

        private void LoadFirstMap()
        {                     
            string[,] map = r.GetMap(2);
            GameMatrix = MatrixConverter(map);
        }

        public  void LoadNextLeftMap()
        {
            GameMatrix = new Element[30, 54];
            string[,] map = r.GetMap(ActualMapNumber-1);
            ActualMapNumber = ActualMapNumber - 1;
            ;
            for (int i = 0; i < map.GetLength(0); i++)
            {
                for (int j = 0; j < map.GetLength(1); j++)
                {
                    GameMatrix[i, j] = ConvertToEnum(map[i, j]);
                }
            }
            ActualMapNumber++;
        } // todo
        public void LoadNextRightMap()
        {
            GameMatrix = new Element[30, 54];
            string[,] map = r.GetMap(ActualMapNumber + 1);
            ActualMapNumber = ActualMapNumber + 1;

            for (int i = 0; i < map.GetLength(0); i++)
            {
                for (int j = 0; j < map.GetLength(1); j++)
                {
                    GameMatrix[i, j] = ConvertToEnum(map[i, j]);
                }
            }
            ActualMapNumber++;
        } // todo

        

        private Element ConvertToEnum(string v)
        {
            
            switch (v)
            {
                case "A": return Element.A;
                case "B": return Element.B;
                case "C": return Element.C;
                case "D": return Element.D;
                case "E": return Element.E;
                case "R": return Element.R;
                case "V": return Element.V;
                case "S": return Element.S;
                case "U": return Element.U;
                case "W": return Element.W;
                case "Z": return Element.Z;
                case "L": return Element.L;
                case "Y": return Element.Y;
                case "PLAYER": return Element.PLAYER;
                case "HOME": return Element.HOME;
                case "NE": return Element.NE;
                case "PRE": return Element.PRE;
                case "CHEST": return Element.CHEST;
                case "CHEST1": return Element.CHEST1;
                default:
                    return Element.X;
            }
        }

    }
}
