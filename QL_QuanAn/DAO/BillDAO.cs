using QL_QuanAn.DTO;
using QuanLyQuanCafe.DAO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QL_QuanAn.DAO
{
    public class BillDAO
    {
        private static BillDAO instance;

        public static BillDAO Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new BillDAO();
                }
                return instance;
            }
            private set { instance = value; }
        }

        public BillDAO() { }

        public BillDTO GetBillByTable(int tableId)
        {
            BillDTO bill = new BillDTO();

            string query = "EXEC USP_GetBillByTable @tableId";

            DataTable data = DataProvider.Instance.ExecuteQuery(query, new object[] { tableId });

            foreach (DataRow row in data.Rows)
            {
                bill = new BillDTO(row);
            }

            return bill;
        }

        public bool InsertBill(DateTime billCreateDate, int tableId, string employeeId)
        {
            if (billCreateDate == DateTime.MinValue || billCreateDate > DateTime.Now)
            {
                throw new ArgumentException("Ngày không hợp lệ.");
            }

            if (!TableDAO.Instance.DoesTableExist(tableId))
            {
                throw new ArgumentException("Tên bàn không hợp lệ.");
            }

            if (string.IsNullOrEmpty(employeeId) || !EmployeeDAO.Instance.DoesEmployeeExist(employeeId))
            {
                throw new ArgumentException("Nhân viên không hợp lệ.");
            }

            string query = "EXEC USP_InsertBill @tableId , @employeeId , @dateIn";

            int result = DataProvider.Instance.ExecuteNonQuery(query, new object[] { tableId, employeeId, billCreateDate });

            return result > 0;
        }

        public bool DeleteBill(int billId)
        {
            string checkBillQuery = "EXEC USP_CheckBill @billId";
            int billCount = (int)DataProvider.Instance.ExecuteScalar(checkBillQuery, new object[] { billId });

            if (billCount == 0)
            {
                throw new ArgumentException("Hóa đơn không tồn tại.");
            }

            string checkForeignKeyQuery = "EXEC USP_CheckForeignKeyConstraint @billId";
            int foreignKeyCount = (int)DataProvider.Instance.ExecuteScalar(checkForeignKeyQuery, new object[] { billId });

            if (foreignKeyCount > 0)
            {
                throw new ArgumentException("Ràng buộc khóa ngoại bị vi phạm.");
            }

            string query = "EXEC USP_DeleteBill @billId";
            int result = DataProvider.Instance.ExecuteNonQuery(query, new object[] { billId });

            if (result <= 0)
            {
                throw new Exception("Xóa hóa đơn thất bại.");
            }

            return result > 0;
        }

        public bool UpdateTableIdForBill(int tableId, int billId)
        {
            string checkBillQuery = "EXEC USP_CheckBill @billId";
            int billCount = (int)DataProvider.Instance.ExecuteScalar(checkBillQuery, new object[] { billId });

            if (billCount == 0)
            {
                throw new ArgumentException("Hóa đơn không tồn tại.");
            }

            if (tableId <= 0)
            {
                throw new ArgumentException("Mã bàn không hợp lệ.");
            }

            string query = "EXEC USP_UpdateTableIdForBill @tableId , @billId";

            int result = DataProvider.Instance.ExecuteNonQuery(query, new object[] { tableId, billId });

            if (result <= 0)
            {
                throw new ArgumentException("Cập nhật mã bàn thất bại.");
            }

            return result > 0;
        }

        public bool UpdateTotalForBill(int billId, int gt)
        {
            string checkBillQuery = "EXEC USP_CheckBill @billId";
            int billCount = (int)DataProvider.Instance.ExecuteScalar(checkBillQuery, new object[] { billId });

            if (billCount == 0)
            {
                throw new ArgumentException("Hóa đơn không tồn tại.");
            }

            // Kiểm tra tổng tiền (gt) có hợp lệ không
            if (gt <= 0)
            {
                throw new ArgumentException("Tổng tiền không hợp lệ.");
            }

            // Thực hiện cập nhật tổng tiền cho hóa đơn
            string query = "USP_UpdateTotalForBill @billId , @total";

            int result = DataProvider.Instance.ExecuteNonQuery(query, new object[] { billId, gt });

            return result > 0;
        }

        public bool UpdateBill(int billId, int gt)
        {
            string query = "USP_UpdateTotalForBill @billId , @total";

            int result = DataProvider.Instance.ExecuteNonQuery(query, new object[] { billId, gt });

            return result > 0;
        }

        public List<BillDTO> GetListBillByDateOut(DateTime dateStart, DateTime dateEnd)
        {
            List<BillDTO> list = new List<BillDTO>();

            string query = "EXEC USP_GetListBillByDateOut @dateStart , @dateEnd";

            DataTable data = DataProvider.Instance.ExecuteQuery(query, new object[] { dateStart, dateEnd });

            foreach (DataRow row in data.Rows)
            {
                BillDTO bill = new BillDTO(row);

                list.Add(bill);
            }

            return list;
        }

        public List<BillDTO> GetListBillByDateOutAndStaff(DateTime dateStart, DateTime dateEnd, string staffId)
        {
            List<BillDTO> list = new List<BillDTO>();

            string query = "EXEC USP_GetListBillByDateOutAndStaff @dateStart , @dateEnd , @staffId";

            DataTable data = DataProvider.Instance.ExecuteQuery(query, new object[] { dateStart, dateEnd, staffId });

            foreach (DataRow row in data.Rows)
            {
                BillDTO bill = new BillDTO(row);

                list.Add(bill);
            }

            return list;
        }
    }
}
