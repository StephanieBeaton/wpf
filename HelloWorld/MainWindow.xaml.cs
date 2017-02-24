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

namespace HelloWorld
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            uxSubmit.IsEnabled = (uxName.Text.Length > 0 && uxPassword.Text.Length > 0);

        }

        private void uxSubmit_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Submitting password:" + uxPassword.Text);
        }


        private void TextBox_KeyUp(object sender, KeyEventArgs e)
        {
            // As soon as the user enter text in uxName and uxPassword, 
            // the uxSubmit button should be enabled
            uxSubmit.IsEnabled = (uxName.Text.Length > 0 && uxPassword.Text.Length > 0);

        }
    }
}
