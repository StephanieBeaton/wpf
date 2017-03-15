
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HW5_Tic_Tac_Toe_with_data_binding.Model
{
    class TicTacToeSquare : INotifyPropertyChanged
    {

        private string _player;

        public string SquarePlayer
        {

            get
            {
                return _player;
            }

            set
            {
                if (_player != value)
                {
                    _player = value;
                    OnPropertyChanged("Player");
                }
            }
        }

        // Constructor
        public TicTacToeSquare(string player)
        {
            SquarePlayer = player;
        }

        public override string ToString()
        {
            return SquarePlayer;
        }

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

    }
}
