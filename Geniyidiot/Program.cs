using GeniyIdiot;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Geniyidiot
{
    public class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Введите Ваше имя");
            string name = Console.ReadLine();

            while (true)
            {
                var questions = QuestionsStorage.GetAll();
                var countQuestions = questions.Count;
                string[] diagnoses = GetDiagnoses();

                int countRightAnswers = 0;

                Random random = new Random();
                List<int> askedQuestions = new List<int>();

                for (int i = 0; i < countQuestions; i++)
                {
                    Console.WriteLine("Вопрос № " + (i + 1));

                    int randomQuestionIndex;
                    do
                    {
                        randomQuestionIndex = random.Next(0, countQuestions);
                    } while (askedQuestions.Contains(randomQuestionIndex));

                    askedQuestions.Add(randomQuestionIndex);
                    Console.WriteLine(questions[randomQuestionIndex].Text);

                    int userAnswer;

                    while (!int.TryParse(Console.ReadLine(), out userAnswer))
                    {
                        Console.WriteLine("Пожалуйста, введите число!");
                    }

                    int rightAnswer = questions[randomQuestionIndex].Answer;
                    if (userAnswer == rightAnswer)
                    {
                        countRightAnswers++;
                    }
                }

                
                int diagnosisIndex = countRightAnswers * (diagnoses.Length - 1) / countQuestions;
                string diagnosis = diagnoses[diagnosisIndex];

                var user = new User(name, countRightAnswers, diagnosis);
                user.Print();


                UsersResultStorage.Add(user);


                bool userChoice = UserChoise($"{user.Name}, Хотите посмотреть предыдущие результаты? (да/нет)");
                if (userChoice)
                {
                    UsersResultStorage.ShowAll();
                }

                
                userChoice = UserChoise($"{user.Name}, Хотите пройти тест снова? (да/нет)");
                if (!userChoice)
                {
                    break;
                }
            }

            Console.WriteLine("Спасибо за игру!");
        }

        
        static bool UserChoise(string question)
        {
            Console.WriteLine(question);
            string response = Console.ReadLine()?.Trim().ToLower();
            return response == "да" || response == "yes";
        }

        static List<Question> GetQuestions()
        {
            var questions = new List<Question>();
            questions.Add(new Question("Сколько будет два плюс два умноженное на два?", 6));
            questions.Add(new Question("Бревно нужно распилить на 10 частей. Сколько распилов нужно сделать?", 9));
            questions.Add(new Question("На двух руках 10 пальцев. Сколько пальцев на 5 руках?", 25));
            questions.Add(new Question("Укол делают каждые полчаса. Сколько нужно минут, чтобы сделать три укола?", 60));
            questions.Add(new Question("Пять свечей горело, две потухли. Сколько свечей осталось?", 2));
            questions.Add(new Question("Сколько будет 5 в квадрате?", 25));
            questions.Add(new Question("Сколько месяцев в году имеют 28 дней?", 12));

            return questions;
        }

        static string[] GetDiagnoses()
        {
            string[] diagnoses = new string[6];
            diagnoses[0] = "кретин";
            diagnoses[1] = "идиот";
            diagnoses[2] = "дурак";
            diagnoses[3] = "нормальный";
            diagnoses[4] = "талант";
            diagnoses[5] = "гений";

            return diagnoses;
        }
    }
}