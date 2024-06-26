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

namespace Sample3
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        private void btnLogin_Click(object sender, EventArgs e)
        {
            string userId = txtUserID.Text;
            string password = txtPassword.Text;

            // 데이터베이스 연결 문자열 가져오기
            string connectionString = ConfigurationManager.ConnectionStrings["MyConnectionString"].ConnectionString;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();

                    // 로그인 프로시저 호출
                    using (SqlCommand cmd = new SqlCommand("YourLoginProcedure", connection))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        // 로그인 프로시저에 전달할 매개변수 설정
                        cmd.Parameters.AddWithValue("@UserID", userId);
                        cmd.Parameters.AddWithValue("@Password", password);

                        // 프로시저 실행
                        SqlDataReader reader = cmd.ExecuteReader();

                        if (reader.HasRows)
                        {
                            // 로그인 성공
                            MessageBox.Show("로그인 성공!");
                        }
                        else
                        {
                            // 로그인 실패
                            MessageBox.Show("로그인 실패!");
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("에러 발생: " + ex.Message);
                }
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
