using OSI_Net.Code;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace OSI_Net.View_model
{
    class View_Model_Main: View_Model_Base
    {
        #region variables

        #region path_for_file
        string path_for_file = "";
        public string Path_for_file { set{ path_for_file = value; OnPropertyChanged(nameof(Path_for_file)); }
        get { return path_for_file; }
        }
        #endregion path_for_file

        #region path_in_internet
        string path_in_internet = "";
        public string Path_in_internet
        {
            set { path_in_internet = value; OnPropertyChanged(nameof(Path_in_internet)); }
            get { return path_in_internet; }
        }
        #endregion path_in_internet

        #region UpDown
        private int _numValue = 0;

        public string NumValue
        {
            get { return _numValue.ToString(); }
            set
            {


                _numValue = Convert.ToInt32(value);
                OnPropertyChanged(nameof(NumValue));

            }
        }


        #region UP

        private DelegateCommand _Command_up_product;
        public ICommand Button_up_product
        {
            get
            {
                if (_Command_up_product == null)
                {
                    _Command_up_product = new DelegateCommand(Execute_up_product, CanExecute_up_product);
                }
                return _Command_up_product;
            }
        }
        private void Execute_up_product(object o)
        {
            _numValue += 1;

        }
        private bool CanExecute_up_product(object o)
        {

            return true;



        }

        #endregion

        #region DOWN

        private DelegateCommand _Command_down_product;
        public ICommand Button_down_product
        {
            get
            {
                if (_Command_down_product == null)
                {
                    _Command_down_product = new DelegateCommand(Execute_down_product, CanExecute_down_product);
                }
                return _Command_down_product;
            }
        }
        private void Execute_down_product(object o)
        {
            _numValue -= 1;

        }
        private bool CanExecute_down_product(object o)
        {
            if (_numValue > 1)
                return true;

            return false;

        }

        #endregion DOWN

        #endregion UpDown

        #endregion variables



        #region Func

        #endregion Func


        #region Command

        #region Open for file

        private DelegateCommand Command_open_for_file;
        public ICommand Button_clik_open_for_file
        {
            get
            {
                if (Command_open_for_file == null)
                {
                    Command_open_for_file = new DelegateCommand(Execute_open_for_file, CanExecute_open_for_file);
                }
                return Command_open_for_file;
            }
        }
        private void Execute_open_for_file(object o)
        {
          
        }
        private bool CanExecute_open_for_file(object o)
        {
            return true;
        }

        #endregion Open for file
        #region delete

        private DelegateCommand Command_delete;
        public ICommand Button_clik_delete
        {
            get
            {
                if (Command_delete == null)
                {
                    Command_delete= new DelegateCommand(Execute_delete, CanExecute_delete);
                }
                return Command_delete;
            }
        }
        private void Execute_delete(object o)
        {

        }
        private bool CanExecute_delete(object o)
        {
            return true;
        }

        #endregion delete
        #region Rename

        private DelegateCommand Command_rename;
        public ICommand Button_clik_rename
        {
            get
            {
                if (Command_rename == null)
                {
                    Command_rename = new DelegateCommand(Execute_rename, CanExecute_rename);
                }
                return Command_rename;
            }
        }
        private void Execute_rename(object o)
        {

        }
        private bool CanExecute_rename(object o)
        {
            return true;
        }

        #endregion Rename


        #region Download

        private DelegateCommand Command_download;
        public ICommand Button_clik_download
        {
            get
            {
                if (Command_download == null)
                {
                    Command_download = new DelegateCommand(Execute_download, CanExecute_download);
                }
                return Command_download;
            }
        }
        private void Execute_download(object o)
        {

        }
        private bool CanExecute_download(object o)
        {
            return true;
        }

        #endregion Download
        #region Pause

        private DelegateCommand Command_pause;
        public ICommand Button_clik_pause
        {
            get
            {
                if (Command_pause == null)
                {
                    Command_pause = new DelegateCommand(Execute_pause, CanExecute_pause);
                }
                return Command_pause;
            }
        }
        private void Execute_pause(object o)
        {

        }
        private bool CanExecute_pause(object o)
        {
            return true;
        }

        #endregion Pause
        #region Stop

        private DelegateCommand Command_stop;
        public ICommand Button_clik_stop
        {
            get
            {
                if (Command_stop == null)
                {
                    Command_stop = new DelegateCommand(Execute_stop, CanExecute_stop);
                }
                return Command_stop;
            }
        }
        private void Execute_stop(object o)
        {

        }
        private bool CanExecute_stop(object o)
        {
            return true;
        }

        #endregion Pause
        #region delete Download

        private DelegateCommand Command_delete_download;
        public ICommand Button_clik_delete_download
        {
            get
            {
                if (Command_delete_download == null)
                {
                    Command_delete_download = new DelegateCommand(Execute_delete_download, CanExecute_delete_download);
                }
                return Command_delete_download;
            }
        }
        private void Execute_delete_download(object o)
        {

        }
        private bool CanExecute_delete_download(object o)
        {
            return true;
        }

        #endregion delete Download
        #endregion Command


    }
}
