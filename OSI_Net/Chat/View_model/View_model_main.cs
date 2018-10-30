using Chat.Code;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Chat.View_model
{
    class View_model_main:View_Model_Base
    {
        ApplicationContext my_db;
        User profile;
        public View_model_main(User profile, ApplicationContext my_db)
        {
            this.my_db = my_db;
            this.profile = profile;
        }

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
           int a =1;
        }
        private bool CanExecute_download(object o)
        {              
            return true;
        }
    }
}
