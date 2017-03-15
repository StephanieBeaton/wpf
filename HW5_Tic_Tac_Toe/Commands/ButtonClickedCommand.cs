using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using HW5_Tic_Tac_Toe.Model;

namespace HW5_Tic_Tac_Toe.Commands
{
    class ButtonClickedCommand : ICommand
    {
        private TicTacToe _ticTacToe;

        // Constructor
        public ButtonClickedCommand(TicTacToe ticTacToe)
        {
            _ticTacToe = ticTacToe;
        }

        // don't understand the body of this
        // ... copied from 
        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            // get row and column of clicked button
            // use Tag ??  or parameter ??
            int row = 0;
            int column = 0;

            // Add() also toggles the player
            _ticTacToe.Add(row, column);

            // did the player win the game?
            // If so, take appropriate actions
            //   set CanExecute to false ??

            //throw new NotImplementedException();
        }
    }
}
