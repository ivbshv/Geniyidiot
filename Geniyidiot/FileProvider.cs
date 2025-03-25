using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace GeniyIdiot
{
    public static class FileProvider
    {
        public static void Append(string fileName, string value)
        {
            using (var writer = new StreamWriter(fileName, true, Encoding.UTF8))
            {
                writer.WriteLine(value);
            }
        }

        public static List<string> Read(string fileName)
        {
            var lines = new List<string>();

            if (!File.Exists(fileName))
                return lines;

            using (var reader = new StreamReader(fileName, Encoding.UTF8))
            {
                while (!reader.EndOfStream)
                {
                    lines.Add(reader.ReadLine());
                }
            }

            return lines;
        }

        public static bool Exists(string fileName)
        {
            return File.Exists(fileName);
        }

        public  static void Clear(string fileName)
        {
            File.WriteAllText(fileName, string.Empty);
        }
    }
}