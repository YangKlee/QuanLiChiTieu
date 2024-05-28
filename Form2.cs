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

namespace QuanLiChiTieu
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            using (SqlConnection conn = new SqlConnection(Form1.ConnectionString))
            using (SqlCommand cmd = new SqlCommand("SELECT dbo.LoginFunc(N'"+username_tx.Text+"','"+password_tx.Text+"' )", conn))
            {
                conn.Open();
                var UID = cmd.ExecuteScalar();
                if ((int)UID != -1)
                {
                    MessageBox.Show("Đăng nhập thành công!");
                    Form1.UID = (int)UID;
                    this.Close();
                }    
                else
                    MessageBox.Show("Đăng nhập thất bại! Tài khoản mật khẩu khum hợp lệ");
                conn.Close();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string query = "AddUser N'" + username_tx.Text + "','" + password_tx.Text + "'";
            using (SqlConnection conn = new SqlConnection(Form1.ConnectionString))
            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                try
                {
                    conn.Open();
                    cmd.ExecuteScalar();
                    MessageBox.Show("Tạo User thành công, vui lòng đăng nhập lại");
                    conn.Close();
                }
                catch
                {
                    MessageBox.Show("Tạo User lỗi!");
                }

            }

        }

        private void Form2_Load(object sender, EventArgs e)
        {
            Form1.UID = -1;
        }
    }
}
