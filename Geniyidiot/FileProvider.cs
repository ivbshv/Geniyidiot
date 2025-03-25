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

        public static void Replace(string fileName, IEnumerable<string> lines)
        {
            using (var writer = new StreamWriter(fileName, false, Encoding.UTF8))
            {
                foreach (var line in lines)
                {
                    writer.WriteLine(line);
                }
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
    }
}