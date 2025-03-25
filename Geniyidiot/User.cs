using System;

namespace Geniyidiot
{
    public class User
    {
        public string Name { get; set; }

        public int CountRightAnswers { get; set; }

        public string Diagnoses { get; set; }

        public User(string name, int countRightAnswers, string diagnoses)
        {
            Name = name;
            CountRightAnswers = countRightAnswers;
            Diagnoses = diagnoses;
        }

        public void Print()
        {
            Console.WriteLine($"{Name}, твой диагноз: {Diagnoses}. Количество правильных ответов - {CountRightAnswers}.");
        }
    }
}