using MySqlConnector;

namespace DataBaseTesting.Tests
{
   
    public class BaseTest
    {
        public MySqlConnection conn;
        public MySqlCommand cmd = null;
        public MySqlDataReader reader = null;
        public List<Dictionary<string, string>> SP_Table = new List<Dictionary<string, string>>();
        public List<Dictionary<string, string>> NQ_Table = new List<Dictionary<string, string>>();

        [SetUp]
        public void Setup()
        {
            openMySqlConnection();
        }

        [TearDown]
        public void teardowm()
        {
            Console.WriteLine("Run Teardown method");
            CloseAndDispose_MySql();
        }

        public void CloseAndDispose_MySql()
        {
            Console.WriteLine("Close MySqlConnection and dispose");
            cmd.Dispose();
            reader?.Close();
            conn.Close();
        }

        public bool CompareResultSet(List<Dictionary<string, string>> list1, List<Dictionary<string, string>> list2)
        {
            Console.WriteLine("Compare Data dictionary of two tables");
            if (list1.Count.Equals(list2.Count))
            {
                for (int i = 0; i < list1.Count; i++)
                {
                    //Console.WriteLine($"Compare each row of two tables, now Row {i + 1}");
                    List<string> listValues1 = list1[i].Values.ToList();
                    List<string> listValues2 = list2[i].Values.ToList();

                    if (!listValues1.Equals(listValues1))
                    {
                        return false;
                    }
                }
            }
            else
            {
                return false;
            }

            return true;
        }

        public void openMySqlConnection()
        {
            Console.WriteLine("Open MySqlConnection object");
            string connectionString = "Server=localhost;Database=classicmodels;Uid=root;Pwd=root;";
            conn = new MySqlConnection(connectionString);
            conn.Open();
        }

        public void WriteResultSetToA_Dictionary(MySqlDataReader resultReader, ref List<Dictionary<string, string>> list)
         {
             list.Clear();
             while (resultReader.Read())
             {
                 var dict = new Dictionary<string, string>();

                 for (int i = 0; i < resultReader.FieldCount; i++)
                 {
                     string name = resultReader.GetName(i);
                     string value = resultReader.GetValue(i).ToString();
                     dict[name] = value;
                 }
                 list.Add((Dictionary<string, string>) dict);
             }
         }
         

    }
}
