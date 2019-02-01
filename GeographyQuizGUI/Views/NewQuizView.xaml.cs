using GeographyQuizGUI.Viewcontroller;
using GeographyQuizGUI.Viewmodel;
using System;
using System.Collections.Generic;
using System.Linq;
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
    /// Interaction logic for NewQuizView.xaml
    /// </summary>
    public partial class NewQuizView : Page
    {
        ViewController controller { get; set; } = null;

        public NewQuizView(ViewController controller) {
            InitializeComponent();

            this.controller = controller;
        }

        private void MainMenu_Click(object sender, RoutedEventArgs e) {
            this.NavigationService.Navigate(new MainMenuView(controller));
        }

        /*
         * Generate a new quiz on click and navigate to the QuizView.
         */
        private void StartQuiz_Click(object sender, RoutedEventArgs e) {
            int quizMode = 0;

            if ((bool) CapitalNameButton.IsChecked) {
                quizMode = 3;
            } else if ((bool) CountryNameButton.IsChecked) {
                quizMode = 1;
            } else if ((bool) FlagNameButton.IsChecked) {
                quizMode = 2;
            }

            controller._viewModel.GenerateNewQuiz(PlayerNameInput.Text, quizMode);
            this.NavigationService.Navigate(new QuizView(controller));
        }

        /*
         * Enable / disable the quiz start button depending on whether 
         * the given username is valid, and a quiz mode has been selected.
         */
        private void updateStartButton() {
            bool isCapital = (bool) CapitalNameButton.IsChecked;
            bool isCountryName = (bool) CountryNameButton.IsChecked;
            bool isFlag = (bool) FlagNameButton.IsChecked;
            var isValidUsername = controller._viewModel.validateUsername(PlayerNameInput.Text);
            

            if ((isCapital || isCountryName || isFlag) && isValidUsername) {
                StartQuizButton.IsEnabled = true;
            } else {
                StartQuizButton.IsEnabled = false;
            }
        }

        private void CountryNameButton_Checked(object sender, RoutedEventArgs e) {
            updateStartButton();
        }

        private void FlagNameButton_Checked(object sender, RoutedEventArgs e) {
            updateStartButton();
        }

        private void CapitalNameButton_Checked(object sender, RoutedEventArgs e) {
            updateStartButton();
        }

        /*
         * Control the color of the playername text input field depending 
         * on the validity of the entered username.
         */
        private void PlayerNameInput_TextChanged(object sender, TextChangedEventArgs e) {
            var userNameIsValid = controller._viewModel.validateUsername(PlayerNameInput.Text);
            var textBox = e.Source as TextBox;

            if (userNameIsValid) {
                textBox.Background = new SolidColorBrush(Color.FromRgb(210, 255, 204));
            } else {
                textBox.Background = new SolidColorBrush(Color.FromRgb(255, 195, 195));
            }

            updateStartButton();
        }
    }
}
