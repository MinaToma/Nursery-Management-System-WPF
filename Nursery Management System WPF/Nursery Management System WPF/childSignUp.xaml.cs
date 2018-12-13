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
    /// Interaction logic for childSignUp.xaml
    /// </summary>
    public partial class childSignUp : Window
    {
        public childSignUp()
        {
            InitializeComponent();
        }

        private void signUpButton_Click(object sender, RoutedEventArgs e)
        {
            SQLQuery mSQLQuery = new SQLQuery();
            if (childName.Text.Length >= 2 && DOBpicker.SelectedDate != null)
            {
                string gender;
                if (female.IsChecked == true)
                    gender = "Female";
                else
                    gender = "Male";

                Child child = new Child(childName.Text, GlobalVariables.globalParent.firstName, GlobalVariables.globalParent.id, -1, gender, DOBpicker.SelectedDate.Value , null, 1);
                mSQLQuery.insertChildData(child);
                MessageBox.Show("Requset has been sent", "Request sent", MessageBoxButton.OK, MessageBoxImage.None);
                this.Close(); 
            }
            else if (childName.Text.Length < 2)
            {
                MessageBox.Show("Please Enter at least 2 letter", "Invaild Child Name", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                MessageBox.Show("Please enter the Date of Birth", "Missing DOB", MessageBoxButton.OK, MessageBoxImage.Error);
            }
          
        }
    }
}
