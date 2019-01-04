﻿using System;
using System.Collections.Generic;
using System.Data;
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
        private bool signedUp = false;
        public parentSignUp()
        {
            InitializeComponent();
        }
        private void addChildButton_Click(object sender, RoutedEventArgs e)
        {
            if (signedUp == true)
            {
                childSignUp signUp = new childSignUp();
                signUp.roomID.Visibility = Visibility.Hidden;
                signUp.Show();
            }
            else
            {
                MessageBox.Show("Please Sign Up first", "Error", MessageBoxButton.OK, MessageBoxImage.None);
            }
        }

        private void signUpButton_Click(object sender, RoutedEventArgs e)
        {
            if (checkEnteredData(false))
            {
                GlobalVariables.globalParent = new Parent(Convert.ToInt64(ID.Text), firstName.Text, lastName.Text, phoneNumber.Text, email.Text
                   , address.Text, creditCard.Text, 1);

                SQLQuery mSQLQuery = new SQLQuery();
                mSQLQuery.insertParentData(GlobalVariables.globalParent);
                mSQLQuery.insertUser(username.Text, password.Password, "Parent", GlobalVariables.globalParent.id);
                signedUp = true;
                MessageBox.Show("Thank you! Your data for  request is being processed ", "Request sent", MessageBoxButton.OK, MessageBoxImage.None);
            }
            else
            {
                MessageBox.Show("check your data", "faild to register", MessageBoxButton.OK, MessageBoxImage.None);
            }
        }

        public Boolean checkEnteredData(bool x)
        {
            bool ans = true;
            ValidateData validator = new ValidateData();
            SQLQuery mSQLQuery = new SQLQuery();
            
            if (!validator.verifyField(firstName.Text))
            {
                ans = false;
                firstNameError.Visibility = Visibility.Visible;
            }
            else
            {
                firstNameError.Visibility = Visibility.Hidden;
            }

            if (!validator.verifyField(lastName.Text))
            {
                ans = false;
                lastNameError.Visibility = Visibility.Visible;
            }
            else
            {
                lastNameError.Visibility = Visibility.Hidden;
            }

            if (!x &&( !validator.checkNationalID(ID.Text) || mSQLQuery.getParentByID(Convert.ToInt64(ID.Text)).Rows.Count != 0 ))
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
            
            if(!validator.verifyField(address.Text))
            {
                ans = false;
                addressError.Visibility = Visibility.Visible;
            }
            else
            {
                addressError.Visibility = Visibility.Hidden;
            }

            if (!validator.checkCreditCardt(creditCard.Text))
            {
                ans = false;
                creditError.Visibility = Visibility.Visible;
            }
            else
            {
                creditError.Visibility = Visibility.Hidden;
            }

            if (mSQLQuery.checkForUsername(username.Text) && !x)
            {
                ans = false;
                usernameError.Visibility = Visibility.Visible;
            }
            else
            {
                usernameError.Visibility = Visibility.Hidden;
            }

            if (!validator.verifyField(password.Password))
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

        private void titleBar_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
                this.DragMove();
        }

        private void minimizeButton_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        private void exitButton_Click(object sender, RoutedEventArgs e)
        {
            signIn sign = new signIn();
            sign.Show();
            this.Close();
        }

        private void backButton_Click(object sender, RoutedEventArgs e)
        {
            signIn sign = new signIn();
            sign.Show();
            this.Close();
        }

        private void Ellipse_MouseDown(object sender, MouseButtonEventArgs e)
        {
            MessageBox.Show("Holaaa :D");
        }

        public void fillPdata1()
        {
            addChildButton.Visibility = Visibility.Hidden;
            addChild_elipse_.Visibility = Visibility.Hidden;
            childrenListView.Visibility = Visibility.Hidden;
            signUpButton.Visibility = Visibility.Hidden;
            ellipse_sign.Visibility = Visibility.Hidden;
            OKButton.Visibility = Visibility.Visible;
            
            firstName.Text = GlobalVariables.globalParent.firstName;
            lastName.Text = GlobalVariables.globalParent.lastName;
            email.Text = GlobalVariables.globalParent.email;
            ID.Text = GlobalVariables.globalParent.id.ToString();
            phoneNumber.Text = GlobalVariables.globalParent.phoneNumber;
            address.Text = GlobalVariables.globalParent.address;
            SQLQuery mSqlquery = new SQLQuery();
            DataTable userAndPass = mSqlquery.selectUsernameByIDAndType(Convert.ToInt64(GlobalVariables.globalParent.id), "Parent");
            username.Text = (userAndPass.Rows[0]["userName"].ToString());
            password.Password = userAndPass.Rows[0]["userPassword"].ToString();
            creditCard.Text = GlobalVariables.globalParent.creditCard;

        }

        public void disabledParent_info1()
        {
            firstName.IsEnabled = false;
            lastName.IsEnabled = false;
            email.IsEnabled = false;
            ID.IsEnabled = false;
            phoneNumber.IsEnabled = false;
            address.IsEnabled = false;

            username.IsEnabled = false;
            
            password.IsEnabled = false;
            creditCard.IsEnabled = false;
        }
        private void OKButton_Click(object sender, RoutedEventArgs e)
        {
            SQLQuery mSqlQuery = new SQLQuery();

            if(checkEnteredData(true))
            {

                GlobalVariables.globalParent.firstName = firstName.Text;
                GlobalVariables.globalParent.lastName = lastName.Text;
                GlobalVariables.globalParent.email = email.Text;
                GlobalVariables.globalParent.id = Convert.ToInt64(ID.Text);
                GlobalVariables.globalParent.phoneNumber = phoneNumber.Text;
                GlobalVariables.globalParent.address = address.Text;

                mSqlQuery.updateParentData(GlobalVariables.globalParent);
                mSqlQuery.updateUsername(GlobalVariables.globalParent.id, "Parent", username.Text, password.Password);
                MessageBox.Show("Updated", "Successfully Updated", MessageBoxButton.OK);
            }
            else
                MessageBox.Show("Check your data", "Update failed", MessageBoxButton.OK , MessageBoxImage.Error);


        }

        
    }

}
