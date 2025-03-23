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
                int countQuestions = 7;

                string[] questions = GetQuestions(countQuestions);
                int[] answers = GetAnswers(countQuestions);
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
                    Console.WriteLine(questions[randomQuestionIndex]);

                    int userAnswer;

                    while (!int.TryParse(Console.ReadLine(), out userAnswer))
                    {
                        Console.WriteLine("Пожалуйста, введите число!");
                    }

                    int rightAnswer = answers[randomQuestionIndex];
                    if (userAnswer == rightAnswer)
                    {
                        countRightAnswers++;
                    }
                }

                int diagnosisIndex = countRightAnswers * (diagnoses.Length - 1) / countQuestions;
                string diagnosis = diagnoses[diagnosisIndex];
                Console.WriteLine($"{name}, Ваш диагноз : {diagnosis}");
                Console.WriteLine("Правильных ответов " + countRightAnswers);

                SaveUserResult(name, countRightAnswers, diagnosis);

                
                bool userChoice = UserChoise("Хотите посмотреть предыдущие результаты? (да/нет)");
                if (userChoice)
                {
                    ShowUserResults();
                }

                
                userChoice = UserChoise("Хотите пройти тест снова? (да/нет)");
                if (!userChoice)
                {
                    break;
                }
            }

            Console.WriteLine("Спасибо за игру!");
        }

        static void ShowUserResults()
        {
            using (StreamReader reader = new StreamReader("userResults.txt", Encoding.UTF8))
            {
                Console.WriteLine("{0,-20}{1,20}{2,15}", "Имя", "Количество правильных ответов", "Диагноз");
                while (!reader.EndOfStream)
                {
                    string line = reader.ReadLine();
                    string[] values = line.Split('*');
                    string name = values[0];
                    int countRightAnswers = Convert.ToInt32(values[1]);
                    string diagnosis = values[2];

                    Console.WriteLine("{0,-20}{1,20}{2,23}", name, countRightAnswers, diagnosis);
                }
            }
        }

        static void SaveUserResult(string name, int countRightAnswers, string diagnosis)
        {
            string value = $"{name}*{countRightAnswers}*{diagnosis}";
            AppendToFile("userResults.txt", value);
        }

        static void AppendToFile(string fileName, string value)
        {
            using (StreamWriter writer = new StreamWriter(fileName, true, Encoding.UTF8))
            {
                writer.WriteLine(value);
            }
        }

        static bool UserChoise(string question)
        {
            Console.WriteLine(question);
            string response = Console.ReadLine()?.Trim().ToLower();
            return response == "да" || response == "yes";
        }

        static string[] GetQuestions(int countQuestions)
        {
            string[] questions = new string[countQuestions];
            questions[0] = "Сколько будет два плюс два умноженное на два?";
            questions[1] = "Бревно нужно распилить на 10 частей. Сколько распилов нужно сделать?";
            questions[2] = "На двух руках 10 пальцев. Сколько пальцев на 5 руках?";
            questions[3] = "Укол делают каждые полчаса. Сколько нужно минут, чтобы сделать три укола?";
            questions[4] = "Пять свечей горело, две потухли. Сколько свечей осталось?";
            questions[5] = "Сколько будет 5 в квадрате?";
            questions[6] = "Сколько месяцев в году имеют 28 дней?";
            return questions;
        }

        static int[] GetAnswers(int countAnswers)
        {
            int[] answers = new int[countAnswers];
            answers[0] = 6;
            answers[1] = 9;
            answers[2] = 25;
            answers[3] = 60;
            answers[4] = 2;
            answers[5] = 25;
            answers[6] = 12;
            return answers;
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