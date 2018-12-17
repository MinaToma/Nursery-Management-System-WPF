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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Nursery_Management_System_WPF
{
    /// <summary>
    /// Interaction logic for RowTemplate.xaml
    /// </summary>
    public partial class RowTemplate : UserControl
    {
        //I wanna send a child , parent or staff to the form 

        //set 0 for child , 1 for parent , 2 for staff , 3 for room
        int idx = -1, cIdx = 0 , pIdx = 0 , sIdx = 0 , rIdx;
        Child mChild; 
        Parent mParent;
        Staff mStaff;
        Room mRoom;
        LinkedList<Child> child;
        LinkedList<Parent> parent;
        LinkedList<Staff> staff;
        LinkedList<Room> room;
        adminWindow aWindow;
        parentWindow pWindow;
        staffWindow sWindow;    
        Grid super;
        
        //specify previous form
        //0 coming from parent , 1 coming from staff , 2 coming from admin 
        public int previousForm = -1;

        public RowTemplate(int idx , int rIdx , LinkedList<Room> room  ,  Grid super , adminWindow aWindow)
        {
            InitializeComponent();
            this.idx = idx;
            this.rIdx = rIdx;
            this.room = room;
            this.mRoom = room.ElementAt(rIdx);
            this.super = super;
            cIdx = -1;
            pIdx = -1;
            sIdx = -1;
            this.aWindow = aWindow;
          //  aWindow.room5.Visibility = Visibility.Hidden;

            initializeR();
        }

        public RowTemplate(int idx , int previousForm  , int cIdx , int pIdx , int sIdx , LinkedList<Child> child , LinkedList<Parent> parent , LinkedList<Staff> staff, Grid super , adminWindow aWindow , parentWindow pWindow , staffWindow sWindow)
        {
            InitializeComponent();
            this.idx = idx;
            this.child = child;
            this.parent = parent;
            this.staff = staff;
            this.previousForm = previousForm;
            this.super = super;
            this.aWindow = aWindow;
            this.pWindow = pWindow;
            this.sWindow = sWindow;
            this.cIdx = cIdx;
            this.pIdx = pIdx;
            this.sIdx = sIdx;

            initialize();
        }

        void initializeR()
        {
            this.parentProfileImage.Visibility = Visibility.Hidden;
            acceptButton.Visibility = Visibility.Hidden;
            declineButton.Content = "Delete";
            name.Content=Convert.ToString(mRoom.number);
        }

        void initialize()

        {
            if(previousForm == 0 || previousForm == 1)
            {
                acceptButton.Visibility = Visibility.Hidden;
                declineButton.Visibility = Visibility.Hidden;
            }

            if(idx == 0)
            {
                name.Content = child.ElementAt(cIdx).firstName + " " + child.ElementAt(cIdx).lastName;
                mChild = child.ElementAt(cIdx);

                ImageOperation op = new ImageOperation();
                if(op.BinaryToImage(mChild.image) != null)
                    profilePicture.ImageSource = op.BinaryToImage(mChild.image);
            }
            else if(idx == 1)
            {
                name.Content = parent.ElementAt(pIdx).firstName + " " + parent.ElementAt(pIdx).lastName;
                mParent = parent.ElementAt(pIdx);
            }
            else if(idx == 2)
            {
                name.Content = staff.ElementAt(sIdx).firstName + " " + staff.ElementAt(sIdx).lastName;
                mStaff = staff.ElementAt(sIdx);
            }

            if (previousForm == 2)
            {
                acceptButton.Visibility = Visibility.Visible;
                declineButton.Visibility = Visibility.Visible;
            }
        }

        private void name_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (idx == 3)
            {
                aWindow.children2.Visibility = Visibility.Visible;
                aWindow.room5.Visibility = Visibility.Visible;
                aWindow.roomScrollerView.Visibility = Visibility.Hidden;
                aWindow.roomTab1.IsSelected = false;
                aWindow.children2.Children.Clear();
                SQLQuery mSQLQuery = new SQLQuery();
                child = mSQLQuery.childToLinkedList(mSQLQuery.getChildByRoomID(mRoom.id));
                
                aWindow.childCount.Content = mSQLQuery.staffToLinkedList(mSQLQuery.getStaffByID(mRoom.staffID)).ElementAt(0).firstName;

                aWindow.childList = child;
                aWindow.roomName.Content = "Room  " + Convert.ToString(mRoom.number);
                aWindow.roomBack.Visibility = Visibility.Visible;

                aWindow.showPendingChildren(aWindow.children2);
            }
            
        }

        private void parentGrid_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if(previousForm == 0)
            {
                childWindow cw = new childWindow(pWindow);
                cw.dailyDetails.IsReadOnly = true;
                GlobalVariables.globalChild = mChild;
                cw.sendDailyDetails.Visibility = Visibility.Hidden;
                cw.roomID.IsEnabled = false;
                cw.editProfileButton.Visibility = Visibility.Visible;
                cw.showDailyDetails.Visibility = Visibility.Visible;
                cw.fillProfile();
                cw.ShowDialog();
            }
            else if(previousForm == 1)
            {
                childWindow cw = new childWindow(pWindow);
                cw.roomID.IsReadOnly = true;
                cw.childName.IsReadOnly = true;
                cw.DOBpicker.IsEnabled = false;
                cw.editProfileButton.Visibility = Visibility.Hidden;
                cw.female.IsEnabled = false;
                cw.male.IsEnabled = false;
                cw.showDailyDetails.Visibility = Visibility.Hidden;
                GlobalVariables.globalChild = mChild;
                cw.fillProfile();
                cw.ShowDialog();
            }
            else if(previousForm == 2)
            {
                //from pending
                //disable all texts and buttons

                if (idx == 0) // Holding child
                {
                    GlobalVariables.globalChild = mChild;
                    childSignUp window = new childSignUp();
                    window.signUpButton.Visibility = Visibility.Hidden;
                    window.childFeaturesList.IsEnabled = false;
                    
                    //fill child data here
                    window.fillCdata();
                    window.disabledChild_info();
                    
                    window.ShowDialog();
                }
                else if(idx == 1) // Holding Parent
                {
                    GlobalVariables.globalParent = mParent;
                    parentSignUp window = new parentSignUp();
                    //fill parent data here
                    window.fillPdata1();
                    window.OKButton.Visibility = Visibility.Hidden;
                    window.disabledParent_info1();
                    
                    //fill parent data here

                    window.ShowDialog();
                }
                else if(idx == 2) //Holding Staff
                {
                    GlobalVariables.globalStaff = mStaff;
                    staffSignUp window = new staffSignUp();
                    window.signUpButton.Visibility = Visibility.Hidden;
                    //fill staff data here
                    window.fillSdata();
                    window.disabledStaff();
                    
                    //fill staff data here

                    window.ShowDialog();
                }
            }
            else if(previousForm == 3)
            {
                //from editDatabase

                if (idx == 0) // Holding child
                {
                    GlobalVariables.globalChild = mChild;
                    childSignUp window = new childSignUp();
                    window.signUpButton.Visibility = Visibility.Hidden;
                    window.OKButton.Visibility = Visibility.Visible;

                    //fill child data here
                    window.fillCdata();

                    window.ShowDialog();
                }
                else if (idx == 1) // Holding Parent
                {
                    GlobalVariables.globalParent = mParent;
                    parentSignUp window = new parentSignUp();

                    //fill parent data here
                    window.fillPdata1();

                    //fill parent data here

                    window.ShowDialog();
                }
                else if (idx == 2) //Holding Staff
                {
                    GlobalVariables.globalStaff = mStaff;
                    staffSignUp window = new staffSignUp();
                    window.signUpButton.Visibility = Visibility.Hidden;
                    window.ID.IsEnabled = false;
                    window.OKButton.Visibility = Visibility.Visible;
                    //fill staff data here
                    window.fillSdata();
                    //fill staff data here

                    window.ShowDialog();
                }
              
            }
        }

        public void removeFromParent()
        {
            
            if (idx == 0)
            {
                if (aWindow.childRow.Count != 0)
                {
                    if (child.Count != 1)
                    {
                        cIdx = child.Count - 1;
                        initialize();
                    }
                    super.Children.Remove(aWindow.childRow.ElementAt(aWindow.childRow.Count - 1));
                    aWindow.childRow.RemoveLast();
                    child.Remove(child.ElementAt(child.Count - 1));

                }
            }
            else if (idx == 1)
            {
                if (aWindow.parentRow.Count != 0)
                {
                    if (parent.Count != 1)
                    {
                        pIdx = parent.Count - 1;
                        initialize();
                    }
                    super.Children.Remove(aWindow.parentRow.ElementAt(aWindow.parentRow.Count - 1));
                    aWindow.parentRow.RemoveLast();
                    parent.Remove(parent.ElementAt(parent.Count - 1));

                }
            }
            else if (idx == 2)
            {
                if (aWindow.staffRow.Count != 0)
                {
                    if (staff.Count != 1)
                    {
                        sIdx = staff.Count - 1;
                        initialize();
                    }
                    super.Children.Remove(aWindow.staffRow.ElementAt(aWindow.staffRow.Count - 1));
                    aWindow.staffRow.RemoveLast();
                    staff.Remove(staff.ElementAt(staff.Count - 1));

                }
            }
            else if (idx == 3)
            {
                if (aWindow.roomRow.Count != 0)
                {
                    if (room.Count != 1)
                    {
                        rIdx = room.Count - 1;
                        initialize();
                    }
                    super.Children.Remove(aWindow.roomRow.ElementAt(aWindow.roomRow.Count - 1));
                    aWindow.roomRow.RemoveLast();
                    room.Remove(room.ElementAt(room.Count - 1));
                }
                //}
            }
        }

        private void acceptButton_Click(object sender, RoutedEventArgs e)
        {
            //set pending to 0 ---> accepted
            SQLQuery mSQLQuery = new SQLQuery();
            if (idx == 0)
            {
                mChild.pending = 0;
                mSQLQuery.updateChildData(mChild);
            }
            else if(idx == 1)
            {
                mParent.pending = 0;
                mSQLQuery.updateParentData(mParent);
            }
            else if(idx == 2)
            {
                mStaff.pending = 0;
                mSQLQuery.updateStaffData(mStaff);
            }
            

            removeFromParent();
        }
        
        private void declineButton_Click(object sender, RoutedEventArgs e)
        {
            //rejecting request
            SQLQuery mSQLQuery = new SQLQuery();
            if (idx == 0)
            {
                LinkedList<int> toDel = new LinkedList<int>();
                toDel.AddLast((int)child.ElementAt(cIdx).id);
                mSQLQuery.deleteChildData(toDel);
            }
            else if (idx == 1)
            {
                LinkedList<Int64> toDel = new LinkedList<Int64>();
                toDel.AddLast(parent.ElementAt(pIdx).id);
                mSQLQuery.deleteParentData(toDel);
            }
            else if (idx == 2)
            {
                LinkedList<Int64> toDel = new LinkedList<Int64>();
                toDel.AddLast(staff.ElementAt(sIdx).id);
                mSQLQuery.deleteStaffData(toDel);
            }
            else if (idx == 3)
            {
                LinkedList<int> toDel = new LinkedList<int>();
                toDel.AddLast(room.ElementAt(rIdx).id);
                mSQLQuery.deleteRoomData(toDel);
            }

            removeFromParent();
        }     
    }
}
