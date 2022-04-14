﻿using IronXL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GUI_20212202_AYZ8R9.Helper
{
    public class ExcelDataReader
    {
        public WorkBook WorkBook { get; set; }
        public WorkSheet[] WorkSheets { get; set; }
        public WorkSheet WorkSheet { get; set; }
        public DataTable Table { get; set; }


        public ExcelDataReader(string path)
        {
            this.WorkBook = WorkBook.Load(path);         
            SetupWorkSheets();         
        }

        private void SetupWorkSheets()
        {
            this.WorkSheets = new WorkSheet[5];
            for (int i = 0; i < WorkSheets.Length; i++)
            {
                WorkSheets[i] = this.WorkBook.GetWorkSheet("Munka" +(i+1));
            }
        }

        private void MapSetup(ref string[,]  map)
        {
            ;string[,] map2 = map;
            for (int i = 0; i < map.GetLength(0); i++)
            {
                for (int j = 0; j < map.GetLength(1); j++)
                {
                    if (i>0)
                    {
                        string aktualis = map[i, j];
                        if (map[i, j] == " ")
                        {
                            string elotte = map[i - 1, j];
                            if (map[i-1, j] == " ")
                            {
                                map[i, j] = " ";
                            }
                            else if (map[i - 1, j] != " "  && map[i - 2, j] == " ") // itt a hiba
                            {
                                map[i, j] = "V";
                            }                                              
                            else if (map[i - 1, j] == "V")
                            {
                                map[i, j] = "U";
                            }
                            else if (map[i - 1, j] == "U")
                            {
                                map[i, j] = "W";
                            }
                            else if (map[i - 1, j] == "W")
                            {
                                map[i, j] = "W";
                            }
                            else if (map[i - 1, j] == "B")
                            {
                                map[i, j] = "V";
                            }
                            else if (map[i - 1, j] == "A")
                            {
                                map[i, j] = "V";
                            }
                            else if (map[i - 1, j] == "C")
                            {
                                map[i, j] = "V";
                            }
                            else if (map[i - 1, j] == "E")
                            {
                                map[i, j] = "V";
                            }
                            else if (map[i - 1, j] == "D")
                            {
                                map[i, j] = "V";
                            }
                            else if (map[i - 1, j] == "R")
                            {
                                map[i, j] = "V";
                            }
                            else if (map[i - 1, j] == "S")
                            {
                                map[i, j] = "V";
                            }
                            else if (map[i-1,j] == "W")
                            {
                                map[i, j] = "W";
                            }                         
                        }
                        
                    }
                }
            }
         
        }

        public string[,] GetMap(int number)
        {
            this.WorkSheet = WorkSheets[number-1];

            this.Table = this.WorkSheet.ToDataTable(true);

            string[,] map = new string[30,54];
            int matrixSorCount = 0;
            
            foreach (DataRow item in this.Table.Rows)
            {            
                for (int i = 0; i < 54; i++)
                {
                    if (item.ItemArray[i].ToString() == "")
                        map[matrixSorCount, i] = " ";
                    else
                        map[matrixSorCount, i] = item.ItemArray[i].ToString();
                }
                matrixSorCount++;
                if (matrixSorCount == 30) break;
            }
            ;
            MapSetup( ref map);           
            return map;
        }       
    }
}
