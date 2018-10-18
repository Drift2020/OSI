

using Chat.Code;
using Chat.View_model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Chat.Viwe_Model
{
    class View_Model_Messege : View_Model_Base
    {

        public View_Model_Messege(Visibility Visibility_ok, Visibility Visibility_on, Visibility Visibility_cancel)
        {


            visibility_ok = Visibility_ok;
            visibility_no = Visibility_on;
            visibility_cancel = Visibility_cancel;
        }

        #region String_button
        string ok = "OK";
        public string OK
        {
            get { return ok; }
            set
            {
                ok = value;
                OnPropertyChanged(nameof(OK));
            }
        }

        string no = "NO";
        public string NO
        {
            get { return no; }
            set
            {
                ok = value;
                OnPropertyChanged(nameof(NO));
            }
        }

        string cancel = "CANCEL";
        public string CANCEL
        {
            get { return cancel; }
            set
            {
                cancel = value;
                OnPropertyChanged(nameof(CANCEL));
            }
        }
        #endregion String_button

        #region Visibility_button
        Visibility visibility_ok;
        public Visibility Visibility_ok
        {
            get { return visibility_ok; }
            set
            {
                visibility_ok = value;
                OnPropertyChanged(nameof(Visibility_ok));
            }
        }

        Visibility visibility_no;
        public Visibility Visibility_no
        {
            get { return visibility_no; }
            set
            {
                visibility_no = value;
                OnPropertyChanged(nameof(Visibility_no));
            }
        }

        Visibility visibility_cancel;
        public Visibility Visibility_cancel
        {
            get { return visibility_cancel; }
            set
            {
                visibility_cancel = value;
                OnPropertyChanged(nameof(Visibility_cancel));
            }
        }
        #endregion Visibility_button

        #region Command_button

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

            is_ok = true;
            _OK();
        }
        private bool CanExecute_ok(object o)
        {

            return true;


        }

        private DelegateCommand _Command_no;
        public ICommand Button_clik_no
        {
            get
            {
                if (_Command_no == null)
                {
                    _Command_no = new DelegateCommand(Execute_no, CanExecute_no);
                }
                return _Command_no;
            }
        }
        private void Execute_no(object o)
        {

            is_no = true;
            _NO();
        }
        private bool CanExecute_no(object o)
        {

            return true;


        }

        private DelegateCommand _Command_cancel;
        public ICommand Button_clik_cancel
        {
            get
            {
                if (_Command_cancel == null)
                {
                    _Command_cancel = new DelegateCommand(Execute_cancel, CanExecute_cancel);
                }
                return _Command_cancel;
            }
        }
        private void Execute_cancel(object o)
        {

            is_cancel = true;
            _CANCEL();
        }
        private bool CanExecute_cancel(object o)
        {

            return true;


        }

        #endregion Command_button


        public bool is_ok;
        public bool is_no;
        public bool is_cancel;

        string messege = "Вы уверены?";
        public string Messege
        {
            get { return messege; }
            set
            {
                messege = value;
                OnPropertyChanged(nameof(Messege));
            }
        }

        string messeg_Titel = "Предупреждение";
        public string Messeg_Titel
        {
            get { return messeg_Titel; }
            set
            {
                messeg_Titel = value;
                OnPropertyChanged(nameof(Messeg_Titel));
            }
        }



        public Action _OK { get; set; }
        public Action _NO { get; set; }
        public Action _CANCEL { get; set; }
    }
}
