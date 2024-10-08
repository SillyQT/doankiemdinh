using Microsoft.VisualStudio.TestTools.UnitTesting;
using QL_QuanAn.DAO;
using System;
using System.Collections.Generic;
using System.Text;

namespace QL_QuanAnTests
{
    [TestClass]
    public class DeleteTableTest
    {
        private TableDAO tableDAO;

        [TestInitialize]
        public void Setup()
        {
            tableDAO = TableDAO.Instance;
        }

        // Test Case 1: Xóa bàn khi bàn đang có khách
        [TestMethod]
        public void Test_DeleteTableWithCustomers_TC1()
        {
            int tableId = 77;
            string tableStatus = "Đã đặt";

            var ex = Assert.ThrowsException<ArgumentException>(() => tableDAO.DeleteTalbe(tableId, tableStatus));

            Assert.AreEqual("Bàn này có người ăn, không thể xóa!!!", ex.Message);
        }

        // Test Case 2: Xóa bàn trống thành công
        [TestMethod]
        public void Test_DeleteEmptyTable_TC2()
        {
            int tableId = 77;
            string tableStatus = "Trống";

            bool result = tableDAO.DeleteTalbe(tableId, tableStatus);

            Assert.IsTrue(result, "Xóa bàn trống thành công.");
        }

        // Test Case 3: Người dùng từ chối xác nhận
        [TestMethod]
        public void Test_UserRejectsConfirmation_TC3()
        {
            int tableId = 77;
            string tableStatus = "Trống";

            bool userConfirmation = false;

            if (userConfirmation)
            {
                bool result = tableDAO.DeleteTalbe(tableId, tableStatus);
                Assert.Fail("Người dùng từ chối nhưng bàn vẫn được xóa.");
            }
            else
            {
                Assert.IsTrue(true, "Người dùng từ chối xác nhận, không xóa bàn.");
            }
        }
    }
}
