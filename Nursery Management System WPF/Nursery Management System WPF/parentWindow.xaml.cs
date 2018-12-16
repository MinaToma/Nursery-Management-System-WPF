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
using System.Windows.Shapes;

namespace Nursery_Management_System_WPF
{
    /// <summary>
    /// Interaction logic for parentWindow.xaml
    /// </summary>
    public partial class parentWindow : Window
    {
        LinkedList<Child> childList;
        public LinkedList<RowTemplate> childRow;
        public parentWindow()
        {
            InitializeComponent();

            childRow = new LinkedList<RowTemplate>();
            childList = new LinkedList<Child>();
        }

        private void windowPanel_MouseDown(object sender, MouseButtonEventArgs e)
        {
        }


        private void exitButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void parentFeedbackButton_Click(object sender, RoutedEventArgs e)
        {
            this.profilePanel.Visibility = Visibility.Hidden;
            this.childrenPanel.Visibility = Visibility.Hidden;
            //show feedback grid
            this.feedbackPanel.Visibility = Visibility.Visible;
        }

        private void childrenButton_Click(object sender, RoutedEventArgs e)
        {
            childRow.Clear();
            children.Children.Clear();

            SQLQuery mSQLQuery = new SQLQuery();

            childList = mSQLQuery.childToLinkedList(mSQLQuery.getChildByParentID(GlobalVariables.globalParent.id));

            LinkedList<Child> notPending = new LinkedList<Child>();
            foreach (Child c in childList)
            {
                if (c.pending == 1)
                    notPending.AddLast(c);
                else c.lastName = GlobalVariables.globalParent.firstName;
            }

            foreach (Child c in notPending)
            {
                childList.Remove(c);
            }

            showPendingChildren();

            this.profilePanel.Visibility = Visibility.Hidden;
            this.feedbackPanel.Visibility = Visibility.Hidden;
            //show children grid
            this.childrenPanel.Visibility = Visibility.Visible;
        }

        private void showPendingChildren()
        {
            double top = childGrid.Margin.Top;
            double bottom = childGrid.Margin.Bottom;
            double left = childGrid.Margin.Left;
            double right = childGrid.Margin.Right;

            for (int i = 0; i < childList.Count; i++)
            {
                RowTemplate rt = new RowTemplate(0, 0, i, 0, 0, childList, null, null, children, null, this, null);
                rt.Margin = new Thickness(left, top, right, bottom);
                top += childGrid.Height;
                childRow.AddLast(rt);
                children.Children.Add(rt);
            }
        }   

        private void parentProfileButton_Click(object sender, RoutedEventArgs e)
        {
            SQLQuery mSqlquery = new SQLQuery();

            firstName.Text = GlobalVariables.globalParent.firstName;
            lastName.Text = GlobalVariables.globalParent.lastName;

            DataTable userAndPass = mSqlquery.selectUsernameByIDAndType(Convert.ToInt64(GlobalVariables.globalParent.id) , "Parent");

            username.Text = userAndPass.Rows[0]["userName"].ToString();
            password.Password = userAndPass.Rows[0]["userPassword"].ToString();

            email.Text = GlobalVariables.globalParent.email;
            creditCard.Text = GlobalVariables.globalParent.creditCard;
            ID.Text = GlobalVariables.globalParent.id.ToString();
            phoneNumber.Text = GlobalVariables.globalParent.phoneNumber;
            address.Text = GlobalVariables.globalParent.address;

            this.childrenPanel.Visibility = Visibility.Hidden;
            this.feedbackPanel.Visibility = Visibility.Hidden;
            //show feedback grid
            this.profilePanel.Visibility = Visibility.Visible;
        }

        private void signOutButton_Click(object sender, RoutedEventArgs e)
        {
            signIn logIn = new signIn();
            logIn.Show();
            this.Close();
        }

        private void minimizeButton_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        private void editProfileButton_Click(object sender, RoutedEventArgs e)
        {
            SQLQuery mSQLQuery = new SQLQuery();

            if (checkEnteredData())
            {
                GlobalVariables.globalParent.firstName = firstName.Text;
                GlobalVariables.globalParent.lastName = lastName.Text;
                GlobalVariables.globalParent.email = email.Text;
                GlobalVariables.globalParent.address = address.Text;
                GlobalVariables.globalParent.creditCard = creditCard.Text;
                GlobalVariables.globalParent.phoneNumber = phoneNumber.Text;
                GlobalVariables.globalParent.id = Convert.ToInt64(ID.Text);
                mSQLQuery.updateUsername(Convert.ToInt64(ID.Text), "Parent" , username.Text , password.Password);

                mSQLQuery.updateParentData(GlobalVariables.globalParent);
                MessageBox.Show("Data Updated sucessfuly !", "Process Finshed", MessageBoxButton.OK, MessageBoxImage.Information);
            }
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
                 emailError.Visibility =Visibility.Hidden;
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
            
            if (!validator.verifyField(address.Text) || address.Text.Equals("Enter address Here"))
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


        private void titleBar_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
                this.DragMove();
        }

        private void submitFeedback_Click(object sender, RoutedEventArgs e)
        {
            SQLQuery mSQLQuery = new SQLQuery();

            mSQLQuery.insertParentFeedback(GlobalVariables.globalParent.id, feedbackText.Text);

        }
    }
}
