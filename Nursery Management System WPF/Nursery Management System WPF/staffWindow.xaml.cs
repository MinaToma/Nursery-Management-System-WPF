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
    /// Interaction logic for staffWindow.xaml
    /// </summary>
    public partial class staffWindow : Window
    {
        public staffWindow()
        {
            InitializeComponent();
        }

        private void exitButton_Click(object sender, RoutedEventArgs e)
        {
           this.Close();
        }

        private void roomButton_Click(object sender, RoutedEventArgs e)
        {
            //hide all other windows
            this.profile.Visibility = Visibility.Hidden;
            this.feedback.Visibility = Visibility.Hidden;
            //show room grid
            this.room.Visibility = Visibility.Visible;
        }

        private void staffFeedbackButton_Click(object sender, RoutedEventArgs e)
        {
            //hide all other windows
            this.profile.Visibility = Visibility.Hidden;
            this.room.Visibility = Visibility.Hidden;
            //show feedback grid
            this.feedback.Visibility = Visibility.Visible;
        }

        private void staffProfileButton_Click(object sender, RoutedEventArgs e)
        {
            //hide all other windows
            this.room.Visibility = Visibility.Hidden;
            this.feedback.Visibility = Visibility.Hidden;
            //show profile grid
            this.profile.Visibility = Visibility.Visible;
        }

        private void signOutButton_Click(object sender, RoutedEventArgs e)
        {
            signIn logIn = new signIn();
            logIn.Show();
            this.Close();
        }

        private void minimizeButton_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
