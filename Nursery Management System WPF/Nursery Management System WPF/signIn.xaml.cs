﻿using System;
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
using System.Data.SqlClient;
using System.Data;

namespace Nursery_Management_System_WPF
{
    /// <summary>
    /// Interaction logic for signIn.xaml
    /// </summary>
    public partial class signIn : Window
    { 
        public signIn()
        {
            InitializeComponent();
        }

        private void loginButton_Click(object sender, RoutedEventArgs e)
        {
            SQLQuery mSqlQuery = new SQLQuery();

            if (!mSqlQuery.checkForUsername(username.Text))
            {
                usernameError.Visibility = Visibility.Visible;
                passwordError.Visibility = Visibility.Visible;
            }
            else if(!mSqlQuery.serachForUser(username.Text , password.Password))
            {
                usernameError.Visibility = Visibility.Hidden;
                passwordError.Visibility = Visibility.Visible;
            }
            else
            {
                usernameError.Visibility = Visibility.Hidden;
                passwordError.Visibility = Visibility.Hidden;

                if (GlobalVariables.globalType.Equals("Staff"))
                {
                    //open staff form
                    
                    staffWindow mStaffWindow = new staffWindow();

                    mStaffWindow.Show();
                }
                else if (GlobalVariables.globalType.Equals("Admin"))
                {
                    //open admin form
                    
                    adminWindow adminForm = new adminWindow();
                    adminForm.Show();
                   
                }
                else if (GlobalVariables.globalType.Equals("Parent"))
                {
                    //open parent form
                    parentWindow mParentWindow = new parentWindow();
                    mParentWindow.Show();
                }
                this.Close();
            }
        }
        private void exitButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void minimizeButton_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        private void signUpButton_Click(object sender, RoutedEventArgs e)
        {
            signUp signUpForm = new signUp();
            signUpForm.Show();
            this.Close();
        }

        private void titleBar_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
                this.DragMove();
        }

        
    }
}
