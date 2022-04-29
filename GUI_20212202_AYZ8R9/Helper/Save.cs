using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GUI_20212202_AYZ8R9.Helper
{
    static class Save
    {
        public static void WriteOutJSON<T>(T file, string url) {
            string newGame = JsonConvert.SerializeObject(file); //Serialize the incoming game object
            File.WriteAllText(url, newGame); //It save it into a file named after the object file name prop
        }

        public static IList<T> ReadFiles<T>(IList<T> games) {
            string[] files = Directory.GetFiles("Games", "*.json"); //Reads out the files from the Games map
            for (int i = 0; i < files.Length; i++)
            {
                if (File.Exists(files[i]))
                {
                    var hq = JsonConvert.DeserializeObject<T>(File.ReadAllText(files[i])); //It fills up the Games list with the content of the save 
                    games.Add(hq);
                }
            }
            return games;
        }

        public static string[] ReadFromTxt(string url) {
            return File.ReadAllLines(url);
        }
    }
}
