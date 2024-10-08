using DevExpress.Office.PInvoke;
using DevExpress.XtraEditors;
using DevExpress.XtraRichEdit.Import.OpenXml;
using QL_QuanAn.DAO;
using QL_QuanAn.DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QL_QuanAn
{
    public partial class ChangePassWord : DevExpress.XtraEditors.XtraForm
    {
        #region Mothod
        Notification notitfy = new Notification(); //Thông báo
        public AccountDTO loginAcc; //Constructor để nhận thông tin tài khoản từ form Home
        public AccountDTO LoginAcc
        {
            get { return loginAcc; }
            set
            {
                loginAcc = value;
            }
        }
        public string Check { get; set; }
        #endregion Method


        public ChangePassWord(AccountDTO acc)
        {
            InitializeComponent();
            this.ControlBox = false;
            this.LoginAcc = acc;
        }

        #region EV_ResetPassWord
        private void btnChangePW_Click(object sender, EventArgs e)
        {
            string userName = loginAcc.UserName;
            string passwordold = txbPassWordOld.Text.Trim();
            string passwordnew = txbPassWordNew.Text.Trim();
            string prepasswordnew = txbPrePassWorkNew.Text.Trim();

            if (string.IsNullOrEmpty(passwordold))
            {
                notitfy.Show("Vui lòng nhập mật khẩu cũ!!!");
                return;
            }

            if (string.IsNullOrEmpty(passwordnew))
            {
                notitfy.Show("Vui lòng nhập mật khẩu mới!!!");
                return;
            }

            if (string.IsNullOrEmpty(prepasswordnew))
            {
                notitfy.Show("Vui lòng nhập lại mật khẩu mới!!!");
                return;
            }

            if (passwordnew != prepasswordnew)
            {
                notitfy.Show("Mật khẩu mới và nhập lại mật khẩu không giống nhau!!!");
                return;
            }

            MessageBoxYesNo msgyn = new MessageBoxYesNo();
            msgyn.Notify = "Bạn có muốn đổi mật khẩu không?";
            msgyn.ShowDialog();
            msgyn.Hide();
            Check = msgyn.Check;

            if (Check == "Có")
            {
                try
                {
                    // Kiểm tra mật khẩu cũ có đúng không
                    if (loginAcc.PassWord != AccountDAO.Instance.HasPassWord(passwordold))
                    {
                        notitfy.Show("Mật khẩu cũ không đúng!!!");
                    }
                    else
                    {
                        // Thực hiện thay đổi mật khẩu
                        AccountDAO.Instance.ChangePassWord(userName, passwordold, passwordnew, prepasswordnew);
                        notitfy.Show("Đổi mật khẩu thành công!!!");

                        // Xóa các trường nhập liệu
                        txbPassWordOld.Clear();
                        txbPassWordNew.Clear();
                        txbPrePassWorkNew.Clear();
                    }
                }
                catch (Exception ex)
                {
                    notitfy.Show($"Đã có lỗi xảy ra: {ex.Message}");
                }
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            MessageBoxYesNo msgyn = new MessageBoxYesNo();
            msgyn.Notify = "Bạn có muốn thoát không?";
            this.Show();
            msgyn.ShowDialog();
            msgyn.Hide();
            Check = msgyn.Check;
            if (Check == "Có")
            {
                this.Close();
            }
        }
        #endregion EV_ResetPassWord
    }
}