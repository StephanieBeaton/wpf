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

using HW5_Tic_Tac_Toe.Model;

namespace HW5_Tic_Tac_Toe
{
    // ======================================================================================
    //
    //  CSHP 220 - C# WPF
    //
    // Homework 5 - Tic Tac Toe
    //
    //  The object is to get three in a row.
    //
    //  Rules:
    //
    //    • Allow placement only on empty spots.
    //    • X goes first, then O
    //    • When you get three in a row, you declare a winner.
    //
    // When a winner is declared, you disable any further development.
    //
    // The menu has File | New Game and File | Exit.
    //
    // New Game will erase the board and start with X again.
    //
    // The solution will be posted on Github after the class ends.
    //
    // You must use the following unmodified MainWindow.xaml.
    //
    // ======================================================================================

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private TicTacToe _tictactoe;

        public MainWindow()
        {
            InitializeComponent();

            _tictactoe = new TicTacToe();

            // Try to bind the StatusBar text to a property in TicTacToe
            this.DataContext = _tictactoe;

            var children = this.uxGrid.Children;

            Button button = (children[0] as Button);

            int[] row_col_key = new int[] { TicTacToe.row_col_start_index,
                                            TicTacToe.row_col_start_index };

            TicTacToeSquare square = _tictactoe.tictactoeDictionary[row_col_key];

            button.Content = square.SquarePlayer.ToString();

            // convert 0 -> 8 (buttons indices) to  [1,1] -> [3, 3] tictactoeDictionary indices
            // and vice versa

        }

        private void uxNewGame_Click(object sender, RoutedEventArgs e)
        {
            // reset a bunch of values
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            // figure out which button clicked.   
            // get row and column of clicked button
            // use Tag 

            string tag = (sender as FrameworkElement).Tag.ToString();

            // parse tag into row and column
            // string tag = "1,2";
            string[] values = tag.Split(',');
            for (int i = 0; i < values.Length; i++)
            {
                values[i] = values[i].Trim();
            }

            int row = Int32.Parse(values[0]);
            int column = Int32.Parse(values[1]);


            // check that TicTacToe.row_col_start_index <= row    <= TicTacToe.row_col_start_index
            // check that TicTacToe.row_col_start_index <= column <= TicTacToe.row_col_start_index

            // Add() also toggles the player
            // Add() returns the char to display in the square as a string type
            string squareDisplayChar = _tictactoe.Add(row, column);

            MessageBox.Show("character to display in Square", squareDisplayChar);

            // check that squareDisplayChar == "X" or "O"

            // ================================================================================
            // display an X or an O in the button
            // 
            (sender as Button).Content = squareDisplayChar;

            // ================================================================================


            // did the player win the game?
            Boolean gameWon = _tictactoe.GameWon(row, column);

            if (gameWon)
            {
                MessageBox.Show("Player {0} won the game!", squareDisplayChar);  
            }


            // If so, take appropriate actions
            //   set CanExecute to false ??

            // determine whether game is over
            //     take position that was clicked and look for three in a row in 4 possible directions
            //        - look east-west
            //        - look north-south
            //        - look southwest - northeast
            //        - look southeast - northwest
            // change the next letter that will be displayed from X to O or O to X  in the Status Bar
        }
    }
}
