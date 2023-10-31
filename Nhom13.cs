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
    // Nhóm em xóa bớt code để vừa đủ 1 trang A4

    public partial class Form1 : Form
    {
        SqlConnection con;   
        DataTable dt;        
        SqlDataAdapter da;   
        SqlCommand cmd;  

        public voit test
        {
            
        }    

        public Form1()
        {
            InitializeComponent();
        }

        // Tải dữ liệu khi thực hiện các hành động như thêm, sửa, xóa.
        public void loadData()
        {
            // Cách 1 khó sử dụng
            // con = new SqlConnection(Properties.Settings.Default.QLVT);
            // Thiết lập kết nối cách 2 dễ dàng sử dụng:
            con = new SqlConnection();
            con.ConnectionString = Properties.Settings.Default.QLVT;

            // Truy vấn SQL để lấy tất cả dữ liệu từ bảng 'VATTU'.
            string sql = "select * from VATTU";

            dt = new DataTable(); 

            con.Open(); 

            // Sử dụng SqlDataAdapter để thực thi truy vấn và điền DataTable với kết quả.
            da = new SqlDataAdapter(sql, con);
            da.Fill(dt);

            dataGridView1.DataSource = dt; 
        }

        // Tải dữ liệu khi form được mở.
        private void Form1_Load(object sender, EventArgs e)
        {
            loadData();
        }

        // Thực hiện chức năng 'Thêm' bằng lệnh SQL insert.
        private void btn_them_Click(object sender, EventArgs e)
        {
            // Truy vấn SQL để thêm một bản ghi mới vào bảng 'VT'. cách nối chuỗi là option 1
            string sql = $"insert into VT values ('{txt_ma.Text}',{txt_ten.Text})";
            // option 2
            // string sql = "insert into VT values ('"+txt_ma.Text+"','"+txt_ten.Text+"')";

            cmd = SqlCommand(sql, con); 

            cmd.ExecuteNonQuery(); 

            int count = cmd.ExecuteNonQuery();

            if (count > 0)
            {
                MessageBox.Show("Thêm thành công");
                loadData(); 
            }
            else
            {
                MessageBox.Show("Thêm thất bại");
            }
        }

        // Hàm để tìm kiếm một bản ghi theo mã sử dụng procedure trong sql.
        public int timKiem(string ma)
        {
            cmd = new SqlCommand("TimKiem", con); 
            cmd.CommandType = CommandType.StoredProcedure; 

            // Thêm một tham số vào thủ tục lưu trữ.
            cmd.Parameters.Add("@MaVT", SqlDbType.Char).Value = ma;

            return cmd.ExecuteNonQuery(); 
        }

        // Xử lý sự kiện click nút 'Thoát' .
        private void btn_thoat_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Bạn có muốn thoát không?", "Xác nhận thoát", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);

            if (result == DialogResult.OK)
            {
                this.Close(); 
            }
        }
    }
}
