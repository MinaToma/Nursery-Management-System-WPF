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
using System.Windows.Controls;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Nursery_Management_System_WPF
{
    /// <summary>
    /// Interaction logic for parentSignUp.xaml
    /// </summary>
    public partial class parentSignUp : Page
    {
        public parentSignUp()
        {
            InitializeComponent();

            firstName.LostFocus += FirstName_LostFocus;
            firstName.GotFocus += FirstName_GotFocus;

            lastName.LostFocus += LastName_LostFocus;
            lastName.GotFocus += LastName_GotFocus;

            ID.LostFocus += ID_LostFocus;
            ID.GotFocus += ID_GotFocus;

            email.LostFocus += Email_LostFocus;
            email.GotFocus += Email_GotFocus;
            
            username.LostFocus += Username_LostFocus;
            username.GotFocus += Username_GotFocus;
        }
        

        private void Username_GotFocus(object sender, RoutedEventArgs e)
        {
            username.Text = "";
        }

        private void Username_LostFocus(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(username.Text))
                username.Text = "Enter Username Here";
        }
        

        private void Email_GotFocus(object sender, RoutedEventArgs e)
        {
            email.Text = "";
        }

        private void Email_LostFocus(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(email.Text))
                email.Text = "Enter Email Here";
        }

        private void ID_GotFocus(object sender, RoutedEventArgs e)
        {
            ID.Text = "";
        }

        private void ID_LostFocus(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(ID.Text))
                ID.Text = "Enter National Number Here";
        }

        private void LastName_GotFocus(object sender, RoutedEventArgs e)
        {
            lastName.Text = "";
        }

        private void LastName_LostFocus(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(lastName.Text))
                lastName.Text = "Enter lastName Here";
        }

        private void FirstName_GotFocus(object sender, RoutedEventArgs e)
        {
            firstName.Text = "";
        }

        private void FirstName_LostFocus(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(firstName.Text))
                firstName.Text = "Enter firstName Here";
        }
  
    }
}
