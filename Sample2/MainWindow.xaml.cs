using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Threading;
using Newtonsoft.Json;
using Oracle.ManagedDataAccess.Client;
using Oracle.ManagedDataAccess.Types;
using static Sample2.GlobalClass;
using static Sample2.MainWindow;


namespace Sample2
{
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        private ObservableCollection<AuthItem> _authList { get; set; } = new ObservableCollection<AuthItem>();
        public ObservableCollection<UserData> _userList { get; set; } = new ObservableCollection<UserData>();

        private ObservableCollection<string> _roleTypeList;

        private string m_host = string.Empty;
        private string _currentDateTime;

        // 데이터베이스 접속 정보 -> GlobalClass
        //public const string OracleConnectionString = "User Id=dbadmin;Password=HyundaiSol2023!;Data Source=(DESCRIPTION=(ADDRESS_LIST=(ADDRESS=(PROTOCOL=TCP)(HOST=hmx-logisol-oracle-db-dev.c5ruasqdgoz7.ap-northeast-2.rds.amazonaws.com)(PORT=1521)))(CONNECT_DATA=(SERVER=DEDICATED)(SERVICE_NAME=OHMXDB)));";

        public event PropertyChangedEventHandler PropertyChanged;

        private void userlist_CellEditEnding(object sender, DataGridCellEditEndingEventArgs e)
        {
            var editedItem = e.Row.Item as UserData;
            if (editedItem != null)
            {
                editedItem.IsEdited = true;
            }
        }
        public class AuthItem
        {
            public int Order { get; set; }
            public string ROL_NME { get; set; }
            public string PGM_NME { get; set; }
            public string DIS_SEQ { get; set; }
            public string EXC_ATH { get; set; }
            public string INQ_ATH { get; set; }
            public string ADD_ATH { get; set; }
            public string DEL_ATH { get; set; }
            public string SAV_ATH { get; set; }
            public string XCL_ATH { get; set; }
            public string INI_ATH { get; set; }
        }


        public class UserData : INotifyPropertyChanged
        {
            private string _selectedRoleItem;
            private List<string> _roleTypeList;

            private bool _isEdited;
            public bool IsEdited
            {
                get { return _isEdited; }
                set
                {
                    if (_isEdited != value)
                    {
                        _isEdited = value;
                        OnPropertyChanged(nameof(IsEdited));
                    }
                }
            }
            public string ID_T_USER { get; set; }
            public int Order { get; set; }
            public string USER_ID { get; set; }

            private string _rolNme;
            public string ROL_NME
            {
                get { return _rolNme; }
                set
                {
                    if (_rolNme != value)
                    {
                        _rolNme = value;
                        OnPropertyChanged(nameof(ROL_NME));
                    }
                }
            }

            public string PWD { get; set; }
            public string NME { get; set; }
            public string REG_DATE { get; set; }
            public string REG_TIME { get; set; }

            public event PropertyChangedEventHandler PropertyChanged;

            protected virtual void OnPropertyChanged(string propertyName)
            {
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
            }
        }


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
        public ObservableCollection<string> RoleTypeList
        {
            get { return _roleTypeList; }
            set
            {
                _roleTypeList = value;
                OnPropertyChanged(nameof(RoleTypeList));
            }
        }


        public ObservableCollection<AuthItem> AuthList
        {
            get { return _authList; }
            set
            {
                if (_authList != value)
                {
                    _authList = value;
                    OnPropertyChanged();
                }
            }
        }

        public ObservableCollection<UserData> UserList
        {
            get { return _userList; }
            set
            {
                if (_userList != value)
                {
                    _userList = value;
                    OnPropertyChanged();
                }
            }
        }


        public MainWindow()
        {
            InitializeComponent();
            DataContext = this;
            loginId.Text = GlobalClass.lg_userId;
            loginTm.Text = GlobalClass.lg_time;
            DispatcherTimer timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += Timer_Tick;
            timer.Start();

        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            CurrentDateTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
        }

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private string getVal(string val)
        {
            return val == "1" ? "가능" : "불가능";
        }


        private void DataGrid_MouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            // 선택된 셀 가져오기
            var cellInfo = userlist.CurrentCell;

            if (cellInfo != null)
            {
                // 선택된 행 가져오기
                var selectedRow = (DataGridRow)userlist.ItemContainerGenerator.ContainerFromItem(userlist.SelectedItem);

                if (selectedRow != null)
                {
                    var selectedUserInfo = selectedRow.Item as UserData;
                    MessageBoxResult result = MessageBox.Show($"{selectedUserInfo.USER_ID}의 사용자 정보를 삭제하시겠습니까?", "확인", MessageBoxButton.YesNo, MessageBoxImage.Question);

                    
                    // 확인 버튼이 클릭되면 UI 스레드에서 삭제 작업 수행
                    if (result == MessageBoxResult.Yes)
                    {
                        // P_DELETE_USER_HJ 호출
                        try
                        {
                            using (OracleConnection connection = new OracleConnection(OracleConnectionString))
                            {
                                connection.Open();

                                string ID_T_USER = selectedUserInfo.ID_T_USER;
                                string USER_ID = selectedUserInfo.USER_ID;
                                //string PWD = selectedUserInfo.PWD;
                                //string ROL_NME = selectedUserInfo.ROL_NME;
                                //string NME = selectedUserInfo.NME;

                                /*
                                 *  P_UPDATE_USER_HJ 호출
                                 */
                                using (OracleCommand command = new OracleCommand("HMX_KCTC.P_DELETE_USER_HJ", connection))
                                {
                                    command.CommandType = CommandType.StoredProcedure;

                                    // 입력 매개변수 (JSON)
                                    string jsonInput = JsonConvert.SerializeObject(new { ID_T_USER, USER_ID });

                                    command.Parameters.Add("PV_JSON_I", OracleDbType.Varchar2).Value = jsonInput;

                                    // 출력 매개변수 설정
                                    command.Parameters.Add("PV_JSON_O", OracleDbType.Varchar2, 4000, "", ParameterDirection.Output);
                                    command.Parameters.Add("PV_RETCOD_O", OracleDbType.Int32, ParameterDirection.Output);
                                    command.Parameters.Add("PV_RETMSG_O", OracleDbType.Varchar2, 4000, "", ParameterDirection.Output);

                                    command.ExecuteNonQuery();

                                    int retCode = command.Parameters["PV_RETCOD_O"].Value != null ? ((OracleDecimal)command.Parameters["PV_RETCOD_O"].Value).ToInt32() : -1;
                                    string retMessage = command.Parameters["PV_RETMSG_O"].Value != null ? command.Parameters["PV_RETMSG_O"].Value.ToString() : "No message";


                                    if (retCode == 0)
                                    {
                                        MessageBox.Show($"{retMessage}", "알람");
                                        Application.Current.Dispatcher.Invoke(() =>
                                        {
                                            UserList.Remove((UserData)selectedRow.Item);
                                        });
                                    }
                                    else
                                        MessageBox.Show($"사용자 정보 삭제 실패 : {retMessage} ; {retCode}", "경고");

                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show($"Error: {ex.Message}", "삭제 오 류");
                        }
                    }
                }
            }
        }


        private void hostName_TextChanged(object sender, RoutedEventArgs e)
        {
            m_host = hostName.Text;
        }

        // 가능->1, 불가능->2 변환
        private string retVal(string val)
        {
            return val == "가능" ? "1" : "2";
        }

        /* 
         * 권한 정보 갱신
         */
        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            m_host = UserIDTextBlock.Text;
            try
            {
                using (OracleConnection connection = new OracleConnection(OracleConnectionString))
                {
                    connection.Open();

                    foreach (AuthItem authItem in AuthList)
                    {
                        // 1. Get ID_T_USER from T_USER table
                        string getUserIdQuery = $@"
                    SELECT ID_T_USER FROM HMX_KCTC.T_USER WHERE USER_ID = '{m_host}'";

                        string idTUser;
                        object idTUserObj;

                        using (OracleCommand getUserIdCommand = new OracleCommand(getUserIdQuery, connection))
                        {
                            idTUserObj = getUserIdCommand.ExecuteScalar();

                            if (idTUserObj != null)
                            {
                                idTUser = idTUserObj.ToString();
                            }
                            else
                            {
                                MessageBox.Show("No ID_T_USER found for the given USER_ID", "알림");
                                continue;
                            }
                        }

                        Random rand = new Random();
                        string idTAuthority = rand.Next().ToString("X");

                        byte[] rawGeneratedData;
                        using (OracleCommand generateRawDataCommand = new OracleCommand("SELECT SYS_GUID() FROM DUAL", connection))
                        {
                            rawGeneratedData = (byte[])generateRawDataCommand.ExecuteScalar();
                        }

                        string idTMenu = BitConverter.ToString(rawGeneratedData).Replace("-", "");

                        // 2. Insert into T_MENU table with the generated RAW data for ID_T_MENU
                        string insertMenuQuery = $@"
                    INSERT INTO HMX_KCTC.T_MENU (ID_T_MENU, ID_PARENTS_MENU, PGM_NME)
                    VALUES (HEXTORAW('{idTMenu}'), null, '{authItem.PGM_NME}')";

                        using (OracleCommand menuCommand = new OracleCommand(insertMenuQuery, connection))
                        {
                            menuCommand.ExecuteNonQuery();
                        }

                        // 3. Insert into T_AUTHORITY table
                        string sINQ = retVal(authItem.INQ_ATH);
                        string sEXC = retVal(authItem.EXC_ATH);
                        string sADD = retVal(authItem.ADD_ATH);
                        string sDEL = retVal(authItem.DEL_ATH);
                        string sSAV = retVal(authItem.SAV_ATH);
                        string sXCL = retVal(authItem.XCL_ATH);
                        string sINI = retVal(authItem.INI_ATH);


                        string insertAuthorityQuery = $@"
                    INSERT INTO HMX_KCTC.T_AUTHORITY (ID_T_AUTHORITY, ID_T_USER, ID_T_MENU, EXC_ATH, INQ_ATH, ADD_ATH, DEL_ATH, SAV_ATH, XCL_ATH, INI_ATH)
                    VALUES ('{idTAuthority}', :idTUser, HEXTORAW('{idTMenu}'), 
                            '{sEXC}', '{sINQ}', '{sADD}', 
                            '{sDEL}', '{sSAV}', '{sXCL}', '{sINI}')";

                        using (OracleCommand authorityCommand = new OracleCommand(insertAuthorityQuery, connection))
                        {
                            authorityCommand.Parameters.Add("idTUser", OracleDbType.Raw).Value = idTUserObj;
                            authorityCommand.ExecuteNonQuery();
                        }

                        // 4. Get ID_T_ROLE from T_USER_ROLE_MAPPING table
                        string getRoleIdQuery = $@"
                    SELECT ID_T_ROLE FROM HMX_KCTC.T_USER_ROLE_MAPPING WHERE ID_T_USER = :idTUser";

                        using (OracleCommand getRoleIdCommand = new OracleCommand(getRoleIdQuery, connection))
                        {
                            getRoleIdCommand.Parameters.Add("idTUser", OracleDbType.Raw).Value = idTUserObj;

                            object roleIdObj = getRoleIdCommand.ExecuteScalar();

                            if (roleIdObj != null)
                            {
                                string idTRole = roleIdObj.ToString();

                                // 5. Update ROL_NME in T_ROLE table
                                string updateRoleNameQuery = $@"
                            UPDATE HMX_KCTC.T_ROLE SET ROL_NME = '{roleName.Text}' WHERE ID_T_ROLE = :idTRole";

                                using (OracleCommand updateRoleNameCommand = new OracleCommand(updateRoleNameQuery, connection))
                                {
                                    updateRoleNameCommand.Parameters.Add("idTRole", OracleDbType.Raw).Value = idTRole;

                                    updateRoleNameCommand.ExecuteNonQuery();
                                }
                            }
                            else
                            {
                                MessageBox.Show("No ID_T_ROLE found for the given ID_T_USER", "알림");
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "업데이트 오류");
            }
        }


        private void UserList_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            AuthList.Clear();
            var selectedUserData = userlist.SelectedItem as UserData;

            if (selectedUserData != null)
            {
                try
                {
                    string userId = selectedUserData.USER_ID;
                    string roleName = selectedUserData.ROL_NME;
                    m_host = userId;
                    UserIDTextBlock.Text = userId;
                    RoleNameTextBlock.Text = roleName;

                    string idTUser = selectedUserData.ID_T_USER;

                    // Join T_ROLE, T_AUTHORITY, and T_MENU to get required information
                    string query = @"
                SELECT R.ROL_NME, M.PGM_NME, T.DIS_SEQ, A.EXC_ATH, A.INQ_ATH, A.ADD_ATH, A.DEL_ATH, A.SAV_ATH, A.XCL_ATH, A.INI_ATH
                FROM HMX_KCTC.T_ROLE R
                INNER JOIN HMX_KCTC.T_AUTHORITY A ON R.ID_T_ROLE = A.ID_T_ROLE
                INNER JOIN HMX_KCTC.T_MENU M ON A.ID_T_MENU = M.ID_T_MENU
                INNER JOIN HMX_KCTC.T_MENU T ON A.ID_T_MENU = T.ID_T_MENU
                WHERE R.ROL_NME = :roleName";

                    using (OracleConnection connection = new OracleConnection(OracleConnectionString))
                    {
                        connection.Open();

                        using (OracleCommand command = new OracleCommand(query, connection))
                        {
                            command.Parameters.Add("roleName", OracleDbType.Varchar2).Value = roleName;

                            using (OracleDataReader reader = command.ExecuteReader())
                            {
                                int order = 1;

                                while (reader.Read())
                                {
                                    AuthList.Add(new AuthItem
                                    {
                                        Order = order++,
                                        ROL_NME = reader["ROL_NME"].ToString(),
                                        PGM_NME = reader["PGM_NME"].ToString(),
                                        DIS_SEQ = reader["DIS_SEQ"].ToString(),
                                        EXC_ATH = getVal(reader["EXC_ATH"].ToString()),
                                        INQ_ATH = getVal(reader["INQ_ATH"].ToString()),
                                        ADD_ATH = getVal(reader["ADD_ATH"].ToString()),
                                        DEL_ATH = getVal(reader["DEL_ATH"].ToString()),
                                        SAV_ATH = getVal(reader["SAV_ATH"].ToString()),
                                        XCL_ATH = getVal(reader["XCL_ATH"].ToString()),
                                        INI_ATH = getVal(reader["INI_ATH"].ToString())
                                    });
                                }
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error: {ex.Message}", "Error");
                }
            }
        }




        /*
         * Func : 모든 사용자 권한 보기
         * 02.15 ~inggggggggggggggggggggv
         * admin -> useryn(1,2) 
         * 그외 -> yser_yn(1)만 출력
        */
        private void btn_showAllUser_Click(object sender, RoutedEventArgs e)
        {
            hostName.Text = "";
            try
            {
                using (OracleConnection connection = new OracleConnection(OracleConnectionString))
                {
                    connection.Open();

                    string getUserListQuery = Sample2.DB.Oracle.SelectAllValidUserInfo();

                    using (OracleCommand getUserListCommand = new OracleCommand(getUserListQuery, connection))
                    {
                        using (OracleDataReader reader = getUserListCommand.ExecuteReader())
                        {
                            UserList.Clear();
                            int order = 1;
                            while (reader.Read())
                            {
                                UserData userData = new UserData
                                {
                                    Order = order,
                                    ID_T_USER = ConvertRawToString(reader["ID_T_USER"]),
                                    USER_ID = reader["USER_ID"].ToString(),
                                    ROL_NME = reader["ROL_NME"].ToString(),
                                    PWD = reader["PWD"].ToString(),
                                    NME = reader["NME"].ToString(),
                                    REG_DATE = reader["REG_DATE"].ToString(),
                                    REG_TIME = reader["REG_TIME"].ToString(),
                                };

                                UserList.Add(userData);
                                order++;
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

        private string ConvertRawToString(object rawValue)
        {
            if (rawValue != DBNull.Value)
            {
                byte[] rawBytes = (byte[])rawValue;
                return BitConverter.ToString(rawBytes).Replace("-", "");
            }

            return null;
        }


        /*
         * 검색 버튼 클릭 이벤트
         */
        private void btn_showUser_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                using (OracleConnection connection = new OracleConnection(OracleConnectionString))
                {
                    connection.Open();

                    string getUserListQuery = Sample2.DB.Oracle.SelectUserInfo(m_host);

                    using (OracleCommand getUserListCommand = new OracleCommand(getUserListQuery, connection))
                    {
                        using (OracleDataReader reader = getUserListCommand.ExecuteReader())
                        {
                            // Check if there are rows in the result set
                            if (!reader.HasRows)
                            {
                                hostName.Text = "";
                                m_host = "";
                                MessageBox.Show("사용자를 찾을 수 없습니다.", "경고");
                                return;
                            }

                            UserList.Clear();
                            int order = 1;

                            while (reader.Read())
                            {
                                UserData userData = new UserData
                                {
                                    Order = order,
                                    ID_T_USER = reader["ID_T_USER"].ToString(),
                                    USER_ID = reader["USER_ID"].ToString(),
                                    ROL_NME = reader["ROL_NME"].ToString(),
                                    PWD = reader["PWD"].ToString(),
                                    NME = reader["NME"].ToString(),
                                    REG_DATE = reader["REG_DATE"].ToString(),
                                    REG_TIME = reader["REG_TIME"].ToString(),
                                };

                                UserList.Add(userData);
                                order++;
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


        private List<string> GetRoleTypeList(OracleConnection connection)
        {
            List<string> roleTypes = new List<string>();

            try
            {
                string getRoleTypesQuery = "SELECT DISTINCT ROL_NME FROM HMX_KCTC.T_ROLE";

                using (OracleCommand getRoleTypesCommand = new OracleCommand(getRoleTypesQuery, connection))
                {
                    using (OracleDataReader roleTypesReader = getRoleTypesCommand.ExecuteReader())
                    {
                        while (roleTypesReader.Read())
                        {
                            roleTypes.Add(roleTypesReader["ROL_NME"].ToString());
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error fetching role types: {ex.Message}", "데이터 로드 오류");
            }

            return roleTypes;
        }


        /*
        * 사용자 추가 버튼 클릭 이벤트
        */
        private void AddUser_Button_Click(object sender, RoutedEventArgs e)
        {
            UserPopup userPopup = new UserPopup();
            userPopup.Owner = this;
            userPopup.ShowDialog();

            string userID = userPopup.UserID;
            string userPW = userPopup.UserPW;
            string roleNM = userPopup.UserRole;

            // 필수값 입력 검사
            if (string.IsNullOrWhiteSpace(userID))
            {
                MessageBox.Show("필수값이 입력되지 않았습니다.", "경고");
            }
            else
            {
                MessageBox.Show($"계정 생성: {userID}", "알림");
                UserData userData = new UserData
                {
                    Order = UserList.Count + 1,
                    ID_T_USER = userID,
                    USER_ID = userID,
                    ROL_NME = roleNM,
                    PWD = userPW,
                };
                UserList.Add(userData);
            }

        }


        /*
         * 사용자 수정 저장 버튼 클릭 이벤트
         */
        private void btn_saveUser_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                using (OracleConnection connection = new OracleConnection(OracleConnectionString))
                {
                    connection.Open();

                    foreach (var item in userlist.Items)
                    {
                        //  수정된 행만을 검색
                        if (item is UserData userData && userData.IsEdited)
                        {
                            string ID_T_USER = userData.ID_T_USER;
                            string USER_ID = userData.USER_ID;
                            string PWD = userData.PWD;
                            string ROL_NME = userData.ROL_NME;
                            string NME = userData.NME;
                            //string REG_DATE = userData.REG_DATE;
                            //string REG_TIME = userData.REG_TIME;


                            /*
                             *  P_UPDATE_USER_HJ 호출
                             */
                            using (OracleCommand command = new OracleCommand("HMX_KCTC.P_UPDATE_USER_HJ", connection))
                            {
                                command.CommandType = CommandType.StoredProcedure;

                                // 입력 매개변수 (JSON)
                                string jsonInput = JsonConvert.SerializeObject(new { ID_T_USER, USER_ID, PWD, ROL_NME, NME });

                                command.Parameters.Add("PV_JSON_I", OracleDbType.Varchar2).Value = jsonInput;


                                // 출력 매개변수 설정
                                command.Parameters.Add("PV_JSON_O", OracleDbType.Varchar2, 4000, "", ParameterDirection.Output);
                                command.Parameters.Add("PV_RETCOD_O", OracleDbType.Int32, ParameterDirection.Output);
                                command.Parameters.Add("PV_RETMSG_O", OracleDbType.Varchar2, 4000, "", ParameterDirection.Output);


                                command.ExecuteNonQuery();


                                int retCode = command.Parameters["PV_RETCOD_O"].Value != null ? ((OracleDecimal)command.Parameters["PV_RETCOD_O"].Value).ToInt32() : -1;
                                string retMessage = command.Parameters["PV_RETMSG_O"].Value != null ? command.Parameters["PV_RETMSG_O"].Value.ToString() : "No message";


                                if (retCode == 0)
                                {
                                    MessageBox.Show($"{retMessage}", "알람");
                                    userData.IsEdited = false;
                                }
                                else
                                {
                                    MessageBox.Show($"사용자 정보 갱신 실패 : {retMessage} ; {retCode}", "경고");
                                    continue;
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "업데이트 오 류");
            }
        }



        // 로그인 이력조회
        private void btn_showHistory_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}