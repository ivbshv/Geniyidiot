using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Geniyidiot
{
    public class Program
    {
        const string ResultsFile = "results.txt";
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

        static void SaveResult(string name, int correctAnswers, string diagnosis)
        {
            using (StreamWriter writer = new StreamWriter(ResultsFile, true))
            {
                writer.WriteLine($"{name},{correctAnswers},{diagnosis}");
            }
        }

        static void ShowResults()
        {
            if (!File.Exists(ResultsFile))
            {
                Console.WriteLine("Результатов пока нет.");
                return;
            }

            Console.WriteLine("\nРезультаты тестирования:");
            Console.WriteLine("ФИО\t\tПравильные ответы\tДиагноз");
            Console.WriteLine(new string('-', 50));

            string[] results = File.ReadAllLines(ResultsFile);
            foreach (var result in results)
            {
                string[] data = result.Split(',');
                Console.WriteLine($"{data[0],-15} {data[1],-20} {data[2]}");
            }
        }

        static void Main(string[] args)
        {
            Console.WriteLine("Введите Ваше имя");
            string name = Console.ReadLine();
            bool repeat;

            do
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
                Console.WriteLine($"{name}, Ваш диагноз : {diagnoses[diagnosisIndex]}");
                Console.WriteLine("Правильных ответов " + countRightAnswers);

                SaveResult(name, countRightAnswers, diagnosis);

                Console.WriteLine("Хотите пройти тест снова? (да/нет)");
                string response = Console.ReadLine().Trim().ToLower();
                repeat = response == "да";

            } while (repeat);

            Console.WriteLine("\nХотите посмотреть таблицу результатов? (да/нет)");
            if (Console.ReadLine().Trim().ToLower() == "да")
            {
                ShowResults();
            }

            Console.WriteLine("Спасибо за игру!");
        }
    }
}