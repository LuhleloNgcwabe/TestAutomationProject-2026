using MySqlConnector;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Reflection.PortableExecutable;
using System.Text;

namespace DataBaseTesting.Utilities
{
    public static class SQLHelper
    {
        private static MySqlConnection conn;
        private static MySqlCommand cmd = null;
        private static MySqlDataReader QueryResult = null;
        private static List<Dictionary<string, string>> DB_Records = new List<Dictionary<string, string>>();
        

        /// <summary>
        /// Opens connection to the database and execute SQL Query
        /// </summary> 
        /// <param name="sQuery"> Fully constructed query to execute on the database </param>
        /// <returns>Returns object of OleDbDataReader </returns>
        public static void ExecuteQuery(string sQuery, CommandType CommandType = CommandType.StoredProcedure)
        {
            try
            {
                openMySqlConnection();
                cmd = new MySqlCommand(sQuery, conn);
                if (CommandType != CommandType.StoredProcedure)
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                }
                QueryResult = cmd.ExecuteReader();
                ReadToDictionary();
            }
            catch (Exception exc)
            {
                CreateTextFileForQueryException(sQuery, exc);
            }
            finally
            {
                //closeConnection();
            }
            
        }

        /// <summary>
        /// Get database records as a list of dictionaries
        /// </summary>
        /// <param name="selectQuery"> fully constructed query to select record from DataBase</param>
        /// <returns>Returns a list of database records as a list of dictionary objects</returns>
        public static List<Dictionary<string, string>> GetListOfDatabaseRecord()
        {
            return DB_Records;
        }

        /// <summary>
        /// Get database record as a dictionaries record
        /// </summary>
        /// <param name="selectQuery">fully constructed query to select record from DataBase</param>
        /// <returns>Returns a single database record as a dictionary object</returns>
        public static Dictionary<string, string> GetDatabaseRecord()
        {
            //ReadToDictionary(selectQuery);
            if (DB_Records.Count == 0)
                throw new IndexOutOfRangeException("No database record found.");
            return DB_Records[0];
        }

        public static MySqlDataReader GetMySqlDataReader()
        {
            return QueryResult;
        }

        /// <summary>
        /// Query Database and store returned records to as  Dictionary variable
        /// </summary>
        /// <param name="selectQuery"> sql query to get DB records</param>
        private static void ReadToDictionary()
        {
            //QueryResult = ExecuteQuery(selectQuery);
            while (QueryResult.Read())
            {
                DB_Records.Clear();
                var dict = new Dictionary<string, string>();

                for (int i = 0; i < QueryResult.FieldCount; i++)
                {
                    string name = QueryResult.GetName(i);
                    string value = QueryResult.GetValue(i).ToString();
                    dict[name] = value;
                }
                DB_Records.Add((Dictionary<string, string>)dict);
            }
            QueryResult.Close();
        }

        private static void openMySqlConnection()
        {
            string connectionString = "Server=localhost;Database=classicmodels;Uid=root;Pwd=root;";
            conn = new MySqlConnection(connectionString);
            conn.Open();
        }
        private static void CreateTextFileForQueryException(string sQuery, Exception exc)
        {
            string txtFilename = "SQLErrors.txt";
            string file = Environment.CurrentDirectory + @"\" + txtFilename;
            File.Delete(@file);
            DateTime date1 = DateTime.Now;
            string timestamp = "[" + date1.ToString("yyyy-MM-dd HH:mm:ss.ffff") + "] ";
            string text = timestamp + "SQL query exception is thrown : " + exc.Message + "\n\r" + sQuery;
            text = text + "\n\rTRace: " + exc.StackTrace;
            File.WriteAllText(@file, text);
        }

        public static void CloseConnection()
        {
            if (conn != null && QueryResult!=null && conn!=null)
            {
                cmd.Dispose();
                QueryResult.Close();
                conn.Close();
            }
        }
    }
}
