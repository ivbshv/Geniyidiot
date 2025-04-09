using GeniyIdiotClassLibrary;
using System.Collections.Generic;
using System;

public class Game
{
    User user;
    private List<Question> questions;
    private Question currentQuestion;
    private int questionNumber = 1;

    public Game(User user)
    {
        this.user = user;
        questions = QuestionsStorage.GetAll();
    }

    public Question GetNextQuestion()
    {
        if (questions.Count == 0)
            return null;

        var random = new Random();
        var randomQuestionIndex = random.Next(0, questions.Count);
        currentQuestion = questions[randomQuestionIndex];

        questionNumber++;
        return currentQuestion;
    }

    public void AcceptAnswer(int userAnswer)
    {
        var rightAnswer = currentQuestion.Answer;
        if (userAnswer == rightAnswer)
        {
            user.IncreaseRightAnswers();
        }
        questions.Remove(currentQuestion);
    }

    public string GetQuestionNumberText()
    {
        return "Вопрос № " + questionNumber;
    }

    public bool End()
    {
        return questions.Count == 0;
    }

    public string CalculateDiagnose()
    {
        var totalQuestions = questionNumber;
        var diagnose = DiagnoseCalculator.Calculate(user.CountRightAnswers, totalQuestions);
        user.Diagnose = diagnose;
        UsersResultStorage.Add(user);

        return $"{user.Name}, твой диагноз: {user.Diagnose}. Количество правильных ответов - {user.CountRightAnswers} из {totalQuestions}.";
    }
}