using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeniyIdiotClassLibrary
{
    public class DiagnoseCalculator
    {
        private static readonly string[] diagnoses = new string[]
        {
            "кретин", "идиот", "дурак", "нормальный", "талант", "гений"
        };

        public static string Calculate(int rightAnswersCount, int totalQuestions)
        {
            int diagnosisIndex = rightAnswersCount * (diagnoses.Length - 1) / totalQuestions;

            if (diagnosisIndex < 0)
                diagnosisIndex = 0;
            else if (diagnosisIndex >= diagnoses.Length)
                diagnosisIndex = diagnoses.Length - 1;

            return diagnoses[diagnosisIndex];
        }



    }
}
