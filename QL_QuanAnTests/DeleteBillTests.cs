using Microsoft.VisualStudio.TestTools.UnitTesting;
using QL_QuanAn.DAO;
using System;
using System.Collections.Generic;
using System.Text;

namespace QL_QuanAnTests
{
    [TestClass()]
    public class DeleteBillTests
    {
        private BillDAO billDAO;

        [TestInitialize]
        public void Setup()
        {
            billDAO = BillDAO.Instance;
        }

        // Test Case 1: Xóa hóa đơn thành công (không liên kết với bảng khác)
        [TestMethod]
        public void Test_DeleteBill_Successful_TC1()
        {
            int billId = 6;

            bool result = billDAO.DeleteBill(billId);

            Assert.IsTrue(result, "Xóa hóa đơn thành công.");
        }

        // Test Case 2: Xóa hóa đơn thất bại do ràng buộc khóa ngoại
        [TestMethod]
        public void Test_DeleteBill_ForeignKeyConstraintFail_TC2()
        {
            int billId = 4; // Hóa đơn liên kết với bảng khác (CHITIETHOADON)

            var ex = Assert.ThrowsException<ArgumentException>(() =>
                billDAO.DeleteBill(billId));

            Assert.AreEqual("Ràng buộc khóa ngoại bị vi phạm.", ex.Message);
        }

        // Test Case 3: Xóa hóa đơn thất bại vì hóa đơn không tồn tại
        [TestMethod]
        public void Test_DeleteBill_NotExist_TC3()
        {
            int billId = 9999; // Hóa đơn liên kết với bảng khác (CHITIETHOADON)

            var ex = Assert.ThrowsException<ArgumentException>(() =>
                billDAO.DeleteBill(billId));

            Assert.AreEqual("Hóa đơn không tồn tại.", ex.Message);
        }
    }
}