using MySqlConnector;
using System;
using System.Data;
using System.Data.OleDb;
using System.Globalization;
using System.Xml.Linq;
namespace DataBaseTesting.Tests
{
    public class Tests :BaseTest
    {
        [Test,Ignore("Not Priority")]
        public void Test_CheckStoredProcedure_Exists()
        {
            TestContext.Out.WriteLine("Run show procedure status to see if SelectAllCustomers exists");
            string query = "show procedure status where name ='SelectAllCustomers'";
            cmd = new MySqlCommand(query, conn);
            reader = cmd.ExecuteReader();

            TestContext.Out.WriteLine("Compare result set value with \"SelectAllCustomers\"");
            while (reader.Read())
            {
                Assert.That(reader.GetString("Name"), Is.EqualTo("SelectAllCustomers"));
            }
        }

        [Test]
        public void Test_SelectAllCustomers()
        {
            TestContext.Out.WriteLine("Run SelectAllCustomers procedure");
            cmd = new MySqlCommand("SelectAllCustomers", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            reader = cmd.ExecuteReader();

            WriteResultSetToA_Dictionary(reader, ref SP_Table);
            cmd.Dispose();
            reader.Close();
            conn.Close();

            TestContext.Out.WriteLine("Run \"Select * from customers\"");
            openMySqlConnection();
            cmd = new MySqlCommand("Select * from customers", conn);
            MySqlDataReader results2 = cmd.ExecuteReader();
            WriteResultSetToA_Dictionary(results2, ref NQ_Table);

            TestContext.Out.WriteLine("Compare to Two results sets");
            CompareResultSet(SP_Table, NQ_Table);
            Assert.That(CompareResultSet(SP_Table, NQ_Table), Is.True);
        }

        [Test]
        public void SelectAllCustomersByCity()
        {
            TestContext.Out.WriteLine("Run SelectAllCustomersByCity procedure");
            cmd = new MySqlCommand("SelectAllCustomersByCity", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            // Add parameter
            cmd.Parameters.AddWithValue("@mycity", "singapore");
            reader = cmd.ExecuteReader();

            WriteResultSetToA_Dictionary(reader, ref SP_Table);
            cmd.Dispose();
            reader.Close();
            conn.Close();


            TestContext.Out.WriteLine("Run \"Select * from customers\"");
            openMySqlConnection();
            cmd = new MySqlCommand("select * from customers where city='singapore'", conn);
            MySqlDataReader results2 = cmd.ExecuteReader();
            WriteResultSetToA_Dictionary(results2, ref NQ_Table);

            TestContext.Out.WriteLine("Compare to Two results sets");
            CompareResultSet(SP_Table, NQ_Table);
            Assert.That(CompareResultSet(SP_Table, NQ_Table), Is.True);
        }
        [Test]
        public void SelectAllCustomersByCityAndPostalCode()
        {
            TestContext.Out.WriteLine("Run SelectAllCustomersByCityAndPostalCode('singapore','079903') procedure");
            cmd = new MySqlCommand("SelectAllCustomersByCityAndPostalCode", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            // Add parameter
            cmd.Parameters.AddWithValue("@mycity", "singapore");
            cmd.Parameters.AddWithValue("@pcode", "079903");
            reader = cmd.ExecuteReader();

            WriteResultSetToA_Dictionary(reader, ref SP_Table);
            CloseAndDispose_MySql();


            TestContext.Out.WriteLine("Run \"Select * from customers where city='singapore' and postalCode ='079903'\"");
            openMySqlConnection();
            cmd = new MySqlCommand("Select * from customers where city='singapore' and postalCode ='079903'", conn);
            MySqlDataReader results2 = cmd.ExecuteReader();
            WriteResultSetToA_Dictionary(results2, ref NQ_Table);

            TestContext.Out.WriteLine("Compare to Two results sets");
            CompareResultSet(SP_Table, NQ_Table);
            Assert.That(CompareResultSet(SP_Table, NQ_Table), Is.True);
        }


        [Test]
        public void Test_get_Order_by_CustNo()
        {
            TestContext.Out.WriteLine("Run get_Order_by_CustNo('141',@shipped,@Cancelled,@Resolved,@Desputed); procedure");
            cmd = new MySqlCommand("get_Order_by_CustNo", conn);
            cmd.CommandType= CommandType.StoredProcedure;

            //set Input Parameter
            TestContext.Out.WriteLine("set Input Parameter");
            cmd.Parameters.AddWithValue("@custNo", 141);

            //Create output parameters and set Direction
            TestContext.Out.WriteLine("Create output parameters and set Direction");
            MySqlParameter shipped = new MySqlParameter("@shipped",MySqlDbType.Int32);
            shipped.Direction = ParameterDirection.Output;

            MySqlParameter cancelled = new MySqlParameter("@Cancelled", MySqlDbType.Int32);
            cancelled.Direction = ParameterDirection.Output;

            MySqlParameter resolved = new MySqlParameter("@Resolved", MySqlDbType.Int32);
            resolved.Direction = ParameterDirection.Output;
            MySqlParameter desputed = new MySqlParameter("@Desputed", MySqlDbType.Int32);
            desputed.Direction = ParameterDirection.Output;

            //Add parameters to the command object
            TestContext.Out.WriteLine("Add parameters to the command object");
            cmd.Parameters.Add(shipped);
            cmd.Parameters.Add(cancelled);
            cmd.Parameters.Add(resolved);
            cmd.Parameters.Add(desputed);

            //Execute procedure
            cmd.ExecuteNonQuery();

            int shippedTot = Convert.ToInt32(cmd.Parameters["@shipped"].Value);
            int cancelledTot = Convert.ToInt32(cmd.Parameters["@Cancelled"].Value);
            int resolvedTot = Convert.ToInt32(cmd.Parameters["@Resolved"].Value);
            int desputedTot = Convert.ToInt32(cmd.Parameters["@Desputed"].Value);
            CloseAndDispose_MySql();

            openMySqlConnection();
            TestContext.Out.WriteLine("Run verification query \"A select statement\"");
            string sQuery = "select (Select Count(*) from orders where status ='shipped' and customerNumber ='141') as Shipped," +
                " (Select Count(*) from orders where status ='Cancelled' and customerNumber ='141') as Cancelled," +
                " (Select Count(*) from orders where status ='Resolved' and customerNumber ='141') as Resolved, " +
                " (Select Count(*) from orders where status ='Resolved' and customerNumber ='141') as Desputed";
           
            cmd = new MySqlCommand(sQuery,conn);
            reader = cmd.ExecuteReader();
            reader.Read();
            TestContext.Out.WriteLine("Run comparison");
            Assert.Multiple(() => {
            Assert.That(shippedTot, Is.EqualTo(reader.GetValue(0)));
            Assert.That(cancelledTot, Is.EqualTo(reader.GetValue(1)));
            Assert.That(resolvedTot, Is.EqualTo(reader.GetValue(2)));
            Assert.That(desputedTot, Is.EqualTo(reader.GetValue(3)));
            });
        }

        [TestCase(112)]
        [TestCase(260)]
        [TestCase(353)]
        public void Test_Shipping_Days(int custId)
        {
            TestContext.Out.WriteLine($"Run  GetCustomerShipping('{custId}',@Shipping) stored procedure");
            cmd = new MySqlCommand("GetCustomerShipping",conn);
            cmd.CommandType = CommandType.StoredProcedure;
            //"Add input parameter
            TestContext.Out.WriteLine("Add input parameter \"customerNumber\"");
            cmd.Parameters.AddWithValue("@custNumber", custId);

            //Create output parameters and set Direction
            TestContext.Out.WriteLine("Create output parameters and set Direction");
            MySqlParameter param_shippingDays = new MySqlParameter("@pShipping", MySqlDbType.VarChar);
            param_shippingDays.Direction = ParameterDirection.Output;

            //add output parameters to mySQLcommand
            TestContext.Out.WriteLine("Create output parameters and set Direction");
            cmd.Parameters.Add(param_shippingDays);

            //cmd ExecutenonQuery
            cmd.ExecuteNonQuery();

            TestContext.Out.WriteLine("Get output parameter");
            string shippingDaysValue = cmd.Parameters["@pShipping"].Value.ToString();

            CloseAndDispose_MySql();


            TestContext.Out.WriteLine("Prepare to run standard \"Select\"");
            openMySqlConnection();
            string sQuery = $"Select country ,\r\n case country\r\n  when 'usa' then '2-day shipping'\r\n  when 'canada' then '3-day shipping'\r\n  else '5-day shipping'\r\n end as shippingTime\r\nfrom customers \r\nwhere customerNumber = '{custId}'";
            cmd = new MySqlCommand(sQuery,conn);
            reader = cmd.ExecuteReader();

            TestContext.Out.WriteLine("Run comparison");
            reader.Read();
            Assert.That(shippingDaysValue, Does.Contain(reader.GetValue("shippingTime").ToString()).IgnoreCase);
        }
    }
}
