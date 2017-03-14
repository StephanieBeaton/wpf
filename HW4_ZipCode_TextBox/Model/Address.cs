using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.ComponentModel;
using System.Collections;

using System.Text.RegularExpressions;   // Regex class

namespace HW4_ZipCode_TextBox.Model
{

    class Address : INotifyPropertyChanged,  IDataErrorInfo
    {
        private string _postalCode = string.Empty;
        private string _postalCodeError;
        private Boolean _submitButtonEnabled;

        // Add ToString method
        public override string ToString()
        {
            return _postalCode;
        }

        #region Properties

        public string PostalCode
        {
            get
            {
                return _postalCode;
            }

            set
            {
                if (value != _postalCode)
                {
                    _postalCode = value;

                    // trigger event 
                    OnPropertyChanged("PostalCode");
                }
            }
        }

        public Boolean SubmitButtonEnabled
        {
            get
            {
                return _submitButtonEnabled;
            }

            set
            {
                if (value != _submitButtonEnabled)
                {
                    _submitButtonEnabled = value;

                    // trigger event 
                    OnPropertyChanged("SubmitButtonEnabled");
                }
            }
        }

        public string PostalCodeError
        {
            get
            {
                return _postalCodeError;
            }
            set
            {
                if (_postalCodeError != value)
                {
                    _postalCodeError = value;
                    OnPropertyChanged("PostalCodeError");
                }
            }
        }
        #endregion


        #region IDataErrorInfo Interface implementation

        public string this[string columnName]
        {
            get
            {
                PostalCodeError = "";

                switch (columnName)
                {
                    case "PostalCode":
                        {
                            //if (PostalCode != "V6N2R9")
                            //{
                            //    PostalCodeError = "Invalid Postal Code";
                            //    SubmitButtonEnabled = false;
                            //    OnPropertyChanged("SubmitButtonEnabled");
                            //}
                            //else
                            //{
                            //    SubmitButtonEnabled = true;
                            //    OnPropertyChanged("SubmitButtonEnabled");
                            //}

                            // ===============================================

                            // U.S. Postal Code
                            string US_PostalCode_Pattern = @"^\d{5}(-\d{4}){0,1}$";

                            // Canadian Postal Code
                            string Canadian_PostalCode_Pattern = @"^[A-Z]\d[A-Z]\d[A-Z]\d$";

                            // =================

                            Regex findValidPostalCodesUS = new Regex(US_PostalCode_Pattern);

                            var postalCodeMatchUS = findValidPostalCodesUS.Matches(PostalCode);

                            // =================

                            Regex findValidPostalCodesCanada = new Regex(Canadian_PostalCode_Pattern);

                            var postalCodeMatchCanada = findValidPostalCodesCanada.Matches(PostalCode);

                            // ================

                            if (postalCodeMatchUS.Count == 0 && postalCodeMatchCanada.Count == 0)
                            {
                                PostalCodeError = "Invalid Postal Code";
                                SubmitButtonEnabled = false;
                                OnPropertyChanged("SubmitButtonEnabled");
                            }
                            else
                            {
                                SubmitButtonEnabled = true;
                                OnPropertyChanged("SubmitButtonEnabled");
                            }


                            // ===============================================




                            break;
                        }
                }

                return PostalCodeError;
            }
        }



        // this method is not used
        public string Error
        {
            get
            {
                throw new NotImplementedException();
                //return PostalCodeError;
            }
        }

        #endregion


        #region INotifyPropertyChanged Interface implementation

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

    }
    #endregion
}

