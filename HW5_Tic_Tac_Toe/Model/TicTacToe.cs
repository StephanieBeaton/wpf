using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.ComponentModel;            // INotifyPropertyChanged Interface
using System.Collections.Specialized;   // INotifyCollectionChanged

using System.Windows.Data;


namespace HW5_Tic_Tac_Toe.Model
{
    // ====================================================================
    //
    //   MyEqualityComparer Class
    //
    //  http://stackoverflow.com/questions/14663168/an-integer-array-as-a-key-for-dictionary
    //
    // ====================================================================

    public class MyEqualityComparer : IEqualityComparer<int[]>
    {
        public bool Equals(int[] x, int[] y)
        {
            if (x.Length != y.Length)
            {
                return false;
            }
            for (int i = 0; i < x.Length; i++)
            {
                if (x[i] != y[i])
                {
                    return false;
                }
            }
            return true;
        }

        public int GetHashCode(int[] obj)
        {
            int result = 17;
            for (int i = 0; i < obj.Length; i++)
            {
                unchecked
                {
                    result = result * 23 + obj[i];
                }
            }
            return result;
        }
    }

    // ====================================================================
    //
    //   TicTacToe Class
    //
    // ====================================================================
    class TicTacToe : IDataErrorInfo, INotifyPropertyChanged
    {
        // ===================================================
        #region Constants
        public const int row_col_start_index = 0;

        public const int row_col_end_index = totalRows - 1;

        public const int totalRows = 3;
        #endregion
        // ===================================================



        // ===================================================
        #region Propeties

        public string Player { get; private set; }

        public Dictionary<int[], TicTacToeSquare> tictactoeDictionary { get; private set; }

        #endregion
        // ===================================================



        // ===================================================
        #region Constructors

        public TicTacToe()
        {
            Player = "Player X";     // X starts game

            //Collection = new CollectionViewSource();

            //Collection.Source = tictactoeDictionary;

            tictactoeDictionary = new Dictionary<int[], TicTacToeSquare>(new MyEqualityComparer() );


            //  initialize the text displayed in all the squares in the Tic Tac Toe board 
            //  ... to the empty string

            int[] row_col_key;

            for (int row = row_col_start_index; row <= row_col_end_index; row++)
            {
                for (int col = row_col_start_index; col <= row_col_end_index; col++)
                {
                    row_col_key = new int[] {row, col};

                    TicTacToeSquare square = new Model.TicTacToeSquare(String.Empty);
                    tictactoeDictionary.Add(row_col_key, square);
                }
            }

            row_col_key = new int[] { row_col_start_index, row_col_start_index };
            var temp = tictactoeDictionary[row_col_key];

        }

        #endregion
        // ===================================================

        // ===================================================
        #region IDataErrorInfo Interface implementation

        public string this[string columnName]
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public string Error
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        #endregion
        // ===================================================

        // ===================================================
        #region INotifyPropertyChanged Interface implementation

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        #endregion
        // ===================================================


        // ==================================================
        public string Add(int row, int column)
        {
            int strLength = "Player X".ToString().Length;

            string returnValue = Player.ToString().Substring(strLength - 1, 1);    // "Player X"   get "X" only


            //string sub = input.Substring(0, 3);

            int[] myKey = new int[] { row, column };

            TicTacToeSquare square;

            if (tictactoeDictionary.TryGetValue(myKey, out square))
            {
                // the squares are all initialized to Empty Strings
                // ... so should always go thru here or an error occured.

                // ... if square contains Empty String set to X or O

                if (tictactoeDictionary[myKey].ToString() == String.Empty )
                {
                    tictactoeDictionary[myKey] = new TicTacToeSquare(returnValue);
                    TogglePlayer();
                }
                else
                {
                    // User clicked on button that is already X or O
                    // ... do nothing
                }

            }
            else
            {
                // the squares are all initialized to Empty Strings
                // ... so should NEVER go thru here or an error occured.
                // throw an Exception

            }

            return returnValue;
        }
        // ==================================================

        // ==================================================
        private void TogglePlayer()
        {
            if (Player == "Player X")
            {
                Player = "Player O";
            }
            else
            {
                Player = "Player X";
            }

            // Notify Status bar Player changed
            OnPropertyChanged("Player");
        }
        // ==================================================

        // ==================================================
        public Boolean GameWon(int row, int column)
        {
            Boolean gameWon = true;

            int[] row_col_key = new int[] { row, column };

            string playerName = tictactoeDictionary[row_col_key].ToString();

            // determine whether game is over
            //     take position that was clicked and look for three in a row in 4 possible directions
            //        - look east-west
            //        - look north-south
            //        - look southwest - northeast
            //        - look southeast - northwest
            // change the next letter that will be displayed from X to O or O to X  in the Status Bar


            gameWon = true;            
            // look east-west
            // ... look at all columns in the row
            for (int tempColumn = row_col_start_index; tempColumn <= row_col_end_index; tempColumn++ )
            {
                int[] temp_row_col_key = new int[] { row, tempColumn };

                string testValue = tictactoeDictionary[temp_row_col_key].ToString();

                if (playerName != testValue)
                {
                    gameWon = false;
                }

            }

            if (gameWon == true) return gameWon;


            gameWon = true;
            // look north-south
            // ... look at all rows in the column
            for (int tempRow = row_col_start_index; tempRow <= row_col_end_index; tempRow++)
            {
                int[] temp_row_col_key = new int[] { tempRow, column };

                string testValue = tictactoeDictionary[temp_row_col_key].ToString();

                if (playerName != testValue)
                {
                    gameWon = false;
                }

            }
            if (gameWon == true) return gameWon;

            gameWon = true;
            // if row, column is in the northwest - southeast diagonal
            // check northwest - southeast diagonal
            // ... look at all squares in northwest to southeast diagonal
            if (row == column)
            {
                int tempColumn = 0;

                for (int tempRow = 0; tempRow <= row_col_start_index; tempRow++)
                {
                    tempColumn = tempRow;
                    int[] temp_row_col_key = new int[] { tempRow, tempColumn };

                    string testValue = tictactoeDictionary[temp_row_col_key].ToString();

                    if (playerName != testValue)
                    {
                        gameWon = false;
                    }
                }
            }
            if (gameWon == true) return gameWon;

            gameWon = true;
            // if row, column is in the northeast - southwest diagonal
            // check northeast - southwest diagonal
            // ... look at all squares in northeast to southwest diagonal
            if ( (totalRows - column) == row)
            {
                int tempColumn = 0;

                for (int tempRow = 0; tempRow <= row_col_start_index; tempRow++)
                {
                    tempColumn = totalRows - tempRow;

                    int[] temp_row_col_key = new int[] { tempRow, tempColumn };

                    string testValue = tictactoeDictionary[temp_row_col_key].ToString();

                    if (playerName != testValue)
                    {
                        gameWon = false;
                        return gameWon;
                    }
                }
            }

            return gameWon;
        }
        // ==================================================

    }
}
