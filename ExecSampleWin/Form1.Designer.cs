namespace ExecSampleWin
{
    partial class Form1
    {
        /// <summary>
        /// 필수 디자이너 변수입니다.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 사용 중인 모든 리소스를 정리합니다.
        /// </summary>
        /// <param name="disposing">관리되는 리소스를 삭제해야 하면 true이고, 그렇지 않으면 false입니다.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form 디자이너에서 생성한 코드

        /// <summary>
        /// 디자이너 지원에 필요한 메서드입니다. 
        /// 이 메서드의 내용을 코드 편집기로 수정하지 마세요.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.txtDBIP = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtUSERID = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtUSERPW = new System.Windows.Forms.TextBox();
            this.btnDBTest = new System.Windows.Forms.Button();
            this.lbDBStatus = new System.Windows.Forms.Label();
            this.dtFromDate = new System.Windows.Forms.DateTimePicker();
            this.dtToDate = new System.Windows.Forms.DateTimePicker();
            this.label5 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.cmbSite = new System.Windows.Forms.ComboBox();
            this.btnSearch = new System.Windows.Forms.Button();
            this.label8 = new System.Windows.Forms.Label();
            this.txtXLSXPath = new System.Windows.Forms.TextBox();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.btnSelectPath = new System.Windows.Forms.Button();
            this.lbCSVStatus = new System.Windows.Forms.Label();
            this.lbTotCnt = new System.Windows.Forms.Label();
            this.btnSave = new System.Windows.Forms.Button();
            this.label11 = new System.Windows.Forms.Label();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.dtResultView = new System.Windows.Forms.DataGridView();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column7 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column8 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column9 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column10 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.chkRegDate = new System.Windows.Forms.CheckBox();
            this.grpDBInfo = new System.Windows.Forms.GroupBox();
            this.label6 = new System.Windows.Forms.Label();
            this.txtDBSID = new System.Windows.Forms.TextBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.cmbSYSID = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.dtSaveView = new System.Windows.Forms.DataGridView();
            this.Column11 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column12 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column14 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cmbSaveOption = new System.Windows.Forms.ComboBox();
            ((System.ComponentModel.ISupportInitialize)(this.dtResultView)).BeginInit();
            this.grpDBInfo.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtSaveView)).BeginInit();
            this.SuspendLayout();
            // 
            // txtDBIP
            // 
            this.txtDBIP.Location = new System.Drawing.Point(114, 16);
            this.txtDBIP.Name = "txtDBIP";
            this.txtDBIP.Size = new System.Drawing.Size(482, 21);
            this.txtDBIP.TabIndex = 0;
            this.txtDBIP.Text = "hmx-logisol-oracle-db-dev.c5ruasqdgoz7.ap-northeast-2.rds.amazonaws.com";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label1.ForeColor = System.Drawing.Color.DimGray;
            this.label1.Location = new System.Drawing.Point(4, 25);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 12);
            this.label1.TabIndex = 1;
            this.label1.Text = "DB IP";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label2.ForeColor = System.Drawing.Color.DimGray;
            this.label2.Location = new System.Drawing.Point(4, 82);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(59, 12);
            this.label2.TabIndex = 3;
            this.label2.Text = "USER ID";
            // 
            // txtUSERID
            // 
            this.txtUSERID.Location = new System.Drawing.Point(114, 73);
            this.txtUSERID.Name = "txtUSERID";
            this.txtUSERID.Size = new System.Drawing.Size(199, 21);
            this.txtUSERID.TabIndex = 2;
            this.txtUSERID.Text = "KY_K_POWDER";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label3.ForeColor = System.Drawing.Color.DimGray;
            this.label3.Location = new System.Drawing.Point(4, 114);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(66, 12);
            this.label3.TabIndex = 5;
            this.label3.Text = "USER PW";
            // 
            // txtUSERPW
            // 
            this.txtUSERPW.Location = new System.Drawing.Point(114, 105);
            this.txtUSERPW.Name = "txtUSERPW";
            this.txtUSERPW.Size = new System.Drawing.Size(199, 21);
            this.txtUSERPW.TabIndex = 4;
            this.txtUSERPW.Text = "12345";
            // 
            // btnDBTest
            // 
            this.btnDBTest.Location = new System.Drawing.Point(333, 106);
            this.btnDBTest.Name = "btnDBTest";
            this.btnDBTest.Size = new System.Drawing.Size(121, 23);
            this.btnDBTest.TabIndex = 6;
            this.btnDBTest.Text = "Connection Test";
            this.btnDBTest.UseVisualStyleBackColor = true;
            this.btnDBTest.Click += new System.EventHandler(this.btnDBTest_Click);
            // 
            // lbDBStatus
            // 
            this.lbDBStatus.AutoSize = true;
            this.lbDBStatus.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.lbDBStatus.ForeColor = System.Drawing.Color.Firebrick;
            this.lbDBStatus.Location = new System.Drawing.Point(488, 111);
            this.lbDBStatus.Name = "lbDBStatus";
            this.lbDBStatus.Size = new System.Drawing.Size(194, 12);
            this.lbDBStatus.TabIndex = 7;
            this.lbDBStatus.Text = "유효하지 않은 접속정보 입니다.";
            // 
            // dtFromDate
            // 
            this.dtFromDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtFromDate.Location = new System.Drawing.Point(93, 22);
            this.dtFromDate.Name = "dtFromDate";
            this.dtFromDate.Size = new System.Drawing.Size(154, 21);
            this.dtFromDate.TabIndex = 8;
            // 
            // dtToDate
            // 
            this.dtToDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtToDate.Location = new System.Drawing.Point(264, 22);
            this.dtToDate.Name = "dtToDate";
            this.dtToDate.Size = new System.Drawing.Size(187, 21);
            this.dtToDate.TabIndex = 9;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(248, 27);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(14, 12);
            this.label5.TabIndex = 10;
            this.label5.Text = "~";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(457, 24);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(42, 12);
            this.label7.TabIndex = 12;
            this.label7.Text = "• SITE";
            // 
            // cmbSite
            // 
            this.cmbSite.FormattingEnabled = true;
            this.cmbSite.Location = new System.Drawing.Point(505, 20);
            this.cmbSite.Name = "cmbSite";
            this.cmbSite.Size = new System.Drawing.Size(121, 20);
            this.cmbSite.TabIndex = 13;
            // 
            // btnSearch
            // 
            this.btnSearch.Location = new System.Drawing.Point(744, 60);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(74, 23);
            this.btnSearch.TabIndex = 14;
            this.btnSearch.Text = "조    회";
            this.btnSearch.UseVisualStyleBackColor = true;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label8.ForeColor = System.Drawing.Color.DimGray;
            this.label8.Location = new System.Drawing.Point(16, 168);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(71, 12);
            this.label8.TabIndex = 16;
            this.label8.Text = "XLSX 경로";
            // 
            // txtXLSXPath
            // 
            this.txtXLSXPath.Location = new System.Drawing.Point(108, 159);
            this.txtXLSXPath.Name = "txtXLSXPath";
            this.txtXLSXPath.ReadOnly = true;
            this.txtXLSXPath.Size = new System.Drawing.Size(199, 21);
            this.txtXLSXPath.TabIndex = 15;
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // btnSelectPath
            // 
            this.btnSelectPath.Location = new System.Drawing.Point(327, 160);
            this.btnSelectPath.Name = "btnSelectPath";
            this.btnSelectPath.Size = new System.Drawing.Size(121, 23);
            this.btnSelectPath.TabIndex = 17;
            this.btnSelectPath.Text = "불러오기";
            this.btnSelectPath.UseVisualStyleBackColor = true;
            this.btnSelectPath.Click += new System.EventHandler(this.btnSelectPath_Click);
            // 
            // lbCSVStatus
            // 
            this.lbCSVStatus.AutoSize = true;
            this.lbCSVStatus.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.lbCSVStatus.ForeColor = System.Drawing.Color.Firebrick;
            this.lbCSVStatus.Location = new System.Drawing.Point(482, 165);
            this.lbCSVStatus.Name = "lbCSVStatus";
            this.lbCSVStatus.Size = new System.Drawing.Size(204, 12);
            this.lbCSVStatus.TabIndex = 19;
            this.lbCSVStatus.Text = "xlsx 불러오기에 실패하였습니다.";
            // 
            // lbTotCnt
            // 
            this.lbTotCnt.AutoSize = true;
            this.lbTotCnt.Location = new System.Drawing.Point(775, 465);
            this.lbTotCnt.Name = "lbTotCnt";
            this.lbTotCnt.Size = new System.Drawing.Size(43, 12);
            this.lbTotCnt.TabIndex = 20;
            this.lbTotCnt.Text = "154 건 ";
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(762, 871);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(74, 23);
            this.btnSave.TabIndex = 21;
            this.btnSave.Text = "저장하기";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(13, 698);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(83, 12);
            this.label11.TabIndex = 23;
            this.label11.Text = "154 건 처리 중";
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(102, 692);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(100, 23);
            this.progressBar1.TabIndex = 24;
            // 
            // dtResultView
            // 
            this.dtResultView.AllowUserToAddRows = false;
            this.dtResultView.AllowUserToDeleteRows = false;
            this.dtResultView.AllowUserToResizeColumns = false;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dtResultView.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dtResultView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dtResultView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column1,
            this.Column2,
            this.Column3,
            this.Column4,
            this.Column5,
            this.Column6,
            this.Column7,
            this.Column8,
            this.Column9,
            this.Column10});
            this.dtResultView.Location = new System.Drawing.Point(3, 89);
            this.dtResultView.Name = "dtResultView";
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dtResultView.RowHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dtResultView.RowHeadersVisible = false;
            this.dtResultView.RowTemplate.DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.dtResultView.RowTemplate.Height = 23;
            this.dtResultView.Size = new System.Drawing.Size(815, 361);
            this.dtResultView.TabIndex = 25;
            // 
            // Column1
            // 
            this.Column1.HeaderText = "ID_T_DEVICE_MAP";
            this.Column1.Name = "Column1";
            this.Column1.ReadOnly = true;
            this.Column1.Visible = false;
            // 
            // Column2
            // 
            this.Column2.HeaderText = "LOCATION";
            this.Column2.Name = "Column2";
            this.Column2.ReadOnly = true;
            // 
            // Column3
            // 
            this.Column3.HeaderText = "SYS_ID";
            this.Column3.Name = "Column3";
            this.Column3.ReadOnly = true;
            // 
            // Column4
            // 
            this.Column4.HeaderText = "EQP_ID";
            this.Column4.Name = "Column4";
            this.Column4.ReadOnly = true;
            // 
            // Column5
            // 
            this.Column5.HeaderText = "TRACK_ID";
            this.Column5.Name = "Column5";
            this.Column5.ReadOnly = true;
            // 
            // Column6
            // 
            this.Column6.HeaderText = "CIM_IP";
            this.Column6.Name = "Column6";
            this.Column6.ReadOnly = true;
            // 
            // Column7
            // 
            this.Column7.HeaderText = "PLC_IP";
            this.Column7.Name = "Column7";
            this.Column7.ReadOnly = true;
            // 
            // Column8
            // 
            this.Column8.HeaderText = "TEST_DATE";
            this.Column8.Name = "Column8";
            this.Column8.ReadOnly = true;
            // 
            // Column9
            // 
            this.Column9.HeaderText = "TEST_TIME";
            this.Column9.Name = "Column9";
            this.Column9.ReadOnly = true;
            // 
            // Column10
            // 
            this.Column10.HeaderText = "CarrieriD";
            this.Column10.Name = "Column10";
            this.Column10.ReadOnly = true;
            // 
            // chkRegDate
            // 
            this.chkRegDate.AutoSize = true;
            this.chkRegDate.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.chkRegDate.ForeColor = System.Drawing.Color.DimGray;
            this.chkRegDate.Location = new System.Drawing.Point(8, 27);
            this.chkRegDate.Name = "chkRegDate";
            this.chkRegDate.Size = new System.Drawing.Size(81, 16);
            this.chkRegDate.TabIndex = 26;
            this.chkRegDate.Text = "조회 일시";
            this.chkRegDate.UseVisualStyleBackColor = true;
            // 
            // grpDBInfo
            // 
            this.grpDBInfo.BackColor = System.Drawing.Color.Transparent;
            this.grpDBInfo.Controls.Add(this.label6);
            this.grpDBInfo.Controls.Add(this.txtDBSID);
            this.grpDBInfo.Controls.Add(this.lbDBStatus);
            this.grpDBInfo.Controls.Add(this.label3);
            this.grpDBInfo.Controls.Add(this.txtUSERPW);
            this.grpDBInfo.Controls.Add(this.label2);
            this.grpDBInfo.Controls.Add(this.txtUSERID);
            this.grpDBInfo.Controls.Add(this.label1);
            this.grpDBInfo.Controls.Add(this.txtDBIP);
            this.grpDBInfo.Controls.Add(this.btnDBTest);
            this.grpDBInfo.Location = new System.Drawing.Point(15, 5);
            this.grpDBInfo.Name = "grpDBInfo";
            this.grpDBInfo.Size = new System.Drawing.Size(821, 140);
            this.grpDBInfo.TabIndex = 27;
            this.grpDBInfo.TabStop = false;
            this.grpDBInfo.Text = "DB Info";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label6.ForeColor = System.Drawing.Color.DimGray;
            this.label6.Location = new System.Drawing.Point(4, 52);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(101, 12);
            this.label6.TabIndex = 9;
            this.label6.Text = "ConnectionSID";
            // 
            // txtDBSID
            // 
            this.txtDBSID.Location = new System.Drawing.Point(114, 43);
            this.txtDBSID.Name = "txtDBSID";
            this.txtDBSID.Size = new System.Drawing.Size(199, 21);
            this.txtDBSID.TabIndex = 8;
            this.txtDBSID.Text = "OHMXDB";
            // 
            // groupBox2
            // 
            this.groupBox2.BackColor = System.Drawing.Color.Transparent;
            this.groupBox2.Controls.Add(this.cmbSYSID);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.chkRegDate);
            this.groupBox2.Controls.Add(this.dtResultView);
            this.groupBox2.Controls.Add(this.lbTotCnt);
            this.groupBox2.Controls.Add(this.btnSearch);
            this.groupBox2.Controls.Add(this.cmbSite);
            this.groupBox2.Controls.Add(this.label7);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.dtToDate);
            this.groupBox2.Controls.Add(this.dtFromDate);
            this.groupBox2.Location = new System.Drawing.Point(12, 193);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(824, 492);
            this.groupBox2.TabIndex = 28;
            this.groupBox2.TabStop = false;
            // 
            // cmbSYSID
            // 
            this.cmbSYSID.FormattingEnabled = true;
            this.cmbSYSID.Location = new System.Drawing.Point(697, 22);
            this.cmbSYSID.Name = "cmbSYSID";
            this.cmbSYSID.Size = new System.Drawing.Size(121, 20);
            this.cmbSYSID.TabIndex = 28;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(632, 25);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(54, 12);
            this.label4.TabIndex = 27;
            this.label4.Text = "• SYS ID";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.dtSaveView);
            this.groupBox3.Location = new System.Drawing.Point(15, 734);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(821, 118);
            this.groupBox3.TabIndex = 29;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "groupBox3";
            // 
            // dtSaveView
            // 
            this.dtSaveView.AllowUserToAddRows = false;
            this.dtSaveView.AllowUserToDeleteRows = false;
            this.dtSaveView.AllowUserToResizeColumns = false;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dtSaveView.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.dtSaveView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dtSaveView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column11,
            this.Column12,
            this.Column14});
            this.dtSaveView.Location = new System.Drawing.Point(-3, 21);
            this.dtSaveView.Name = "dtSaveView";
            this.dtSaveView.RowHeadersVisible = false;
            this.dtSaveView.RowTemplate.DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.dtSaveView.RowTemplate.Height = 23;
            this.dtSaveView.Size = new System.Drawing.Size(824, 91);
            this.dtSaveView.TabIndex = 0;
            // 
            // Column11
            // 
            this.Column11.HeaderText = "PLC IP";
            this.Column11.Name = "Column11";
            this.Column11.ReadOnly = true;
            // 
            // Column12
            // 
            this.Column12.HeaderText = "DATA";
            this.Column12.Name = "Column12";
            this.Column12.ReadOnly = true;
            // 
            // Column14
            // 
            this.Column14.HeaderText = "TRACK";
            this.Column14.Name = "Column14";
            this.Column14.ReadOnly = true;
            // 
            // cmbSaveOption
            // 
            this.cmbSaveOption.FormattingEnabled = true;
            this.cmbSaveOption.Location = new System.Drawing.Point(630, 874);
            this.cmbSaveOption.Name = "cmbSaveOption";
            this.cmbSaveOption.Size = new System.Drawing.Size(121, 20);
            this.cmbSaveOption.TabIndex = 1;
            this.cmbSaveOption.SelectedIndexChanged += new System.EventHandler(this.cmbSaveOption_SelectedIndexChanged);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(848, 908);
            this.Controls.Add(this.cmbSaveOption);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.grpDBInfo);
            this.Controls.Add(this.progressBar1);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.lbCSVStatus);
            this.Controls.Add(this.btnSelectPath);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.txtXLSXPath);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Form1";
            this.Text = "BOSK CSV Save Sample";
            ((System.ComponentModel.ISupportInitialize)(this.dtResultView)).EndInit();
            this.grpDBInfo.ResumeLayout(false);
            this.grpDBInfo.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dtSaveView)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtDBIP;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtUSERID;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtUSERPW;
        private System.Windows.Forms.Button btnDBTest;
        private System.Windows.Forms.Label lbDBStatus;
        private System.Windows.Forms.DateTimePicker dtFromDate;
        private System.Windows.Forms.DateTimePicker dtToDate;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.ComboBox cmbSite;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox txtXLSXPath;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.Button btnSelectPath;
        private System.Windows.Forms.Label lbCSVStatus;
        private System.Windows.Forms.Label lbTotCnt;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.DataGridView dtResultView;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column3;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column4;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column5;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column6;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column7;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column8;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column9;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column10;
        private System.Windows.Forms.CheckBox chkRegDate;
        private System.Windows.Forms.GroupBox grpDBInfo;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.ComboBox cmbSYSID;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.DataGridView dtSaveView;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column11;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column12;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column14;
        private System.Windows.Forms.ComboBox cmbSaveOption;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtDBSID;
    }
}

