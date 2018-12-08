using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nursery_Management_System_WPF
{
    public class Child : Human
    {
        public Int64 parentID { get; set; }
        public int roomID { get; set; }
        public string gender { get; set; }
        public DateTime DOB { get; set; }
        public byte[] image { get; set; }
        
        public Child() {}
        public Child(String _firstName , string _lastName , Int64 _parentID , int _roomID , string _gender , DateTime _DOB , byte[] _image , int _pending) : 
            base( 1 , _firstName , _lastName , _pending)
        {
            parentID = _parentID;
            roomID = _roomID;
            gender = _gender;
            DOB = _DOB;
            image = _image;
            
        }
    }
}
