using GeniyIdiotClassLibrary;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinFormApp
{
    public partial class MainForm : Form
    {
        private List<Question> questions;

        private int countQuestions;

        private int countRightAnswers = 0;

        private Question currentQuestion;

        private int questionNumber = 1;

        User user;

        public MainForm()
        {
            InitializeComponent();
        }

        private void mainForm_Load(object sender, EventArgs e)
        {
            var welcomeForm = new WelcomeForm();
            welcomeForm.ShowDialog();

            user = new User(welcomeForm.userNameTextBox.Text);
            questions = QuestionsStorage.GetAll();
            countQuestions = questions.Count;
            

            ShowNextQuestion();
            
        }

        private void ShowNextQuestion()
        {
            var random = new Random();
            var randomQuestionIndex = random.Next(0, questions.Count);
            currentQuestion = questions[randomQuestionIndex];
            questionTextLabel.Text = currentQuestion.Text;

            questionNumberLabel.Text = $"Вопрос № {questionNumber}";
            questionNumber++;
        }

        private void nextButton_Click(object sender, EventArgs e)
        {
            var userAnswer = Convert.ToInt32(userAnswerTextBox.Text);
            int rightAnswer = currentQuestion.Answer;
            if (userAnswer == rightAnswer)
            {
                countRightAnswers++;
                user.IncreaseRightAnswers();
            }
            questions.Remove(currentQuestion);

            var endGame = questions.Count == 0;

            if (endGame)
            {
                user.CountRightAnswers = countRightAnswers;
                user.Diagnose = DiagnoseCalculator.Calculate(countRightAnswers, countQuestions);

                UsersResultStorage.Add(user);

                MessageBox.Show(($"{user.Name}, твой диагноз: {user.Diagnose}. Количество правильных ответов - {user.CountRightAnswers}."));
                return;
            }
            ShowNextQuestion();
        }

        
    }
}
