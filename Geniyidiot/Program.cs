using GeniyIdiot;
using GeniyIdiotClassLibrary;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace GeniyIdiot
{
    public class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Введите Ваше имя");
            string name = Console.ReadLine();
            var user = new User(name);

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
                        user.IncreaseRightAnswers();
                    }
                }


                int diagnosisIndex = countRightAnswers * (diagnoses.Length - 1) / countQuestions;
                string diagnosis = diagnoses[diagnosisIndex];

                user.Diagnose = diagnosis;


                user.Print();


                UsersResultStorage.Add(user);


                bool userChoice = UserChoise($"{user.Name}, Хотите посмотреть предыдущие результаты? (да/нет)");
                if (userChoice)
                {
                    UsersResultStorage.ShowAll();
                }

                userChoice = UserChoise($"{user.Name}, Хотите добавить новый вопрос? (да/нет)");
                if (userChoice)
                {
                    AddNewQuestion();

                }

                userChoice = UserChoise($"{user.Name}, Хотите удалить существующий вопрос? (да/нет)");
                if (userChoice)
                {
                    RemoveQuestion();

                }

                userChoice = UserChoise($"{user.Name}, Хотите пройти тест снова? (да/нет)");
                if (!userChoice)
                {
                    break;
                }
            }

            Console.WriteLine("Спасибо за игру!");
        }

        private static void RemoveQuestion()
        {
            Console.WriteLine("Введите номер вопроса, который хотите удалить");
            var questions = QuestionsStorage.GetAll();

            for (int i = 0; i < questions.Count; i++)
            {
                Console.WriteLine((i + 1) + ". " + questions[i].Text);
            }
            var number = int.Parse(Console.ReadLine());

            while (number < 1 || number > questions.Count)
            {
                Console.WriteLine($"Введите число от 1 до {questions.Count}");
                number = int.Parse(Console.ReadLine());
            }

            var removeQuestion = questions[number - 1];

            QuestionsStorage.Remove(removeQuestion);
        }

        static void AddNewQuestion()
        {
            Console.WriteLine("Введите текст нового вопроса");
            var text = Console.ReadLine();
            Console.WriteLine("Введите ответ на данный вопрос");
            var answer = int.Parse(Console.ReadLine());

            var newQuestion = new Question(text, answer);
            QuestionsStorage.Add(newQuestion);
        }

        static bool UserChoise(string question)
        {
            Console.WriteLine(question);
            string response = Console.ReadLine()?.Trim().ToLower();
            return response == "да" || response == "yes";
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