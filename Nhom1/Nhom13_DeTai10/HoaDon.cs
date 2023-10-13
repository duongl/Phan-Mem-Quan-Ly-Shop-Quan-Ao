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
    public partial class frmHoaDon : Form
    {
        public frmAdmin frmAd = new frmAdmin();
        SqlConnection con = new SqlConnection();
        SqlDataAdapter da_quanao;
        DataTable dt_quanao;
        DataView dtv_quanao;
        DataSet dts_quanao = new DataSet();
        DBConnect conn = new DBConnect();
        public frmHoaDon()
        {

            InitializeComponent();
            string sql_ketnoi = @"Data Source=DESKTOP-C09DJSM;Initial Catalog=QL_ShopQuanAo;Integrated Security=True"; //123
            //string sql_ketnoi = @"Data Source =ADMIN-PC; Initial Catalog = QL_ShopQuanAo; User ID = sa; Password = sa2012"; //123
            con = new SqlConnection(sql_ketnoi);
        }
        
        #region Methods
        public void funData(Label lblTongTien)
        {
            lbBill_TongTien.Text = lblTongTien.Text;
        }
        public void funData2(Label lblTongTienCuoiCung)
        {
            lbBill_ThanhToan.Text = lblTongTienCuoiCung.Text;
        }
        public void funData3(TextBox txtHoTen)
        {
            lbBill_TenKH.Text = txtHoTen.Text;
        }
        public void funData4(MaskedTextBox txtSDT)
        {
            lbBill_SDTKH.Text = txtSDT.Text;
        }
        public void funData5(TextBox txtDiaChi)
        {
            lbBill_DiaChiKH.Text = txtDiaChi.Text;
        }

        public void funDTGV(DataGridView btnThanhToan)
        {

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
            dtgvBill.DataSource = dt;
        }


        private void Reset()
        {
            LoadDS_HoaDon();
        }
        #endregion

        #region Event
        private void frmHoaDon_Load(object sender, EventArgs e)
        {
            LoadDS_HoaDon();
        }

        private void btnResetQA_Click(object sender, EventArgs e)
        {
            Reset();
        }
        #endregion

        

    }
}
