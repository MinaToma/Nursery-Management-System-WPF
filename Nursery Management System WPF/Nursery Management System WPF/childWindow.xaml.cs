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
    /// Interaction logic for childWindow.xaml
    /// </summary>
    public partial class childWindow : Window
    {

        Dictionary<string, int> FeatureToID = new Dictionary<string, int>();
        Dictionary<int, int> IsChecked = new Dictionary<int, int>();
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
            fillProfile();
            
            this.dailyDetailsPanel.Visibility = Visibility.Hidden;
            this.profilePanel.Visibility = Visibility.Visible;
        }

        public void fillProfile()
        {
            childName.Text = GlobalVariables.globalChild.firstName;
            DOBpicker.SelectedDate = GlobalVariables.globalChild.DOB;
            addFeaturesToList();
            if (GlobalVariables.globalChild.gender == "Male")
            {
                male.IsChecked = true;
            }
            else
            {
                female.IsChecked = true;
            }
            roomID.Text = Convert.ToString(GlobalVariables.globalChild.roomID);
            getCheckedFeatures();
            addFeaturesToList();
        }

        public List<Features> checkedFeatures()
        {
            List<Features> list = new List<Features>();
            foreach (var item in childFeaturesList.SelectedItems)
            {
                list.Add((Features)item);
            }
            return list;
        }

        private void dailyDetailsButton_Click(object sender, RoutedEventArgs e)
        {
            this.dailyDetailsPanel.Visibility = Visibility.Visible;
            this.profilePanel.Visibility = Visibility.Hidden;
        }

        private void backButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void titleBar_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
                this.DragMove();
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            if (selectedDay.SelectedDate != null)
            {
                SQLQuery mSQLQuery = new SQLQuery();
                string details = mSQLQuery.getChildDailyDetails(selectedDay.SelectedDate.Value, GlobalVariables.globalChild.id);
                dailyDetailsContent.Text = details;

            }
        }

        private void sendFeedback_Click(object sender, RoutedEventArgs e)
        {
            if(selectedDay.SelectedDate != null)
            {
                SQLQuery mSQLQuery = new SQLQuery();
                mSQLQuery.insertDailyChildDetails(selectedDay.SelectedDate.Value, dailyDetailsContent.Text , (int)GlobalVariables.globalChild.id);
            }
        }

        public void addFeaturesToList()
        {
            FeatureToID.Clear();
            List<Features> list = new List<Features>();
            List<Features> list2 = new List<Features>();
            SQLQuery sQLQuery = new SQLQuery();
            DataTable allFeatures;
            allFeatures = sQLQuery.allFeatures();
            foreach (DataRow dr in allFeatures.Rows)
            {
                Features ft = new Features(dr[1].ToString());
                
                FeatureToID.Add(dr[1].ToString(), Int32.Parse(dr[0].ToString()));
                list.Add(ft);

                int id = Int32.Parse(dr[0].ToString());
                
                if (IsChecked.ContainsKey(id)==true)
                {
                    
                    list2.Add(ft);
                }
            }
            selcetedFeatures.ItemsSource = list2;
            childFeaturesList.ItemsSource = list;
            childFeaturesList.SelectionMode = SelectionMode.Multiple;
            IsChecked.Clear();

        }
        private void getCheckedFeatures()
        {
            
            SQLQuery sQLQuery = new SQLQuery();
            DataTable dt =sQLQuery.getChildFeaturesByID(GlobalVariables.globalChild.id.ToString());
            foreach(DataRow dr in dt.Rows)
            {
               
                IsChecked.Add(Int32.Parse(dr[0].ToString()),1);
            }
        }

        private void editProfileButton_Click(object sender, RoutedEventArgs e)
        {
            SQLQuery mSQLQuery = new SQLQuery();
            

            if (childName.Text.Length >= 2 && DOBpicker.SelectedDate != null)
            {
                string gender;
                if (female.IsChecked == true)
                    gender = "Female";
                else
                    gender = "Male";

                
                mSQLQuery.deleteChildFeature((int)GlobalVariables.globalChild.id);
                GlobalVariables.globalChild.DOB = DOBpicker.SelectedDate.Value;
                GlobalVariables.globalChild.firstName = childName.Text;
                GlobalVariables.globalChild.lastName = GlobalVariables.globalParent.firstName;
                GlobalVariables.globalChild.gender = gender;

                List<Features> featurs = new List<Features>();

                featurs = checkedFeatures();
                foreach (var item in featurs)
                {
                    mSQLQuery.insertChildFeature((int)GlobalVariables.globalChild.id, FeatureToID[item.featureName]);
                }
                mSQLQuery.updateChildData(GlobalVariables.globalChild);
                MessageBox.Show("Updated", "Updated successfully ", MessageBoxButton.OK, MessageBoxImage.None);
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

        private void featuresComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
