using GeographyQuizGUI.Viewcontroller;
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
    /// Interaction logic for MainMenuView.xaml
    /// </summary>
    public partial class MainMenuView : Page
    {
        public ViewController controller { get; set; } = null;

        public MainMenuView(ViewController controller) {
            InitializeComponent();

            this.controller = controller;
        }

        private void HighScore_Click(object sender, RoutedEventArgs e) {
            this.NavigationService.Navigate(new HighScoreView(controller));
        }

        private void NewQuiz_Click(object sender, RoutedEventArgs e) {
            this.NavigationService.Navigate(new NewQuizView(controller));
        }

        private void ExitButton_Click(object sender, RoutedEventArgs e) {
            Environment.Exit(0);
        }
    }
}
