using GeographyQuizGUI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeographyQuizGUI.Viewmodel
{
    public class Quiz
    {
        public List<Question> Questions { get; set; }
        public List<Country> Countries { get; set; }
        public string Player { get; set; }
        public int Mode { get; set; }
        public int Score { get; set; } = 0;
        public int QuestionProgressCounter { get; set; } = 0;
        public bool CurrentQuestionIsAnswered { get; set; } = false;

        /*
         * Construct a new quiz and prepare all questions and answers.
         */
        public Quiz(List<Country> countries, string player, int mode) {
            Questions = new List<Question>();
            Countries = countries;
            Player = player;
            Mode = mode;
            Random rng = new Random();

            Shuffle(Countries);

            for (int i = 0; i < 10; i++) {
                var answer = Countries[i];
                var choices = new List<Country>();

                choices.Add(answer);

                for (int j = 0; j < 3; j++) {
                    int random = rng.Next(10, Countries.Count - 2);
                    choices.Add(Countries[random]);
                }

                Shuffle(choices);
                var question = new Question(answer, choices);
                Questions.Add(question);
            }
        }

        /*
         * Shuffle the elements of a list of Country objects
         *
         * @param {List<Country>} list to be shuffled
         */
        public void Shuffle(List<Country> list) {
            Random rng = new Random();
            int size = list.Count;
            while (size > 1) {
                size--;
                int random = rng.Next(size + 1);
                Country temp = list[random];
                list[random] = list[size];
                list[size] = temp;
            }
        }
    }
}
