using GeniyIdiotClassLibrary;
using System;
using System.Windows.Forms;
using System.Threading;

namespace WinFormApp
{
    public partial class MainForm : Form
    {
        Game game;
        User user;
        private System.Windows.Forms.Timer questionTimer;
        private int timeLeft = 10;

        public MainForm()
        {
            InitializeComponent();

            questionTimer = new System.Windows.Forms.Timer();
            questionTimer.Interval = 1000; 
            questionTimer.Tick += QuestionTimer_Tick;
        }

        private void mainForm_Load(object sender, EventArgs e)
        {
            var welcomeForm = new WelcomeForm();
            welcomeForm.ShowDialog();

            user = new User(welcomeForm.userNameTextBox.Text);
            game = new Game(user);

            ShowNextQuestion();
        }

        private void ShowNextQuestion()
        {
            timeLeft = 10;
            timerLabel.Text = $"Осталось: {timeLeft} сек.";
            questionTimer.Start();

            var currentQuestion = game.GetNextQuestion();
            questionTextLabel.Text = currentQuestion.Text;
            questionNumberLabel.Text = game.GetQuestionNumberText();
            userAnswerTextBox.Clear();
            userAnswerTextBox.Focus();
        }

        private void QuestionTimer_Tick(object sender, EventArgs e)
        {
            timeLeft--;
            timerLabel.Text = $"Осталось: {timeLeft} сек.";

            if (timeLeft <= 0)
            {
                questionTimer.Stop();
                ProcessTimeout();
            }
        }

        private void ProcessTimeout()
        {
            game.AcceptAnswer(0); 

            if (game.End())
            {
                ShowFinalResults();
            }
            else
            {
                ShowNextQuestion();
            }
        }

        private void nextButton_Click(object sender, EventArgs e)
        {
            questionTimer.Stop(); 

            var parsed = InputValidator.TryParseToNumber(userAnswerTextBox.Text, out int userAnswer, out string errorMessage);
            if (!parsed)
            {
                MessageBox.Show(errorMessage);
                questionTimer.Start(); 
            }
            else
            {
                game.AcceptAnswer(userAnswer);

                if (game.End())
                {
                    ShowFinalResults();
                }
                else
                {
                    ShowNextQuestion();
                }
            }
        }

        private void ShowFinalResults()
        {
            var message = game.CalculateDiagnose();
            MessageBox.Show(message);

            userAnswerTextBox.Enabled = false;
            nextButton.Enabled = false;
        }

        
        private void выходToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void рестартToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Restart();
        }

        private void показатьПредыдущиеРезультатыToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var resultsForm = new ResultsForm();
            resultsForm.ShowDialog();
        }
    }
}