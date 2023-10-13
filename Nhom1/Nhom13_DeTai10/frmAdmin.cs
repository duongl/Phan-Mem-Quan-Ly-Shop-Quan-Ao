using DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Nhom13_DeTai10
{
    public partial class frmAdmin : Form
    {
        SqlConnection con = new SqlConnection();
        SqlDataAdapter da_quanao;
        DataTable dt_quanao;
        DataView dtv_quanao;
        DataSet dts_quanao = new DataSet();
        DBConnect conn = new DBConnect();

        public frmAdmin()
        {
            string sql_ketnoi = @"Data Source=DESKTOP-C09DJSM;Initial Catalog=QL_ShopQuanAo;Integrated Security=True"; //123
            //string sql_ketnoi = @"Data Source =ADMIN-PC; Initial Catalog = QL_ShopQuanAo; User ID = sa; Password = sa2012"; //123
            con = new SqlConnection(sql_ketnoi);
            InitializeComponent();
        }

        #region Methods
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
            dtgvQuanAo.DataSource = dt; 
        }

        void LoadDS_KhachHang()
        {
            con.Open();
            string sql = "select * from KhachHang";
            SqlCommand com = new SqlCommand(sql, con);
            com.CommandType = CommandType.Text;
            SqlDataAdapter da = new SqlDataAdapter(com);
            DataTable dt = new DataTable();
            da.Fill(dt);
            con.Close();
            dtgvKH.DataSource = dt;
        }

        void LoadDS_TaiKhoan()
        {
            con.Open();
            string sql = "select * from QuanTriVien";
            SqlCommand com = new SqlCommand(sql, con);
            com.CommandType = CommandType.Text;
            SqlDataAdapter da = new SqlDataAdapter(com);
            DataTable dt = new DataTable();
            da.Fill(dt);
            con.Close();
            dtgvTaiKhoan.DataSource = dt;
        }

        void Load_top5kh()
        {
            con.Open();
            string sql = "EXEC sp_select_top5KhachHangcosolanmuahangnhieunhat";
            SqlCommand com = new SqlCommand(sql, con);
            com.CommandType = CommandType.Text;
            SqlDataAdapter da = new SqlDataAdapter(com);
            DataTable dt = new DataTable();
            da.Fill(dt);
            con.Close();
            dgvTop5kh.DataSource = dt;
        }

        void Load_top5spbc()
        {
            con.Open();
            string sql = "EXEC sp_select_sanphambanchaynhat";
            SqlCommand com = new SqlCommand(sql, con);
            com.CommandType = CommandType.Text;
            SqlDataAdapter da = new SqlDataAdapter(com);
            DataTable dt = new DataTable();
            da.Fill(dt);
            con.Close();
            dgvTop5spbc.DataSource = dt;
        }

        void Load_Doanhthutungthang() //sửa thành top loại sản phẩm bán chạy 
        {
            con.Open();
            string sql = "EXEC sp_select_Master_LoaiQA";
            SqlCommand com = new SqlCommand(sql, con);
            com.CommandType = CommandType.Text;
            SqlDataAdapter da = new SqlDataAdapter(com);
            DataTable dt = new DataTable();
            da.Fill(dt);
            con.Close();
            dgvDoanhthutungthang.DataSource = dt;
        }


        void TimKiem_QA()
        {
            con.Open();
            string sql = "select * from QuanAo where Ten_QA=N'" + txtTimQA.Text + "'";
            SqlCommand com = new SqlCommand(sql, con);
            com.CommandType = CommandType.Text;
            SqlDataAdapter da = new SqlDataAdapter(com);
            DataTable dt = new DataTable();
            da.Fill(dt);
            con.Close();
            dtgvQuanAo.DataSource = dt;
        }

        void TimKiem_QA2()
        {
            con.Open();
            string sql = "select * from QuanAo where GiaBan between'" + txtTimTuGiaQA.Text + "' and '" + txtTimDenGiaQA.Text + "' ";
            SqlCommand com = new SqlCommand(sql, con);
            com.CommandType = CommandType.Text;
            SqlDataAdapter da = new SqlDataAdapter(com);
            DataTable dt = new DataTable();
            da.Fill(dt);
            con.Close();
            dtgvQuanAo.DataSource = dt;
        }

        void TimKiem_KhachHang()
        {
            con.Open();
            string sql = "select * from KhachHang where SDT='" + txtKhachHang_TimKiem_TenSDT.Text + "'";
            SqlCommand com = new SqlCommand(sql, con);
            com.CommandType = CommandType.Text;
            SqlDataAdapter da = new SqlDataAdapter(com);
            DataTable dt = new DataTable();
            da.Fill(dt);
            con.Close();
            dtgvKH.DataSource = dt;
        }

        void TimKiem_KhachHang2()
        {
            con.Open();
            string sql = "select * from KhachHang where HoTen=N'" + txtKhachHang_TimKiem_TenSDT.Text + "'";
            SqlCommand com = new SqlCommand(sql, con);
            com.CommandType = CommandType.Text;
            SqlDataAdapter da = new SqlDataAdapter(com);
            DataTable dt = new DataTable();
            da.Fill(dt);
            con.Close();
            dtgvKH.DataSource = dt;
        }

        private void Reset()
        {
            txtID.Text = "";
            txtTK_TenDangNhap.Text = "";
            txtTK_MatKhau.Text = "";
            txtKTK.Text = "";
        }
        private void ResetQA()
        {
            txtIDQA.Text = "";
            txtTenQA.Text = "";
            txtSizeQA.Text = "";
            txtLoaiQA.Text = "";
            txtGiaBan.Text = "";
            txtSoLuong.Text = "";
            txtGhiChu.Text = "";
            txtTrangThai.Text = "";
            LoadDS_QuanAo();
        }

        public bool KTThongTin()
        {
            if (txtID.Text == "")
            {
                MessageBox.Show("Vui lòng nhập ID!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtID.Focus();
                return false;
            }
            if (txtTK_TenDangNhap.Text == "")
            {
                MessageBox.Show("Vui lòng nhập tên đăng nhập!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtTK_TenDangNhap.Focus();
                return false;
            }
            if (txtTK_MatKhau.Text == "")
            {
                MessageBox.Show("Vui lòng nhập mật khẩu!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtTK_MatKhau.Focus();
                return false;
            }
            return true;
        }

        public bool KTThongTinQA()
        {
            if (txtIDQA.Text == "")
            {
                MessageBox.Show("Vui lòng nhập ID Quần Áo!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtIDQA.Focus();
                return false;
            }
            if (txtTenQA.Text == "")
            {
                MessageBox.Show("Vui lòng nhập Tên Quần Áo!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtTenQA.Focus();
                return false;
            }
            if (txtLoaiQA.Text == "")
            {
                MessageBox.Show("Vui lòng nhập Loại Quần Áo!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtLoaiQA.Focus();
                return false;
            }
            if (txtSizeQA.Text == "")
            {
                MessageBox.Show("Vui lòng nhập Size Quần Áo!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtSizeQA.Focus();
                return false;
            }
            if (txtSoLuong.Text == "")
            {
                MessageBox.Show("Vui lòng nhập Số lượng Quần Áo!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtSoLuong.Focus();
                return false;
            }
            if (txtGhiChu.Text == "")
            {
                MessageBox.Show("Vui lòng nhập ghi chú!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtGhiChu.Focus();
                return false;
            }
            if (txtGiaBan.Text == "")
            {
                MessageBox.Show("Vui lòng nhập giá bán Quần Áo!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtGiaBan.Focus();
                return false;
            }
            if (txtTrangThai.Text == "")
            {
                MessageBox.Show("Vui lòng nhập trạng thái Quần Áo!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtTrangThai.Focus();
                return false;
            }
            return true;
        }
        #endregion

        #region Event
        private void frmAdmin_Load(object sender, EventArgs e)
        {
            LoadDS_QuanAo();

            LoadDS_KhachHang();

            LoadDS_TaiKhoan();

            Load_top5kh();

            Load_top5spbc();

            Load_Doanhthutungthang(); //sửa thành top loại sản phẩm bán chạy 
        }
        

        private void frmAdmin_FormClosing(object sender, FormClosingEventArgs e)
        {
            string msg = "Bạn có chắc chắn muốn thoát chương trình ?";
            var result = MessageBox.Show(msg, "Thoát", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result != DialogResult.Yes)
                e.Cancel = true;
        }
        private void btnTimQA_Click(object sender, EventArgs e)
        {
            if (txtTimQA.Text == string.Empty )
            {
                TimKiem_QA2();
            }
            else if (txtTimTuGiaQA.Text == string.Empty && txtTimDenGiaQA.Text == string.Empty)
            {
                TimKiem_QA();
            }
        }

        private void btnKhachHang_TaiLaiDS_Click(object sender, EventArgs e)
        {
            TimKiem_KhachHang();
            TimKiem_KhachHang2();
        }

        private void btnKhachHang_XoaBoLoc_Click(object sender, EventArgs e)
        {
            LoadDS_KhachHang();
            txtKhachHang_TimKiem_TenSDT.ResetText();
        }

        private void dtgvKH_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int numrow;
            numrow = e.RowIndex;
            txtKhachHang_Ten.Text = dtgvKH.Rows[numrow].Cells[0].Value.ToString();
            txtKhachHang_SDT.Text = dtgvKH.Rows[numrow].Cells[1].Value.ToString();
            txtKhachHang_DiaChi.Text = dtgvKH.Rows[numrow].Cells[2].Value.ToString();
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            Reset();
        }

        private void btnTK_Them_Click(object sender, EventArgs e)
        {
            if (KTThongTin())
            {
                try
                {
                    SqlCommand cmd = new SqlCommand();

                    cmd.CommandText = "SP_ThemTK";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@ID", SqlDbType.Int).Value = txtID.Text;
                    cmd.Parameters.Add("@TaiKhoan", SqlDbType.NVarChar).Value = txtTK_TenDangNhap.Text;
                    cmd.Parameters.Add("@MatKhau", SqlDbType.NVarChar).Value = txtTK_MatKhau.Text;
                    cmd.Parameters.Add("@KTK", SqlDbType.Int).Value = txtKTK.Text;
                    cmd.Connection = con;
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                    LoadDS_TaiKhoan();
                    Reset();
                    MessageBox.Show("Đã thêm mới tài khoản thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch
                {
                    MessageBox.Show("Thêm mới tài khoản thất bại", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        private void dtgvTaiKhoan_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int numrow;
            numrow = e.RowIndex;
            txtID.Text = dtgvTaiKhoan.Rows[numrow].Cells[0].Value.ToString();
            txtTK_TenDangNhap.Text = dtgvTaiKhoan.Rows[numrow].Cells[1].Value.ToString();
            txtTK_MatKhau.Text = dtgvTaiKhoan.Rows[numrow].Cells[2].Value.ToString();
            txtKTK.Text = dtgvTaiKhoan.Rows[numrow].Cells[3].Value.ToString();
        }

        private void btnTK_Xoa_Click(object sender, EventArgs e)
        {
            if (txtID.Text == "")
            {
                MessageBox.Show("Vui lòng nhập ID cần xóa", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtID.Focus();
            }
            else
                try
                {
                    SqlCommand cmd = new SqlCommand();
                    cmd.CommandText = "SP_XoaTaiKhoan";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@IDQTV", SqlDbType.NVarChar).Value = txtID.Text;
                    cmd.Connection = con;
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                    LoadDS_TaiKhoan();
                    Reset();
                    MessageBox.Show("Đã xóa tài khoản thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch
                {
                    MessageBox.Show("Xóa tài khoản thất bại", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
        }

        private void btnResetQA_Click(object sender, EventArgs e)
        {
            ResetQA();
            txtTimQA.ResetText();
            txtTimTuGiaQA.ResetText();
            txtTimDenGiaQA.ResetText();
        }

        private void dtgvQuanAo_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int numrow;
            numrow = e.RowIndex;
            txtIDQA.Text = dtgvQuanAo.Rows[numrow].Cells[0].Value.ToString();
            txtTenQA.Text = dtgvQuanAo.Rows[numrow].Cells[1].Value.ToString();
            txtSizeQA.Text = dtgvQuanAo.Rows[numrow].Cells[2].Value.ToString();
            txtLoaiQA.Text = dtgvQuanAo.Rows[numrow].Cells[3].Value.ToString();
            txtGiaBan.Text = dtgvQuanAo.Rows[numrow].Cells[4].Value.ToString();
            txtSoLuong.Text = dtgvQuanAo.Rows[numrow].Cells[5].Value.ToString();
            txtGhiChu.Text = dtgvQuanAo.Rows[numrow].Cells[6].Value.ToString();
            txtTrangThai.Text = dtgvQuanAo.Rows[numrow].Cells[7].Value.ToString();
        }

        private void btnThemQA_Click(object sender, EventArgs e)
        {
            if (KTThongTinQA())
            {
                try
                {
                    SqlCommand cmd = new SqlCommand();

                    cmd.CommandText = "SP_ThemQA";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@IDQA", SqlDbType.Int).Value = Convert.ToInt32(txtIDQA.Text);
                    cmd.Parameters.Add("@TENQA", SqlDbType.NVarChar).Value = txtTenQA.Text;
                    cmd.Parameters.Add("@Size", SqlDbType.NVarChar).Value = txtSizeQA.Text;
                    cmd.Parameters.Add("@IDLQA", SqlDbType.Int).Value = Convert.ToInt32(txtLoaiQA.Text);
                    cmd.Parameters.Add("@GiaBan", SqlDbType.Int).Value = Convert.ToInt32(txtGiaBan.Text);
                    cmd.Parameters.Add("@SoLuong", SqlDbType.Int).Value = Convert.ToInt32(txtSoLuong.Text);
                    cmd.Parameters.Add("@GhiChu", SqlDbType.NVarChar).Value = txtGhiChu.Text;
                    cmd.Parameters.Add("@TrangThai", SqlDbType.Int).Value = Convert.ToInt32(txtTrangThai.Text);
                    cmd.Connection = con;
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                    LoadDS_QuanAo();
                    Reset();
                    MessageBox.Show("Đã thêm mới sản phẩm thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch
                {
                    MessageBox.Show("Thêm mới sản phẩm thất bại", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        private void btnXoaQA_Click(object sender, EventArgs e)
        {
            if (txtIDQA.Text == "")
            {
                MessageBox.Show("Vui lòng nhập ID cần xóa", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtIDQA.Focus();
            }
            else
                try
                {
                    SqlCommand cmd = new SqlCommand();
                    cmd.CommandText = "SP_XoaQuanAo";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@IDQA", SqlDbType.NVarChar).Value = txtIDQA.Text;
                    cmd.Connection = con;
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                    LoadDS_QuanAo();
                    Reset();
                    MessageBox.Show("Đã xóa tài khoản thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch
                {
                    MessageBox.Show("Xóa tài khoản thất bại", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
        }

        private void btnSuaQA_Click(object sender, EventArgs e)
        {
            if (txtIDQA.Text == "")
            {
                MessageBox.Show("Vui lòng nhập ID cần sửa", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtIDQA.Focus();
            }
            else if (KTThongTinQA())
            {
                try
                {
                    SqlCommand cmd = new SqlCommand();

                    cmd.CommandText = "SP_SuaQuanAo";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@IDQA", SqlDbType.Int).Value = Convert.ToInt32(txtIDQA.Text);
                    cmd.Parameters.Add("@TENQA", SqlDbType.NVarChar).Value = txtTenQA.Text;
                    cmd.Parameters.Add("@Size", SqlDbType.NVarChar).Value = txtSizeQA.Text;
                    cmd.Parameters.Add("@IDLQA", SqlDbType.Int).Value = Convert.ToInt32(txtLoaiQA.Text);
                    cmd.Parameters.Add("@GiaBan", SqlDbType.Int).Value = Convert.ToInt32(txtGiaBan.Text);
                    cmd.Parameters.Add("@SoLuong", SqlDbType.Int).Value = Convert.ToInt32(txtSoLuong.Text);
                    cmd.Parameters.Add("@GhiChu", SqlDbType.NVarChar).Value = txtGhiChu.Text;
                    cmd.Parameters.Add("@TrangThai", SqlDbType.Int).Value = Convert.ToInt32(txtTrangThai.Text);
                    cmd.Connection = con;
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                    LoadDS_QuanAo();
                    Reset();
                    MessageBox.Show("Đã sửa sản phẩm thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch
                {
                    MessageBox.Show("Sửa tài khoản thất bại", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        #endregion

    }
}
