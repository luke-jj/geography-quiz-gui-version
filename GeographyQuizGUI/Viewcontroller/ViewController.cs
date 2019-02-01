using GeographyQuizGUI.Viewmodel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeographyQuizGUI.Viewcontroller
{
    public class ViewController
    {
        public ViewModel _viewModel { get; set; }

        public ViewController(ViewModel viewModel) {
            _viewModel = viewModel;
        }
    }
}
