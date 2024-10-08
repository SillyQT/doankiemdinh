using Microsoft.VisualStudio.TestTools.UnitTesting;
using QL_QuanAn.DAO;
using System;

namespace QL_QuanAnTests
{
    [TestClass]
    public class UpdateEmployeeTests
    {
        private EmployeeDAO employeeDAO;
        [TestInitialize]
        public void Setup()
        {
            employeeDAO = EmployeeDAO.Instance;
        }

        [TestMethod]
        public void Test_UpdateEmployee_TC1()
        {
            string staffid = "cccccccccc";
            string name = "UpdateNhanVien";
            string sex = "Nam";
            DateTime dayofbirth = DateTime.Parse("2003-10-26");
            string address = "17B Tân Trụ";
            string phone = "0362111265";
            DateTime dayofwork = DateTime.Parse("2024-10-07");
            double basicsalary = 100000;

            bool result = employeeDAO.UpdateEmployee(staffid, name, sex, dayofbirth, address, phone, dayofwork, basicsalary);
            Assert.IsTrue(result, "Sửa nhân viên thành công.");
        }

        [TestMethod]
        public void Test_UpdateEmployee_TC2()
        {
            string staffid = "";
            string name = "";
            string sex = "";
            DateTime dayofbirth = DateTime.Parse("2003-10-26");
            string address = "";
            string phone = "";
            DateTime dayofwork = DateTime.Parse("2024-10-07");
            double basicsalary = 00;

            var ex = Assert.ThrowsException<ArgumentException>(() => employeeDAO.InsertEmployee(staffid, name, sex, dayofbirth, address, phone, dayofwork, basicsalary));
            Assert.AreEqual("Mã nhân viên không được để trống.", ex.Message);
        }

        [TestMethod]
        public void Test_UPdateEmployee_TC3()
        {
            string staffid = "NVTest";
            string name = "";
            string sex = "";
            DateTime dayofbirth = DateTime.Parse("2003-10-26");
            string address = "";
            string phone = "";
            DateTime dayofwork = DateTime.Parse("2024-10-07");
            double basicsalary = 00;

            var ex = Assert.ThrowsException<ArgumentException>(() => employeeDAO.InsertEmployee(staffid, name, sex, dayofbirth, address, phone, dayofwork, basicsalary));
            Assert.AreEqual("Tên nhân viên không được để trống.", ex.Message);
        }

        [TestMethod]
        public void Test_UpdateEmployee_TC4()
        {
            string staffid = "NVTest";
            string name = "TEST";
            string sex = "Đực";
            DateTime dayofbirth = DateTime.Parse("2003-10-26");
            string address = "";
            string phone = "";
            DateTime dayofwork = DateTime.Parse("2024-10-07");
            double basicsalary = 00;

            var ex = Assert.ThrowsException<ArgumentException>(() => employeeDAO.InsertEmployee(staffid, name, sex, dayofbirth, address, phone, dayofwork, basicsalary));
            Assert.AreEqual("Giới tính là Nam hoặc Nữ.", ex.Message);
        }

        [TestMethod]
        public void Test_UpdateEmployee_TC5()
        {
            string staffid = "NVTest";
            string name = "TEST";
            string sex = "Nam";
            DateTime dayofbirth;
            if (!DateTime.TryParse("2024-10-32", out dayofbirth))
            {
                dayofbirth = DateTime.MinValue; // Giả lập giá trị ngày không hợp lệ
            }
            string address = "";
            string phone = "";
            DateTime dayofwork = DateTime.Parse("2024-10-07");
            double basicsalary = 00;

            var ex = Assert.ThrowsException<ArgumentException>(() => employeeDAO.InsertEmployee(staffid, name, sex, dayofbirth, address, phone, dayofwork, basicsalary));
            Assert.AreEqual("Vui lòng nhập ngày sinh hợp lệ.", ex.Message);
        }

        [TestMethod]
        public void Test_UpdateEmployee_TC6()
        {
            string staffid = "NVTest";
            string name = "TEST";
            string sex = "Nam";
            DateTime dayofbirth = DateTime.Parse("2003-10-26");
            string address = "";
            string phone = "";
            DateTime dayofwork = DateTime.Parse("2024-10-07");
            double basicsalary = 00;

            var ex = Assert.ThrowsException<ArgumentException>(() => employeeDAO.InsertEmployee(staffid, name, sex, dayofbirth, address, phone, dayofwork, basicsalary));
            Assert.AreEqual("Địa chỉ không được để trống.", ex.Message);
        }

        [TestMethod]
        public void Test_UpdateEmployee_TC7()
        {
            string staffid = "NVTest";
            string name = "TEST";
            string sex = "Nam";
            DateTime dayofbirth = DateTime.Parse("2003-10-26");
            string address = "17B";
            string phone = "ádsadsadas";
            DateTime dayofwork = DateTime.Parse("2024-10-07");
            double basicsalary = 00;

            var ex = Assert.ThrowsException<ArgumentException>(() => employeeDAO.InsertEmployee(staffid, name, sex, dayofbirth, address, phone, dayofwork, basicsalary));
            Assert.AreEqual("Số điện thoại không hợp lệ. Vui lòng nhập đúng 10 chữ số.", ex.Message);
        }

        [TestMethod]
        public void Test_UpdateEmployee_TC8()
        {
            string staffid = "NVTest";
            string name = "TEST";
            string sex = "Nam";
            DateTime dayofbirth = DateTime.Parse("2003-10-26");
            string address = "17B";
            string phone = "0362111265";
            DateTime dayofwork;
            if (!DateTime.TryParse("2024-10-32", out dayofwork))
            {
                dayofwork = DateTime.MinValue; // Giả lập giá trị ngày không hợp lệ
            }
            double basicsalary = 00;

            var ex = Assert.ThrowsException<ArgumentException>(() => employeeDAO.InsertEmployee(staffid, name, sex, dayofbirth, address, phone, dayofwork, basicsalary));
            Assert.AreEqual("Vui lòng nhập ngày vào làm hợp lệ.", ex.Message);
        }

        [TestMethod]
        public void Test_UpdateEmployee_TC9()
        {
            // Test case cho lương cơ bản null
            string staffid = "NVTest";
            string name = "TEST";
            string sex = "Nam";
            DateTime dayofbirth = DateTime.Parse("2003-10-26");
            string address = "17B";
            string phone = "0362111265";
            DateTime dayofwork = DateTime.Parse("2023-10-26");
            double? basicsalary = null; // Lương cơ bản null để test

            var ex = Assert.ThrowsException<ArgumentException>(() => employeeDAO.InsertEmployee(staffid, name, sex, dayofbirth, address, phone, dayofwork, basicsalary));

            // Kiểm tra thông báo lỗi
            Assert.AreEqual("Lương cơ bản không hợp lệ. Vui lòng nhập giá trị lớn hơn 0.", ex.Message);
        }
    }
}
