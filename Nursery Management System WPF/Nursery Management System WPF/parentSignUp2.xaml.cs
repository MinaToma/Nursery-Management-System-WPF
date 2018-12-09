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
    public partial class parentSignUp2 : Page
    {
        public parentSignUp2()
        {
            InitializeComponent();

            phoneNumber.LostFocus += PhoneNumber_LostFocus;
            phoneNumber.GotFocus += PhoneNumber_GotFocus;

            creditCard.LostFocus += CreditCard_LostFocus; ;
            creditCard.GotFocus += CreditCard_GotFocus;

            address.LostFocus += Address_LostFocus;
            address.GotFocus += Address_GotFocus;
        }

        private void Address_GotFocus(object sender, RoutedEventArgs e)
        {
            address.Text = "";
        }

        private void Address_LostFocus(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(address.Text))
                address.Text = "Enter Phone Number Here";
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

        private void PhoneNumber_GotFocus(object sender, RoutedEventArgs e)
        {
            phoneNumber.Text = "";
        }

        private void PhoneNumber_LostFocus(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(phoneNumber.Text))
                phoneNumber.Text = "Enter Phone Number Here";
        }

        private void addChildButton_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("hO");
        }

        private void signUpButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void address_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }
}
