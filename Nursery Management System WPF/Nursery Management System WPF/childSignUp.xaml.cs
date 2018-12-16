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
using System.Windows.Shapes;
using System.Data;
namespace Nursery_Management_System_WPF
{
    /// <summary>
    /// Interaction logic for childSignUp.xaml
    /// </summary>
    public partial class childSignUp : Window
    {


        Dictionary<string, int> FeatureToID = new Dictionary<string, int>();
        public childSignUp()
        {
            InitializeComponent();
            addFeaturesToList();

        }
        public void addFeaturesToList()
        {
            List<Features> list = new List<Features>();
            SQLQuery sQLQuery = new SQLQuery();
            DataTable allFeatures;
            allFeatures = sQLQuery.allFeatures();
            foreach (DataRow dr in allFeatures.Rows)
            {
                Features ft = new Features(dr[1].ToString());
                
                FeatureToID.Add(dr[1].ToString(), Int32.Parse(dr[0].ToString()));
                list.Add(ft);
            }

            childFeaturesList.ItemsSource = list;
            childFeaturesList.SelectionMode = SelectionMode.Multiple;
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

                Child child = new Child(childName.Text, GlobalVariables.globalParent.firstName, GlobalVariables.globalParent.id, -1, gender, DOBpicker.SelectedDate.Value, null, 1);
                mSQLQuery.insertChildData(child);
                int childID = mSQLQuery.getIDForChild(childName.Text, GlobalVariables.globalParent.id.ToString());
                List<Features> featurs = new List<Features>();
                featurs =checkedFeatures();
                foreach(var item in featurs)
                {
                    mSQLQuery.insertChildFeature(childID, FeatureToID[item.featureName]);
                }
                


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
        public List<Features> checkedFeatures()
        {
            List<Features> list = new List<Features>();
            foreach (var item in childFeaturesList.SelectedItems)
            {
                list.Add((Features)item);
            }
            return list;
        }

        private void minimizeButton_Click(object sender, RoutedEventArgs e)
        {
            
            WindowState = WindowState.Minimized;
        }

        private void exitButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void featuresComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void titleBar_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
                this.DragMove();
        }

        private void backButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        public void fillCdata()
        {
            childName.Text = GlobalVariables.globalChild.firstName;
            DOBpicker.SelectedDate = GlobalVariables.globalChild.DOB;
            if (GlobalVariables.globalChild.gender == "Male")
            {
                male.IsChecked = true;
            }
            else
            {
                female.IsChecked = true;
            }
            roomID.Text = Convert.ToString(GlobalVariables.globalChild.roomID);
        }

        public void disabledChild_info()
        {
            childName.IsEnabled = false;
            genderBox.IsEnabled = false;
            DOBpicker.IsEnabled = false;

        }

        private void OKButton_Click(object sender, RoutedEventArgs e)
        {
            SQLQuery mSqlQuery = new SQLQuery();
            if (roomID.Text != null || roomID.Text != " room ID ")
            {
                GlobalVariables.globalChild.roomID = Convert.ToInt32(roomID.Text);
                mSqlQuery.updateChildData(GlobalVariables.globalChild);
                MessageBox.Show("Data Updated Successflly", "Process Finshed", MessageBoxButton.OK, MessageBoxImage.Information);

            }
        }
    }
}
