using Microsoft.VisualStudio.TestTools.UnitTesting;
using QL_QuanAn.DAO;
using System;

namespace QL_QuanAnTests
{
    [TestClass]
    public class DeleteEmployeeTests
    {
        private EmployeeDAO employeeDAO;
        [TestInitialize]
        public void Setup()
        {
            employeeDAO = EmployeeDAO.Instance;
        }

        [TestMethod]
        public void Test_DeleteEmployee_TC1 ()
        {
            string staffid = "cccccccccc";

            bool result = employeeDAO.DeleteEmployee(staffid);
            Assert.IsTrue(result, "Xóa nhân viên thành công.");
        }

        [TestMethod]
        public void Test_DeleteEmployee_TC2()
        {
            string staffid = "";

            var ex = Assert.ThrowsException<ArgumentException>(() => employeeDAO.DeleteEmployee(staffid));
            Assert.AreEqual("Mã nhân viên không được để trống.", ex.Message);
        }
    }
}
