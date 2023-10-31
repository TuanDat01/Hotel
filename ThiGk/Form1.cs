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
using System.Data.Sql;

namespace ThiGk
{
    public partial class Form1 : Form
    {
        SqlConnection con;
        DataTable dt;
        SqlDataAdapter da;
        SqlCommand cmd;
        public Form1()
        {
            InitializeComponent();
        }
        public void loadData()
        {
            con = new SqlConnection();
            con.ConnectionString = Properties.Settings.Default.QLVT;
            string sql = "select * from VATTU";
            dt = new DataTable();
            con.Open();
            da = new SqlDataAdapter(sql, con);
            da.Fill(dt);
            dataGridView1.DataSource = dt;
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            loadData();
        }

        private void btn_them_Click(object sender, EventArgs e)
        {
            cmd = new SqlCommand("Them_VatTu", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@MaVT", SqlDbType.Char).Value = txt_ma.Text.Trim();
            cmd.Parameters.Add("@TenVT", SqlDbType.NVarChar).Value = txt_ten.Text.Trim();
            cmd.Parameters.Add("@NhaSX",SqlDbType.NChar).Value = txt_nhavattu.Text.Trim();
            cmd.Parameters.Add("@NgaySX", SqlDbType.DateTime).Value = Convert.ToDateTime(dt_ngaysx.Text);
            cmd.Parameters.Add("@HanSD", SqlDbType.Int).Value = Convert.ToInt32(txt_han.Text);
            cmd.Parameters.Add("@SoLuong", SqlDbType.Int).Value = Convert.ToInt32(txt_soluong.Text);
            cmd.Parameters.Add("@DonGia", SqlDbType.Float).Value = Convert.ToSingle(txt_dongia.Text);
            int count = cmd.ExecuteNonQuery();
            if (count>0)
            {
                MessageBox.Show("Thêm thành công");
                loadData();
            }
            else
            {
                MessageBox.Show("Thêm thất bại");
            }    
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex>=0 && e.ColumnIndex>=0)
            {
                txt_ma.Text = dataGridView1.CurrentRow.Cells["MaVT"].Value.ToString();
                txt_ten.Text = dataGridView1.CurrentRow.Cells["TENVATTU"].Value.ToString();
                txt_nhavattu.Text = dataGridView1.CurrentRow.Cells["NHASX"].Value.ToString();
                string dateString = dataGridView1.CurrentRow.Cells["NGAYSX"].Value.ToString();
                DateTime dateTimeValue;
                if (DateTime.TryParse(dateString, out dateTimeValue))
                {
                    dt_ngaysx.Value = dateTimeValue;
                }
                txt_han.Text = dataGridView1.CurrentRow.Cells["HANSD"].Value.ToString();
                txt_soluong.Text = dataGridView1.CurrentRow.Cells["SOLUONG"].Value.ToString();
                txt_dongia.Text = dataGridView1.CurrentRow.Cells["DONGIA"].Value.ToString();
            }
        }

        private void btn_sua_Click(object sender, EventArgs e)
        {
            cmd = new SqlCommand("Sua_VatTu", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@MaVT", SqlDbType.Char).Value = txt_ma.Text.Trim();
            cmd.Parameters.Add("@TenVT", SqlDbType.NVarChar).Value = txt_ten.Text.Trim();
            cmd.Parameters.Add("@NhaSX", SqlDbType.NChar).Value = txt_nhavattu.Text.Trim();
            cmd.Parameters.Add("@NgaySX", SqlDbType.DateTime).Value = Convert.ToDateTime(dt_ngaysx.Text);
            cmd.Parameters.Add("@HanSD", SqlDbType.Int).Value = Convert.ToInt32(txt_han.Text);
            cmd.Parameters.Add("@SoLuong", SqlDbType.Int).Value = Convert.ToInt32(txt_soluong.Text);
            cmd.Parameters.Add("@DonGia", SqlDbType.Float).Value = Convert.ToSingle(txt_dongia.Text);
            int count = cmd.ExecuteNonQuery();
            if (count > 0)
            {
                MessageBox.Show("Sua thành công");
                loadData();
            }
            else
            {
                MessageBox.Show("Sua thất bại");
            }
        }

        private void btn_xoa_Click(object sender, EventArgs e)
        {
            cmd = new SqlCommand("Xoa_VatTu", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@MatVT", SqlDbType.Char).Value = txt_ma.Text.Trim();
            int count = cmd.ExecuteNonQuery();
            if (count > 0)
            {
                MessageBox.Show("Xóa thành công");
                txt_ma.ResetText();
                txt_ten.ResetText();
                txt_dongia.ResetText();
                txt_han.ResetText();
                txt_nhavattu.ResetText();
                txt_soluong.ResetText();
                dt_ngaysx.ResetText();
                txt_ma.Focus();
                loadData();
            }
            else
            {
                MessageBox.Show("Xóa thất bại");
            }
        }
        public int timKiem(string ma)
        {
            cmd = new SqlCommand("TimKiem", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@MaVT", SqlDbType.Char).Value = ma;
            return cmd.ExecuteNonQuery();
        }

        private void btn_thoat_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Bạn có muốn thoát không?", "Xác nhận thoát", MessageBoxButtons.OKCancel,MessageBoxIcon.Warning);

  
            if (result == DialogResult.OK)
            {

                this.Close();
            }
            else
            {
               
            }
        }

    }
}
