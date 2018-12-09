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
    /// Interaction logic for adminWindow.xaml
    /// </summary>
    public partial class adminWindow : Window
    {
        public adminWindow()
        {
            InitializeComponent();
        }

        private void button_Copy3_Click(object sender, RoutedEventArgs e)
        {
            signIn logIn = new signIn();
            logIn.Show();
            this.Close();
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {

        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            profileGrid.Visibility = Visibility.Visible;
        }

        public void resetGrid()
        {
            profileGrid.Visibility = Visibility.Hidden;
        }

        public void Sfill_info()
        {
            SQL mysql = new SQL();

            GlobalVariables.globalAdmin.ToString();
            GlobalVariables.globalStaff.ToString();
            if (GlobalVariables.globalStaff.type == "Staff")
            {
                string staff_username = "select userName from User_Password where staffID like '" + (GlobalVariables.globalStaff.id).ToString() + "' ";
                string staff_password = "select userPassword from User_Password where staffID  like'" + (GlobalVariables.globalStaff.id).ToString() + "' ";
                string un = mysql.retrieveQuery(staff_username).ToString();
                string pass = mysql.retrieveQuery(staff_password).ToString();
                firstName.Text = GlobalVariables.globalStaff.firstName;
                lastName.Text = GlobalVariables.globalStaff.lastName;
                username.Text = un;
                email.Text = GlobalVariables.globalStaff.email;
                phoneNumber.Text = GlobalVariables.globalStaff.phoneNumber;
                password.Password = pass;
                ID.Text = (GlobalVariables.globalStaff.id).ToString();
            }
            else
            {
                string admin_username = "select userName from User_Password where staffID like '" + (GlobalVariables.globalAdmin.id).ToString() + "' ";
                string admin_password = "select userPassword from User_Password where staffID  like'" + (GlobalVariables.globalAdmin.id).ToString() + "' ";
                string un = mysql.retrieveQuery(admin_username).ToString();
                string pass = mysql.retrieveQuery(admin_password).ToString();
                firstName.Text = GlobalVariables.globalAdmin.firstName;
                lastName.Text = GlobalVariables.globalAdmin.lastName;
                username.Text = un;
                email.Text = GlobalVariables.globalAdmin.email;
                phoneNumber.Text = GlobalVariables.globalAdmin.phoneNumber;
                password.Password = pass;
                ID.Text = (GlobalVariables.globalAdmin.id).ToString();

            }
        }
    }
 
}
