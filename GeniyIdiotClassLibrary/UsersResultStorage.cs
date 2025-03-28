using GeniyIdiotClassLibrary;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GeniyIdiotClassLibrary
{
    public static class UsersResultStorage
    {
        private const string ResultsFileName = "userResults.txt";

        public static void Add(User user)
        {
            var line = $"{user.Name}*{user.CountRightAnswers}*{user.Diagnose}";
            FileProvider.Append(ResultsFileName, line);
        }

        public static List<User> GetAll()
        {
            var lines = FileProvider.Read(ResultsFileName);
            var users = new List<User>();

            foreach (var line in lines)
            {
                var parts = line.Split('*');
                if (parts.Length != 3)
                    continue;

                var name = parts[0];
                var countRightAnswers = int.Parse(parts[1]);
                var diagnosis = parts[2];

                var user = new User(name);
                user.CountRightAnswers = countRightAnswers;
                user.Diagnose = diagnosis;
                users.Add(user);
            }

            return users;
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
    }
}