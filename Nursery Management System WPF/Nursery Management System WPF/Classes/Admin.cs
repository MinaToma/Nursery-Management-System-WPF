using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nursery_Management_System_WPF
{
    public class Admin : Staff
    {
        public Admin() { }
        public Admin(Int64 _id, String _firstName , string _lastName , string _phoneNumber , string email , double _salary , int _pending , string _type) :
            base(_id , _firstName , _lastName , _phoneNumber , email , _salary , _pending , _type)
        {

        }

        public Admin ToAdmin(Staff staff)
        {
            return new Admin(staff.id, staff.firstName, staff.lastName, staff.phoneNumber, staff.email , staff.salary, staff.pending, staff.type);
        }
    }
}
