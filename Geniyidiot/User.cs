using System;

namespace Geniyidiot
{
    public class User
    {
        public string Name { get; set; }

        public int CountRightAnswers { get; set; }

        public string Diagnose { get; set; }

        public User(string name)
        {
            Name = name;
            Diagnose = "Неизвестен";
        }

        public void Print()
        {
            Console.WriteLine($"{Name}, твой диагноз: {Diagnose}. Количество правильных ответов - {CountRightAnswers}.");
        }

        public void IncreaseRightAnswers() => CountRightAnswers++;
    }
}