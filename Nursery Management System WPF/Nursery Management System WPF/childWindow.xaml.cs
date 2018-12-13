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
    /// Interaction logic for childWindow.xaml
    /// </summary>
    public partial class childWindow : Window
    {
        Window prevWindow = null;
        public childWindow()
        {
            InitializeComponent();
        }
        public childWindow(Window prevWindow)
        {
            InitializeComponent();
            this.prevWindow = prevWindow;
        }

        private void windowPanel_MouseDown(object sender, MouseButtonEventArgs e)
        {

        }

        private void exitButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void childProfileButton_Click(object sender, RoutedEventArgs e)
        {
            childName.Text = GlobalVariables.globalChild.firstName;
            DOBpicker.SelectedDate = GlobalVariables.globalChild.DOB;
            if (GlobalVariables.globalChild.gender == "Male")
            {
                male.IsChecked = true;
            }
            else
            {

            }
                female.IsChecked = true;
            roomID.Text = Convert.ToString(GlobalVariables.globalChild.roomID);

            this.dailyDetailsPanel.Visibility = Visibility.Hidden;
            this.profilePanel.Visibility = Visibility.Visible;
        }

        private void dailyDetailsButton_Click(object sender, RoutedEventArgs e)
        {
            SQLQuery mSQLQuery = new SQLQuery();
            string details = mSQLQuery.getChildDailyDetails(selectedDay.SelectedDate.Value, GlobalVariables.globalChild.id);
            dailyDetailsContent.Text = details;

            this.dailyDetailsPanel.Visibility = Visibility.Visible;
            this.profilePanel.Visibility = Visibility.Hidden;
        }

        private void backButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
            prevWindow.Show();  
        }

    }
}
