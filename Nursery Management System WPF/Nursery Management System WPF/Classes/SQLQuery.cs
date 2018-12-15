using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;

namespace Nursery_Management_System_WPF
{
    class SQLQuery
    {
        public SQLQuery() { }

        /****************  USER AUTHENTICATION  ****************/

        public bool serachForUser(string name, string password)
        {
            SQL mSql = new SQL();
            string query = "select * from User_Password where userName like '" + name + "' and userPassword like '" + password + "'";
            DataTable dt = null;
            dt = mSql.retrieveQuery(query);

            if (dt.Rows.Count == 0)
                return false;

            string type = dt.Rows[0]["userType"].ToString();

            if (type.Equals("Parent"))
            {
                GlobalVariables.globalType = "Parent";
                Int64 id = Convert.ToInt64(dt.Rows[0]["parentID"].ToString());
                GlobalVariables.globalParent = parentToLinkedList(getParentByID(id)).ElementAt(0);
                if (GlobalVariables.globalParent.pending == 1)
                    return false;
            }
            else if (type.Equals("Staff"))
            {
                GlobalVariables.globalType = "Staff";
                Int64 id = Convert.ToInt64(dt.Rows[0]["staffID"].ToString());
                GlobalVariables.globalStaff = staffToLinkedList(getStaffByID(id)).ElementAt(0);
                if (GlobalVariables.globalStaff.pending == 1)
                    return false;
            }
            else if (type.Equals("Admin"))
            {
                GlobalVariables.globalType = "Admin";
                Int64 id = Convert.ToInt64(dt.Rows[0]["staffID"].ToString());
                GlobalVariables.globalAdmin = GlobalVariables.globalAdmin.ToAdmin(staffToLinkedList(getStaffByID(id)).ElementAt(0));
                if (GlobalVariables.globalAdmin.pending == 1)
                    return false;
            }

            return true;
        }

        public bool checkForUsername(string username)
        {
            SQL mSql = new SQL();
            string query = "select * from User_Password where userName like '" + username + "'";
            DataTable dt = null;
            dt = mSql.retrieveQuery(query);

            if (dt.Rows.Count == 0)
                return false;

            return true;
        }

        public DataTable selectUsernameByIDAndType(Int64 id, String type)
        {
            SQL mSQL = new SQL();
            string query;
            if (type == "Staff" || type == "Admin")
            {
                query = "select * from User_Password where staffID  =  " + Convert.ToString(id);
            }
            else
            {
                query = "select * from User_Password where parentID  =  " + Convert.ToString(id);
            }

            return mSQL.retrieveQuery(query);
        }


        /****************  INSERTING DATA INTO DATABASE  ****************/

        //child data insertion
        public void insertChildData(Child child)
        {
            SQL mSQL = new SQL();
            SqlCommand mCommand = new SqlCommand("insertChildData");
            mCommand.CommandType = CommandType.StoredProcedure;

            mCommand.Parameters.AddWithValue("@childName", child.firstName);
            mCommand.Parameters.AddWithValue("@parentID", child.parentID);
            mCommand.Parameters.AddWithValue("@DOB", child.DOB);
            mCommand.Parameters.AddWithValue("@gender", child.gender);
            if(child.image == null)
            {
                mCommand.Parameters.Add("@picture", SqlDbType.VarBinary).Value = DBNull.Value;
            }
            else
            {
                mCommand.Parameters.AddWithValue("@picture", child.image);
            }

            mCommand.Parameters.AddWithValue("@roomID", DBNull.Value);

            mCommand.Parameters.AddWithValue("@childPending", child.pending);

            mSQL.insertQuery(mCommand);

            return;
        }

        //parent data insertion
        public void insertParentData(Parent parent)
        {
            SQL mSQL = new SQL();
            SqlCommand mCommand = new SqlCommand("insertParentData");
            mCommand.CommandType = CommandType.StoredProcedure;

            mCommand.Parameters.AddWithValue("@parentID", parent.id);
            mCommand.Parameters.AddWithValue("@parentFirstName", parent.firstName);
            mCommand.Parameters.AddWithValue("@parentLastName", parent.lastName);
            mCommand.Parameters.AddWithValue("@parentAddress", parent.address);
            mCommand.Parameters.AddWithValue("@parentPhoneNumber", parent.phoneNumber);
            mCommand.Parameters.AddWithValue("@parentCreditCard", parent.creditCard);
            mCommand.Parameters.AddWithValue("@parentEmail", parent.email);
            mCommand.Parameters.AddWithValue("@parentPending", parent.pending);

            mSQL.insertQuery(mCommand);

            return;
        }

        //staff data insertion
        public void insertStaffData(Staff staff, string department)
        {
            SQL mSQL = new SQL();
            SqlCommand mCommand = new SqlCommand("insertStaffData");
            mCommand.CommandType = CommandType.StoredProcedure;

            mCommand.Parameters.AddWithValue("@staffID", staff.id);
            mCommand.Parameters.AddWithValue("@staffFirstName", staff.firstName);
            mCommand.Parameters.AddWithValue("@staffLastName", staff.lastName);
            mCommand.Parameters.AddWithValue("@staffPhoneNumber", staff.phoneNumber);
            mCommand.Parameters.AddWithValue("@staffEmail", staff.email);
            if(staff.salary == -1)
            {
                mCommand.Parameters.AddWithValue("@staffSalary", DBNull.Value);
            }
            else
            {
                mCommand.Parameters.AddWithValue("@staffSalary", staff.salary);
            }
            mCommand.Parameters.AddWithValue("@staffType", department);
            mCommand.Parameters.AddWithValue("@staffPending", staff.pending);

            mSQL.insertQuery(mCommand);

            return;
        }

        //room data insertion
        public void insertRoomData(Room room)
        {
            SQL mSQL = new SQL();
            SqlCommand mCommand = new SqlCommand("insertRoomData");
            mCommand.CommandType = CommandType.StoredProcedure;

            mCommand.Parameters.AddWithValue("@roomNumber", room.number);
            if (room.staffID == -1)
                mCommand.Parameters.AddWithValue("@roomStaffID", DBNull.Value);
            else
                mCommand.Parameters.AddWithValue("@roomStaffID", room.staffID);
            mSQL.insertQuery(mCommand);

            return;
        }

        //insert child feature
        public void insertChildFeature(int childID, int featureID)
        {
            SQL mSQL = new SQL();
            SqlCommand mCommand = new SqlCommand("insertChildFeature");
            mCommand.CommandType = CommandType.StoredProcedure;

            mCommand.Parameters.AddWithValue("@childID", childID);
            mCommand.Parameters.AddWithValue("@featureID", featureID);

            mSQL.insertQuery(mCommand);
            return;
        }

        //insert feature
        public void insertFeature(string featureName)
        {
            SQL mSQL = new SQL();
            SqlCommand mCommand = new SqlCommand("insertFeature");
            mCommand.CommandType = CommandType.StoredProcedure;

            mCommand.Parameters.AddWithValue("@FeaturesName", featureName);
            mSQL.insertQuery(mCommand);
            return;

        }

        //insert daily child details
        public void insertDailyChildDetails(DateTime detailsDate , string childDetails , int childID)
        {
            SQL mSQL = new SQL();
            SqlCommand mCommand = new SqlCommand("insertChildDailyDetails");
            mCommand.CommandType = CommandType.StoredProcedure;

            mCommand.Parameters.AddWithValue("@detailsDate", detailsDate);
            mCommand.Parameters.AddWithValue("@childDetails", childDetails);
            mCommand.Parameters.AddWithValue("@childID", childID);
            mSQL.insertQuery(mCommand);
            return;

        }

        //insert User
        public void insertUser(string name , string password , string type , Int64 id)
        {
            SQL mSQL = new SQL();
            SqlCommand mCommand = new SqlCommand("insertUser");
            mCommand.CommandType = CommandType.StoredProcedure;

            mCommand.Parameters.AddWithValue("@userName", name);

            if(type == "Parent")
            {
                mCommand.Parameters.AddWithValue("@staffID", DBNull.Value);
                mCommand.Parameters.AddWithValue("@parentID", id);
            }
            else
            {
                mCommand.Parameters.AddWithValue("@staffID", id);
                mCommand.Parameters.AddWithValue("@parentID", DBNull.Value);
            }

            mCommand.Parameters.AddWithValue("@userPassword", password);
            mCommand.Parameters.AddWithValue("@userType", type);

            mSQL.insertQuery(mCommand);
        }

        /****************  RETRIEVING CHILD DATA FROM DATABASE  ****************/

        private DataTable getChild(string query)
        {
            SQL sql = new SQL();

            DataTable dt = new DataTable();
            dt = sql.retrieveQuery(query);

            return dt;
        }
        public LinkedList<Child> childToLinkedList(DataTable dt)
        {
            LinkedList<Child> child = new LinkedList<Child>();
            foreach (DataRow dr in dt.Rows)
            {
                Child currentChild = new Child();

                currentChild.id = Convert.ToInt32(dr["childID"].ToString());
                currentChild.firstName = dr["childName"].ToString();
                currentChild.parentID = Convert.ToInt64(dr["parentID"].ToString());
                currentChild.DOB = Convert.ToDateTime(dr["DOB"].ToString());
                currentChild.gender = dr["gender"].ToString();
                if(dr["roomID"] == DBNull.Value)
                {
                    currentChild.roomID = -1;
                }
                else
                {
                    currentChild.roomID = Convert.ToInt32(dr["roomID"].ToString());
                }

                if (dr["picture"] == DBNull.Value)
                {
                    currentChild.image = null;
                }
                else
                {
                     currentChild.image =(byte[])(dr["picture"]);
                }

                currentChild.pending = Convert.ToInt32(dr["childIsPending"].ToString());

                child.AddLast(currentChild);
            }

            return child;
        }




        //uses specific query to select all children from database
        public DataTable getAllChildren()
        {
            string query = "select * from Child";
            return getChild(query);
        }

        //uses specific query to select child by ID from database
        public DataTable getChildByID(Int64 id)
        {
            string query = "select * from Child where childID = " + Convert.ToString(id);
            return getChild(query);
        }

        //uses specific query to select child by parent's ID from database
        public DataTable getChildByParentID(Int64 id)
        {
            string query = "select * from Child where parentID = " + Convert.ToString(id);
            return getChild(query);
        }
        
        // retreiv child profile
        public DataTable Child_Data(Int64 id)
        {
            SQL mSQL = new SQL();
            SqlCommand mCommand = new SqlCommand("Child_Data");
            mCommand.CommandType = CommandType.StoredProcedure;
            mCommand.Parameters.AddWithValue("@parentId", id);
            DataTable dt = new DataTable();
             dt = getChildByParentID(id);
            return dt;
        }

        //uses specific query to select child by room's ID from database
        public DataTable getChildByRoomID(int id)
        {
            string query = "select * from Child where roomID = " + Convert.ToString(id);
            return getChild(query);
        }

        //uses specific query to select pending child by parent's ID from database
        public DataTable getPendingChildByParentID(Int64 id)
        {
            string query = "select * from Child where parentID = " + Convert.ToString(id) + " and childIsPending = 1";
            return getChild(query);
        }
        public DataTable getPendingChild()
        {
            string query = "select * from Child where childIsPending = 1";
            return getChild(query);
        }

        public DataTable getNotPendingChild()
        {
            string query = "select * from Child where childIsPending = 0";
            return getChild(query);
        }

        /****************  RETRIEVING PARENT DATA FROM DATABASE  ****************/

        private DataTable getParent(string query)
        {
            SQL sql = new SQL();

            DataTable dt = new DataTable();
            dt = sql.retrieveQuery(query);

            return dt;
        }
        
        public LinkedList<Parent> parentToLinkedList(DataTable dt)
        {
            LinkedList<Parent> parent = new LinkedList<Parent>();
            foreach (DataRow dr in dt.Rows)
            {
                Parent currentParent = new Parent();

                currentParent.id = Convert.ToInt64(dr["parentID"].ToString());
                currentParent.firstName = dr["parentFirstName"].ToString();
                currentParent.lastName = dr["parentLastName"].ToString();
                currentParent.address = dr["parentAddress"].ToString();
                currentParent.phoneNumber = dr["parentPhoneNumber"].ToString();
                currentParent.creditCard = dr["parentCreditCard"].ToString();
                currentParent.email = dr["parentEmail"].ToString();
                currentParent.pending = Convert.ToInt32(dr["parentIsPending"].ToString());

                parent.AddLast(currentParent);
            }
            return parent;
        }

        //uses specific query to select all parents from database
        public DataTable getAllParent()
        {
            string query = "select * from Parent";
            return getParent(query);
        }


        //uses specific query to select parent by ID from database
        public DataTable getParentByID(Int64 id)
        {
            string query = "select * from Parent where parentID = " + Convert.ToString(id);
            return getParent(query);
        }

        //uses specific query to select pending parents from database
        public DataTable getPendingParent()
        {
            string query = "select * from Parent where parentIsPending = 1";
            return getParent(query);
        }
        public DataTable getNotPendingParent()
        {
            string query = "select * from Parent where parentIsPending = 0";
            return getParent(query);
        }


        /****************  RETRIEVING STAFF DATA FROM DATABASE  ****************/

        private DataTable getStaff(string query)
        {
            SQL sql = new SQL();

            DataTable dt = new DataTable();
            dt = sql.retrieveQuery(query);

            return dt;
        }

        public LinkedList<Staff> staffToLinkedList(DataTable dt)
        {
            LinkedList<Staff> staff = new LinkedList<Staff>();
            foreach (DataRow dr in dt.Rows)
            {
                Staff currentStaff = new Staff();

                currentStaff.id = Convert.ToInt64(dr["staffID"].ToString());
                currentStaff.firstName = dr["staffFirstName"].ToString();
                currentStaff.lastName = dr["staffLastName"].ToString();
                currentStaff.phoneNumber = dr["staffPhoneNumber"].ToString();
                currentStaff.email = dr["staffEmail"].ToString();
                currentStaff.type = dr["staffType"].ToString();
                currentStaff.pending = Convert.ToInt32(dr["staffIsPending"].ToString());
                if (currentStaff.pending == 1)
                {
                    currentStaff.salary = -1;
                }
                else
                {
                    currentStaff.salary = Convert.ToDouble(dr["staffSalary"].ToString());
                }

                staff.AddLast(currentStaff);
            }

            return staff;
        }

        //uses specific query to select all staff members from database
        public DataTable getAllStaff()
        {
            string query = "select * from Staff";
            return getStaff(query);
        }

        //uses specific query to select staff member type from database (admin, staff)
        public DataTable getStaffByType(string type)
        {
            string query = "select * from Staff where staffType = " + type;
            return getStaff(query);
        }

        //uses specific query to select staff member by ID from database
        public DataTable getStaffByID(Int64 id)
        {
            string query = "select * from Staff where staffID = " + Convert.ToString(id);
            return getStaff(query);
        }

        //uses specific query to select staff member by room ID from database
        public DataTable getStaffByRoomID(int id)
        {
            LinkedList<Room> room = new LinkedList<Room>();
            room = roomToLinkedList(getRoomByID(id));

            if (room.Count() == 0)
                return new DataTable();

            string query = "select * from Staff where staffID = " + Convert.ToString(room.ElementAt(0).staffID);
            return getStaff(query);
        }

        //uses specific query to select pending staff member requests from database
        public DataTable getPendingStaff()
        {
            string query = "select * from Staff where staffIsPending = 1";
            return getStaff(query);
        }

        public DataTable getNotPendingStaff()
        {
            string query = @"select * from Staff where staffIsPending = 0 and staffType like 'Staff'";
            return getStaff(query);
        }

        /****************  RETRIEVING ROOM DATA FROM DATABASE  ****************/

        private DataTable getRoom(string query)
        {
            SQL sql = new SQL();

            DataTable dt = new DataTable();
            dt = sql.retrieveQuery(query);

            return dt;
        }

        public LinkedList<Room> roomToLinkedList(DataTable dt)
        {
            LinkedList<Room> room = new LinkedList<Room>();
            foreach (DataRow dr in dt.Rows)
            {
                Room currentRoom = new Room();

                currentRoom.id = Convert.ToInt32(dr["roomID"].ToString());
                currentRoom.number = Convert.ToInt32(dr["roomNumber"].ToString());
                currentRoom.staffID = Convert.ToInt32(dr["roomStaffID"].ToString());

                room.AddLast(currentRoom);
            }
            return room;
        }

        public void insertParentFeedback(Int64 parentID , string feedback)
        {
            SQL mSQL = new SQL();

            SqlCommand mCommand = new SqlCommand("insertParentFeedback");
            mCommand.CommandType = CommandType.StoredProcedure;

            mCommand.Parameters.AddWithValue("@parentID", parentID);
            mCommand.Parameters.AddWithValue("@description", feedback);

            mSQL.insertQuery(mCommand);

            return;
        }

        public LinkedList<Tuple<Tuple<int , string > , string>> getAllParentFeedback()
        {
            SQL mSQL = new SQL();
            SqlCommand mCommand = new SqlCommand("getAllParentFeedback");
            LinkedList<Tuple<Tuple<int , string> , string>> feedback = new LinkedList<Tuple<Tuple<int , string> , string>>();
            
            DataTable dt = mSQL.retrieveQuery(mCommand);

            foreach (DataRow dr in dt.Rows)
            {
                feedback.AddLast(new Tuple<Tuple<int , string> , string>( new Tuple<int, string>( Convert.ToInt32(dr["feedbackID"]) , dr["feedbackDescription"].ToString() ) , dr["parentFirstName"].ToString()
                    + dr["parentLastName"].ToString() ) ) ;    
            }

            return feedback;
        }

        public void deleteParentFeedback(int feedbackID)
        {
            SQL mSQL = new SQL();

            SqlCommand mCommand = new SqlCommand("deleteParentFeedback");
            mCommand.CommandType = CommandType.StoredProcedure;

            mCommand.Parameters.AddWithValue("@feedbackID", feedbackID);

            mSQL.deleteQuery(mCommand);

            return;
        }

        //uses specific query to select all rooms from database
        public DataTable getAllRooms()
        {
            string query = "select * from Room";
            return getRoom(query);
        }

        //uses specific query to select room by ID from database
        public DataTable getRoomByID(int id)
        {
            string query = "select * from Room where roomID = " + Convert.ToString(id);
            return getRoom(query);
        }

        //uses specific query to select room by staff member's ID from database
        public DataTable getRoomByStaffID(Int64 id)
        {
            string query = "select * from Room where roomStaffID = " + Convert.ToString(id);
            return getRoom(query);
        }

        /**************** Get Child Daily Details  ****************/
        public string  getChildDailyDetails(DateTime detailsDate, Int64 childID)
        {
            DataTable dt = new DataTable();
            SQL mSQL = new SQL();
            string childDetails;

            string query = "select childDetails from childDailyDetails where detailsDate = " + detailsDate + " and childID = " + Convert.ToString(childID);
            dt = mSQL.retrieveQuery(query);
            childDetails = dt.Rows[0]["childDetails"].ToString();

            return childDetails;
        }



        /****************  UPDATING DATA FROM DATABASE  ****************/

        public void updateChildData(Child child)
        {
            SQL mSQL = new SQL();
            SqlCommand mCommand = new SqlCommand("updateChildData");
            mCommand.CommandType = CommandType.StoredProcedure;

            mCommand.Parameters.AddWithValue("@childID", child.id);
            mCommand.Parameters.AddWithValue("@childName", child.firstName);

            if (child.image == null)
            {
                SqlParameter imageParameter = new SqlParameter("@picture", SqlDbType.Image);
                imageParameter.Value = DBNull.Value;
                mCommand.Parameters.Add(imageParameter);
            }
            else
            {
                mCommand.Parameters.AddWithValue("@picture" , child.image);
            }
            mCommand.Parameters.AddWithValue("@parentID", child.parentID);
            mCommand.Parameters.AddWithValue("@DOB", child.DOB);
            mCommand.Parameters.AddWithValue("@gender", child.gender);
            if(child.roomID == -1)
            {
                mCommand.Parameters.AddWithValue("@roomID", DBNull.Value);
            }
            else
            {
                mCommand.Parameters.AddWithValue("@roomID", child.roomID);
            }
            mCommand.Parameters.AddWithValue("@childPending", child.pending);

            mSQL.updateQuery(mCommand);

            return;
        }

        public void updateParentData(Parent parent)
        {
            SQL mSQL = new SQL();
            SqlCommand mCommand = new SqlCommand("updateParentData");
            mCommand.CommandType = CommandType.StoredProcedure;

            mCommand.Parameters.AddWithValue("@parentID", parent.id);
            mCommand.Parameters.AddWithValue("@parentFirstName", parent.firstName);
            mCommand.Parameters.AddWithValue("@parentLastName", parent.lastName);
            mCommand.Parameters.AddWithValue("@parentAddress", parent.address);
            mCommand.Parameters.AddWithValue("@parentPhoneNumber", parent.phoneNumber);
            mCommand.Parameters.AddWithValue("@parentCreditCard", parent.creditCard);
            mCommand.Parameters.AddWithValue("@parentEmail", parent.email);
            mCommand.Parameters.AddWithValue("@parentPending", parent.pending);

            mSQL.updateQuery(mCommand);

            return;
        }

        public void updateStaffData(Staff staff)
        {
            SQL mSQL = new SQL();
            SqlCommand mCommand = new SqlCommand("updateStaffData");
            mCommand.CommandType = CommandType.StoredProcedure;

            mCommand.Parameters.AddWithValue("@staffID", staff.id);
            mCommand.Parameters.AddWithValue("@staffFirstName", staff.firstName);
            mCommand.Parameters.AddWithValue("@staffLasttName", staff.lastName);
            mCommand.Parameters.AddWithValue("@staffPhoneNumber", staff.phoneNumber);
            mCommand.Parameters.AddWithValue("@staffEmail", staff.email);
            mCommand.Parameters.AddWithValue("@staffSalary", staff.salary);
            mCommand.Parameters.AddWithValue("@staffType", staff.type);
            mCommand.Parameters.AddWithValue("@staffPending", staff.pending);

            mSQL.updateQuery(mCommand);

            return;
        }

        public void updateRoomData(Room room)
        {
            SQL mSQL = new SQL();
            SqlCommand mCommand = new SqlCommand("updateRoomData");
            mCommand.CommandType = CommandType.StoredProcedure;

            mCommand.Parameters.AddWithValue("@roomNumber", room.number);
            if (room.staffID == -1)
                mCommand.Parameters.AddWithValue("@roomStaffID", DBNull.Value);
            else
                mCommand.Parameters.AddWithValue("@roomStaffID", room.staffID);
            mSQL.updateQuery(mCommand);

            return;
        }

        public void updateUsername(Int64 id , string type , string newUsername , string newPassword)
        {
            SQL mSQL = new SQL();

            DataTable table = new DataTable ();
            table = selectUsernameByIDAndType(id, type);

            SqlCommand mCommand = new SqlCommand("updateUsername");
            mCommand.CommandType = CommandType.StoredProcedure;

            if(type == "Staff" || type == "Admin")
            {
                mCommand.Parameters.AddWithValue("@staffID" , id);
                mCommand.Parameters.AddWithValue("@parentID", DBNull.Value);
            }
            else
            {
                mCommand.Parameters.AddWithValue("@parentID" , id);
                mCommand.Parameters.AddWithValue("@staffID", DBNull.Value);
            }
            mCommand.Parameters.AddWithValue("@type", table.Rows[0]["userType"]);
            mCommand.Parameters.AddWithValue("@newUsername", newUsername);
            mCommand.Parameters.AddWithValue("@newPassword", newPassword);
            
            mSQL.updateQuery(mCommand);
        }

        /****************  DELETING DATA FROM DATABASE  ****************/

        private void deleteUser(string query)
        {
            SQL mSql = new SQL();
            mSql.deleteQuery(query);
        }

        public void deleteFeature(int featureID)
        {
            SQL mSql = new SQL();
            string query = "delete from Feature where featureID = " + Convert.ToString(featureID);
            mSql.deleteQuery(query);
        }

        public void deleteChildDailyDetails(int childDailyDetailsID)
        {
            SQL mSql = new SQL();
            string query = "delete from childDailyDetails where childDetailsID = " + Convert.ToString(childDailyDetailsID);
            mSql.deleteQuery(query);
        }

        public void deleteChildFeature(int featureID)
        {
            SQL mSql = new SQL();
            string query = "delete from Child_Feature where featureID = " +Convert. ToString(featureID);
            mSql.deleteQuery(query);
        }

        public void deleteChildData(LinkedList<int> childIDs)
        {
            string query = "delete from Child where childID in(" + string.Join(",", childIDs) + ")";
            deleteUser(query);
        }

        public void deleteParentData(LinkedList<Int64> parentIDs)
        {
            string query = "delete from Parent where parentID in(" + string.Join(",", parentIDs) + ")";
            deleteUser(query);
        }

        public void deleteStaffData(LinkedList<Int64> staffIDs)
        {
            string query = "delete from Staff where staffID in(" + string.Join(",", staffIDs) + ")";
            deleteUser(query);
        }

        public void deleteRoomData(LinkedList<int> roomIDs)
        {
            string query = "delete from Room where roomID in(" + string.Join(",", roomIDs) + ")";
            deleteUser(query);
        }      
     }
}
