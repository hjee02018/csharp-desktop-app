using System;
using System.Collections.Generic;
//using System.Data.OracleClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
//using Sample2.GlobalClass;
using Oracle.ManagedDataAccess.Client;
using Newtonsoft.Json;
using Oracle.ManagedDataAccess.Types;

namespace Sample2.View
{
    public partial class LoginView : Window
    {
        public LoginView()
        {
            InitializeComponent();
        }

        private void btn_Login_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // 사용자가 입력한 값 가져오기
                string USER_ID = txtUserId.Text.Trim().ToString();
                string PWD = txtPassword.Password.Trim().ToString();

                using (OracleConnection connection = new OracleConnection(GlobalClass.OracleConnectionString))
                {
                    connection.Open();

                    using (OracleCommand command = new OracleCommand("HMX_KCTC.P_LOG_IN_JH", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        // 입력 매개변수 설정
                        string jsonInput = JsonConvert.SerializeObject(new {USER_ID, PWD });
                        command.Parameters.Add("pv_json_i", OracleDbType.Varchar2).Value = jsonInput;

                        // 출력 매개변수 설정
                        command.Parameters.Add("pv_json_o", OracleDbType.Varchar2, 4000, "", ParameterDirection.Output);
                        command.Parameters.Add("pv_retcod_o", OracleDbType.Int32, ParameterDirection.Output);
                        command.Parameters.Add("pv_retmsg_o", OracleDbType.Varchar2, 4000, "", ParameterDirection.Output);


                        // 프로시저 실행(JH)
                        
                        command.ExecuteNonQuery();

                        // 결과 값 확인
                        int retCode = command.Parameters["pv_retcod_o"].Value != null ? ((OracleDecimal)command.Parameters["pv_retcod_o"].Value).ToInt32() : -1;
                        string retMessage = command.Parameters["pv_retmsg_o"].Value != null ? command.Parameters["pv_retmsg_o"].Value.ToString() : "No message";


                        // 로그인 성공
                        if (retCode == 0)
                        {
                            MessageBox.Show($"{USER_ID} 로그인 성공: {retMessage}");

                            // MainWindow.xaml 로드 & 로그인 정보 갱신
                            GlobalClass.lg_userId = USER_ID;
                            GlobalClass.lg_userPw = PWD;
                            GlobalClass.lg_time = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

                            MainWindow mainWindow = new MainWindow();
                            //mainWindow.Show();
                            this.Close();
                        }
                        // 로그인 실패
                        else
                        {
                            MessageBox.Show($"로그인 실패: {retMessage} ; {retCode}","경고");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"오류 발생: {ex.Message}");
            }
        }
    }
}
