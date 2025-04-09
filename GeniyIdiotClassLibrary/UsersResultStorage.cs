using GeniyIdiotClassLibrary;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace GeniyIdiotClassLibrary
{
    public static class UsersResultStorage
    {
        public static string Path = "userResults.json";

        public static void Add(User user)
        {
            var userResults = GetAll();
            userResults.Add(user);
            Save(userResults);
        }

        public static List<User> GetAll()
        {
            if (!File.Exists(Path) || new FileInfo(Path).Length == 0)
            {
                return new List<User>();
            }

            var jsonData = File.ReadAllText(Path, Encoding.UTF8); 
            return JsonConvert.DeserializeObject<List<User>>(jsonData) ?? new List<User>();
        }

        public static void ShowAll()
        {
            var users = GetAll();

            Console.WriteLine("{0,-20}{1,20}{2,15}", "Имя", "Правильные ответы", "Диагноз");
            foreach (var user in users)
            {
                Console.WriteLine("{0,-20}{1,20}{2,23}",
                    user.Name, user.CountRightAnswers, user.Diagnose);
            }
        }

        static void Save(List<User> usersResults)
        {
            var jsonData = JsonConvert.SerializeObject(usersResults, Formatting.Indented);
            File.WriteAllText(Path, jsonData, Encoding.UTF8); 
        }
    }
}