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

namespace Nursery_Management_System_WPF
{
    /// <summary>
    /// Interaction logic for parentWindow.xaml
    /// </summary>
    public partial class parentWindow : Window
    {
        public parentWindow()
        {
            InitializeComponent();
        }

        private void windowPanel_MouseDown(object sender, MouseButtonEventArgs e)
        {
            //enables dragging cause there's no border for this window
            if (e.ChangedButton == MouseButton.Left)
                this.DragMove();
        }


        private void exitButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void parentFeedbackButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void childrenButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void parentProfileButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void signOutButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void minimizeButton_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
