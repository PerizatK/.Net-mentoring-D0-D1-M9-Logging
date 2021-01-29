using System;
using System.Collections.Generic;
using System.IO;

namespace BasicParser
{
    class Program
    {
        static void Main(string[] args)
        {
            List<Log> list = new List<Log>();
            string path = @"..\..\..\..\..\Logging\BrainstormSessions\bin\Debug\netcoreapp3.0\Logs\log4Net.log";
            list = Load(path);

            int i = 0;
            foreach (var log in list)
            {
                if (log.sType == "ERROR")
                {
                    Console.WriteLine($"Error {i+1}: {log.sType} {log.text}");
                    i++;
                }
            }

            Console.WriteLine($"Total: {list.Count}");

            Console.ReadKey();
        }

        private static List<Log> Load(string path)
        {
            List<Log> tempList = new List<Log>();
            try
            {
                using StreamReader sr = new StreamReader(path, System.Text.Encoding.Default);
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    Log log = new Log();
                    var delimiter = " | ";
                    string[] substrings = line.Split(delimiter);
                    log.createdDate = substrings[0];
                    log.sType = substrings[1];
                    log.text = substrings[2];

                    tempList.Add(log);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            return tempList;
        }
    }
}
