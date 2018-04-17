using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Mime;
using System.Text;
using System.Threading.Tasks;

namespace changeCharaktersInSrtAndTxt
{
    class Program
    {
        static void Main(string[] args)
        {
            System.Console.WriteLine("Program needs files in the same directory named 'convert.rules' with replacement rules and 'files.regex' with regular expressions which files should be converted");

            List<string> filesToChange = new List<string>();

            StreamReader regexReader = new StreamReader(AppDomain.CurrentDomain.BaseDirectory+"files.regex");
            System.Console.WriteLine("\nYour regular expressions:");
            while (!regexReader.EndOfStream)
            {
                string regex = regexReader.ReadLine();
                System.Console.WriteLine(regex);
                filesToChange.AddRange(System.IO.Directory.GetFiles(AppDomain.CurrentDomain.BaseDirectory, regex));
            }
            
            System.Console.WriteLine("\nThese files will be changed with your rules:");
            foreach (var file in filesToChange)
            {
                System.Console.WriteLine(file);
            }

            foreach (var file in filesToChange)
            {
                StreamReader reader = new StreamReader(file);
                string fileReadToEnd = reader.ReadToEnd();
                reader.Close();
                StreamReader changes = new StreamReader(AppDomain.CurrentDomain.BaseDirectory + "convert.rules");
                string[] oldAndNewStrings = new string[2];
                while (!changes.EndOfStream)
                {
                    oldAndNewStrings = changes.ReadLine().Split(' ');
                    fileReadToEnd = fileReadToEnd.Replace(oldAndNewStrings[0], oldAndNewStrings[1]);
                }
                changes.Close();
                StreamWriter writer = new StreamWriter(file);
                writer.Write(fileReadToEnd);
                writer.Close();
            }
            System.Console.WriteLine("\nWork done, press enter to quit");
            System.Console.Read();
        }
    }
}
