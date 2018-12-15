using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nursery_Management_System_WPF
{
    public class Room
    {
        public int id { get; set; }
        public int number { get; set;}
        public Int64 staffID { get; set; }
        
        public Room()
        {
            staffID = -1;
        }
        public Room(int _id , int _number , int _staffID = -1)
        {
            id = _id;
            number = _number;
            staffID = _staffID;
        }
    }
}
