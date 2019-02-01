using GeographyQuizGUI.Models;
using GeographyQuizGUI.Viewcontroller;
using GeographyQuizGUI.Views;
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

namespace GeographyQuizGUI
{
    /// <summary>
    /// Interaction logic for HighScoreView.xaml
    /// </summary>
    public partial class HighScoreView : Page
    {
        public ViewController controller { get; set; } = null;

        public HighScoreView(ViewController controller) {
            InitializeComponent();

            this.controller = controller;
        }

        private void HighScoreView_Load(object sender, RoutedEventArgs e) {
            var list = controller._viewModel.GetOrderedHighscores();
            foreach(var record in list) {
                this.HighscoreTableName.Text += $"{record.PlayerName}\n";
                this.HighscoreTableScore.Text += $"{record.Score}\n";
            }
        }

        private void MainMenu_Click(object sender, RoutedEventArgs e) {
            this.NavigationService.Navigate(new MainMenuView(controller));
        }

        /*
         * Update the highscore table on keyboard input. 
         */
        private void PlayerNameInput_TextChanged(object sender, TextChangedEventArgs e) {
            if (PlayerNameInput.Text.Trim() == "") {
                var list = controller._viewModel.GetOrderedHighscores();
                this.HighscoreTableName.Text = "";
                this.HighscoreTableScore.Text = "";
                foreach(var record in list) {

                    this.HighscoreTableName.Text += $"{record.PlayerName}\n";
                    this.HighscoreTableScore.Text += $"{record.Score}\n";
                }
            }
            else {
                var list = controller._viewModel.GetOrderedHighscores(PlayerNameInput.Text);
                this.HighscoreTableName.Text = "";
                this.HighscoreTableScore.Text = "";
                foreach(var record in list) {
                    this.HighscoreTableName.Text += $"{record.PlayerName}\n";
                    this.HighscoreTableScore.Text += $"{record.Score}\n";
                }
            }
        }
    }
}
