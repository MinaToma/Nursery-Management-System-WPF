using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nursery_Management_System_WPF
{
    public class Parent : Human
    {
        public string phoneNumber { get; set; }
        public string email { get; set; }
        public string address { get; set; }
        public string creditCard { get; set; }

        public Parent() { }

        public Parent(Int64 _id , string _firstName, string _lastName, string _phoneNumber, string _email, string _address , string _creditCard , int _pending) :
            base(_id, _firstName, _lastName , _pending)
        {
            phoneNumber = _phoneNumber;
            email = _email;
            address = _address;
            creditCard = _creditCard;
        }
    }
}
