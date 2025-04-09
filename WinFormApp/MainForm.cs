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
        Game game;

        User user;

        public MainForm()
        {
            InitializeComponent();
        }

        private void mainForm_Load(object sender, EventArgs e)
        {
            var welcomeForm = new WelcomeForm();
            welcomeForm.ShowDialog();

            var user = new User(welcomeForm.userNameTextBox.Text);
            game = new Game(user);
           

            ShowNextQuestion();
            
        }

        private void ShowNextQuestion()
        {

            var currentQuestion =  game.GetNextQuestion();
            questionTextLabel.Text = currentQuestion.Text;

            questionNumberLabel.Text = game.GetQuestionNumberText();
            
        }

        private void nextButton_Click(object sender, EventArgs e)
        {
            var parsed = InputValidator.TryParseToNumber(userAnswerTextBox.Text, out int userAnswer, out string errorMessage);
            if (!parsed)
            {
                MessageBox.Show(errorMessage);
            }
            else
            {
                game.AcceptAnswer(userAnswer);

               

                if (game.End())
                {
                    var message = game.CalculateDiagnose();

                    MessageBox.Show(message);
                    return;
                }
                ShowNextQuestion();
            }
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
