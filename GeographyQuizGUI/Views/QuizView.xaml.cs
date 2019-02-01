using GeographyQuizGUI.Viewcontroller;
using GeographyQuizGUI.Viewmodel;
using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace GeographyQuizGUI.Views
{
    /// <summary>
    /// Interaction logic for QuizView.xaml
    /// </summary>
    public partial class QuizView : Page
    {
        ViewController controller { get; set; } = null;

        public QuizView(ViewController controller) {
            InitializeComponent();

            this.controller = controller;
        }

        /*
         * Update this view to display the current question of the current quiz.
         */
        private void LoadCurrentQuestion() {
            int mode = controller._viewModel.quiz.Mode;
            Question currentQuestion = controller._viewModel.quiz.Questions[controller._viewModel.quiz.QuestionProgressCounter];
            controller._viewModel.quiz.Shuffle(currentQuestion.Choices);

            string answer = currentQuestion.Answer.Alpha_2 + ".png";
            string choice1 = currentQuestion.Choices[0].Alpha_2 + ".png";
            string choice2 = currentQuestion.Choices[1].Alpha_2 + ".png";
            string choice3 = currentQuestion.Choices[2].Alpha_2 + ".png";
            string choice4 = currentQuestion.Choices[3].Alpha_2 + ".png";

            var answerImageUri = new Uri(System.IO.Path.Combine(Environment.CurrentDirectory, "../../assets/64x64", answer));
            var choice1ImageUri = new Uri(System.IO.Path.Combine(Environment.CurrentDirectory, "../../assets/32x32", choice1));
            var choice2ImageUri = new Uri(System.IO.Path.Combine(Environment.CurrentDirectory, "../../assets/32x32", choice2));
            var choice3ImageUri = new Uri(System.IO.Path.Combine(Environment.CurrentDirectory, "../../assets/32x32", choice3));
            var choice4ImageUri = new Uri(System.IO.Path.Combine(Environment.CurrentDirectory, "../../assets/32x32", choice4));

            GamePlayerName.Text = $"Player: {controller._viewModel.quiz.Player}";
            GameQuestionCounter.Text = $"Question: {controller._viewModel.quiz.QuestionProgressCounter + 1}";
            GameScore.Text = $"Score: {controller._viewModel.quiz.Score}";
            controller._viewModel.quiz.CurrentQuestionIsAnswered = false;
            ChoiceBox1.IsEnabled = true;
            ChoiceBox2.IsEnabled = true;
            ChoiceBox3.IsEnabled = true;
            ChoiceBox4.IsEnabled = true;

            // Guess the country mode
            if (mode == 1) {             
                QuestionText.Text = $"Capital: {currentQuestion.Answer.Capital}";
                QuestionImage.Source = new BitmapImage(answerImageUri);

                ChoiceBox1Text.Text = $"{currentQuestion.Choices[0].Name}";
                ChoiceBox2Text.Text = $"{currentQuestion.Choices[1].Name}";
                ChoiceBox3Text.Text = $"{currentQuestion.Choices[2].Name}";
                ChoiceBox4Text.Text = $"{currentQuestion.Choices[3].Name}";


            // Guess the flag mode
            } else if (mode == 2) {
                QuestionText.Text = $"Country: {currentQuestion.Answer.Name}\nCapital: {currentQuestion.Answer.Capital}";
                ChoiceBox1Text.Text = "";
                ChoiceBox2Text.Text = "";
                ChoiceBox3Text.Text = "";
                ChoiceBox4Text.Text = "";
                ChoiceImage1.Source = new BitmapImage(choice1ImageUri);
                ChoiceImage2.Source = new BitmapImage(choice2ImageUri);
                ChoiceImage3.Source = new BitmapImage(choice3ImageUri);
                ChoiceImage4.Source = new BitmapImage(choice4ImageUri);

            // Guess the capital mode
            } else if (mode == 3) {
                QuestionText.Text = $"Country: {currentQuestion.Answer.Name}";
                QuestionImage.Source = new BitmapImage(answerImageUri);

                ChoiceBox1Text.Text = $"{currentQuestion.Choices[0].Capital}";
                ChoiceBox2Text.Text = $"{currentQuestion.Choices[1].Capital}";
                ChoiceBox3Text.Text = $"{currentQuestion.Choices[2].Capital}";
                ChoiceBox4Text.Text = $"{currentQuestion.Choices[3].Capital}";
                
            }
        }

        /*
         * Generate the endscreen once the quiz has ended, save the 
         * highscore and navigate back to the main menu. 
         */
        private void EndScreen() {
            string playername = controller._viewModel.quiz.Player;
            int score = controller._viewModel.quiz.Score;
            MessageBox.Show($"Congratulations {playername}!\nYou answered {score / 10} out of 10 questions correctly for a total score of {score}!");
            controller._viewModel.SaveNewHighScore();
            this.NavigationService.Navigate(new MainMenuView(controller));
        }

        private void MainMenu_Click(object sender, RoutedEventArgs e) {
            if (!controller._viewModel.quiz.CurrentQuestionIsAnswered) {
                this.NavigationService.Navigate(new MainMenuView(controller));
            }
        }

        private void Page_Loaded(object sender, RoutedEventArgs e) {
            LoadCurrentQuestion();      
        }

        private void Page_PreviewMouseDown(object sender, MouseButtonEventArgs e) {
            if (controller._viewModel.quiz.CurrentQuestionIsAnswered) {
                LoadCurrentQuestion();
            }
        }

        /*
         * Determine whether or not the user given answer is correct.
         */
        private void ChoiceBox_Click(object sender, RoutedEventArgs e) {
            Question currentQuestion = controller._viewModel.quiz.Questions[controller._viewModel.quiz.QuestionProgressCounter];
            Button source = e.Source as Button;

            char lastChar = source.Name[source.Name.Length-1];
            int choiceNumber = int.Parse(lastChar.ToString());
            
            if (currentQuestion.Choices[choiceNumber - 1] == currentQuestion.Answer) {
                controller._viewModel.quiz.Score += 10;
                GameScore.Text = $"Score: {controller._viewModel.quiz.Score}";
                MessageBox.Show("Correct");
            } else {
                MessageBox.Show("False");
            }

            controller._viewModel.quiz.CurrentQuestionIsAnswered = true;
            controller._viewModel.quiz.QuestionProgressCounter += 1;
            ChoiceBox1.IsEnabled = false;
            ChoiceBox2.IsEnabled = false;
            ChoiceBox3.IsEnabled = false;
            ChoiceBox4.IsEnabled = false;

            if (controller._viewModel.quiz.QuestionProgressCounter == 10) {
                EndScreen();
            }
        }
    }
}
