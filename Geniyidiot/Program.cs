using GeniyIdiotClassLibrary;
using System;
using System.Collections.Generic;

namespace GeniyIdiot
{
    public class Program
    {
        static void Main(string[] args)
        {
            while (true)
            {
                Console.WriteLine("Введите Ваше имя");
                string name = Console.ReadLine();
                var user = new User(name);
                var game = new Game(user);

                while (!game.End())
                {
                    var currentQuestion = game.GetNextQuestion();
                    Console.WriteLine(game.GetQuestionNumberText());
                    Console.WriteLine(currentQuestion.Text);

                    int userAnswer = GetNumber();
                    game.AcceptAnswer(userAnswer);
                }

                var message = game.CalculateDiagnose();
                Console.WriteLine(message);

                if (!HandleUserChoices(user))
                {
                    break;
                }
            }

            Console.WriteLine("Спасибо за игру!");
        }

        private static bool HandleUserChoices(User user)
        {
            if (UserChoice($"{user.Name}, Хотите посмотреть предыдущие результаты? (да/нет)"))
            {
                UsersResultStorage.ShowAll();
            }

            if (UserChoice($"{user.Name}, Хотите добавить новый вопрос? (да/нет)"))
            {
                AddNewQuestion();
            }

            if (UserChoice($"{user.Name}, Хотите удалить существующий вопрос? (да/нет)"))
            {
                RemoveQuestion();
            }

            return UserChoice($"{user.Name}, Хотите пройти тест снова? (да/нет)");
        }

        private static int GetNumber()
        {
            int number;
            while (!InputValidator.TryParseToNumber(Console.ReadLine(), out number, out string errorMessage))
            {
                Console.WriteLine(errorMessage);
            }
            return number;
        }

        private static void RemoveQuestion()
        {
            var questions = QuestionsStorage.GetAll();
            Console.WriteLine("Введите номер вопроса, который хотите удалить");

            for (int i = 0; i < questions.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {questions[i].Text}");
            }

            int number = GetNumber();

            while (number < 1 || number > questions.Count)
            {
                Console.WriteLine($"Введите число от 1 до {questions.Count}");
                number = GetNumber();
            }

            QuestionsStorage.Remove(questions[number - 1]);
        }

        private static void AddNewQuestion()
        {
            Console.WriteLine("Введите текст нового вопроса");
            var text = Console.ReadLine();

            Console.WriteLine("Введите ответ на данный вопрос");
            var answer = GetNumber();

            QuestionsStorage.Add(new Question(text, answer));
        }

        private static bool UserChoice(string question)
        {
            Console.WriteLine(question);
            string response = Console.ReadLine()?.Trim().ToLower();
            return response == "да" || response == "yes";
        }
    }
}