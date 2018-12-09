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
    /// Interaction logic for signIn.xaml
    /// </summary>
    public partial class signIn : Window
    {
        public signIn()
        {
            InitializeComponent();

            username.LostFocus += addUserNameText;
            username.GotFocus += removeUserNameText;
        }

        private void loginButton_Click(object sender, RoutedEventArgs e)
        {
            SQLQuery mSqlQuery = new SQLQuery();

            if (mSqlQuery.checkForUsername(username.Text) == false)
            {
                usernameError.Visibility = Visibility.Visible;
                passwordError.Visibility = Visibility.Visible;
            }
            else if(mSqlQuery.serachForUser(username.Text , password.Password ) == false)
            {
                usernameError.Visibility = Visibility.Hidden;
                passwordError.Visibility = Visibility.Visible;
            }
            else
            {
                usernameError.Visibility = Visibility.Hidden;
                passwordError.Visibility = Visibility.Hidden;

                MessageBox.Show("Hello, " + username.Text + "!", "Logged In Successfully", MessageBoxButton.OK, MessageBoxImage.None);

                if (GlobalVariables.globalType.Equals("Staff"))
                {
                    //open staff form
                }
                else if (GlobalVariables.globalType.Equals("Admin"))
                {
                    //open admin form
                }
                else if (GlobalVariables.globalType.Equals("Parent"))
                {
                    //open parent form
                }
            }
        }

        
        public void removeUserNameText(object sender, EventArgs e)
        {
            username.Text = "";
        }

        public void addUserNameText(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(username.Text))
                username.Text = "Enter Username Here";
        }
        private void username_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            signUp signUpForm = new signUp();
            signUpForm.Show();
            this.Close();
        }
    }
}
