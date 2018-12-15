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
        int idx = -1;
        Child child;
        Parent parent;
        Staff staff;
        Grid super;

        //specify previous form
        //0 coming from parent , 1 coming from staff , 2 coming from admin
        int previousForm = -1;

        public RowTemplate(int idx , int previousForm , Child child , Parent parent , Staff staff , Grid super)
        {
            InitializeComponent();
            this.idx = idx;
            this.child = child;
            this.parent = parent;
            this.staff = staff;
            this.previousForm = previousForm;
            this.super = super;

            initialize();
        }

        void initialize()
        {
            if(idx == 0)
            {
                name.Content = child.firstName + " " + child.lastName;
            }
            else if(idx == 1)
            {
                name.Content = parent.firstName + " " + parent.lastName;
            }
            else if(idx == 2)
            {
                name.Content = staff.firstName + " " + staff.lastName;
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
                window.Show();
            }
            else if(idx == 1) // Holding Parent
            {
                parentSignUp window = new parentSignUp();
                window.Show();
            }
            else if(idx == 2) //Holding Staff
            {
                staffSignUp window = new staffSignUp();
                window.Show();
            }
        }

        private void acceptButton_Click(object sender, RoutedEventArgs e)
        {
            //set pending to 0 ---> accepted
            SQLQuery mSQLQuery = new SQLQuery();
            if (idx == 0)
            {
                child.pending = 0;
                mSQLQuery.updateChildData(child);
            }
            else if(idx == 1)
            {
                parent.pending = 0;
                mSQLQuery.updateParentData(parent);
            }
            else if(idx == 2)
            {
                staff.pending = 0;
                mSQLQuery.updateStaffData(staff);
            }

            super.Children.Remove(this);
        }
        
        private void declineButton_Click(object sender, RoutedEventArgs e)
        {
            //rejecting request
            SQLQuery mSQLQuery = new SQLQuery();
            if (idx == 0)
            {
                LinkedList<int> toDel = new LinkedList<int>();
                toDel.AddLast((int)child.id);
                mSQLQuery.deleteChildData(toDel);
            }
            else if (idx == 2)
            {
                LinkedList<Int64> toDel = new LinkedList<Int64>();
                toDel.AddLast(staff.id);
                mSQLQuery.deleteStaffData(toDel);
            }
            else if (idx == 1)
            {
                LinkedList<Int64> toDel = new LinkedList<Int64>();
                toDel.AddLast(parent.id);
                mSQLQuery.deleteParentData(toDel);
            }
            super.Children.Remove(this);
        }
    }
}
