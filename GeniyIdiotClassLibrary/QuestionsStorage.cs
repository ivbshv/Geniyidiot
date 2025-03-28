using GeniyIdiotClassLibrary;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GeniyIdiotClassLibrary
{
    public class QuestionsStorage
    {
        private const string FileName = "questions.txt";

        public static List<Question> GetAll()
        {
            var questions = new List<Question>();

            if (FileProvider.Exists(FileName))
            {
                foreach (var line in FileProvider.Read(FileName))
                {
                    var parts = line.Split('#');
                    if (parts.Length == 2 && int.TryParse(parts[1], out int answer))
                    {
                        questions.Add(new Question(parts[0], answer));
                    }
                }
            }
            else
            {
                questions.Add(new Question("Сколько будет два плюс два умноженное на два?", 6));
                questions.Add(new Question("Бревно нужно распилить на 10 частей. Сколько распилов нужно сделать?", 9));
                questions.Add(new Question("На двух руках 10 пальцев. Сколько пальцев на 5 руках?", 25));
                questions.Add(new Question("Укол делают каждые полчаса. Сколько нужно минут, чтобы сделать три укола?", 60));
                questions.Add(new Question("Пять свечей горело, две потухли. Сколько свечей осталось?", 2));
                questions.Add(new Question("Сколько будет 5 в квадрате?", 25));
                questions.Add(new Question("Сколько месяцев в году имеют 28 дней?", 12));
                SaveQuestions(questions);
            }

            return questions;
        }

        private static void SaveQuestions(List<Question> questions)
        {
            foreach (var question in questions)
            {
                Add(question);
            }
        }

        public static void Add(Question newQuestion)
        {
            var value = $"{newQuestion.Text}#{newQuestion.Answer}";

            FileProvider.Append("questions.txt", value);
        }

        public static void Remove(Question removeQuestion)
        {
            var questions = GetAll();

            for (int i = 0; i < questions.Count; i++)
            {
                if(questions[i].Text == removeQuestion.Text)
                {
                    questions.RemoveAt(i);
                    break;
                }
            }

            questions.Remove(removeQuestion);

            FileProvider.Clear("questions.txt");

            SaveQuestions(questions);
        }
    }
}