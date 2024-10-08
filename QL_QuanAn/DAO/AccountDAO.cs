﻿using QL_QuanAn.DTO;
using QuanLyQuanCafe.DAO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace QL_QuanAn.DAO
{
    public class AccountDAO
    {
        private static AccountDAO instance;
        public static AccountDAO Instance
        {
            get
            {
                if (instance == null)
                    instance = new AccountDAO();
                return instance;
            }
            private set { instance = value; }
        }
        private AccountDAO() { }

        public string HasPassWord(string passWord)
        {
            byte[] temp = ASCIIEncoding.ASCII.GetBytes(passWord);
            byte[] hasData = new MD5CryptoServiceProvider().ComputeHash(temp);

            string hasPass = "";

            foreach (byte item in hasData)
            {
                hasPass += item;
            }
            return hasPass;
        }

        public bool Login(string userName, string passWord)
        {
            if (string.IsNullOrEmpty(userName) || string.IsNullOrEmpty(passWord))
            {
                throw new ArgumentException("Sai tên tài khoản hoặc mật khẩu.");
            }

            string query = "USP_Login @userName , @passWord";
            DataTable result = DataProvider.Instance.ExecuteQuery(query, new object[] { userName, HasPassWord(passWord) });

            if (result.Rows.Count > 0)
            {
                DataRow row = result.Rows[0];
                string status = row["TRANGTHAI"].ToString();

                // Kiểm tra trạng thái tài khoản
                if (status == "Hoạt động")
                {
                    return true; // Tài khoản đang hoạt động
                }
                else if (status == "Ðã xóa")
                {
                    throw new ArgumentException("Tài khoản đã bị xóa.");
                }
                else
                {
                    // Các trạng thái khác không hoạt động
                    throw new ArgumentException("Tài khoản không hoạt động.");
                }
            }

            // Trả về false khi không tìm thấy tài khoản phù hợp
            //return false;
            throw new ArgumentException("Sai tên tài khoản hoặc mật khẩu.");
        }


        public AccountDTO GetAccountByUserName(string userName)
        {
            string query = string.Format("USP_GetAccountByUserName @userName");
            DataTable data = DataProvider.Instance.ExecuteQuery(query, new object[] { userName });

            foreach (DataRow item in data.Rows)
            {
                return new AccountDTO(item);
            }
            return null;
        }

        public List<AccountDTO> GetListAccount()
        {
            List<AccountDTO> listacc = new List<AccountDTO>();

            string query = "USP_GetListAccount";
            DataTable data = DataProvider.Instance.ExecuteQuery(query);

            foreach (DataRow item in data.Rows)
            {
                AccountDTO acc = new AccountDTO(item);
                listacc.Add(acc);
            }
            return listacc;
        }

        public AccountDTO GetAccountByStaffID(string staffid)
        {
            AccountDTO account = null;

            string query = string.Format("USP_GetAccountByStaffID @staffid");
            DataTable data = DataProvider.Instance.ExecuteQuery(query, new object[] { staffid });

            foreach (DataRow item in data.Rows)
            {
                account = new AccountDTO(item);
                return account;
            }
            return account;
        }

        public bool InsertAccount(string userName, string staffid, string displayName, int permissionGroupId, string status)
        {
            string query = string.Format("USP_InsertAccount @userName , @staffid , @displayName , @permissionGroupId , @status");
            int result = DataProvider.Instance.ExecuteNonQuery(query, new object[] { userName, staffid, displayName, permissionGroupId, status });

            return result > 0;
        }

        public bool DeleteAccount(string userName)
        {
            string query = string.Format("USP_DeleteAccount @userName");
            int result = DataProvider.Instance.ExecuteNonQuery(query, new object[] { userName });

            return result > 0;
        }

        public bool UpdateAccount(int permissionGroupId, string status, string userName)
        {
            string query = string.Format("USP_UpdateAccount @permissionGroupId , @status , @userName");
            int result = DataProvider.Instance.ExecuteNonQuery(query, new object[] { permissionGroupId, status, userName });

            return result > 0;
        }

        public bool ResetPassWord(string userName)
        {
            string query = string.Format("USP_ResetPassWord @userName");
            int result = DataProvider.Instance.ExecuteNonQuery(query, new object[] { userName });

            return result > 0;
        }

        public bool ChangePassWord(string userName, string password, string newPassword, string prePasswordNew)
        {
            if (string.IsNullOrEmpty(password))
            {
                throw new ArgumentException("Mật khẩu cũ không để trống.");
            }

            if (string.IsNullOrEmpty(newPassword))
            {
                throw new ArgumentException("Mật khẩu mới trống.");
            }

            if (string.IsNullOrEmpty(prePasswordNew))
            {
                throw new ArgumentException("Nhập lại mật khẩu trống.");
            }

            if (newPassword != prePasswordNew)
            {
                throw new ArgumentException("Mật khẩu mới và nhập lại mật khẩu không giống nhau.");
            }

            // Kiểm tra mật khẩu cũ trong CSDL
            string checkPasswordQuery = "USP_CheckOldPassword @userName , @password";
            object resultCheck = DataProvider.Instance.ExecuteScalar(checkPasswordQuery, new object[] { userName, HasPassWord(password) });

            if (resultCheck == null || (int)resultCheck == 0) // Không có kết quả hoặc kết quả không hợp lệ
            {
                throw new ArgumentException("Mật khẩu không đúng.");
            }

            string query = string.Format("USP_ChangePassword @userName , @password , @newPassword");
            int result = DataProvider.Instance.ExecuteNonQuery(query, new object[] { userName, HasPassWord(password), HasPassWord(newPassword) });

            return result > 0;
        }

        public bool DeleteAccountByStaffID(string staffID)
        {
            string query = string.Format("USP_DeleteAccountByStaffID @staffid");
            int result = DataProvider.Instance.ExecuteNonQuery(query, new object[] { staffID });

            return result > 0;
        }

        public int GetPermissionGroupId(string permission)
        {
            string query = "EXEC USP_GetPermissionGroupId @permission";

            return Int32.Parse(DataProvider.Instance.ExecuteScalar(query, new object[] { permission }).ToString());
        }

        public string GetPermissionGroupById(int id)
        {
            return DataProvider.Instance.ExecuteScalar("EXEC USP_GetPermissionGroupById @id", new object[] {id}).ToString();
        }
    }
}
