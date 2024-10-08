using Microsoft.VisualStudio.TestTools.UnitTesting;
using QL_QuanAn.DAO;
using QL_QuanAn.DTO;
using System;

namespace QL_QuanAnTests
{
    [TestClass]
    public class LoginTests
    {
        private AccountDAO accountDAO;

        [TestInitialize]
        public void Setup()
        {
            accountDAO = AccountDAO.Instance;
        }

        [TestMethod]
        public void Test_Login_TC1()
        {
            string userName = "admin";
            string passWord = "12";

            bool result = accountDAO.Login(userName, passWord);

            Assert.IsTrue(result, "Đăng nhập thành công.");
        }

        [TestMethod]
        public void Test_Login_TC2()
        {
            string userName = "";
            string passWord = "1";

            var ex = Assert.ThrowsException<ArgumentException>(() => accountDAO.Login(userName, passWord));

            Assert.AreEqual("Sai tên tài khoản hoặc mật khẩu.", ex.Message);
        }

        [TestMethod]
        public void Test_Login_TC3()
        {
            string userName = "admin";
            string passWord = "";

            var ex = Assert.ThrowsException<ArgumentException>(() => accountDAO.Login(userName, passWord));

            Assert.AreEqual("Sai tên tài khoản hoặc mật khẩu.", ex.Message);
        }

        [TestMethod]
        public void Test_Login_TC4()
        {
            string userName = "test";
            string passWord = "1";

            var ex = Assert.ThrowsException<ArgumentException>(() => accountDAO.Login(userName, passWord));

            Assert.AreEqual("Tài khoản đã bị xóa.", ex.Message);
        }

        [TestMethod]
        public void Test_Login_TC5()
        {
            string userName = "admin";
            string passWord = "123";

            var ex = Assert.ThrowsException<ArgumentException>(() => accountDAO.Login(userName, passWord));

            Assert.AreEqual("Sai tên tài khoản hoặc mật khẩu.", ex.Message);
        }
    }
}
