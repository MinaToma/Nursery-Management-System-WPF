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
using System.Data;
using System.Data.SqlClient;

namespace Nursery_Management_System_WPF
{
    class SQL
    {
        private SqlConnection mConnection;
        public SqlCommand mCommand; 
        private SqlDataAdapter mAdapter;

        public SQL()
        {
            connectionString st = new connectionString();
            mConnection = new SqlConnection(@st.serverName);
        }

        public DataTable retrieveQuery(string query)
        {
            DataTable mDataTable = new DataTable();
            /*try
            {*/
                mCommand = new SqlCommand(query, mConnection);
                mConnection.Open();
                mAdapter = new SqlDataAdapter(mCommand);
                mAdapter.Fill(mDataTable);
            /*}
            catch
            {
                MessageBox.Show("There was an error while connecting to data base , please check your connection and try again", "Sql Connection Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            finally
            {*/
                mConnection.Close();
                mAdapter.Dispose();
            //}
            
            return mDataTable;
        }


        public DataTable retrieveQuery(SqlCommand command)
        {
            DataTable mDataTable = new DataTable();
            /*try
            {*/
            mCommand = command;
            mConnection.Open();
            mAdapter = new SqlDataAdapter(mCommand);
            mAdapter.Fill(mDataTable);
            /*}
            catch
            {
                MessageBox.Show("There was an error while connecting to data base , please check your connection and try again", "Sql Connection Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            finally
            {*/
            mConnection.Close();
            mAdapter.Dispose();
            //}

            return mDataTable;
        }
        public bool insertQuery(SqlCommand command)
        {
            mCommand = command;
            mCommand.Connection = mConnection;
           /* try
            {*/
                mConnection.Open();
                mCommand.ExecuteNonQuery();
            /*}
            catch
            {
               MessageBox.Show("There was an error while connecting to data base , please check your connection and try again", "Sql Connection Error", MessageBoxButton.OK, MessageBoxImage.Error);

                return false;
            }
            finally
            {*/
                mConnection.Close();
            //}
            return true;
        }

        public void updateQuery(SqlCommand command)
        {
            mCommand = command;
            mCommand.Connection = mConnection;
            /*try
            {*/
                mConnection.Open();
                mCommand.ExecuteNonQuery();
            /*}
            catch
            {

                MessageBox.Show("There was an error while connecting to data base , please check your connection and try again", "Sql Connection Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            finally
            {*/
                mConnection.Close();
            //}
            return;
        }

        public void deleteQuery(string query)
        {
            /*try
            {*/
                mCommand = new SqlCommand(query, mConnection);
                mConnection.Open();
                mCommand.ExecuteNonQuery();
            /*}
            catch
            {
                MessageBox.Show("There was an error while connecting to data base , please check your connection and try again", "Sql Connection Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            finally
            {*/
                mConnection.Close();
            //}

            return;
        }

        public void deleteQuery(SqlCommand command)
        {
            /*try
            {*/
            mCommand = command;
            mConnection.Open();
            mCommand.ExecuteNonQuery();
            /*}
            catch
            {
                MessageBox.Show("There was an error while connecting to data base , please check your connection and try again", "Sql Connection Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            finally
            {*/
            mConnection.Close();
            //}

            return;
        }
    }
}
