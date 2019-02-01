using GeographyQuizGUI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeographyQuizGUI.Viewmodel
{
    public class Question
    {
        public Country Answer { get; set; }
        public List<Country> Choices { get; set; }

        public Question(Country answer, List<Country> choices) {
            Answer = answer;
            Choices = choices;
        }

    }
}
