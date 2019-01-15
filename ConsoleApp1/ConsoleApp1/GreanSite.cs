using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    class GreanSite: INotifyPropertyChanged
    {
        private string name;
        private string hootkey;
        private string url;
        public int Id { get; set; }
        public string Name
        {
            set
            {
                name = value;
                OnPropertyChanged("Name");

            }
            get
            {
                return name;
            }
        }
        public string Hootkey
        {
            set
            {
                hootkey = value;
                OnPropertyChanged("Hootkey");

            }
            get
            {
                return hootkey;
            }
        }
        public string URL
        {
            set
            {
                url = value;
                OnPropertyChanged("URL");

            }
            get
            {
                return url;
            }
        }
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }



    }
}
