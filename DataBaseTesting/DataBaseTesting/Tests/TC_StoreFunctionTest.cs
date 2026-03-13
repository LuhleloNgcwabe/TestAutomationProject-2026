using MySqlConnector;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace DataBaseTesting.Tests
{
    public class TC_StoreFunctionTest:BaseTest
    {
        [Test]
        public void Test_StoredFunction_CustomerLevelExists()
        {
            TestContext.Out.WriteLine("Run show function status where name ='Customerlevel'");
            cmd = new MySqlCommand("show function status where name ='Customerlevel'", conn);
            reader = cmd.ExecuteReader();
            TestContext.Out.WriteLine("Run Comparison'");
            reader.Read();
            Assert.That(reader.GetValue("Name").ToString(), Is.EqualTo("CustomerLevel"));
        }

        [Test]
        public void Test_Checkstoredfunction_customerLevel_returnsValue()
        {
            TestContext.Out.WriteLine("Run select customerName, CustomerLevel(creditLimit) as CustomerLevel from customers");
            cmd = new MySqlCommand("select customerName, CustomerLevel(creditLimit) as CustomerLevel from customers;", conn);
            //cmd.CommandType = CommandType.StoredProcedure;
            reader = cmd.ExecuteReader();
            WriteResultSetToA_Dictionary(reader, ref SP_Table);
            CloseAndDispose_MySql();

            TestContext.Out.WriteLine("Run a test \"select Query\"");
            openMySqlConnection();
            string sQuery = "select customerName, \r\n\tcase\r\n\t\twhen creditLimit >= 50000 then 'Platinum'\r\n\t\twhen creditLimit >= 10000 AND creditLimit <= 50000 then 'Gold'\r\n        when creditLimit < 10000 then 'Silver'\r\n\tend as CustomerLevel from customers";
            cmd = new MySqlCommand(sQuery, conn);
            reader = cmd.ExecuteReader();
            WriteResultSetToA_Dictionary(reader, ref NQ_Table);

            TestContext.Out.WriteLine("Run Comparison");
            CompareResultSet(SP_Table, NQ_Table);
        }

        [Test]
        public void TestCustomerLevel_withStoredProcedure()
        {
            TestContext.Out.WriteLine(" Run store procedure \"GetCustomerLevel(112,@CustomerLevel)\"");
            cmd = new MySqlCommand("GetCustomerLevel", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@customerNo", 112);

            //Create output parameters and set Direction
            TestContext.Out.WriteLine("Create output parameters and set Direction");
            MySqlParameter customerLevel = new MySqlParameter("@customerLevel", MySqlDbType.VarChar);
            customerLevel.Direction = ParameterDirection.Output;

            //add output parameters to mySQLcommand
            TestContext.Out.WriteLine("Create output parameters and set Direction");
            cmd.Parameters.Add(customerLevel);

            //Execute query
            cmd.ExecuteNonQuery();
            string custLevel = cmd.Parameters["@CustomerLevel"].Value?.ToString();
            if (custLevel == null)
            {
                custLevel = "";
            }
            CloseAndDispose_MySql() ;

            //TestContext.Out.WriteLine("" + cmd.Parameters["@customerLevel"].Value.ToString());
            TestContext.Out.WriteLine("Run select \"Test Query\"");
            string sQuery = "select customerName, \r\n\tcase\r\n\t\t" +
                "when creditLimit >= 50000 then 'Platinum'\r\n\t\t" +
                "when creditLimit >= 10000 AND creditLimit <= 50000 then 'Gold'\r\n" +
                "when creditLimit < 10000 then 'Silver'\r\n\tend as CustomerLevel from customers\r\n" +
                "where customerNumber= '112'";
            openMySqlConnection();
            cmd = new MySqlCommand(sQuery, conn);
            reader = cmd.ExecuteReader();
            reader.Read();
            Assert.That(custLevel, Is.EqualTo(reader.GetValue("CustomerLevel").ToString()));

        }
    }
}
