using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nursery_Management_System_WPF
{
    public class Human
    {
        public Int64 id {get;set;}
        public String firstName { get; set; }
        public String lastName { get; set; }
        public int pending { get; set; }
        protected Human() {}

        protected Human(Int64 _id , String _firstName , string _lastName , int _pending)
        {
            id = _id;
            firstName = _firstName;
            lastName = _lastName;
            pending = _pending;
        }
        
        public String className()
        {
            return GetType().ToString();
        }
    }
}
