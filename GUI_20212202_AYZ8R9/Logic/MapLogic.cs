﻿using GUI_20212202_AYZ8R9.Helper;
using GUI_20212202_AYZ8R9.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace GUI_20212202_AYZ8R9.Logic
{
    public class MapLogic : IGameModel
    {   
        public enum Element
        {
            A, B, C, D, E, R, S, V, U, W, X, Z,L,Y,NE,PRE,HOME,PLAYER,CH,CH1,EN,F,G,H,END,HC, HOMEB, HOMEC, HOMELC, HOMELW, HOMERC, HOMERW,P1, P2, P3, P4
        }

        int number;
        public int ActualMapNumber { get; set; }
        public int ActualBGNumber { get; set; }
        public Element[][,] AllMap { get; set; }
        public Element[,] ActualMap { get; set; }
        public List<Block> Blocks { get; set; }
        private ExcelDataReader r { get; set; }
        private Size size { get; set; }
        public event EventHandler Changed;

        public MapLogic()
        {

        }
        public MapLogic(Size size)
        {
           
            r = new ExcelDataReader(Path.Combine(Directory.GetCurrentDirectory(), "Maps","map.xlsx"));
            this.Blocks = new List<Block>();
            LoadInAllMap();
            LoadFirstMap();
            this.size = size;
            SetupBlock(this.size);
        }

        public void LoadInAllMap()
        {
            AllMap = new Element[7][,];
            for (int i = 0; i < AllMap.Length; i++)
            {
                string[,] map = r.GetMap(i+1);
                Element[,] matrix = MatrixConverter(map);
                AllMap[i] = matrix;                              
            }
            ;
        }

        private Element[,] MatrixConverter(string[,] map)
        {
            Element[,] matrix = new Element[30, 54];           
            
            for (int i = 0; i < map.GetLength(0); i++)
            {
                for (int j = 0; j < map.GetLength(1); j++)
                {
                    matrix[i, j] = ConvertToEnum(map[i, j]);
                }
            }
            return matrix;
        }
        public void LoadFirstMap()
        {
            number = 1;
            ActualMapNumber = 0;
            ActualBGNumber = 1;
            ActualMap = AllMap[ActualMapNumber];
        }

        public void LoadFirstMap(int map)
        {
            number = 1;
            ActualMapNumber = map;
            ActualBGNumber = map;
            ActualMap = AllMap[ActualMapNumber];
        }

        public void LoadNextRightMap()
        {
            ;
            ActualBGNumber++;
            if (ActualBGNumber == 5)
            {
                ActualBGNumber = 1;
            }
            

            ActualMapNumber++;
            ActualMap = AllMap[ActualMapNumber];
            SetupBlock(this.size);
            Changed?.Invoke(this, null);
        }

        public  void LoadNextLeftMap()
        {
            ;
            
            ActualBGNumber--;
            if (ActualBGNumber == -1)
            {
                ActualBGNumber = 4;
            }

            ActualMapNumber--;
            ActualMap = AllMap[ActualMapNumber];
            ;
            SetupBlock(this.size);
            Changed?.Invoke(this, null);
            ;
        } 
        

        public void SetupBlock(Size size)
        {
            this.Blocks = new List<Block>();
            double rectWidth = size.Width / ActualMap.GetLength(1);
            double rectHeight = size.Height / ActualMap.GetLength(0);
            for (int i = 0; i < ActualMap.GetLength(0); i++)
            {
                for (int j = 0; j < ActualMap.GetLength(1); j++)
                {
                    if (ActualMap[i, j] != Element.X)
                    {
                        this.Blocks.Add(new Models.Block() { BlockType = ActualMap[i, j], Positon = new Rect(j * rectWidth, i * rectHeight, rectWidth, rectHeight) });
                    }
                }
            }
        }
        
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
                case "CH": return Element.CH;
                case "CH1": return Element.CH1;
                case "F": return Element.F;
                case "G": return Element.G;
                case "H": return Element.H;
                case "EN": return Element.EN;
                case "END": return Element.END;
                case "HC": return Element.HC;
                case "HOMEB": return Element.HOMEB;
                case "HOMEC": return Element.HOMEC;
                case "HOMELC": return Element.HOMELC;
                case "HOMELW": return Element.HOMELW;
                case "HOMERC": return Element.HOMERC;
                case "HOMERW": return Element.HOMERW;
                case "P1": return Element.P1;
                case "P2": return Element.P2;
                case "P3": return Element.P3;
                case "P4": return Element.P4;

                default:
                    return Element.X;
            }
        }
    }
}
