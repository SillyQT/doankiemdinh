using DevExpress.XtraEditors;
using QL_QuanAn.DTO;
using QuanLyQuanCafe.DAO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace QL_QuanAn.DAO
{
    public class EmployeeDAO
    {
        private static EmployeeDAO instance;
        public static EmployeeDAO Instance
        {
            get
            {
                if (instance == null)
                    instance = new EmployeeDAO();
                return instance;
            }
            private set { instance = value; }
        }
        private EmployeeDAO() { }

        public bool DoesEmployeeExist(string employeeId)
        {
            string query = "EXEC USP_DoesEmployeeExist @employeeId";

            int count = (int)DataProvider.Instance.ExecuteScalar(query, new object[] { employeeId });

            return count > 0;
        }

        public List<EmployeeDTO> GetListEmployee()
        {
            List<EmployeeDTO> listemloyee = new List<EmployeeDTO>();

            string query = "USP_GetListEmployee";
            DataTable data = DataProvider.Instance.ExecuteQuery(query);

            foreach (DataRow item in data.Rows)
            {
                EmployeeDTO employee = new EmployeeDTO(item);
                listemloyee.Add(employee);
            }
            return listemloyee;
        }

        public EmployeeDTO GetEmployeeByStaffID(string staffid)
        {
            EmployeeDTO employee = null;

            string query = string.Format("USP_GetEmployeeByStaffID @staffid");
            DataTable data = DataProvider.Instance.ExecuteQuery(query, new object[] { staffid });

            foreach (DataRow item in data.Rows)
            {
                employee = new EmployeeDTO(item);
                return employee;
            }
            return employee;
        }

        public bool InsertEmployee(string staffid, string name, string sex, DateTime? dateofbirth, string address, string phone, DateTime? dateofwork, double? basicsalary)
        {
            // Kiểm tra các trường bắt buộc
            if (string.IsNullOrEmpty(staffid))
            {
                throw new ArgumentException("Mã nhân viên không được để trống.");
            }

            if (string.IsNullOrEmpty(name))
            {
                throw new ArgumentException("Tên nhân viên không được để trống.");
            }

            if (sex != "Nam" && sex != "Nữ")
            {
                throw new ArgumentException("Giới tính là Nam hoặc Nữ.");
            }

            if (!dateofbirth.HasValue || dateofbirth < new DateTime(1900, 1, 1) || dateofbirth > DateTime.Now)
            {
                throw new ArgumentException("Vui lòng nhập ngày sinh hợp lệ.");
            }

            if (string.IsNullOrEmpty(address))
            {
                throw new ArgumentException("Địa chỉ không được để trống.");
            }

            if (string.IsNullOrEmpty(phone) || phone.Length != 10 || !phone.All(char.IsDigit))
            {
                throw new ArgumentException("Số điện thoại không hợp lệ. Vui lòng nhập đúng 10 chữ số.");
            }

            if (!dateofwork.HasValue || dateofwork < new DateTime(1900, 1, 1) || dateofwork > DateTime.Now)
            {
                throw new ArgumentException("Vui lòng nhập ngày vào làm hợp lệ.");
            }

            if (basicsalary == null || basicsalary <= 0)
            {
                throw new ArgumentException("Lương cơ bản không hợp lệ. Vui lòng nhập giá trị lớn hơn 0.");
            }

            // Kiểm tra logic: Ngày vào làm phải lớn hơn ngày sinh
            if (dateofwork <= dateofbirth)
            {
                throw new ArgumentException("Ngày vào làm phải lớn hơn ngày sinh.");
            }

            if (GetEmployeeByStaffID(staffid) != null)
            {
                throw new ArgumentException("Nhân viên đã tồn tại trong hệ thống hoặc đã bị xóa.");
            }       

            string query = string.Format("USP_InsertEmployee @staffid , @name , @sex , @dateofbirth , @address , @phone , @dateofwork , @basicsalary");
            int result = DataProvider.Instance.ExecuteNonQuery(query, new object[] { staffid, name, sex, dateofbirth, address, phone, dateofwork, basicsalary });

            return result > 0;
        }

        public bool DeleteEmployee(string staffid)
        {
            if (string.IsNullOrEmpty(staffid))
            {
                throw new ArgumentException("Mã nhân viên không được để trống.");
            }

            string query = string.Format("USP_DeleteEmployee @staffid");
            int result = DataProvider.Instance.ExecuteNonQuery(query, new object[] { staffid });

            return result > 0;
        }

        public bool UpdateEmployee(string staffid, string name, string sex, DateTime? dateofbirth, string address, string phone, DateTime? dateofwork, double basicsalary)
        {
            // Kiểm tra các trường bắt buộc
            if (string.IsNullOrEmpty(staffid))
            {
                throw new ArgumentException("Mã nhân viên không được để trống.");
            }

            if (string.IsNullOrEmpty(name))
            {
                throw new ArgumentException("Tên nhân viên không được để trống.");
            }

            if (sex != "Nam" && sex != "Nữ")
            {
                throw new ArgumentException("Giới tính là Nam hoặc Nữ.");
            }

            if (!dateofbirth.HasValue || dateofbirth < new DateTime(1900, 1, 1) || dateofbirth > DateTime.Now)
            {
                throw new ArgumentException("Vui lòng nhập ngày sinh hợp lệ.");
            }

            if (string.IsNullOrEmpty(address))
            {
                throw new ArgumentException("Địa chỉ không được để trống.");
            }

            if (string.IsNullOrEmpty(phone) || phone.Length != 10 || !phone.All(char.IsDigit))
            {
                throw new ArgumentException("Số điện thoại không hợp lệ. Vui lòng nhập đúng 10 chữ số.");
            }

            if (!dateofwork.HasValue || dateofwork < new DateTime(1900, 1, 1) || dateofwork > DateTime.Now)
            {
                throw new ArgumentException("Vui lòng nhập ngày vào làm hợp lệ.");
            }

            if (basicsalary == null || basicsalary <= 0)
            {
                throw new ArgumentException("Lương cơ bản không hợp lệ. Vui lòng nhập giá trị lớn hơn 0.");
            }

            // Kiểm tra logic: Ngày vào làm phải lớn hơn ngày sinh
            if (dateofwork <= dateofbirth)
            {
                throw new ArgumentException("Ngày vào làm phải lớn hơn ngày sinh.");
            }

            string query = string.Format("USP_UpdateEmployee @staffid , @name , @sex , @dateofbirth , @address , @phone , @dateofwork , @basicsalary");
            int result = DataProvider.Instance.ExecuteNonQuery(query, new object[] { staffid, name, sex, dateofbirth, address, phone, dateofwork, basicsalary });

            return result > 0;
        }

        public List<EmployeeDTO> SearchEmployeeByName(string name)
        {
            List<EmployeeDTO> list = new List<EmployeeDTO>();

            string query = string.Format("USP_SearchEmployeeByName @name");

            DataTable data = DataProvider.Instance.ExecuteQuery(query, new object[] { name });

            foreach (DataRow item in data.Rows)
            {
                EmployeeDTO emp = new EmployeeDTO(item);
                list.Add(emp);
            }
            return list;
        }

        public List<EmployeeDTO> SearchEmployeeBySalary(double basicsalary)
        {
            List<EmployeeDTO> list = new List<EmployeeDTO>();

            string query = string.Format("USP_SearchEmployeeBySalary @basicsalary");

            DataTable data = DataProvider.Instance.ExecuteQuery(query, new object[] { basicsalary });

            foreach (DataRow item in data.Rows)
            {
                EmployeeDTO emp = new EmployeeDTO(item);
                list.Add(emp);
            }
            return list;
        }

        public List<EmployeeDTO> SearchEmployeeBySex(string sex)
        {
            List<EmployeeDTO> list = new List<EmployeeDTO>();

            string query = string.Format("USP_SearchEmployeeBySex @sex");

            DataTable data = DataProvider.Instance.ExecuteQuery(query, new object[] { sex });

            foreach (DataRow item in data.Rows)
            {
                EmployeeDTO emp = new EmployeeDTO(item);
                list.Add(emp);
            }
            return list;
        }

        public List<EmployeeDTO> SearchEmployeeBySalaryAndSex(double basicsalary, string sex)
        {
            List<EmployeeDTO> list = new List<EmployeeDTO>();

            string query = string.Format("USP_SearchEmployeeBySalaryAndSex @basicsalary , @sex");

            DataTable data = DataProvider.Instance.ExecuteQuery(query, new object[] { basicsalary, sex });

            foreach (DataRow item in data.Rows)
            {
                EmployeeDTO emp = new EmployeeDTO(item);
                list.Add(emp);
            }
            return list;
        }

        public void TakeTheSalary(LabelControl lbWorkShifts, LabelControl lbSalary, string staffid, double basicsalasy)
        {
            List<EmployeeDTO> list = new List<EmployeeDTO>();
            DateTime day = DateTime.Now;
            double salary;

            string query = string.Format("USP_GetEmployeeByStaffIDMonthYear @staffid , @month , @year");
            DataTable data = DataProvider.Instance.ExecuteQuery(query, new object[] { staffid, day.Month, day.Year });

            foreach (DataRow item in data.Rows)
            {
                EmployeeDTO emp = new EmployeeDTO(item);
                list.Add(emp);
            }

            lbWorkShifts.Text = "Tổng ca tháng " + day.Month + ": " + list.Count().ToString() + " ca";
            salary = list.Count() * basicsalasy;
            lbSalary.Text = "Tổng lương: " + string.Format(new CultureInfo("vi-VN"), "{0:#,##0} VND", salary);
        }
    }
}
