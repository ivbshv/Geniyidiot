using Geniyidiot;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GeniyIdiot
{
    public static class QuestionsStorage
    {
        private static List<Question> _questions = new List<Question>
        {
            new Question("Сколько будет два плюс два умноженное на два?", 6),
            new Question("Бревно нужно распилить на 10 частей. Сколько распилов нужно сделать?", 9),
            new Question("На двух руках 10 пальцев. Сколько пальцев на 5 руках?", 25),
            new Question("Укол делают каждые полчаса. Сколько нужно минут, чтобы сделать три укола?", 60),
            new Question("Пять свечей горело, две потухли. Сколько свечей осталось?", 2),
            new Question("Сколько будет 5 в квадрате?", 25),
            new Question("Сколько месяцев в году имеют 28 дней?", 12)
        };

        public static List<Question> GetAll() => _questions;

        public static void Add(Question question)
        {
            if (question == null)
                throw new ArgumentNullException(nameof(question));

            if (string.IsNullOrWhiteSpace(question.Text))
                throw new ArgumentException("Текст вопроса не может быть пустым");

            if (_questions.Any(q => q.Text.Equals(question.Text, StringComparison.OrdinalIgnoreCase)))
                throw new ArgumentException("Такой вопрос уже существует");

            _questions.Add(question);
        }

        public static void Remove(int index)
        {
            if (index < 0 || index >= _questions.Count)
                throw new ArgumentOutOfRangeException(nameof(index));

            _questions.RemoveAt(index);
        }
    }
}