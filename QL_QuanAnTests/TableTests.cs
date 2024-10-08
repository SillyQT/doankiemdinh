using Microsoft.VisualStudio.TestTools.UnitTesting;
using QL_QuanAn;
using QL_QuanAn.DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QL_QuanAn
{
    [TestClass()]
    public class TableTests
    {
        private TableDAO tableDAO;

        [TestInitialize]
        public void Setup()
        {
            tableDAO = TableDAO.Instance;
        }

        [TestMethod]
        public void Test_AddValidTable_TC1()
        {
            string tableName = "Bàn 1001";
            int areaId = 1;
            int quantity = 4;
            string status = "Trống";

            bool result = tableDAO.InsertTable(areaId, tableName, quantity, status);

            Assert.IsTrue(result, "Thêm bàn thành công.");
        }

        [TestMethod]
        public void Test_AddTableWithSpecialCharacters_TC2()
        {
            string tableName = "Bàn@!";
            int areaId = 1;
            int quantity = 4;
            string status = "Trống";

            var ex = Assert.ThrowsException<ArgumentException>(() =>
                tableDAO.InsertTable(areaId, tableName, quantity, status));

            Assert.AreEqual("Tên bàn không được chứa ký tự đặc biệt.", ex.Message);
        }

        [TestMethod]
        public void Test_AddTableWithEmptyName_TC3()
        {
            string tableName = "";
            int areaId = 1;
            int quantity = 4;
            string status = "Trống";

            var ex = Assert.ThrowsException<ArgumentException>(() =>
                tableDAO.InsertTable(areaId, tableName, quantity, status));

            Assert.AreEqual("Tên bàn không được để trống.", ex.Message);
        }

        [TestMethod]
        public void Test_AddTableWithInvalidNameLength_TC4()
        {
            string shortName = "B";
            int areaId = 1;
            int quantity = 4;
            string status = "Trống";

            var exShortName = Assert.ThrowsException<ArgumentException>(() =>
                tableDAO.InsertTable(areaId, shortName, quantity, status));

            Assert.AreEqual("Tên bàn phải có độ dài từ 2 đến 20 ký tự.", exShortName.Message);

            string longName = "Bận7890098765432112345678";

            var exLongName = Assert.ThrowsException<ArgumentException>(() =>
                tableDAO.InsertTable(areaId, longName, quantity, status));

            Assert.AreEqual("Tên bàn phải có độ dài từ 2 đến 20 ký tự.", exLongName.Message);
        }
    }
}