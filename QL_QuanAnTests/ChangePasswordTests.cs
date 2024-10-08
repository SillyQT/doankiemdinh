using Microsoft.VisualStudio.TestTools.UnitTesting;
using QL_QuanAn.DAO;
using System;

namespace QL_QuanAnTests
{
    [TestClass]
    public class ChangePasswordTests
    {
        private AccountDAO accountDAO;
        [TestInitialize]
        public void Setup()
        {
            accountDAO = AccountDAO.Instance;
        }

        [TestMethod]
        public void Test_ChangePassword_TC1()
        {
            string userName = "admin";
            string password = "1";
            string passwordNew = "12";
            string prePasswordNew = "12";
            bool result = accountDAO.ChangePassWord(userName, password, passwordNew, prePasswordNew);
            Assert.IsTrue(result, "Đăng nhập thành công.");
        }

        [TestMethod]
        public void Test_ChangePassword_TC2()
        {
            string userName = "admin";
            string passwordOld = "123"; // Giả lập mật khẩu cũ sai
            string passwordNew = "123";
            string prePasswordNew = "123";

            // Kiểm tra ngoại lệ khi mật khẩu cũ không đúng
            var ex = Assert.ThrowsException<ArgumentException>(() => accountDAO.ChangePassWord(userName, passwordOld, passwordNew, prePasswordNew));

            // Kiểm tra xem thông báo lỗi có đúng không
            Assert.AreEqual("Mật khẩu không đúng.", ex.Message);
        }

        [TestMethod]
        public void Test_ChangePassword_TC3()
        {
            string userName = "";
            string passwordOld = ""; 
            string passwordNew = "";
            string prePasswordNew = "";

            // Kiểm tra ngoại lệ khi mật khẩu cũ không đúng
            var ex = Assert.ThrowsException<ArgumentException>(() => accountDAO.ChangePassWord(userName, passwordOld, passwordNew, prePasswordNew));

            // Kiểm tra xem thông báo lỗi có đúng không
            Assert.AreEqual("Mật khẩu cũ không để trống.", ex.Message);
        }

        [TestMethod]
        public void Test_ChangePassword_TC4()
        {
            string userName = "admin";
            string passwordOld = "1";
            string passwordNew = "";
            string prePasswordNew = "";

            // Kiểm tra ngoại lệ khi mật khẩu cũ không đúng
            var ex = Assert.ThrowsException<ArgumentException>(() => accountDAO.ChangePassWord(userName, passwordOld, passwordNew, prePasswordNew));

            // Kiểm tra xem thông báo lỗi có đúng không
            Assert.AreEqual("Mật khẩu mới trống.", ex.Message);
        }

        [TestMethod]
        public void Test_ChangePassword_TC5()
        {
            string userName = "admin";
            string passwordOld = "1";
            string passwordNew = "1";
            string prePasswordNew = "";

            // Kiểm tra ngoại lệ khi mật khẩu cũ không đúng
            var ex = Assert.ThrowsException<ArgumentException>(() => accountDAO.ChangePassWord(userName, passwordOld, passwordNew, prePasswordNew));

            // Kiểm tra xem thông báo lỗi có đúng không
            Assert.AreEqual("Nhập lại mật khẩu trống.", ex.Message);
        }

        [TestMethod]
        public void Test_ChangePassword_TC6()
        {
            string userName = "admin";
            string passwordOld = "1";
            string passwordNew = "1";
            string prePasswordNew = "12";

            // Kiểm tra ngoại lệ khi mật khẩu cũ không đúng
            var ex = Assert.ThrowsException<ArgumentException>(() => accountDAO.ChangePassWord(userName, passwordOld, passwordNew, prePasswordNew));

            // Kiểm tra xem thông báo lỗi có đúng không
            Assert.AreEqual("Mật khẩu mới và nhập lại mật khẩu không giống nhau.", ex.Message);
        }
    }
}
