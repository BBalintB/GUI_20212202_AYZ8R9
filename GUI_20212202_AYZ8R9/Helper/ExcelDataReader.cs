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

            this.WorkSheets = new WorkSheet[10];

            //SetupWorkSheets();         
        }

        private void SetupWorkSheets()
        {
            for (int i = 1; i < WorkSheets.Length+1; i++)
            {
                WorkSheets[i] = this.WorkBook.GetWorkSheet("Munka"+i);
            }
        }

        private string[,] MapSetup(string[,] map)
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
                                map[i, j] = "Z";
                            }                          
                            else
                            {
                                map[i, j] = "Z";
                            }
                        }
                        
                    }
                }
            }

            return map2;

        }

        public string[,] GetMap(int number)
        {
            this.WorkSheet = this.WorkBook.GetWorkSheet("Munka"+number);

            this.Table = this.WorkSheet.ToDataTable(true);

            string[,] map = new string[30,54];
            int sorCount = 0;
            ;
            foreach (DataRow item in this.Table.Rows)
            {                           
                for (int i = 0; i < 54; i++)
                {
                    if (item.ItemArray[i].ToString() == "")
                    {
                        map[sorCount, i] = " ";

                    }
                    else
                    {
                        string karakter = item.ItemArray[i].ToString();
                        map[sorCount, i] = karakter;
                    }
                                      
                }

                sorCount++;
                if (sorCount == 30)
                {
                    break;
                }
                
            }
            ;
            map = MapSetup(map);
            ;
            return map;

        }
        
    }
}