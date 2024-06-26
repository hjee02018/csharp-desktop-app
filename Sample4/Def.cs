using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Collections.Concurrent;
using System.Data;
using System.Drawing;

namespace Sample4
{
    //class Def
    //{
    //}

    /// <summary>
    /// 공유메모리 전역 변수 선언 (WHS CNV STC AGV RTV)
    /// 
    /// </summary>

    
    public struct stWarehouseInfo
    {
        public bool enable;

        public override string ToString()
        {
            return base.ToString();
        }
    }

    /// <summary>
    /// 컨베이어 공유메모리 변수
    /// </summary>
    public struct stConveyorInfo
    {
        public int cnvNo;
        public bool opMode;
        public bool doorStatus;
        public bool runStatus;
        public string mode;
        public bool opEmergency;
        public bool timeOver;
        public bool diverterOver;
        public bool stopperOver;
        public bool doorOver;
        public bool motorTrip;
        public bool clampError;
        public bool servoError;
        public bool dataError;
        public bool updownError;
        public bool fwdrevError;
        public bool limchkError;
        public bool emerSwitch;
        public bool liftError;
        public bool scInAllow;
        public bool scOutAllow;
        public int pltSize;
        public bool rtvAllow;
        public bool frontExists;
        public bool rearExists;
        public bool homeStLftUp;
        public bool homeStLftDown;
        public int bcrArriveCnt;
        public int workNo;
        public int destination;
        public int weight1;
        public int weight2;
        public bool commStatus;
        public bool sizeLengthError;
        public bool sizeWidthError;
        public bool sizeHeightError;

        public override string ToString()
        {
            return base.ToString();
        }
    }




    public struct stOrder
    {
        public bool enable;
        public int status;
        public int setTime;
        public int sndTime;
        public int ackTime;
        public int sndCount;

        public override string ToString()
        {
            return base.ToString();
        }
    }
    /// <summary>
    /// 바코드 공유 메모리 변수
    /// </summary>

    public struct stBarcodeInfo
    {
        public int bcrNo;
        public int flrNo;
        public bool enable;
        public bool comStatus;
        public string loopTime;
        public string enqTime;
        public string answerTime;
        public int sequnceNum;
        public int arlCount;
        public string barcode;
        public string backBarcode;
        public int processResult;
        public string stepStatus;

        public stOrder[] order;

        public override string ToString()
        {
            return base.ToString();
        }
    }
    /// <summary>
    /// 
    /// </summary>
    public struct stExists
    {
        public bool bed;

        public override string ToString()
        {
            return base.ToString();
        }
    }

    /// <summary>
    ///  RTV 공유메모리 변수 
    /// </summary>
    public struct stRTVInfo
    {
        public int rtvNo;
        public int flrNo;
        public bool operationMode;
        public string loopTime;
        public string enqTime;
        public string answerTime;
        public bool active;
        public int workType;
        public bool enable;
        public bool commStatus;
        public int currentPosition;
        public bool error;
        public int errorCode;
        public int errorSubCode;
        public bool emergency;
        public int status;      //1. 대기, 2. 작중
        public int speed;
        public bool timeOver;
        public bool completeFlag;

        public int workNo;

        public stExists[] exists;

        public System.Drawing.Point[] rtvLocation;

        public override string ToString()
        {
            return base.ToString();
        }
    }
    public class Def
    {
        public const int WM_NCLBUTTONDOWN = 0xA1;
        public const int HT_CAPTION = 0x2;
        [System.Runtime.InteropServices.DllImport("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
        [System.Runtime.InteropServices.DllImport("user32.dll")]
        public static extern bool ReleaseCapture();

        // 공통 클래스 선언

        // 모드 설정
        // server : 장비와 직접 통신처리를 하며(OPC) RAW 데이터를 기준으로 모니터링을 처리한다.
        // client : 장비와 직접 통신을 하지 않으며 로컬 db의 데이터로 데이터 모니터링을 처리한다.
        public static string systemMode = "";

        //public static DbOracle dbOracle = null;
        //public static DbMsSql dbMsSql = null;

        //public static frmM001 formM001 = null;
        //public static frmP110 formP110 = null;

        // 프로그램 별 그룹변수 선언      
        public static string gsProgrameName = "";
        public static string gsProgramPassword = "";
        public static string gsMACHCD = "";
        public static string gsUserID = "";
        public static string gsUserNM = "";
        public static string gsShift = "";

        public static int nTagMessage = 0;
        public static int nConfirmMessage = 0;

        public static stWarehouseInfo[] WHSInfo;
        public static stConveyorInfo[] CNVInfo;
        //public static stStackerCraneInfo[] STCInfo;
        public static stRTVInfo[] RTVInfo;
        public static stBarcodeInfo[] BCRInfo;
        //public static stRtvOrder[] RTVOrderInfo;
        //public static stEmergency EMERInfo;

        public static DataTable WorkDataTable;
        public static DataTable WorkDetailDataTable;          // KMS - WORK DETAIL VIEW
        public static DataTable WorkPriorityDataTable;        // KMS - WORK 우선순위 조정
        public static DataTable WorkSelectedDataTable;        // KMS - WORK 우선순위 조정, WORK 긴급출고 변경
        public static DataTable WorkMonitoringDataTable;        // KMS - WORK 우선순위 조정, WORK 긴급출고 변경

        public static DataTable StationDataTable;
        public static DataTable ReturnHistoryDataTable;
        public static DataTable RackSummaryDataTable;
        public static DataTable WCSErrorDescTable;

        public static string[] RemainRack = new string[6];  // RACK 사용률
        public static int WorkTotalCount = 0;
        public static int WorkInCount = 0;
        public static int WorkOutCount = 0;
        public static int WorkMoveCount = 0;                // KMS - 이동(R to R, S to S) 작업 카운트

        public static bool isResizeMode = false;            // KMS - GRID 사이즈 조절(마우스 이벤트)
        public static Rectangle CropR;                      // KMS - GRID 사이즈 조절(마우스 이벤트)
        public static bool MouseInLeft { get; set; }
        public static bool MouseInRight { get; set; }
        public static bool MouseInTop { get; set; }
        public static bool MouseInBottom { get; set; }

        public static string[] colNm = new string[100];     // KMS - GRID 동적 컬럼 할당
        public static string[] splColNm = new string[100];  // KMS - GRID 동적 컬럼 할당
        public static string ViewNm;                        // KMS - GRID 동적 컬럼 할당

        public static ConcurrentDictionary<string, string> ccdStcError = new ConcurrentDictionary<string, string>();
        public static ConcurrentDictionary<string, string> ccdSystemStatus = new ConcurrentDictionary<string, string>();
    }


    


    }

