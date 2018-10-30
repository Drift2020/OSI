using Cash.Command;
using Cash.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Cash.ViweModel
{
    class View_Model_Reset : View_Model_Base
    {
        CashDB myDB;

        public View_Model_Reset(CashDB temp)
        {
            myDB = temp;
        }
        void OpenMessege(string s, string title)
        {
            Messege messege = new Messege();
            View_Model_Messege messege_view_Model = new View_Model_Messege(System.Windows.Visibility.Visible, System.Windows.Visibility.Hidden, System.Windows.Visibility.Hidden);

            if (messege_view_Model._OK == null)
                messege_view_Model._OK = new Action(messege.Close);


            messege.DataContext = messege_view_Model;
            messege_view_Model.Messege = s;
            messege_view_Model.Messeg_Titel = title;
            messege.ShowDialog();
        }
        #region pole
        Regex regex_password = new Regex(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*[0-9])(?=.*[^a-zA-Z0-9])\S{1,16}$");
        #region password
        string password;
        public string Password
        {
            get
            {
                return password;
            }
            set
            {
                password = value;
                OnPropertyChanged(nameof(Password));
            }
        }
        #endregion
        #region password2
        string password2;
        public string Password2
        {
            get
            {
                return password2;
            }
            set
            {
                password2 = value;
                OnPropertyChanged(nameof(Password2));
            }
        }
        #endregion
        #region secret_word
        string secret_word;
        public string Secret_word
        {
            get
            {
                return secret_word;
            }
            set
            {
                secret_word = value;
                OnPropertyChanged(nameof(Secret_word));
            }
        }
        #endregion secret_word
        #region login
        string login;
        public string Login
        {
            get
            {
                return login;
            }
            set
            {
                login = value;
                OnPropertyChanged(nameof(Login));
            }
        }
        #endregion
        #endregion pole

        #region Action
        public bool is_ok;

       


        public Action _OK { get; set; }

        #endregion
        #region OK
        private DelegateCommand _Command_ok;
        public ICommand Button_clik_ok
        {
            get
            {
                if (_Command_ok == null)
                {
                    _Command_ok = new DelegateCommand(Execute_ok, CanExecute_ok);
                }
                return _Command_ok;
            }
        }
        private void Execute_ok(object o)
        {
            try
            {
                var temp = myDB.People.ToList().Find(x => x.Login.Contains(login));

                if (temp == null)
                {
                    OpenMessege("There is no such login or it is not true.", "Error");
                    return;
                }
                if (temp.Secret_word.Contains(secret_word))
                {
                    OpenMessege("The secret password is not suitable, contact your family administrator.", "Error");
                    return;
                }

                bool is_oks = regex_password.IsMatch(password);


                if (password != password2 || !is_oks)
                {
                    OpenMessege("The password must be at least one digit, one letter (English), a large letter and any character that is not a digit and not a letter, the maximum password length is 16 characters.", "Error");
                    return;
                }




                temp.Password = password;


                myDB.SaveChanges();

                is_ok = true;

                OpenMessege("The recovery was successful.", "Success");
                _OK();
            }catch (Exception e) { OpenMessege(e.Message, "Error"); }

        }
        private bool CanExecute_ok(object o)
        {

            if ((login != null && login != "") &&
                (password != null && password != "") &&
                (password2 != null && password2 != "") &&
                
                secret_word != null && secret_word.Length > 0
                )
                return true;
            return false;

        }
        #endregion
    }
}
