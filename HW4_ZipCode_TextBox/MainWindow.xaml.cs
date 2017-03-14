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

using HW4_ZipCode_TextBox.Model;

namespace HW4_ZipCode_TextBox
{
    // ==============================================================================
    //
    //  CSHP 220 - C#  WPF 
    //
    // Homework 4: ZipCode TextBox           
    //
    // Create a new WPF Project with a TextBox that only accepts:
    //
    //   • US Zip Codes ##### or #####-####
    //   • Canadian Postal Codes: A#B#C#
    //
    // The window contains a Submit button that is only enabled when a valid zip code or postal code is entered.
    //
    // So for example, a user could enter 98122 or 98012-4444 or T1R2X4 and the Submit button would be enabled.
    //
    // The Submit button does not need to perform any action.
    //
    // ==============================================================================



    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            Address myAddress = new Address();

            uxGrid.DataContext = myAddress;
        }

 
    }
}
