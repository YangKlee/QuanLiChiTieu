using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuanLiChiTieu
{
    public partial class Form3 : Form
    {
        string ConnectionString = Class1.ConnectionString;
        public Form3()
        {
            InitializeComponent();
        }

        private void Form3_Load(object sender, EventArgs e)
        {
            if (Form1.ModeInput == 1)
                button1.Text = "Nạp";
            else if (Form1.ModeInput == 0)
                button1.Text = "Rút";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if(Form1.ModeInput == 1)
            {
                string query = "NapRutTien " + Form1.UID+ "," +1 +","+textBox1.Text+"," + "N'" + textBox2.Text + "'";
                using (SqlConnection conn = new SqlConnection(ConnectionString))
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    conn.Open();
                    cmd.ExecuteScalar();
                    conn.Close();
                }
                MessageBox.Show("Nạp tiền thành công!");
                this.Close();
            }
            else if(Form1.ModeInput == 0)
            {
                int sodu = 0;
                string queryCommand1 = "SELECT dbo.GetSoDu('" + Form1.UID + "')";
                string query = "NapRutTien " + Form1.UID + "," + 0 + "," + textBox1.Text + "," + "N'" + textBox2.Text + "'";
                using (SqlConnection conn = new SqlConnection(ConnectionString))
                using (SqlCommand cmd = new SqlCommand(queryCommand1, conn))
                {
                    conn.Open();
                    var raw_sd = cmd.ExecuteScalar();
                    conn.Close();
                    sodu = (int)raw_sd;
                }
                if(sodu >= Int64.Parse(textBox1.Text))
                {
                    using (SqlConnection conn = new SqlConnection(ConnectionString))
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        conn.Open();
                        cmd.ExecuteScalar();
                        conn.Close();
                    }
                    MessageBox.Show("Rút tiền thành công!");
                    this.Close();
                }
               else
                {
                    MessageBox.Show("Số dư hem đủ");
                }

            }
        }
    }
}
