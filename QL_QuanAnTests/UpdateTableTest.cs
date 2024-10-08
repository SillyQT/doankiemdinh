using Microsoft.VisualStudio.TestTools.UnitTesting;
using QL_QuanAn.DAO;
using System;
using System.Collections.Generic;
using System.Text;

namespace QL_QuanAnTests
{
    [TestClass()]
    public class UpdateTableTests
    {
        private TableDAO tableDAO;

        [TestInitialize]
        public void Setup()
        {
            tableDAO = TableDAO.Instance;
        }

        [TestMethod]
        public void Test_UpdateValidTable_TC1()
        {
            int tableId = 77;
            string tableName = "Bàn 22092024";
            int areaId = 1;
            int quantity = 4;
            string status = "Trống";

            bool result = tableDAO.UpdateTable(tableId, areaId, tableName, quantity, status);

            Assert.IsTrue(result, "Cập nhật bàn thành công.");
        }

        [TestMethod]
        public void Test_UpdateTableWithSpecialCharacters_TC2()
        {
            int tableId = 1;
            string tableName = "Bàn@!";
            int areaId = 1;
            int quantity = 4;
            string status = "Trống";

            var ex = Assert.ThrowsException<ArgumentException>(() =>
                tableDAO.UpdateTable(tableId, areaId, tableName, quantity, status));

            Assert.AreEqual("Tên bàn không được chứa ký tự đặc biệt!", ex.Message);
        }

        [TestMethod]
        public void Test_UpdateTableWithEmptyName_TC3()
        {
            int tableId = 1;
            string tableName = "";
            int areaId = 1;
            int quantity = 4;
            string status = "Trống";

            var ex = Assert.ThrowsException<ArgumentException>(() =>
                tableDAO.UpdateTable(tableId, areaId, tableName, quantity, status));

            Assert.AreEqual("Tên bàn không được để trống!", ex.Message);
        }

        [TestMethod]
        public void Test_UpdateTableWithInvalidNameLength_TC4()
        {
            int tableId = 1;
            string shortName = "B";
            int areaId = 1;
            int quantity = 4;
            string status = "Trống";

            var exShortName = Assert.ThrowsException<ArgumentException>(() =>
                tableDAO.UpdateTable(tableId, areaId, shortName, quantity, status));

            Assert.AreEqual("Tên bàn phải có độ dài từ 2 đến 20 ký tự!", exShortName.Message);

            string longName = "Bàn1234567890123456789012345678901";

            var exLongName = Assert.ThrowsException<ArgumentException>(() =>
                tableDAO.UpdateTable(tableId, areaId, longName, quantity, status));

            Assert.AreEqual("Tên bàn phải có độ dài từ 2 đến 20 ký tự!", exLongName.Message);
        }

        [TestMethod]
        public void Test_UpdateTableWithInvalidStatus_TC5()
        {
            int tableId = 1;
            string tableName = "Bàn 2";
            int areaId = 1;
            int quantity = 4;
            string status = "Không hợp lệ"; // Trạng thái không hợp lệ

            var ex = Assert.ThrowsException<ArgumentException>(() =>
                tableDAO.UpdateTable(tableId, areaId, tableName, quantity, status));

            Assert.AreEqual("Trạng thái bàn không hợp lệ!", ex.Message);
        }
    }
}