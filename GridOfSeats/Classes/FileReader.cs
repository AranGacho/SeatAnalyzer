using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using SeatAnalyzer.Classes;

namespace SeatAnalyzer
{
    public class FileReader : IFileReader
    {
        public string[] ReadFile(string path)
        {
            if(!File.Exists(path))
                return null;

            return File.ReadAllLines(path);
        }


    }
}
