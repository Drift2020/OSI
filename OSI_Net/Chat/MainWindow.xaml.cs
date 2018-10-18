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
using System.Windows.Navigation;
using System.Windows.Shapes;
using MahApps.Metro.IconPacks;
using MahApps.Metro.Controls;
namespace Chat
{

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : MetroWindow
    {
        public WindowCommandsOverlayBehavior LeftWindowCommandsOverlayBehavior;
        public WindowCommandsOverlayBehavior RightWindowCommandsOverlayBehavior;
        public WindowCommandsOverlayBehavior WindowButtonCommandsOverlayBehavior;
        public WindowCommandsOverlayBehavior IconOverlayBehavior;


        public MainWindow()
        {
            InitializeComponent();
        }

        private void Menu_Click(object sender, RoutedEventArgs e)
        {
            DropMenu.Visibility = Visibility.Visible;
            Menu.Visibility = Visibility.Collapsed;
            Menu_Hidden.Visibility = Visibility.Visible;
            Search_list.Visibility = Visibility.Collapsed;
            
        }

        private void Menu_Hidden_Click(object sender, RoutedEventArgs e)
        {
            DropMenu.Visibility = Visibility.Collapsed;
            Menu.Visibility = Visibility.Visible;
            Menu_Hidden.Visibility = Visibility.Collapsed;
            Search_list.Visibility = Visibility.Visible;
        }



       
     
    }
}
