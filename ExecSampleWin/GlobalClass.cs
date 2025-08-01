using ExecSampleWin.DB;
using System;
using System.Collections.Generic;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExecSampleWin
{
    internal class GlobalClass
    {
        // DB 접속 정보 관련 공용변수
        private string DB_IP = "";
        private string DB_USER_ID = "";
        private string DB_USER_PW = "";

        public static DbOracle dbOracle = null;
    }
}
