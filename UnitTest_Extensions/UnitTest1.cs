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
    }
}
