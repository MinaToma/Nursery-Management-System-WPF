using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nursery_Management_System_WPF
{
    public class GlobalVariables
    {
        public static Parent globalParent = new Parent();
        public static Child globalChild = new Child();
        public static Staff globalStaff = new Staff();
        public static Admin globalAdmin = new Admin();
        public static string globalType = "";
    }
}
