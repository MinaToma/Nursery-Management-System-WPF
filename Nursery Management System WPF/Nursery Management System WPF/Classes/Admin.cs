using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nursery_Management_System_WPF
{
    public class Admin : Human
    {
        public string phoneNumber { get; set; }
        public String email { get; set; }
        public double salary { get; set; }
        public string type { get; set; }

        public Admin() { }
        public Admin(Int64 _id, String _firstName , string _lastName , string _phoneNumber , string email ) :
            base(_id , _firstName , _lastName , 0)
        {
            this.email = email;
            this.phoneNumber = _phoneNumber;
        }
        
    }
}
