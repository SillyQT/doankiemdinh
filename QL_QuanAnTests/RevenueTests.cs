using Microsoft.VisualStudio.TestTools.UnitTesting;
using QL_QuanAn;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Text;

namespace QL_QuanAnTests
{
    [TestClass()]
    public class RevenueTests
    {
        private Revenue revenue;

        [TestInitialize]
        public void Setup()
        {
            revenue = new Revenue();
        }

        // TC1: Tính doanh thu thành công với tháng và năm hợp lệ
        [TestMethod]
        public void Test_CalculateRevenue_ValidMonthYear_TC1()
        {
            string validMonth = "9";
            string validYear = "2024";

            DataTable result = CalculateRevenue(validMonth, validYear);

            Assert.IsNotNull(result, "Tính doanh thu thành công.");
        }

        // TC2: Tính doanh thu thất bại (năm không hợp lệ)
        [TestMethod]
        public void Test_CalculateRevenue_InvalidYear_TC2()
        {
            string validMonth = "9";
            string invalidYear = "2200";

            var ex = Assert.ThrowsException<ArgumentException>(() =>
                CalculateRevenue(validMonth, invalidYear));

            Assert.AreEqual("Năm không hợp lệ.", ex.Message);
        }

        // TC3: Tính doanh thu thất bại (tháng không hợp lệ)
        [TestMethod]
        public void Test_CalculateRevenue_InvalidMonth_TC3()
        {
            string invalidMonth = "15";
            string validYear = "2024";

            var ex = Assert.ThrowsException<ArgumentException>(() =>
                CalculateRevenue(invalidMonth, validYear));

            Assert.AreEqual("Tháng không hợp lệ.", ex.Message);
        }

        private DataTable CalculateRevenue(string month, string year)
        {
            if (!int.TryParse(month, out int monthValue) || monthValue < 1 || monthValue > 12)
            {
                throw new ArgumentException("Tháng không hợp lệ.");
            }

            if (!int.TryParse(year, out int yearValue) || yearValue < 2023 || yearValue > 2123)
            {
                throw new ArgumentException("Năm không hợp lệ.");
            }

            string connectionString = @"Data Source=LAPTOP-UQJ6G1J9;Initial Catalog=QL_QuanAn;Integrated Security=True";
            using (SqlConnection sql = new SqlConnection(connectionString))
            {
                sql.Open();

                SqlCommand cmd = new SqlCommand($"SELECT * FROM DOANHTHU WHERE THANG = {month} AND NAM = {year}", sql);

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);

                return dt;
            }
        }
    }
}