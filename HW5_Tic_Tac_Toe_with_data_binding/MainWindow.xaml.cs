
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

using HW5_Tic_Tac_Toe_with_data_binding.Model;

namespace HW5_Tic_Tac_Toe_with_data_binding
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

            //  xxxxxxxx  Test Code here xxxxxxxxxxxxxxxxxxxxxxx
            //var children = this.uxGrid.Children;

            //Button button = (children[0] as Button);

            //int[] row_col_key = new int[] { TicTacToe.row_col_start_index,
            //                                TicTacToe.row_col_start_index };

            //TicTacToeSquare square = _tictactoe.tictactoeDictionary[row_col_key];

            //button.Content = square.SquarePlayer.ToString();

            // convert 0 -> 8 (buttons indices) to  [0,0] -> [2,2] tictactoeDictionary indices
            // and vice versa

        }

        private void uxNewGame_Click(object sender, RoutedEventArgs e)
        {
            // reset a bunch of values
            _tictactoe = new TicTacToe();

            // Try to bind the StatusBar text to a property in TicTacToe
            this.DataContext = _tictactoe;

            // refresh the Tic Tac Toe buttons
            // not necesary if data binding was working
            Refresh();

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (_tictactoe.gameWon == true) return;

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

            // check that squareDisplayChar == "X" or "O"

            // ================================================================================
            // display an X or an O in the button
            // 
            (sender as Button).Content = squareDisplayChar;

            // ================================================================================


            // did the player win the game?
            // GameWon() sets public property "gameWon" to true if game was won in _tictactoe instance
            Boolean gameWon = _tictactoe.GameWon(row, column);

            if (gameWon)
            {
                MessageBox.Show(String.Format("Player {0} won the game!", squareDisplayChar));
            }


            // If game won, take appropriate actions
            //   set CanExecute to false ??

        }

        private void OnClose_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Refresh()
        {
            var children = this.uxGrid.Children;

            int row = 0;
            int column = 0;

            for (int i = 0; i < children.Count; i++)
            {
                Button button = (children[i] as Button);

                // convert 0 -> 8 (buttons indices) to  [0,0] -> [2,2] tictactoeDictionary indices
                row = i / TicTacToe.totalRows;
                column = i % TicTacToe.totalRows;

                int[] row_col_key = new int[] { row, column };

                TicTacToeSquare square = _tictactoe.tictactoeDictionary[row_col_key];

                button.Content = square.SquarePlayer.ToString();

            }

        }
    }
}
