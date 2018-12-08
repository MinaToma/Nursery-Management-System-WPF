using System.IO;
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
using System.Windows.Shapes;

namespace Nursery_Management_System_WPF
{ 
    class connectionString
    {
        public string serverName { set; get;}
       
        public connectionString()
        {
            try
            {
                StreamReader sr = new StreamReader(@"ServerName\serverName.txt");
                serverName = "Server=" + sr.ReadLine().ToString() + "; DataBase=Nursery; Integrated Security=true;";
            }
            catch
            {
                MessageBox.Show("Error server isn't found" , "Error in Conenction" , MessageBoxButton.OK , MessageBoxImage.Error);
            }
        }
    }
}
