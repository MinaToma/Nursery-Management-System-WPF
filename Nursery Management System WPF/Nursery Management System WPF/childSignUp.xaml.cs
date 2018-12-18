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
using System.IO;
using Microsoft.Win32;


namespace Nursery_Management_System_WPF
{
    /// <summary>
    /// Interaction logic for childSignUp.xaml
    /// </summary>
    public partial class childSignUp : Window
    {


        Dictionary<string, int> FeatureToID = new Dictionary<string, int>();
        Dictionary<int, int> getRoomID = new Dictionary<int, int>();
        bool mainPic=false;
        public childSignUp()
        {
            InitializeComponent();
            addFeaturesToList();
            fillRoomID();
        }
        public void addFeaturesToList()
        {
            FeatureToID.Clear();
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
            List<Features> featurs = new List<Features>();
            featurs = checkedFeatures();
            if (childName.Text.Length >= 2 && DOBpicker.SelectedDate != null && featurs.Count>0 && mainPic!=false)
            {
                string gender;
                if (female.IsChecked == true)
                    gender = "Female";
                else
                    gender = "Male";
               
                    ImageOperation OP = new ImageOperation();
                profileHeader.Source = profileImage.ImageSource;
                    Child child = new Child(childName.Text, GlobalVariables.globalParent.firstName, GlobalVariables.globalParent.id, -1, gender, DOBpicker.SelectedDate.Value, OP.ImageToBinary(profileHeader) , 1);
                    mSQLQuery.insertChildData(child);

                
                
                int childID = mSQLQuery.getIDForChild(childName.Text, GlobalVariables.globalParent.id.ToString());
                
                foreach(var item in featurs)
                {
                    mSQLQuery.insertChildFeature(childID, FeatureToID[item.featureName]);
                }
                
                MessageBox.Show("Requset has been sent", "Request sent", MessageBoxButton.OK, MessageBoxImage.None);
                this.Close();
            }
            else if(featurs.Count==0)
            {
                MessageBox.Show("Please Enter atleast one feature", "Invaild Data", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else if(mainPic==false)
            {
                MessageBox.Show("Please Enter child image", "Invaild Data", MessageBoxButton.OK, MessageBoxImage.Error);
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

            addFeaturesToList();
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
            List<Features> featurs = new List<Features>();
            featurs = checkedFeatures();
            SQLQuery mSqlQuery = new SQLQuery();
            if (roomID.SelectedIndex>-1 && featurs.Count>0)
            {

                mSqlQuery.deleteChildFeature((int)GlobalVariables.globalChild.id);
                foreach (var item in featurs)
                {
                    mSqlQuery.insertChildFeature(((int)GlobalVariables.globalChild.id), FeatureToID[item.featureName]);
                }
                GlobalVariables.globalChild.roomID = Convert.ToInt32(getRoomID[int.Parse(roomID.Text.ToString())]);
                mSqlQuery.updateChildData(GlobalVariables.globalChild);
                MessageBox.Show("Data Updated Successflly", "Process Finshed", MessageBoxButton.OK, MessageBoxImage.Information);

            }
            else if(featurs.Count==0)
            {
                MessageBox.Show("Please Enter atleast one feature", "Invaild Data", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                MessageBox.Show("Please Enter the Room number", "Invaild Data", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void import_Pic_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog op = new OpenFileDialog();
            op.Title = "Select a picture";
            op.Filter = "All supported graphics|*.jpg;*.jpeg;*.png|" +
              "JPEG (*.jpg;*.jpeg)|*.jpg;*.jpeg|" +
              "Portable Network Graphic (*.png)|*.png";
            if (op.ShowDialog() == true)
            {
                profileImage.ImageSource = new BitmapImage(new Uri(op.FileName));
                mainPic = true;
             //   childImage.Visibility = Visibility.Hidden;
            }
            
        }
        private void fillRoomID()
        {
            SQLQuery mSql = new SQLQuery();
            DataTable dt = mSql.getAllRooms();

            foreach (DataRow dr in dt.Rows)
            {
                roomID.Items.Add(dr[1].ToString());
                getRoomID.Add(int.Parse(dr[1].ToString()), int.Parse(dr[0].ToString()));
            }


        }
    }
}
