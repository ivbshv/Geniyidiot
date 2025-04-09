using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GeniyIdiotClassLibrary
{
    public class QuestionsStorage
    {
        public static string Path = "questions.json";

        public static List<Question> GetAll()
        {
            if (!FileProvider.Exists(Path))
            {
                var defaultQuestions = GetDefaultQuestions();
                SaveAll(defaultQuestions);
                return defaultQuestions;
            }

            var lines = FileProvider.Read(Path);
            var json = string.Join("", lines);

            return JsonConvert.DeserializeObject<List<Question>>(json) ?? new List<Question>();
        }

        private static List<Question> GetDefaultQuestions()
        {
            return new List<Question>
            {
                new Question("Сколько будет два плюс два умноженное на два?", 6),
                new Question("Бревно нужно распилить на 10 частей. Сколько распилов нужно сделать?", 9),
                new Question("На двух руках 10 пальцев. Сколько пальцев на 5 руках?", 25),
                new Question("Укол делают каждые полчаса. Сколько нужно минут, чтобы сделать три укола?", 60),
                new Question("Пять свечей горело, две потухли. Сколько свечей осталось?", 2),
                new Question("Сколько будет 5 в квадрате?", 25),
                new Question("Сколько месяцев в году имеют 28 дней?", 12)
            };
        }

        public static void Add(Question newQuestion)
        {
            var questions = GetAll();
            questions.Add(newQuestion);
            SaveAll(questions);
        }

        public static void Remove(Question removeQuestion)
        {
            var questions = GetAll();
            questions.RemoveAll(q => q.Text == removeQuestion.Text);
            SaveAll(questions);
        }

        private static void SaveAll(List<Question> questions)
        {
            var json = JsonConvert.SerializeObject(questions, Formatting.Indented);
            FileProvider.Replace(Path, json);
        }
    }
}