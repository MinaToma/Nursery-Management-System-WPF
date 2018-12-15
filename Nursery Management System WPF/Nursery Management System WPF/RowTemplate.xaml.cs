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

        //set 0 for child , 1 for parnet , 2 for staff
        int idx = -1, cIdx = 0 , pIdx = 0 , sIdx = 0;
        LinkedList<Child> child;
        LinkedList<Parent> parent;
        LinkedList<Staff> staff;
        adminWindow aWindow;
        parentWindow pWindow;
        staffWindow sWindow;    
        Grid super;
        
        //specify previous form
        //0 coming from parent , 1 coming from staff , 2 coming from admin
        int previousForm = -1;

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

        void initialize()
        {
            if(idx == 0)
            {
                name.Content = child.ElementAt(cIdx).firstName + " " + child.ElementAt(cIdx).lastName;
            }
            else if(idx == 1)
            {
                name.Content = parent.ElementAt(pIdx).firstName + " " + parent.ElementAt(pIdx).lastName;
            }
            else if(idx == 2)
            {
                name.Content = staff.ElementAt(sIdx).firstName + " " + staff.ElementAt(sIdx).lastName;
            }

            if (previousForm == 2)
            {
                acceptButton.Visibility = Visibility.Visible;
                declineButton.Visibility = Visibility.Visible;
            }
        }

        private void parentGrid_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if(idx == 0) // Holding child
            {
                childSignUp window = new childSignUp();

                //fill child data here

                window.Show();
            }
            else if(idx == 1) // Holding Parent
            {
                parentSignUp window = new parentSignUp();

                //fill parent data here

                window.Show();
            }
            else if(idx == 2) //Holding Staff
            {
                staffSignUp window = new staffSignUp();

                //fill staff data here

                window.Show();
            }
        }

        public void removeFromParent()
        {
            /*if(previousForm == 0)
            {
                if(idx == 0)
                {
                    aWindow.childRow.RemoveLast();
                    super.Children.Remove(aWindow.childRow.ElementAt(aWindow.childRow.Count - 1));
                }
                else if(idx == 1)
                {
                    aWindow.parentRow.RemoveLast();
                    super.Children.Remove(aWindow.parentRow.ElementAt(aWindow.parentRow.Count - 1));
                }
                else if(idx == 2)
                {
                    aWindow.staffRow.RemoveLast();
                    super.Children.Remove(aWindow.staffRow.ElementAt(aWindow.staffRow.Count - 1));
                }
            }
            else if(previousForm == 1)
            { 
                if(idx == 0)
                {
                    aWindow.childRow.RemoveLast();
                    super.Children.Remove(aWindow.childRow.ElementAt(aWindow.childRow.Count - 1));
                }
                else if(idx == 1)
                {
                    aWindow.parentRow.RemoveLast();
                    super.Children.Remove(aWindow.parentRow.ElementAt(aWindow.parentRow.Count - 1));
                }
                else if(idx == 2)
                {
                    aWindow.staffRow.RemoveLast();
                    super.Children.Remove(aWindow.staffRow.ElementAt(aWindow.staffRow.Count - 1));
                }
            }
            else*/
            if (previousForm == 2)
            {
                if(idx == 0)
                {
                    if(aWindow.childRow.Count != 0)
                    {
                        super.Children.Remove(aWindow.childRow.ElementAt(aWindow.childRow.Count - 1));
                        aWindow.childRow.RemoveLast();
                    }
                }
                else if(idx == 1)
                {
                    if(aWindow.parentRow.Count != 0)
                    {
                        super.Children.Remove(aWindow.parentRow.ElementAt(aWindow.parentRow.Count - 1));
                        aWindow.parentRow.RemoveLast();
                    }
                }
                else if(idx == 2)
                {
                    if(aWindow.staffRow.Count != 0)
                    {
                        super.Children.Remove(aWindow.staffRow.ElementAt(aWindow.staffRow.Count - 1));
                        aWindow.staffRow.RemoveLast();
                    }
                }
            }
        }

        private void acceptButton_Click(object sender, RoutedEventArgs e)
        {
            //set pending to 0 ---> accepted
            SQLQuery mSQLQuery = new SQLQuery();
            bool removeMe = false;
            if (idx == 0)
            {
                child.ElementAt(cIdx).pending = 0;
                mSQLQuery.updateChildData(child.ElementAt(cIdx));
                child.Remove(child.ElementAt(cIdx));

                if (child.Count == cIdx)
                    removeMe = true;
                else cIdx = child.Count - 1;
            }
            else if(idx == 1)
            {
                parent.ElementAt(pIdx).pending = 0;
                mSQLQuery.updateParentData(parent.ElementAt(pIdx));
                parent.Remove(parent.ElementAt(pIdx));

                if (parent.Count == pIdx)
                    removeMe = true;
                else pIdx = parent.Count - 1;
            }
            else if(idx == 2)
            {
                staff.ElementAt(sIdx).pending = 0;
                mSQLQuery.updateStaffData(staff.ElementAt(sIdx));
                staff.Remove(staff.ElementAt(sIdx));

                if (staff.Count == sIdx)
                    removeMe = true;
                else sIdx = staff.Count - 1;
            }

            if (removeMe)
                super.Children.Remove(this);
            else
            {
                initialize();
                removeFromParent();
            }
        }
        
        private void declineButton_Click(object sender, RoutedEventArgs e)
        {
            bool removeMe = false;
            //rejecting request
            SQLQuery mSQLQuery = new SQLQuery();
            if (idx == 0)
            {
                LinkedList<int> toDel = new LinkedList<int>();
                toDel.AddLast((int)child.ElementAt(cIdx).id);
                mSQLQuery.deleteChildData(toDel);

                child.Remove(child.ElementAt(cIdx));

                if (child.Count == cIdx)
                    removeMe = true;
                else cIdx = child.Count - 1;
            }
            else if (idx == 2)
            {
                LinkedList<Int64> toDel = new LinkedList<Int64>();
                toDel.AddLast(staff.ElementAt(sIdx).id);
                mSQLQuery.deleteStaffData(toDel);

                staff.Remove(staff.ElementAt(sIdx));

                if (staff.Count == sIdx)
                    removeMe = true;
                else sIdx = staff.Count - 1;
            }
            else if (idx == 1)
            {
                LinkedList<Int64> toDel = new LinkedList<Int64>();
                toDel.AddLast(parent.ElementAt(pIdx).id);
                mSQLQuery.deleteParentData(toDel);

                parent.Remove(parent.ElementAt(pIdx));

                if (parent.Count == pIdx)
                    removeMe = true;
                else pIdx = parent.Count - 1;
            }

            if (removeMe)
                super.Children.Remove(this);
            else
            {
                initialize();
                removeFromParent();
            }
        }     
    }
}
