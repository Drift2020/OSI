using OSI_Net.Code;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace OSI_Net.View_model
{
    class View_model_edit_name:View_Model_Base
    {
       public Action Window_close;
        public View_model_edit_name(string name)
        {
            old_name = name;
        }
        string old_name;

        public bool isOk = false;

        string name;
        public string Name
        {
            set
            {
                name = value;
                OnPropertyChanged(nameof(Name));
            }
            get
            {
                return name;
            }
        }

        #region OK

        private DelegateCommand Command_OK;
        public ICommand Button_clik_OK
        {
            get
            {
                if (Command_OK == null)
                {
                    Command_OK = new DelegateCommand(Execute_OK, CanExecute_OK);
                }
                return Command_OK;
            }
        }
        private void Execute_OK(object o)
        {
            isOk = true;
            Window_close();
        }
        private bool CanExecute_OK(object o)
        {
            if (name != null && name.Length > 0 && old_name.ToLower()!= name.ToLower())
                return true;
            return false;
        }

        #endregion OK
    }
}
