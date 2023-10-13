using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using DTO;

namespace Nhom13_DeTai10
{
    public partial class frmLogin : Form
    {

        public frmLogin()
        {
            InitializeComponent();
        }

        bool CheckLogin(string userName, string passWord)
        {
            for (int i = 0; i < ListUser.Instances.ListAccountUser.Count; i++ )
            {
                if (userName == ListUser.Instances.ListAccountUser[i].UserName && passWord == ListUser.Instances.ListAccountUser[i].PassWord)
                {
                    Const.AccountType = ListUser.Instances.ListAccountUser[i].AccountType;

                    return true;
                }
            }

                return false;
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void frmLogin_FormClosing(object sender, FormClosingEventArgs e)
        {
            string msg = "Bạn có chắc chắn muốn thoát chương trình ?";
            var result = MessageBox.Show(msg, "Thoát", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result != DialogResult.Yes)
                e.Cancel = true;
        }

        private void cbShowPass_CheckedChanged(object sender, EventArgs e)
        {
            if(cbShowPass.Checked)
            {
                txtPassWord.UseSystemPasswordChar = false;
            }
            if(!cbShowPass.Checked)
            {
                txtPassWord.UseSystemPasswordChar = true;
            }
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            string userName = txtUserName.Text;
            string passWord = txtPassWord.Text;
            if(CheckLogin(userName, passWord))
            {
                frmQuanLy frmQL = new frmQuanLy();
                frmQL.Show();
                this.Hide();
                frmQL.Logout += F_Logout;
            }
            else if (txtUserName.Text == string.Empty)
            {
                MessageBox.Show("Vui lòng nhập tên tài khoản!!!", "Lỗi", MessageBoxButtons.OK);
                txtUserName.Focus();
                return;
            }
            else if(txtPassWord.Text == string.Empty)
            {
                MessageBox.Show("Vui lòng nhập mật khẩu!!!", "Lỗi", MessageBoxButtons.OK);
                txtUserName.Focus();
                return;
            }
            else if(txtUserName.Text== string.Empty && txtPassWord.Text == string.Empty)
            {
                MessageBox.Show("Vui lòng nhập tên tài khoản và mật khẩu!!!", "Lỗi", MessageBoxButtons.OK);
                txtUserName.Focus();
                return;
            }
            else
            {
                MessageBox.Show("Sai tên tài khoản hoặc mật khẩu!","Lỗi",MessageBoxButtons.OK);
                txtUserName.Focus();
                return;
            }
        }

        private void F_Logout(object sender, EventArgs e)
        {
            (sender as frmQuanLy).isExit = false;
            (sender as frmQuanLy).Close();
            this.Show();
        }

        private void frmLogin_Load(object sender, EventArgs e)
        {
            txtUserName.Focus();
        }

        private void txtPass_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter) 
                btnLogin_Click(sender, e);
        }

        private void frmLogin_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }
    }
}
