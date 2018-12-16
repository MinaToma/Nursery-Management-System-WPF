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
        //                     id , feedback , name
        LinkedList<Tuple<Tuple<int, string>, string>> feedback;
       public  LinkedList<Child> childList;
        LinkedList<Parent> parentList;
        LinkedList<Staff> staffList;
        LinkedList<Room> roomList;
        public LinkedList<RowTemplate> childRow;
        public LinkedList<RowTemplate> staffRow;
        public LinkedList<RowTemplate> parentRow;
        public LinkedList<RowTemplate> roomRow;

        public adminWindow()
        {
            InitializeComponent();

            childRow = new LinkedList<RowTemplate>();
            parentRow = new LinkedList<RowTemplate>();
            staffRow = new LinkedList<RowTemplate>();
            roomRow = new LinkedList<RowTemplate>();
            childList = new LinkedList<Child>();
            parentList = new LinkedList<Parent>();
            staffList = new LinkedList<Staff>();
            roomList = new LinkedList<Room>();
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

            roomPanel.Visibility = Visibility.Hidden;
            this.profilePanel.Visibility = Visibility.Visible;
            AdminFeedback.Visibility = Visibility.Hidden;
            pendingRequestsPanel.Visibility = Visibility.Hidden;
            this.editDatabasePanel.Visibility = Visibility.Hidden;
            featuresPanel.Visibility = Visibility.Hidden;
        }

        private void editDatabase_Click(object sender, RoutedEventArgs e)
        {
            staffs1.Children.Clear();
            parents1.Children.Clear();
            children1.Children.Clear();
            rooms.Children.Clear();

            childRow.Clear();
            staffRow.Clear();
            parentRow.Clear();
            roomRow.Clear();
            

            SQLQuery mSQLQuery = new SQLQuery();

            childList = mSQLQuery.childToLinkedList(mSQLQuery.getNotPendingChild());
            parentList = mSQLQuery.parentToLinkedList(mSQLQuery.getNotPendingParent());
            staffList = mSQLQuery.staffToLinkedList(mSQLQuery.getNotPendingStaff());
            roomList = mSQLQuery.roomToLinkedList(mSQLQuery.getAllRooms());
            
            showPendingStaff(staffs1);
            showPendingChildren(children1);
            showPendingParent(parents1);
            showAllRoomsAndChildren(rooms);

            roomPanel.Visibility = Visibility.Hidden;
            this.AdminFeedback.Visibility = Visibility.Hidden;
            this.pendingRequestsPanel.Visibility = Visibility.Hidden;
            this.profilePanel.Visibility = Visibility.Hidden;
            this.editDatabasePanel.Visibility = Visibility.Visible;
            featuresPanel.Visibility = Visibility.Hidden;
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
            this.editDatabasePanel.Visibility = Visibility.Hidden;
            this.featuresPanel.Visibility = Visibility.Hidden;
            roomPanel.Visibility = Visibility.Hidden;
        }

        private void signOutButton_Click(object sender, RoutedEventArgs e)
        {
            signIn logIn = new signIn();
            logIn.Show();
            this.Close();
        }

        private void pendingRequests_Click(object sender, RoutedEventArgs e)
        { 
            staffs.Children.Clear();
            parents.Children.Clear();
            children.Children.Clear();

            childRow.Clear();
            parentRow.Clear();
            staffRow.Clear();

            SQLQuery mSQLQuery = new SQLQuery();
            
            childList= mSQLQuery.childToLinkedList(mSQLQuery.getPendingChild());
            parentList = mSQLQuery.parentToLinkedList(mSQLQuery.getPendingParent());
            staffList = mSQLQuery.staffToLinkedList(mSQLQuery.getPendingStaff());
            
            LinkedList<Child> notPending = new LinkedList<Child>();
            foreach(Child c in childList)
            {
               DataTable dt = mSQLQuery.getParentByID(c.parentID);
                if (Convert.ToInt32(dt.Rows[0]["parentIsPending"]) == 1)
                    notPending.AddLast(c);
                else c.lastName = dt.Rows[0]["parentFirstName"].ToString();
            }

            foreach(Child c in notPending)
            {
                childList.Remove(c);
            }

            showPendingStaff(staffs);
            showPendingChildren(children);
            showPendingParent(parents);

            this.pendingRequestsPanel.Visibility = Visibility.Visible;
            this.profilePanel.Visibility = Visibility.Hidden;
            this.AdminFeedback.Visibility = Visibility.Hidden;
            this.editDatabasePanel.Visibility = Visibility.Hidden;
            roomPanel.Visibility = Visibility.Hidden;
            this.featuresPanel.Visibility = Visibility.Hidden;
        }

        private void MinimizeButton_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        private void editProfileButton_Click(object sender, RoutedEventArgs e)
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

                mSQLQuery.deleteParentFeedback(id);
                parentNameLabel.Content = "";
                feedbackText.Text = "";

                feedback.Remove(feedback.ElementAt(feedbackIdx));
                feedbackIdx--;
            }
        }

        public void showFeedBack()
        {
            if(feedbackIdx >= 0 && feedbackIdx < feedback.Count)
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

        private void parentName_MouseDown(object sender, MouseButtonEventArgs e)
        {

        }

        private void parentsTab_MouseDown(object sender, MouseButtonEventArgs e)
        {
        }

        private void showPendingParent(Grid super)
        {
            double top = parentGrid.Margin.Top;
            double bottom = parentGrid.Margin.Bottom;
            double left = parentGrid.Margin.Left;
            double right = parentGrid.Margin.Right;
            
            for(int i = 0; i < parentList.Count; i++)
            { 
                RowTemplate rt = new RowTemplate(1 , 2 , 0 , i , 0 , null , parentList , null , super , this , null , null);
                rt.Margin = new Thickness(left , top , right , bottom);
                top += parentGrid.Height;
                if(super == parents1)
                {
                    rt.previousForm = 3;
                    rt.declineButton.Content = "Delete";
                    rt.acceptButton.Visibility = Visibility.Hidden;
                }
                parentRow.AddLast(rt);
                super.Children.Add(rt);
            }
        }

        private void showAllRoomsAndChildren(Grid super)
        {
            double top = roomGrid.Margin.Top;
            double bottom = roomGrid.Margin.Bottom;
            double left = roomGrid.Margin.Left;
            double right = roomGrid.Margin.Right;

            for (int i=0; i<roomList.Count;i++)
            {
                RowTemplate rt = new RowTemplate(3, i, roomList, super,this);
                rt.Margin = new Thickness(left, top, right, bottom);
                top  += roomGrid.Height;
                
                roomRow.AddLast(rt);
                super.Children.Add(rt);
            }
        }

        private void childrenTab_MouseDown(object sender, MouseButtonEventArgs e)
        {
        }

        public void showPendingChildren(Grid super)
        {
            double top = childGrid.Margin.Top;
            double bottom = childGrid.Margin.Bottom;
            double left = childGrid.Margin.Left;
            double right = childGrid.Margin.Right;

            for(int i = 0; i < childList.Count; i++)
            {
                RowTemplate rt = new RowTemplate(0, 2 , i , 0 , 0 , childList, null, null, super , this, null, null);
                rt.Margin = new Thickness(left, top, right, bottom);
                top += childGrid.Height;
                if (super == children1)
                {
                    rt.previousForm = 3;
                    rt.declineButton.Content = "Delete";
                    rt.acceptButton.Visibility = Visibility.Hidden;
                }

                if (super == children2)
                {
                    rt.previousForm = 2;
                    rt.acceptButton.Visibility = Visibility.Hidden;
                    rt.declineButton.Visibility = Visibility.Hidden;
                }
                childRow.AddLast(rt);
                super.Children.Add(rt);
            }
        }

        private void staffTab_MouseDown(object sender, MouseButtonEventArgs e)
        {

        }

        private void showPendingStaff(Grid super)
        {
            double top = staffGrid.Margin.Top;
            double bottom = staffGrid.Margin.Bottom;
            double left = staffGrid.Margin.Left;
            double right = staffGrid.Margin.Right;

            for(int i = 0; i < staffList.Count; i++)
            { 
                RowTemplate rt = new RowTemplate(2, 2, 0 , 0 , i , null, null , staffList, super, this, null, null);
                rt.Margin = new Thickness(left, top, right, bottom);
                top += staffGrid.Height;
                if (super == staffs1)
                {
                    rt.previousForm = 3;
                    rt.declineButton.Content = "Delete";
                    rt.acceptButton.Visibility = Visibility.Hidden;
                }
                staffRow.AddLast(rt);
                super.Children.Add(rt);
            }
        }

        private void staffTab_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            
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

        private void titleBar_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
                this.DragMove();
        }

        private void roomBack_Click(object sender, RoutedEventArgs e)
        {
            children2.Visibility = Visibility.Hidden;
            room5.Visibility = Visibility.Hidden;
            roomScrollerView.Visibility = Visibility.Visible;
            roomBack.Visibility = Visibility.Hidden;
        }

        private void adminFeatureButton_Click(object sender, RoutedEventArgs e)
        {
            featuresPanel.Visibility = Visibility.Visible;
            profilePanel.Visibility = Visibility.Hidden;
            editDatabasePanel.Visibility = Visibility.Hidden;
            AdminFeedback.Visibility = Visibility.Hidden;
            pendingRequestsPanel.Visibility = Visibility.Hidden;
            roomPanel.Visibility = Visibility.Hidden;
        }
        
        private void submitFeatureButton_Click(object sender, RoutedEventArgs e)
        {
            if(featureTextBox.Text.Length == 0)
            {
                MessageBox.Show("Please enter a feature", "Faild to submit", MessageBoxButton.OK, MessageBoxImage.None);
            }
            else
            {
                SQLQuery mSQLQuery = new SQLQuery();
                mSQLQuery.insertFeature(featureTextBox.Text);
                MessageBox.Show("Feature added successfully!", "Submission done!", MessageBoxButton.OK, MessageBoxImage.None);
            }
        }

        private void submitRoomButton_Click(object sender, RoutedEventArgs e)
        {
            //check for empty entries
            if (roomNumberTextBox.Text.Length==0)
            {
                MessageBox.Show("Please enter a room number", "Faild to submit", MessageBoxButton.OK, MessageBoxImage.None);
            }
            if (roomStaffIDTextBox.Text.Length == 0)
            {
                MessageBox.Show("Please enter staff id", "Faild to submit", MessageBoxButton.OK, MessageBoxImage.None);
            }
            else
            { 
                SQLQuery mSQLQuery = new SQLQuery();
                bool canEnter = true;
                Room mRoom;
                DataTable dtStaffID = mSQLQuery.getStaffByID(Convert.ToInt64(roomStaffIDTextBox.Text));
                DataTable dtRoomNo = mSQLQuery.getRoomByNumber(Convert.ToInt32(roomNumberTextBox.Text));
                //check for room number entry
                if (dtRoomNo.Rows.Count!=0) 
                {
                    MessageBox.Show("Room number already exists!", "Faild to submit", MessageBoxButton.OK, MessageBoxImage.None);
                    canEnter = false;
                }
                
                //check for staff id entry
                if (dtStaffID.Rows.Count == 0 || dtStaffID.Rows[0]["staffType"].ToString() == "Admin")
                {
                    MessageBox.Show("Staff ID doesn't exist!", "Faild to submit", MessageBoxButton.OK, MessageBoxImage.None);
                    canEnter = false;
                }
                
                if (canEnter)
                {
                    mRoom = new Room(-1 , Convert.ToInt32(roomNumberTextBox.Text), Convert.ToInt64(roomStaffIDTextBox.Text));
                    mSQLQuery.insertRoomData(mRoom);
                    MessageBox.Show("Room added successfully!", "Submission Done!", MessageBoxButton.OK, MessageBoxImage.None);
                }
            }
        }

        private void roomButton_Click(object sender, RoutedEventArgs e)
        {
            featuresPanel.Visibility = Visibility.Hidden;
            profilePanel.Visibility = Visibility.Hidden;
            editDatabasePanel.Visibility = Visibility.Hidden;
            AdminFeedback.Visibility = Visibility.Hidden;
            pendingRequestsPanel.Visibility = Visibility.Hidden;
            roomPanel.Visibility = Visibility.Visible;
        }
    }
}