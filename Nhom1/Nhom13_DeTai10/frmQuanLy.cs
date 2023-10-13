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
    public delegate void delPassData(TextBox text);

    public partial class frmQuanLy : Form
    {
        SqlConnection con = new SqlConnection();
        SqlDataAdapter da_quanao;
        DataTable dt_quanao;
        DataView dtv_quanao;
        DataSet dts_quanao = new DataSet();
        DBConnect conn = new DBConnect();
        public bool isExit = true;

        public event EventHandler Logout;

        public frmQuanLy()
        {
            string sql_ketnoi = @"Data Source=DESKTOP-C09DJSM;Initial Catalog=QL_ShopQuanAo;Integrated Security=True"; //123
            //string sql_ketnoi = @"Data Source =ADMIN-PC; Initial Catalog = QL_ShopQuanAo; User ID = sa; Password = sa2012"; //123
            con = new SqlConnection(sql_ketnoi);
            InitializeComponent();
        }
     

        #region Method
        void Decentralization() // phân quyền
        {
            // false: Nhân Viên -> Khoá lại những gì mà nhân viên không có nghĩa vụ thực hiện
            // true: Admin -> Sẽ mở ra những nghiệp vụ mà chỉ có Admin có quyền
            if(Const.AccountType == false)
            {
                quảnLýTàiKhoảnToolStripMenuItem.Enabled  = false;
            }
        }

        void LoadDS_QuanAo()
        {
            con.Open();
            string sql = "select * from QuanAo";  
            SqlCommand com = new SqlCommand(sql, con);
            com.CommandType = CommandType.Text;
            SqlDataAdapter da = new SqlDataAdapter(com); 
            DataTable dt = new DataTable(); 
            da.Fill(dt);  
            con.Close(); 
            dgvQuanAo.DataSource = dt; 
        }

        void LoadDS_DonHang()
        {
            con.Open();
            string sql = "select * from BanHang";  
            SqlCommand com = new SqlCommand(sql, con); 
            com.CommandType = CommandType.Text;
            SqlDataAdapter da = new SqlDataAdapter(com); 
            DataTable dt = new DataTable(); 
            da.Fill(dt);  
            con.Close();  
            dgvCTDH.DataSource = dt; 
        }

        void TimKiem_QuanAo()
        {
            con.Open();
            string sql = "select * from QuanAo where Ten_QA=N'" + textBox1.Text + "'";
            SqlCommand com = new SqlCommand(sql, con);
            com.CommandType = CommandType.Text;
            SqlDataAdapter da = new SqlDataAdapter(com);
            DataTable dt = new DataTable();
            da.Fill(dt);
            con.Close();
            dgvQuanAo.DataSource = dt;
        }

        void LoadDS_HoaDon()
        {
            con.Open();
            string sql = "select * from HoaDon";
            SqlCommand com = new SqlCommand(sql, con);
            com.CommandType = CommandType.Text;
            SqlDataAdapter da = new SqlDataAdapter(com);
            DataTable dt = new DataTable();
            da.Fill(dt);
            con.Close();

        }

        public bool KTthongtinKH()
        {
            if(txtHoTen.Text == "")
            {
                MessageBox.Show("Vui lòng nhập Họ và tên!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtHoTen.Focus();
                return false;
            }
            if (txtDiaChi.Text == "")
            {
                MessageBox.Show("Vui lòng nhập địa chỉ!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtDiaChi.Focus();
                return false;
            }
            if(txtSDT.Text =="")
            {
                MessageBox.Show("Vui lòng nhập số điện thoại!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtSDT.Focus();
                return false;
            }
            return true;
        }

        private void ResetKH()
        {
            txtHoTen.Text = "";
            txtDiaChi.Text = "";
            txtSDT.Text = "";
        }

        
        #endregion

        #region Event
        private void frmQuanLy_FormClosed(object sender, FormClosedEventArgs e)
        {
            if(isExit)
            {
                Application.Exit();
            }
        }

        private void frmQuanLy_FormClosing(object sender, FormClosingEventArgs e)
        {
            //if(isExit)
            //{
                string msg = "Bạn có chắc chắn muốn thoát chương trình ?";
                var result = MessageBox.Show(msg, "Thoát", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result != DialogResult.Yes)
                //if (MessageBox.Show("Bạn có chắc chắn muốn thoát chương trình ?", "Thông Báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question) !=DialogResult.Yes)
                    e.Cancel = true;
            //}
        }

        private void đăngXuấtToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Logout(this, new EventArgs());
        }

        private void frmQuanLy_Load(object sender, EventArgs e)
        {
            LoadDS_DonHang();
            LoadDS_QuanAo();
            Decentralization();

        }

        private void tsmAdmin_Click(object sender, EventArgs e)
        {
            if(Const.AccountType == false)
            {
                MessageBox.Show("Bạn không phải là Admin !!", "Cảnh Báo");
                return;
            }
            else
            {
                frmAdmin frmAd = new frmAdmin();
                frmAd.ShowDialog();
            }
        }

        private void gameCaroToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GameCaro frmGameCaro = new GameCaro();
            frmGameCaro.ShowDialog();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            TimKiem_QuanAo();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            LoadDS_QuanAo();
            textBox1.ResetText();
        }

        private void dgvQuanAo_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int numrow;
            numrow = e.RowIndex;
            int tongtien = 0;
            tongtien = int.Parse(dgvQuanAo.Rows[numrow].Cells[4].Value.ToString()) * int.Parse(numericUpDown1.Value.ToString());

            lblTongTien.Text = tongtien.ToString();
            lblTongTienCuoiCung.Text = (tongtien - (tongtien * int.Parse(nmGiamGia.Value.ToString()) / 100)).ToString();
            lblTongTienGhiBangChu.Text = TienIch.So_chu(int.Parse(lblTongTienCuoiCung.Text.Replace(".", string.Empty)));
        }

        private void btnHuyDon_Click(object sender, EventArgs e)
        {
            LoadDS_QuanAo();
            lblTongTien.Text = "0";
            lblTongTienCuoiCung.Text = "0";
            lblTongTienGhiBangChu.Text = TienIch.So_chu(int.Parse(lblTongTienCuoiCung.Text.Replace(".", string.Empty)));
            ResetKH();
        }

        private void btnThanhToan_Click(object sender, EventArgs e)
        {
            if(KTthongtinKH())
            {
                try
                {
                    frmHoaDon frmHD = new frmHoaDon();
                    frmHD.funData(this.lblTongTien);
                    frmHD.funData2(this.lblTongTienCuoiCung);
                    frmHD.funData3(this.txtHoTen);
                    frmHD.funData4(this.txtSDT);
                    frmHD.funData5(this.txtDiaChi);
                    frmHD.Show();


                    SqlCommand cmd = new SqlCommand();
                    cmd.CommandText = "SP_ThemHD";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@HoTenKH", SqlDbType.NVarChar).Value = txtHoTen.Text;
                    cmd.Parameters.Add("@SDT", SqlDbType.Int).Value = txtSDT.Text;
                    cmd.Parameters.Add("@DiaChi", SqlDbType.NVarChar).Value = txtDiaChi.Text;
                    cmd.Parameters.Add("@TongTien", SqlDbType.Int).Value = lblTongTien.Text;
                    cmd.Parameters.Add("@TongTienSauGG", SqlDbType.Int).Value = lblTongTienCuoiCung.Text;
                    cmd.Connection = con;
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                    LoadDS_DonHang();
                    ResetKH();
                    MessageBox.Show("Đã thêm mới hoá đơn thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch
                {
                    MessageBox.Show("Thêm mới hoá đơn thất bại", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        #endregion


    }
}
