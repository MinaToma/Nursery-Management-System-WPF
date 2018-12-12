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

namespace Nursery_Management_System_WPF
{
    /// <summary>
    /// Interaction logic for parentSignUp2.xaml
    /// </summary>
    public partial class parentSignUp2 : Window
    {
        public parentSignUp2()
        {
            InitializeComponent();
            
            username.LostFocus += Username_LostFocus;
            username.GotFocus += Username_GotFocus;

            creditCard.LostFocus += CreditCard_LostFocus; ;
            creditCard.GotFocus += CreditCard_GotFocus;
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
        
        private void CreditCard_GotFocus(object sender, RoutedEventArgs e)
        {

            creditCard.Text = "";
        }

        private void CreditCard_LostFocus(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(creditCard.Text))
                creditCard.Text = "Enter Credit Number Here";
        }

        private void addChildButton_Click(object sender, RoutedEventArgs e)
        {
            childSignUp signUp = new childSignUp();
            signUp.roomID.Visibility = Visibility.Hidden;
            signUp.Show();
        }

        private void signUpButton_Click(object sender, RoutedEventArgs e)
        {
            if (checkEnteredData())
            {
                GlobalVariables.globalParent.creditCard = creditCard.Text;

                SQLQuery mSQLQuery = new SQLQuery();
                mSQLQuery.insertUser(username.Text, password.Password, "Parent", GlobalVariables.globalParent.id);

                MessageBox.Show("Thank you! Your data for  request is being processed ", "Request sent", MessageBoxButton.OK, MessageBoxImage.None);
            }
        }

        private void address_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        public bool checkEnteredData()
        {
            bool ans = true;
            ValidateData validator = new ValidateData();
            SQLQuery mSql = new SQLQuery();
            
            if(!validator.checkCreditCardt(creditCard.Text))
            {
                ans = false;
                creditError.Visibility = Visibility.Visible;
            }
            else
            {
                creditError.Visibility = Visibility.Visible;
            }

            if (mSql.checkForUsername(username.Text) || username.Text.Equals("Enter username Here"))
            {
                ans = false;
                usernameError.Visibility = Visibility.Visible;
            }
            else
            {
                usernameError.Visibility = Visibility.Hidden;
            }

            if (validator.verifyField(password.Password))
            {
                ans = false;
                passwordError.Visibility = Visibility.Visible;
            }
            else
            {
                passwordError.Visibility = Visibility.Hidden;
            }

            return ans;
        }

        private void username_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }
}
