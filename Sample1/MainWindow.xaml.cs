using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.SqlClient;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Threading;

namespace Sample1
{
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        private const string ConnectionString = "Server=hmx-logisol-mssql-db-dev.c5ruasqdgoz7.ap-northeast-2.rds.amazonaws.com,1433;Database=HMX_CHEONGNA;User=dbadmin;Password=HyundaiSol2023!;Trusted_Connection=False;MultipleActiveResultSets=true;";
        private string m_host = string.Empty;
        private string _currentDateTime;

        public event PropertyChangedEventHandler PropertyChanged;

        public string CurrentDateTime
        {
            get { return _currentDateTime; }
            set
            {
                if (_currentDateTime != value)
                {
                    _currentDateTime = value;
                    OnPropertyChanged();
                }
            }
        }

        public MainWindow()
        {
            InitializeComponent();

            // 타이머를 사용하여 1초마다 현재 날짜 및 시간 업데이트
            DispatcherTimer timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += Timer_Tick;
            timer.Start();
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            // 현재 날짜 및 시간을 업데이트
            CurrentDateTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
        }

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private string getVal(string val)
        {
            if (val == "1")
                return "가능";
            else
                return "불가능";
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(ConnectionString))
                {
                    connection.Open();
                    // 예제 SELECT 쿼리
                    string query = "SELECT * FROM "; // 여기에 실제 테이블 이름을 입력하세요
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                // 예제: string value = reader["ColumnName"].ToString();
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "연결 오류");
            }
        }

        private void hostName_TextChanged(object sender, RoutedEventArgs e)
        {
            m_host = hostName.Text;
        }
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            try
            {
                // resultList 초기화: 비움
                resultList.Items.Clear();

                // m_host 변수에 저장된 값을 사용
                string userId = m_host;

                // 메시지 박스에 userId 표시
                MessageBox.Show($"{userId}의 권한 정보 확인", "알림");

                using (SqlConnection connection = new SqlConnection(ConnectionString))
                {
                    connection.Open();

                    // T_USER에서 ID_T_ROLE 및 ID_T_MENU와 함께 권한 정보를 가져옴
                    string query = $@"
                SELECT TU.ID_T_ROLE, TA.ID_T_MENU, TM.PGM_NME,
                       TA.EXC_ATH, TA.INQ_ATH, TA.ADD_ATH, TA.DEL_ATH, TA.SAV_ATH, TA.XCL_ATH, TA.INI_ATH
                FROM T_USER TU
                JOIN T_AUTHORITY TA ON TU.ID_T_ROLE = TA.ID_T_ROLE
                JOIN (SELECT DISTINCT ID_T_MENU, PGM_NME FROM T_MENU) TM ON TA.ID_T_MENU = TM.ID_T_MENU
                WHERE TU.USER_ID = '{userId}'";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                // 결과를 resultList 그리드에 추가
                                resultList.Items.Add(new
                                {
                                    //Order = resultList.Items.Count + 1, // 순서
                                    PGM_NME = reader["PGM_NME"].ToString(),
                                    EXC_ATH = getVal(reader["EXC_ATH"].ToString()),
                                    INQ_ATH = getVal(reader["INQ_ATH"].ToString()),
                                    ADD_ATH = getVal(reader["ADD_ATH"].ToString()),
                                    DEL_ATH = getVal(reader["DEL_ATH"].ToString()),
                                    SAV_ATH = getVal(reader["SAV_ATH"].ToString()),
                                    XCL_ATH = getVal(reader["XCL_ATH"].ToString()),
                                    INI_ATH = getVal(reader["INI_ATH"].ToString())
                                });

                                // T_ROLE에서 ID_T_ROLE에 해당하는 ROL_NME 가져오기
                                string roleId = reader["ID_T_ROLE"].ToString();
                                string queryRoleName = $"SELECT ROL_NME FROM T_ROLE WHERE ID_T_ROLE = '{roleId}'";

                                using (SqlCommand commandRoleName = new SqlCommand(queryRoleName, connection))
                                {
                                    object roleNameObj = commandRoleName.ExecuteScalar();
                                    if (roleNameObj != null)
                                    {
                                        string roleName = roleNameObj.ToString();

                                        // ROLE_NAME을 바인딩된 TextBlock에 표시
                                        RoleNameTextBlock.Text = $"{roleName}";
                                    }
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "연결 오류");
            }
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {

        }
    }
}
