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
    /// Interaction logic for parentSignUp.xaml
    /// </summary>
    public partial class parentSignUp : Window
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

            phoneNumber.LostFocus += PhoneNumber_LostFocus;
            phoneNumber.GotFocus += PhoneNumber_GotFocus;

            address.LostFocus += Address_LostFocus;
            address.GotFocus += Address_GotFocus;
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

        private void Address_GotFocus(object sender, RoutedEventArgs e)
        {
            address.Text = "";  
        }

        private void Address_LostFocus(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(address.Text))
                address.Text = "Enter address Here";
        }

        private void PhoneNumber_GotFocus(object sender, RoutedEventArgs e)
        {
            phoneNumber.Text = "";
        }

        private void PhoneNumber_LostFocus(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(phoneNumber.Text))
                phoneNumber.Text = "Enter Phone Number Here";
        }

        private void FirstName_LostFocus(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(firstName.Text))
                firstName.Text = "Enter firstName Here";
        }

        private void nextButton_Click(object sender, RoutedEventArgs e)
        {
            if(checkEnteredData())
            {
                GlobalVariables.globalParent = new Parent(Convert.ToInt64(ID.Text), firstName.Text, lastName.Text, "", email.Text
                    , address.Text , "", 1);
                parentSignUp2 signUp2 = new parentSignUp2();
                signUp2.Show();
                this.Close();
            }
        }

        public Boolean checkEnteredData()
        {
            bool ans = true;
            ValidateData validator = new ValidateData();
            
            if (!validator.verifyField(firstName.Text) || firstName.Text.Equals("Enter First Name Here"))
            {
                ans = false;
                firstNameError.Visibility = Visibility.Visible;
            }
            else
            {
                firstNameError.Visibility = Visibility.Hidden;
            }

            if (!validator.verifyField(lastName.Text) || lastName.Text.Equals("Enter Last Name Here"))
            {
                ans = false;
                lastNameError.Visibility = Visibility.Visible;
            }
            else
            {
                lastNameError.Visibility = Visibility.Hidden;
            }

            if (!validator.checkNationalID(ID.Text))
            {
                ans = false;
                IDError.Visibility = Visibility.Visible;
            }
            else
            {
                IDError.Visibility = Visibility.Hidden;
            }

            if (!validator.checkMails(email.Text))
            {
                ans = false;
                emailError.Visibility = Visibility.Visible;
            }
            else
            {
                emailError.Visibility = Visibility.Hidden;
            }

            if (!validator.checkPhoneNum(phoneNumber.Text))
            {
                ans = false;
                phoneError.Visibility = Visibility.Visible;
            }
            else
            {
                phoneError.Visibility = Visibility.Hidden;
            }
            
            if(!validator.verifyField(address.Text) || address.Text.Equals("Enter address Here"))
            {
                ans = false;
                addressError.Visibility = Visibility.Visible;
            }
            else
            {
                addressError.Visibility = Visibility.Hidden;
            }

            return ans;
         }
    }
}
