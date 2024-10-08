using Microsoft.VisualStudio.TestTools.UnitTesting;
using QL_QuanAn.DAO;
using System;
using System.Collections.Generic;
using System.Text;

namespace QL_QuanAnTests
{
    [TestClass()]
    public class UpdateTableIdForBillTest
    {
        private BillDAO billDAO;

        [TestInitialize]
        public void Setup()
        {
            billDAO = new BillDAO();
        }

        // TC1: Cập nhật mã bàn thành công
        [TestMethod]
        public void Test_UpdateValidTableIdForBill_TC1()
        {
            int validTableId = 5;
            int validBillId = 1;

            bool result = billDAO.UpdateTableIdForBill(validTableId, validBillId);

            Assert.IsTrue(result, "Cập nhật mã bàn cho hóa đơn thành công.");
        }

        // TC2: Cập nhật mã bàn thất bại do mã bàn không hợp lệ
        [TestMethod]
        public void Test_UpdateInvalidTableIdForBill_TC2()
        {
            int invalidTableId = -1; // Mã bàn không hợp lệ
            int validBillId = 1;

            var ex = Assert.ThrowsException<ArgumentException>(() =>
                billDAO.UpdateTableIdForBill(invalidTableId, validBillId));

            Assert.AreEqual("Mã bàn không hợp lệ.", ex.Message);
        }

        // TC3: Cập nhật mã bàn thất bại do hóa đơn không tồn tại
        [TestMethod]
        public void Test_UpdateTableIdForNonExistentBill_TC3()
        {
            int validTableId = 5;
            int nonExistentBillId = 9999; // Hóa đơn không tồn tại

            var ex = Assert.ThrowsException<ArgumentException>(() =>
                billDAO.UpdateTableIdForBill(validTableId, nonExistentBillId));

            Assert.AreEqual("Hóa đơn không tồn tại.", ex.Message);
        }
    }
}
