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

namespace QuanLiChiTieu
{
   
    public partial class Form1 : Form
    {
        public static  string ConnectionString = Class1.ConnectionString;
        public Form1()
        {
            InitializeComponent();
        }
        public static int UID;
       void GetInfo()
       {
            string queryCommand = "SELECT dbo.GetUserName('"+UID+"')";
            string queryCommand1 = "SELECT dbo.GetSoDu('" + UID + "')";
            dataGridView1.DataSource = GetSaoKe().Tables[0];

            using (SqlConnection conn = new SqlConnection(ConnectionString))
            using (SqlCommand cmd = new SqlCommand(queryCommand, conn))
            using (SqlCommand cmd1 = new SqlCommand(queryCommand1, conn))
            {
                conn.Open();
                username_tx.Text = cmd.ExecuteScalar().ToString();
                sodu.Text = cmd1.ExecuteScalar().ToString();
                conn.Close();   
            }
            DataSet GetSaoKe()
            {
               
                DataSet SaoKe = new DataSet();
                string query = "GetSaoKe " + UID + "," + 2 + "";
                using (SqlConnection conn = new SqlConnection(ConnectionString))
                {
                    conn.Open();
                    SqlDataAdapter BangSaoKe = new SqlDataAdapter(query, conn);
                    BangSaoKe.Fill(SaoKe);
                    conn.Close();
                }

                return SaoKe;
            }

       }
       private void Form1_Load(object sender, EventArgs e)
        {
            new Form2().ShowDialog();
            if(UID == -1)
            {
                this.Close();
            }
            GetInfo();


            
        }
        public static int ModeInput;
        private void nap_bt_Click(object sender, EventArgs e)
        {
            ModeInput = 1;
            new Form3().ShowDialog();
            GetInfo();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            Form1_Load(sender, e);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            ModeInput = 0;
            new Form3().ShowDialog();
            GetInfo();

        }

        private void button3_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Hiện tại tính năng này đang phát triển!"); 
        }
    }
}
