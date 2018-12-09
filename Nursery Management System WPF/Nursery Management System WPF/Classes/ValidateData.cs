using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nursery_Management_System_WPF
{
    class ValidateData
    {
        private bool checkMails(string mail)
        {
            if (mail.Length < 6)
                return false;
            int posOFdind = -1;
            for(int i=0; i<mail.Length; i++)
            {
                if(mail[i]=='@')
                {
                    posOFdind = i + 1;
                    break;
                }
            }
            int size = mail.Length;
            if (mail[size - 1] == 'm' && mail[size - 2] == 'o' && mail[size - 3] == 'c' && mail[size - 4] == '.' && size - 5 > posOFdind && posOFdind != -1 && posOFdind > 0)
                return true;
            return false;
        }
        private bool checkPhoneNum(string phoneNum)
        {

            if(phoneNum.Length!=11 || phoneNum[0]!='0' || phoneNum[1]!='1' || !(phoneNum[2]=='0' || phoneNum[2] == '1' || phoneNum[2] == '2' || phoneNum[2] == '5'))
            return false;
            for(int i=0; i< phoneNum.Length; i++)
            {
                if (!(phoneNum[i] >= '0' && phoneNum[i] <= '9'))
                    return false;
            
            }
            return true;
        }
        private bool checkNationalID(string ID)
        {
            for (int i = 0; i < ID.Length; i++)
            {
                if (!(ID[i] >= '0' && ID[i] <= '9'))
                    return false;

            }
            if (ID.Length != 11     || !(ID[0] == '2' || ID[0] == '3'))
                return false;
            return true;

        }
        private bool checkCreditCardt(string ID)
        {
            for (int i = 0; i < ID.Length; i++)
            {
                if (!(ID[i] >= '0' && ID[i] <= '9'))
                    return false;

            }
            if (ID.Length != 16)
                return false;
            return true;

        }
        public bool vaildDataForParent(string username, string email, string ID,string phoneNumber, string creditCard , int numberOfChildren , ref string headProblem , ref string problem)
        {
            SQLQuery mSQLQuery = new SQLQuery();
            if (username.Length == 0 || email.Length == 0 || ID.Length == 0 || phoneNumber.Length == 0 || creditCard.Length == 0)
            {
                headProblem = "Wrong in data";
                problem = "Please continue your data";
                return true;
            }
            else if (mSQLQuery.checkForUsername(username) == true)
            {
                headProblem = "Wrong Username or Password";
                problem = "Username already exists";
                return true;
            }
            else if (numberOfChildren == 0)
            {
                problem = "Parent should have at least one Child";
                headProblem = "No Children";
                return true;
            }
            else if (!checkMails(email))
            {
                problem = "Please Enter correct email";
                headProblem="Invaild email";
                return true;
            }
            else if (!checkNationalID(ID))
            {
                problem = "Please Enter correct ID";
                headProblem="Invaild ID";
                return true;
            }
            else if (!checkPhoneNum(phoneNumber))
            {
                problem = "Please Enter correct Phone Number";
                headProblem = "Invaild Phone Number";
                return true;
            }
            else if (!checkCreditCardt(creditCard))
            {
                problem = "Please Enter correct Credit Card";
                headProblem = "Invaild Credit Card";
                return true;
            }


            return false;
        }
        public bool vaildDataForParent(string firstName, string email, string ID, string phoneNumber, string creditCard,  ref string headProblem, ref string problem)
        {
            SQLQuery mSQLQuery = new SQLQuery();
            if(firstName.Length==0 || email.Length == 0 || ID.Length == 0 || phoneNumber.Length == 0 || creditCard.Length == 0  )
            {
                headProblem = "Wrong in data";
                problem = "Please continue your data";
                return true;
            }
            else if (mSQLQuery.checkForUsername(firstName) == true)
            {
                headProblem = "Wrong Username or Password";
                problem = "Username already exists";
                return true;
            }
            else if (!checkMails(email))
            {
                problem = "Please Enter correct email";
                headProblem = "Invaild email";
                return true;
            }
            else if (!checkNationalID(ID))
            {
                problem = "Please Enter correct ID";
                headProblem = "Invaild ID";
                return true;
            }
            else if (!checkPhoneNum(phoneNumber))
            {
                problem = "Please Enter correct Phone Number";
                headProblem = "Invaild Phone Number";
                return true;
            }
            else if (!checkCreditCardt(creditCard))
            {
                problem = "Please Enter correct Credit Card";
                headProblem = "Invaild Credit Card";
                return true;
            }
            return false;
        }
        public bool vaildDataForStaff(string firstName, string email, string ID, string phoneNumber, ref string headProblem, ref string problem)
        {
            SQLQuery mSQLQuery = new SQLQuery();
            if (firstName.Length == 0 || email.Length == 0 || ID.Length == 0 || phoneNumber.Length == 0 )
            {
                headProblem = "Wrong in data";
                problem = "Please continue your data";
                return true;
            }
            else if (mSQLQuery.checkForUsername(firstName) == true)
            {
                headProblem = "Wrong Username or Password";
                problem = "Username already exists";
                return true;
            }
            else if (!checkMails(email))
            {
                problem = "Please Enter correct email";
                headProblem = "Invaild email";
                return true;
            }
            else if (!checkNationalID(ID))
            {
                problem = "Please Enter correct ID";
                headProblem = "Invaild ID";
                return true;
            }
            else if (!checkPhoneNum(phoneNumber))
            {
                problem = "Please Enter correct Phone Number";
                headProblem = "Invaild Phone Number";
                return true;
            }
            
            return false;
        }

    }
}
