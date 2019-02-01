using GeographyQuizGUI.Views;
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
using GeographyQuizGUI.Viewmodel;
using GeographyQuizGUI.Data;

namespace GeographyQuizGUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        ViewController controller { get; set; } = null;

        /*
         * MainWindow Initializes the controller, viewmodel and context 
         * class to be used for the runtime of this program.
         */
        public MainWindow() {
            InitializeComponent();

            this.controller = new ViewController(new ViewModel(new Context()));
            Loaded += MyWindow_Loaded;
        }

        private void MyWindow_Loaded(object sender, RoutedEventArgs e) {
            _WindowNavigator.NavigationService.Navigate(new MainMenuView(controller));
        }
    }
}
