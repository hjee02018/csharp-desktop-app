using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sample2
{

    public class GlobalClass
    {
        // 데이터베이스 접속 정보
        public const string OracleConnectionString = "User Id=dbadmin;Password=HyundaiSol2023!;Data Source=(DESCRIPTION=(ADDRESS_LIST=(ADDRESS=(PROTOCOL=TCP)(HOST=hmx-logisol-oracle-db-dev.c5ruasqdgoz7.ap-northeast-2.rds.amazonaws.com)(PORT=1521)))(CONNECT_DATA=(SERVER=DEDICATED)(SERVICE_NAME=OHMXDB)));";
       
        // 정적으로 선언된 속성
        public static string lg_userId = "";
        public static string lg_userPw = "";
        public static string lg_time = "";
        // 로그인 정보
        
        //private string _lg_userId = "";
        //public string lg_userId
        //{
        //    get { return _lg_userId; }
        //    set { _lg_userId = value; }
        //}

        //private string _lg_userPw = "";
        //public string lg_userPw
        //{
        //    get { return _lg_userPw; }
        //    set { _lg_userPw = value; }
        //}

        //private string _lg_time = "";
        //public string lg_time
        //{
        //    get { return _lg_time; }
        //    set { _lg_time = value; }
        //}

        public string ConvertRawToString(object rawValue)
        {
            if (rawValue != DBNull.Value)
            {
                byte[] rawBytes = (byte[])rawValue;
                return BitConverter.ToString(rawBytes).Replace("-", "");
            }

            return null;
        }

    }
}
