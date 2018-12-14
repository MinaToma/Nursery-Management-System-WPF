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
using System.Data;
using System.Data.SqlClient;
namespace Nursery_Management_System_WPF
{
    /// <summary>
    /// Interaction logic for adminWindow.xaml
    /// </summary>
    public partial class adminWindow : Window
    {
        int feedbackIdx = -1;
        LinkedList<Tuple<Tuple<int, string>, string>> feedback;

        public adminWindow()
        {
            InitializeComponent();
        }

        private void exitButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void adminProfileButton_Click(object sender, RoutedEventArgs e)
        {
            SQLQuery mSqlquery = new SQLQuery();
            DataTable dt = new DataTable();

            dt = mSqlquery.selectUsernameByIDAndType(GlobalVariables.globalAdmin.id, "Admin");
            username.Text = dt.Rows[0]["userName"].ToString();
            password.Password = dt.Rows[0]["userPassword"].ToString();

            firstName.Text = GlobalVariables.globalAdmin.firstName;
            lastName.Text = GlobalVariables.globalAdmin.lastName;
            email.Text = GlobalVariables.globalAdmin.email;
            phoneNumber.Text = GlobalVariables.globalAdmin.phoneNumber;
            ID.Text = (GlobalVariables.globalAdmin.id).ToString();

             this.profilePanel.Visibility = Visibility.Visible;
            AdminFeedback.Visibility = Visibility.Hidden;
            pendingRequestsPanel.Visibility = Visibility.Hidden;
        }

        private void editDatabase_Click(object sender, RoutedEventArgs e)
        {
            AdminFeedback.Visibility = Visibility.Hidden;
            pendingRequestsPanel.Visibility = Visibility.Hidden;
            this.profilePanel.Visibility = Visibility.Hidden;
        }

        private void adminFeedbackButton_Click(object sender, RoutedEventArgs e)
        {
            SQLQuery mSQLQuery = new SQLQuery();

            feedback = new LinkedList<Tuple<Tuple<int, string>, string>>();

            feedback = mSQLQuery.getAllParentFeedback();

            if(feedback.Count != 0)
            {
                feedbackIdx = 0;
                showFeedBack();
            }

            this.AdminFeedback.Visibility = Visibility.Visible;
            this.profilePanel.Visibility = Visibility.Hidden;
            this.pendingRequestsPanel.Visibility = Visibility.Hidden;
        }

        private void signOutButton_Click(object sender, RoutedEventArgs e)
        {
            signIn logIn = new signIn();
            logIn.Show();
            this.Close();
        }

        private void pendingRequests_Click(object sender, RoutedEventArgs e)
        {
            pendingRequestsPanel.Visibility = Visibility.Visible;
            profilePanel.Visibility = Visibility.Hidden;
            AdminFeedback.Visibility = Visibility.Hidden;
        }

        private void MinimizeButton_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        public bool checkEnteredData()
        {
            bool ans = true;
            ValidateData validator = new ValidateData();
            SQLQuery mSql = new SQLQuery();

            if (!validator.verifyField(firstName.Text) || firstName.Text.Equals("Enter First Name Here"))
            {
                ans = false;
                MessageBox.Show("Please Correct Your First Name !", "Error Occur", MessageBoxButton.OK, MessageBoxImage.Hand);
                firstNameError.Visibility = Visibility.Visible;
            }
            else
            {
                firstNameError.Visibility = Visibility.Hidden;
            }

            if (!validator.verifyField(lastName.Text) || lastName.Text.Equals("Enter Last Name Here"))
            {
                ans = false;
                MessageBox.Show("Please Correct Your Last Name !", "Error Occur", MessageBoxButton.OK, MessageBoxImage.Hand);
                lastNameError.Visibility = Visibility.Visible;
            }
            else
            {
                lastNameError.Visibility = Visibility.Hidden;
            }

            if (!validator.checkNationalID(ID.Text))
            {
                ans = false;
                MessageBox.Show("Please Correct Your ID !", "Error Occur", MessageBoxButton.OK, MessageBoxImage.Hand);
                IDError.Visibility = Visibility.Visible;
            }
            else
            {
                IDError.Visibility = Visibility.Hidden;
            }

            if (!validator.checkMails(email.Text))
            {
                ans = false;
                MessageBox.Show("Please Correct Your Email !", "Error Occur", MessageBoxButton.OK, MessageBoxImage.Hand);
                emailError.Visibility = Visibility.Visible;
            }
            else
            {
                emailError.Visibility = Visibility.Hidden;
            }

            if (!validator.checkPhoneNum(phoneNumber.Text))
            {
                ans = false;
                MessageBox.Show("Please Correct Your Phone Number !", "Error Occur", MessageBoxButton.OK, MessageBoxImage.Hand);
                phoneError.Visibility = Visibility.Visible;

            }
            else
            {
                phoneError.Visibility = Visibility.Hidden;
            }

            if (mSql.checkForUsername(username.Text) || username.Text.Equals("Enter Username Here"))
            {
                ans = false;
                MessageBox.Show("Please Correct Your UserName !", "Error Occur", MessageBoxButton.OK, MessageBoxImage.Hand);

                usernameError.Visibility = Visibility.Visible;
            }
            else
            {
                usernameError.Visibility = Visibility.Hidden;
            }

            if (!validator.verifyField(password.Password))
            {
                ans = false;
                MessageBox.Show("Please Correct Your Password !", "Error Occur", MessageBoxButton.OK, MessageBoxImage.Hand);

                passwordError.Visibility = Visibility.Visible;
            }
            else
            {
                passwordError.Visibility = Visibility.Hidden;
            }

            return ans;
        }

        private void editProfileButton_Click_2(object sender, RoutedEventArgs e)
        {
            SQLQuery mSql = new SQLQuery();

            if (checkEnteredData())
            {
                GlobalVariables.globalAdmin.id = Convert.ToInt64(ID.Text);
                GlobalVariables.globalAdmin.firstName = firstName.Text;
                GlobalVariables.globalAdmin.lastName = lastName.Text;

                mSql.updateUsername(Convert.ToInt64(ID.Text), "Admin", username.Text, password.Password);

                GlobalVariables.globalAdmin.email = email.Text;
                GlobalVariables.globalAdmin.phoneNumber = phoneNumber.Text;
                mSql.updateStaffData(GlobalVariables.globalAdmin);

                MessageBox.Show("Data Updated sucessfuly !", "Process Finshed", MessageBoxButton.OK, MessageBoxImage.Information);
            }

        }

        private void deleteFeedback_Click(object sender, RoutedEventArgs e)
        {
            if (feedback.Count != 0 && feedbackIdx != -1)
            {
                SQLQuery mSQLQuery = new SQLQuery();
                int id = feedback.ElementAt(feedbackIdx).Item1.Item1;
                MessageBox.Show(Convert.ToString(id));

                mSQLQuery.deleteParentFeedback(id);
                parentNameLabel.Content = "";
                feedbackText.Text = "";

                feedback.Remove(feedback.ElementAt(feedbackIdx));
            }
        }

        public void showFeedBack()
        {
            if (feedbackIdx >= 0 && feedbackIdx < feedback.Count)
            {
                parentNameLabel.Content = feedback.ElementAt(feedbackIdx).Item2;
                feedbackText.Text = feedback.ElementAt(feedbackIdx).Item1.Item2;
            }
            else
            {
                parentNameLabel.Content = "";
                feedbackText.Text = "";
            }
        }

        private void previousButton_Click(object sender, RoutedEventArgs e)
        {
            if (feedback.Count != 0)
            {
                feedbackIdx = (feedbackIdx - 1 + feedback.Count) % feedback.Count;
                showFeedBack();
            }
        }

        private void nextButton_Click(object sender, RoutedEventArgs e)
        {
            if (feedback.Count != 0)
            {
                feedbackIdx = (feedbackIdx + 1) % feedback.Count;
                showFeedBack();
            }
        }
    }
}