using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using JBOpale;
#if NETFRAMEWORK
using System.Data.SqlClient;
#else
using Microsoft.Data.SqlClient;
#endif

namespace UnitTest_Extensions
{
    [TestClass]
    public class UnitTest1
    {
        /// <summary>
        /// Variable d'environnement contenant la chaîne de connexion de la base de test
        /// (tests d'intégration, ignorés si elle est absente).
        /// </summary>
        private const string ConnectionVariable = "JBOPALE_TEST_SQL";

        class T_LOGS
        {
            public int Id;
            public string Nom;
        }

        [TestMethod]
        public void TestMethod_Password()
        {
            string p = JBOpale.Extensions.GeneratePassword(10);
            Debug.WriteLine(p);
            Assert.AreEqual(10, p.Length);
            Assert.AreEqual(8, JBOpale.Extensions.GeneratePassword().Length);
        }

        [TestMethod]
        public void TestMethod_ToDatableStruct_Parametre()
        {
            List<Int64> lst = new List<long> { 0, 1, 2 };
            SqlParameter p = lst.ToDataTableS("[dbo].[Int64List]", "Id", "@Int64List");

            Assert.AreEqual("@Int64List", p.ParameterName);
            Assert.AreEqual(SqlDbType.Structured, p.SqlDbType);
            Assert.AreEqual("[dbo].[Int64List]", p.TypeName);
            DataTable table = (DataTable)p.Value;
            Assert.AreEqual(3, table.Rows.Count);
            Assert.AreEqual("Id", table.Columns[0].ColumnName);
        }

        [TestMethod]
        public void TestMethod_ToDatableString_Parametre()
        {
            List<string> lst = new List<string> { "a", "b" };
            SqlParameter p = lst.ToDataTableC("[dbo].[StringList]", "@StringList");

            Assert.AreEqual(SqlDbType.Structured, p.SqlDbType);
            Assert.AreEqual(2, ((DataTable)p.Value).Rows.Count);
        }

        [TestMethod]
        public void TestMethod_ToDatableClass_Parametre()
        {
            List<T_LOGS> lst = new List<T_LOGS>
            {
                new T_LOGS { Id = 0, Nom = "s0" },
                new T_LOGS { Id = 1, Nom = "s1" }
            };
            SqlParameter p = lst.ToDataTableC("[dbo].[TypeList]", "@TypeList");

            DataTable table = (DataTable)p.Value;
            Assert.AreEqual(2, table.Rows.Count);
            Assert.AreEqual("Id", table.Columns[0].ColumnName);
            Assert.AreEqual("Nom", table.Columns[1].ColumnName);
            Assert.AreEqual("s1", table.Rows[1]["Nom"]);
        }

        [TestMethod]
        public void TestMethod_ToDatableStruct_SqlServer()
        {
            Executer("TestInt64List", new List<Int64> { 0, 1, 2 }.ToDataTableS("[dbo].[Int64List]", "Id", "@Int64List"), "Id");
        }

        [TestMethod]
        public void TestMethod_ToDatableStructDateTime_SqlServer()
        {
            List<DateTime> lst = new List<DateTime> { DateTime.Now.AddDays(1), DateTime.Now.AddDays(2) };
            Executer("TestTypeDateTime", lst.ToDataTableS("[dbo].[DateTimeList]", "date", "@DateTimeList"), "date");
        }

        [TestMethod]
        public void TestMethod_ToDatableString_SqlServer()
        {
            List<string> lst = new List<string> { "123", "456" };
            Executer("TestTypeString", lst.ToDataTableC("[dbo].[StringList]", "@StringList"), "s");
        }

        [TestMethod]
        public void TestMethod_ToDatableClass_SqlServer()
        {
            List<T_LOGS> lst = new List<T_LOGS> { new T_LOGS { Id = 0, Nom = "s0" }, new T_LOGS { Id = 1, Nom = "s1" } };
            Executer("TestType", lst.ToDataTableC("[dbo].[TypeList]", "@TypeList"), "Id");
        }

        /// <summary>
        /// Exécute une procédure stockée de test sur la base désignée par la variable d'environnement.
        /// </summary>
        private static void Executer(string procedure, SqlParameter parametre, string colonne)
        {
            string connexion = Environment.GetEnvironmentVariable(ConnectionVariable);
            if (string.IsNullOrWhiteSpace(connexion))
                Assert.Inconclusive("Variable d'environnement " + ConnectionVariable + " absente : test d'intégration SQL Server ignoré.");

            using (SqlConnection cn = new SqlConnection(connexion))
            using (SqlCommand cmd = new SqlCommand(procedure, cn))
            {
                cn.Open();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(parametre);
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                        Debug.WriteLine(colonne + " : " + reader[colonne]);
                }
            }
        }
    }
}
