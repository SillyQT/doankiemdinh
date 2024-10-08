using Microsoft.VisualStudio.TestTools.UnitTesting;
using QL_QuanAn.DAO;
using System;
using QL_QuanAn;

namespace QL_QuanAnTests
{
    [TestClass]
    public class OrderTable
    {
        private TableDAO tableDAO;

        [TestInitialize]
        public void Setup()
        {
            tableDAO = TableDAO.Instance;
        }
        [TestMethod]
        public void Test_OrderTable_TC1()
        {
            Assert.IsEmpty(TableDAO.Instance);
        }
    }
}
