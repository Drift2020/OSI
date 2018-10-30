using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Chat
{
    /// <summary>
    /// Interaction logic for Login.xaml
    /// </summary>
    public partial class Login : Window
    {
        public Login()
        {
            InitializeComponent();
        }

        public void None_user()
        {
            MessageBox.Show("No user, register a new user.");

        }

        public void Ok()
        {
            MessageBox.Show("Signed in.");

        }

        public void Visibility_off()
        {
            Visibility = Visibility.Hidden;
        }
        public void Visibility_on()
        {
            Visibility = Visibility.Visible;
        }

        public void No()
        {
            //Close();
        }
    }
}
