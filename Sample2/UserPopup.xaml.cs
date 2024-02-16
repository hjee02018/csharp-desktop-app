using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
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
using Sample2;

namespace Sample2
{
    /// <summary>
    /// UserPopup.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class UserPopup : Window
    {
        public string UserID { get; private set; }
        public string UserPW { get; private set; }
        public string UserPWR { get; private set; }
        public string UserRole { get; private set; }

        public const string OracleConnectionString = "User Id=dbadmin;Password=HyundaiSol2023!;Data Source=(DESCRIPTION=(ADDRESS_LIST=(ADDRESS=(PROTOCOL=TCP)(HOST=hmx-logisol-oracle-db-dev.c5ruasqdgoz7.ap-northeast-2.rds.amazonaws.com)(PORT=1521)))(CONNECT_DATA=(SERVER=DEDICATED)(SERVICE_NAME=OHMXDB)));";
        private RoleList roleListData;

        public UserPopup()
        {
            InitializeComponent();
            DataContext = this;
            LoadComboBoxData(); // LoadComboBoxData를 호출하여 데이터를 가져옴
        }

        class RoleList
        {
            public List<string> RoleListItems { get; set; } = new List<string>();
        }

        // 기능 드랍다운 추가
        private void LoadComboBoxData()
        {
            try
            {
                // 데이터베이스 연결
                using (OracleConnection connection = new OracleConnection(OracleConnectionString))
                {
                    connection.Open();

                    // 쿼리 실행
                    string query = Sample2.DB.Oracle.selectRoles();
                    using (OracleCommand command = new OracleCommand(query, connection))
                    {
                        using (OracleDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                // PGM_NME 값을 RoleListItems에 추가
                                string pgmName = reader["ROL_NME"].ToString();
                                ComboBoxItem menu = new ComboBoxItem { Content = pgmName };
                                menu.Width = 70;
                                roleList.Items.Add(menu);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "데이터 로드 오류");
            }
        }


        private void ConfirmButton_Click(object sender, RoutedEventArgs e)
        {
            // 확인 버튼 클릭 시 데이터를 설정하고 윈도우를 닫음
            UserID = UserIDTextBox.Text;
            UserPW = UserPWTextBox.Text;
            UserPWR = UserPWRTextBox.Text;
            
            // PW 검사
            if (UserPW != UserPWR)
            {
                MessageBox.Show("비밀번호가 일치하지 않습니다. 다시 입력해주세요.");
                return; // 창을 닫지 않고 함수를 종료합니다.
            }

            //UserRole = ;
            Close();
        }

        private void roleList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox comboBox = sender as ComboBox;
            ComboBoxItem selectedItem = comboBox.SelectedItem as ComboBoxItem;
            UserRole = selectedItem?.Content?.ToString();
            //UserRole = comboBox.SelectedItem?.ToString();
        }
    }
}
