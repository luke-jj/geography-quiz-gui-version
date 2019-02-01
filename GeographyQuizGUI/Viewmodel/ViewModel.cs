using GeographyQuizGUI.Data;
using GeographyQuizGUI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace GeographyQuizGUI.Viewmodel
{
    /*
     * The ViewModel class manages the business logic for the Views and
     * their Controller.
     */
    public class ViewModel
    {
        public Context context;
        public Quiz quiz { get; set; } = null;

        public ViewModel(Context context) {
            this.context = context;
        }

        /*
         * Return true if username consists of alphanumeric characters, may
         * also contain underscore or hypen, except for beginning and end of
         * name.
         *
         * @param {string} the username to be tested
         * @return {bool} true or false
         */
        public bool validateUsername(string username) {
            Regex rx = new Regex(@"^(?=.{1,15}$)[A-Za-z0-9]+(?:[_-][A-Za-z0-9]+)*$");
            return rx.IsMatch(username) ? true : false;
        }

        /*
         * Return a list of the top ten highscores ordered by the highest score
         *
         * @return {IEnumerable<Highscore>} list of all highscores
         */
        public IEnumerable<Highscore> GetOrderedHighscores() {
            // return context.highscores.OrderByDescending(score => score.Score);
            return (from highscore in context.highscores
                orderby highscore.Score descending
                select highscore).Take(10);
        }

        /*
         * Return a list of the top ten highscores of a specific player ordered
         * by the highest score.
         *
         * @param {string} part of, or the whole of the playername to be queried
         * @return {IEnumerable<Highscore>} list of a players highscores
         */
        public IEnumerable<Highscore> GetOrderedHighscores(string playername) {
            return (from highscore in context.highscores
                where highscore.PlayerName
                        .ToLower()
                        .Contains(playername.ToLower())
                orderby highscore.Score descending
                select highscore).Take(10);
        }

        /*
         * Create a new quiz object and store it as a property of this ViewModel. 
         */
        public void GenerateNewQuiz(string playerName, int mode) {
            this.quiz = new Quiz(context.countries, playerName, mode);
        }

        /*
         * Generate new highscore and save it to the database.
         */
        public void SaveNewHighScore() {
            int newId = context.highscores.Count;
            var highScore = new Highscore(newId, quiz.Player, quiz.Score);
            context.highscores.Add(highScore);
            context.SaveToDatabase();
        }
    }
}
