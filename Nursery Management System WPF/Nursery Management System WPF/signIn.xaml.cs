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
        }

        private void loginButton_Click(object sender, RoutedEventArgs e)
        {
            SQLQuery mSqlQuery = new SQLQuery();

            if (mSqlQuery.serachForUser(username.Text, password.Text) == false)
            {
                MessageBox.Show("Username doesn't exist", "Wrong Username or Password", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                MessageBox.Show("Hello, " + username.Text  + " " + GlobalVariables.globalAdmin.firstName + " " + GlobalVariables.globalAdmin.firstName + "!", "Logged In Successfully", MessageBoxButton.OK, MessageBoxImage.None);
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
    }
}
