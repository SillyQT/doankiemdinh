using Microsoft.VisualStudio.TestTools.UnitTesting;
using QL_QuanAn.DAO;
using System;
using System.Collections.Generic;
using System.Text;

namespace QL_QuanAnTests
{
    [TestClass()]
    public class BillTests
    {
        private BillDAO billDAO;

        [TestInitialize]
        public void Setup()
        {
            billDAO = new BillDAO();
        }

        // TC1: Thêm hóa đơn thành công
        [TestMethod]
        public void Test_AddValidBill_TC1()
        {
            DateTime billCreateDate = new DateTime(2024, 9, 22);
            int tableId = 1;
            string employeeId = "NV001";

            DateTime billDateFormat = billCreateDate.Date;

            bool result = billDAO.InsertBill(billDateFormat, tableId, employeeId);

            Assert.IsTrue(result, "Thêm hóa đơn thành công.");
        }

        // TC2: Thêm hóa đơn thất bại (ngày không hợp lệ)
        [TestMethod]
        public void Test_AddBillWithInvalidDate_TC2()
        {
            DateTime invalidBillCreateDate = DateTime.MinValue; // Ngày không hợp lệ
            int tableId = 2;
            string employeeId = "NV02";

            var ex = Assert.ThrowsException<ArgumentException>(() =>
                billDAO.InsertBill(invalidBillCreateDate, tableId, employeeId));

            Assert.AreEqual("Ngày không hợp lệ.", ex.Message);
        }

        // TC3: Thêm hóa đơn thất bại (tên bàn không hợp lệ)
        [TestMethod]
        public void Test_AddBillWithInvalidTable_TC3()
        {
            DateTime billCreateDate = new DateTime(2024, 9, 22);
            int invalidTableId = -1;
            string employeeId = "NV04";

            var ex = Assert.ThrowsException<ArgumentException>(() =>
                billDAO.InsertBill(billCreateDate, invalidTableId, employeeId));

            Assert.AreEqual("Tên bàn không hợp lệ.", ex.Message);
        }

        // TC4: Thêm hóa đơn thất bại (tên nhân viên không hợp lệ)
        [TestMethod]
        public void Test_AddBillWithInvalidEmployee_TC4()
        {
            DateTime billCreateDate = new DateTime(2024, 9, 22);
            int invalidTableId = 2;
            string employeeId = "Không hợp lệ";

            var ex = Assert.ThrowsException<ArgumentException>(() =>
                billDAO.InsertBill(billCreateDate, invalidTableId, employeeId));

            Assert.AreEqual("Nhân viên không hợp lệ.", ex.Message);
        }
    }
}
