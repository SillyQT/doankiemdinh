using Microsoft.VisualStudio.TestTools.UnitTesting;
using QL_QuanAn.DAO;
using System;
using System.Collections.Generic;
using System.Text;

namespace QL_QuanAnTests
{
    [TestClass()]
    public class UpdateTotalForBillTests
    {
        private BillDAO billDAO;

        [TestInitialize]
        public void Setup()
        {
            billDAO = new BillDAO();
        }

        // TC1: Cập nhật thành tiền thành công
        [TestMethod]
        public void Test_UpdateTotalForValidBill_TC1()
        {
            int billId = 1;
            int total = 1000000;

            bool result = billDAO.UpdateTotalForBill(billId, total);

            Assert.IsTrue(result, "Cập nhật thành tiền thành công.");
        }

        // TC2: Cập nhật thất bại (total không hợp lệ)
        [TestMethod]
        public void Test_UpdateTotalWithInvalidAmount_TC2()
        {
            int billId = 1;
            int invalidTotal = -500;

            var ex = Assert.ThrowsException<ArgumentException>(() =>
                billDAO.UpdateTotalForBill(billId, invalidTotal));

            Assert.AreEqual("Tổng tiền không hợp lệ.", ex.Message);
        }

        // TC3: Cập nhật thất bại (hóa đơn không tồn tại)
        [TestMethod]
        public void Test_UpdateTotalForNonExistentBill_TC3()
        {
            int nonExistentBillId = 9999;
            int total = 500000;

            var ex = Assert.ThrowsException<ArgumentException>(() =>
                billDAO.UpdateTotalForBill(nonExistentBillId, total));

            Assert.AreEqual("Hóa đơn không tồn tại.", ex.Message);
        }
    }
}
