using System;
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
        public parentSignUp()
        {
            InitializeComponent();
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
                GlobalVariables.globalParent = new Parent(Convert.ToInt64(ID.Text), firstName.Text, lastName.Text, phoneNumber.Text, email.Text
                   , address.Text, creditCard.Text, 1);

                SQLQuery mSQLQuery = new SQLQuery();
                mSQLQuery.insertParentData(GlobalVariables.globalParent);
                mSQLQuery.insertUser(username.Text, password.Password, "Parent", GlobalVariables.globalParent.id);

                MessageBox.Show("Thank you! Your data for  request is being processed ", "Request sent", MessageBoxButton.OK, MessageBoxImage.None);
            }
            else
            {
                MessageBox.Show("check your data", "faild to register", MessageBoxButton.OK, MessageBoxImage.None);
            }
        }

        public Boolean checkEnteredData()
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

            if (!validator.checkNationalID(ID.Text) || mSQLQuery.getParentByID(Convert.ToInt64(ID.Text)).Rows.Count != 0)
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

            if (mSQLQuery.checkForUsername(username.Text))
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
            this.Close();
        }

        private void backButton_Click(object sender, RoutedEventArgs e)
        {
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
        private void OkButton_Click(object sender, RoutedEventArgs e)
        {
            SQLQuery mSqlQuery = new SQLQuery();
            mSqlQuery.updateParentData(GlobalVariables.globalParent);
            this.Hide();
            adminWindow awindow = new adminWindow();
            awindow.Show();
        }

     
    }

}
