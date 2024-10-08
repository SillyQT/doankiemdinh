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
    public class TableDAO
    {
        private static TableDAO instance;

        public static TableDAO Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new TableDAO();
                }
                return instance;
            }
            private set { instance = value; }
        }

        public TableDAO() { }

        public bool DoesTableExist(int tableId)
        {
            string query = "EXEC USP_DoesTableExist @tableId";

            int count = (int)DataProvider.Instance.ExecuteScalar(query, new object[] { tableId });

            return count > 0;
        }

        public List<TableDTO> GetListTable()
        {
            List<TableDTO> list = new List<TableDTO>();

            string query = "USP_GetListTable";

            DataTable data = DataProvider.Instance.ExecuteQuery(query);

            foreach (DataRow row in data.Rows)
            {
                TableDTO table = new TableDTO(row);
                list.Add(table);
            }
            return list;
        }

        public List<TableDTO> GetListTableByArea(int areaId)
        {
            List<TableDTO> list = new List<TableDTO>();

            string query = "EXEC USP_GetListTableByArea @areaId";

            DataTable data = DataProvider.Instance.ExecuteQuery(query, new object[] { areaId });

            foreach (DataRow row in data.Rows)
            {
                TableDTO table = new TableDTO(row);
                list.Add(table);
            }
            return list;
        }

        public string GetStatusByTable(int tableId)
        {
            string query = "EXEC USP_GetStatusByTable @tableId";

            string result = "";

            result = DataProvider.Instance.ExecuteScalar(query, new object[] { tableId }).ToString();

            return result;
        }

        public bool UpdateStatusTable(int tableId, string status)
        {
            string query = "EXEC USP_UpdateStatusTable @tableId , @status";

            int result = DataProvider.Instance.ExecuteNonQuery(query, new object[] { tableId, status });

            return result > 0;
        }

        public TableDTO GetTableByTableId(int tableId)
        {
            TableDTO table = null;

            string query = "EXEC USP_GetTableByTableId @tableId";

            DataTable data = DataProvider.Instance.ExecuteQuery(query, new object[] { tableId });

            foreach (DataRow row in data.Rows)
            {
                table = new TableDTO(row);
            }

            return table;
        }

        public int GetTableIdByTableName(string tableName)
        {
            string query = "EXEC USP_GetTableIdByTableName @tableName";

            int result = Int32.Parse(DataProvider.Instance.ExecuteScalar(query, new object[] { tableName }).ToString());

            return result;
        }

        public bool InsertTable(int areaid, string tablename, int quanlity, string status)
        {
            if (areaid <= 0)
            {
                throw new ArgumentException("Khu vực không hợp lệ.");
            }

            if (string.IsNullOrEmpty(tablename))
            {
                throw new ArgumentException("Tên bàn không được để trống.");
            }

            if (tablename.Length < 2 || tablename.Length > 20)
            {
                throw new ArgumentException("Tên bàn phải có độ dài từ 2 đến 20 ký tự.");
            }

            if (tablename.Any(c => !char.IsLetterOrDigit(c) && !char.IsWhiteSpace(c)))
            {
                throw new ArgumentException("Tên bàn không được chứa ký tự đặc biệt.");
            }

            if (quanlity <= 0)
            {
                throw new ArgumentException("Số lượng bàn phải là số dương.");
            }

            string query = string.Format("USP_InsertTable @areaid , @tablename , @quanlity , @status");
            int result = DataProvider.Instance.ExecuteNonQuery(query, new object[] { areaid, tablename, quanlity, status });

            return result > 0;
        }

        public bool DeleteTalbe(int tableid, string tableStatus)
        {
            // Kiểm tra trạng thái của bàn
            if (tableStatus == "Đã đặt")
            {
                throw new ArgumentException("Bàn này có người ăn, không thể xóa!!!");
            }

            string query = string.Format("USP_DeleteTable @tableid");
            int result = DataProvider.Instance.ExecuteNonQuery(query, new object[] { tableid });

            return result > 0;
        }

        public bool UpdateTable(int tableid, int areaid, string tablename, int quanlity, string status)
        {
            if (string.IsNullOrEmpty(tablename))
            {
                throw new ArgumentException("Tên bàn không được để trống!");
            }

            if (tablename.Any(c => !char.IsLetterOrDigit(c) && !char.IsWhiteSpace(c)))
            {
                throw new ArgumentException("Tên bàn không được chứa ký tự đặc biệt!");
            }

            if (tablename.Length < 2 || tablename.Length > 20)
            {
                throw new ArgumentException("Tên bàn phải có độ dài từ 2 đến 20 ký tự!");
            }

            if (status != "Trống" && status != "Đã đặt")
            {
                throw new ArgumentException("Trạng thái bàn không hợp lệ!");
            }

            if (quanlity <= 0)
            {
                throw new ArgumentException("Số lượng người phải lớn hơn 0!");
            }
            string query = string.Format("USP_UpdateTable @tableid , @areaid , @tablename , @quanlity , @status");
            int result = DataProvider.Instance.ExecuteNonQuery(query, new object[] { tableid, areaid, tablename, quanlity, status });

            return result > 0;
        }
    }
}
