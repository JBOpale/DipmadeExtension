using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using DIPMADE;
using System.Data.SqlClient;

namespace UnitTest_Extensions
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod_Password()
        {
            Debug.WriteLine(DIPMADE.Extensions.GeneratePassword(10));
            Thread.Sleep(100);
            Debug.WriteLine(DIPMADE.Extensions.GeneratePassword(8));
            Thread.Sleep(100);
            Debug.WriteLine(DIPMADE.Extensions.GeneratePassword(8));
            Thread.Sleep(100);
            Debug.WriteLine(DIPMADE.Extensions.GeneratePassword(10));
            Thread.Sleep(100);
            Debug.WriteLine(DIPMADE.Extensions.GeneratePassword(10, false, false));
            Thread.Sleep(100);
            Debug.WriteLine(DIPMADE.Extensions.GeneratePassword(10));
            /*
            0k9#jFguf9
            |7HrH*i2
            8?O364K-
            882D2=lQTm
            H4U9kya2up
            kv}D9=k+£-
             */
        }
        class T_LOGS
        {
            public int Id;
            public string Nom;
        }

        [TestMethod]
        public void TestMethod_ToDatableStruct()
        {
            List<Int64> lst = new List<long>();
            lst.Add(0);
            lst.Add(1);
            lst.Add(2);
            SqlParameter p = lst.ToDataTableS("[dbo].[Int64List]", "Id", "@Int64List");

            SqlConnection myConnectionOPQUAL = new SqlConnection(
               "Data Source=tcp:SERVEUR-SQL,1433;Initial Catalog=GIC;"
               + "User ID=sa;Password=***;");

            if (myConnectionOPQUAL.State != System.Data.ConnectionState.Open) myConnectionOPQUAL.Open();

            SqlCommand myCommand = null;
            try
            {
                if (myConnectionOPQUAL.State != System.Data.ConnectionState.Open) myConnectionOPQUAL.Open();

                //Execution de la requête
                myCommand = new SqlCommand("TestInt64List", myConnectionOPQUAL);
                myCommand.Parameters.Add(p);
                myCommand.CommandType = System.Data.CommandType.StoredProcedure;
                SqlDataReader reader = myCommand.ExecuteReader();
                while (reader.Read())
                {
                    Debug.WriteLine("Id : " + reader["Id"]);
                }
                reader.Close();

            }
            catch (Exception exp)
            {
                Debug.WriteLine(exp.Message);
            }
            finally
            {
                if (myCommand != null) myCommand.Dispose();
            }
        }
        [TestMethod]
        public void TestMethod_ToDatableStructDateTime()
        {
            List<DateTime> lst = new List<DateTime>();
            lst.Add(DateTime.Now.AddDays(1));
            lst.Add(DateTime.Now.AddDays(2));
            lst.Add(DateTime.Now.AddDays(3));
            SqlParameter p = lst.ToDataTableS("[dbo].[DateTimeList]","date", "@DateTimeList");

            SqlConnection myConnectionOPQUAL = new SqlConnection(
               "Data Source=tcp:SERVEUR-SQL,1433;Initial Catalog=GIC;"
               + "User ID=sa;Password=***;");

            if (myConnectionOPQUAL.State != System.Data.ConnectionState.Open) myConnectionOPQUAL.Open();

            SqlCommand myCommand = null;
            try
            {
                if (myConnectionOPQUAL.State != System.Data.ConnectionState.Open) myConnectionOPQUAL.Open();

                //Execution de la requête
                myCommand = new SqlCommand("TestTypeDateTime", myConnectionOPQUAL);
                myCommand.Parameters.Add(p);
                myCommand.CommandType = System.Data.CommandType.StoredProcedure;
                SqlDataReader reader = myCommand.ExecuteReader();
                while (reader.Read())
                {
                    Debug.WriteLine("date : " + reader["date"]);
                }
                reader.Close();

            }
            catch (Exception exp)
            {
                Debug.WriteLine(exp.Message);
            }
            finally
            {
                if (myCommand != null) myCommand.Dispose();
            }
        }
        [TestMethod]
        public void TestMethod_ToDatableSting()
        {
            List<string> lst = new List<string>();
            lst.Add(DateTime.Now + "123");
            lst.Add(DateTime.Now + "456");
            lst.Add(DateTime.Now + "789");
            SqlParameter p = lst.ToDataTableC("[dbo].[StringList]", "@StringList");

            SqlConnection myConnectionOPQUAL = new SqlConnection(
               "Data Source=tcp:SERVEUR-SQL,1433;Initial Catalog=GIC;"
               + "User ID=sa;Password=***;");

            if (myConnectionOPQUAL.State != System.Data.ConnectionState.Open) myConnectionOPQUAL.Open();

            SqlCommand myCommand = null;
            try
            {
                if (myConnectionOPQUAL.State != System.Data.ConnectionState.Open) myConnectionOPQUAL.Open();

                //Execution de la requête
                myCommand = new SqlCommand("TestTypeString", myConnectionOPQUAL);
                myCommand.Parameters.Add(p);
                myCommand.CommandType = System.Data.CommandType.StoredProcedure;
                SqlDataReader reader = myCommand.ExecuteReader();
                while (reader.Read())
                {
                    Debug.WriteLine("s : " + reader["s"]);
                }
                reader.Close();

            }
            catch (Exception exp)
            {
                Debug.WriteLine(exp.Message);
            }
            finally
            {
                if (myCommand != null) myCommand.Dispose();
            }
        }

        [TestMethod]
        public void TestMethod_ToDatableClass()
        {

            SqlConnection myConnection = new SqlConnection(
               "Data Source=tcp:SERVEUR-SQL,1433;Initial Catalog=GIC;"
               + "User ID=sa;Password=***;");

            if (myConnection.State != System.Data.ConnectionState.Open) myConnection.Open();

            SqlCommand myCommand = null;
            try
            {
                if (myConnection.State != System.Data.ConnectionState.Open) myConnection.Open();

                List<T_LOGS> lst = new List<T_LOGS>();
                lst.Add(new T_LOGS() { Id = 0, Nom = "s0" });
                lst.Add(new T_LOGS() { Id = 1, Nom = "s1" });
                lst.Add(new T_LOGS() { Id = 2, Nom = "s2" });
                SqlParameter p = lst.ToDataTableC("[dbo].[TypeList]", "@TypeList");
                //Execution de la requête
                myCommand = new SqlCommand("TestType", myConnection);
                myCommand.Parameters.Add(p);
                myCommand.CommandType = System.Data.CommandType.StoredProcedure;
                SqlDataReader reader =  myCommand.ExecuteReader();
                while(reader.Read())
                {
                    Debug.WriteLine("Id : " + reader["Id"]);
                    Debug.WriteLine("Nom : " + reader["Nom"]);
                }
                reader.Close();

            }
            catch (Exception exp)
            {
                Debug.WriteLine(exp.Message);
            }
            finally
            {
                if (myCommand != null) myCommand.Dispose();
            }
        }
    }
}
