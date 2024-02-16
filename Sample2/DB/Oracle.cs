using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sample2.DB
{
    internal class Oracle
    {
        // T_USER 정보
        public static string SelectAllUsers()
        {
            return "SELECT * FROM HMX_KCTC.T_USER";
        }

        // T_USER, T_ROLE 로부터 사용자 정보 
        public static string SelectALLUserInfo()
        {
            return @"
        SELECT HEXTORAW(U.ID_T_USER) AS ID_T_USER, U.USER_ID, R.ROL_NME, U.PWD, U.NME, U.REG_DATE, U.REG_TIME
        FROM HMX_KCTC.T_USER U
        LEFT JOIN HMX_KCTC.T_ROLE R ON U.ID_T_ROLE = R.ID_T_ROLE";
        }

        
        // T_USER, T_ROLE로부터 유효한(USE_YN==1) 사용자 호출
        public static string SelectAllValidUserInfo()
        {
            return @"
        SELECT HEXTORAW(U.ID_T_USER) AS ID_T_USER, U.USER_ID, R.ROL_NME, U.PWD, U.NME, U.REG_DATE, U.REG_TIME
        FROM HMX_KCTC.T_USER U
        LEFT JOIN HMX_KCTC.T_ROLE R ON U.ID_T_ROLE = R.ID_T_ROLE
        WHERE U.USE_YN = 1";
        }

        // T_USER, T_ROLE 로부터 특정 사용자 호출
        public static string SelectUserInfo(string userId)
        {
            return $@"
        SELECT HEXTORAW(U.ID_T_USER) AS ID_T_USER, U.USER_ID, R.ROL_NME, U.PWD, U.NME, U.REG_DATE, U.REG_TIME
        FROM HMX_KCTC.T_USER U
        LEFT JOIN HMX_KCTC.T_ROLE R ON U.ID_T_ROLE = R.ID_T_ROLE
        WHERE U.USER_ID = '{userId}'";
        }

        // T_USER, T_ROLE로부터 유효한(USE_YN==1) 사용자 호출
        public static string SelectValidUserInfo(string userId)
        {
            return $@"
        SELECT HEXTORAW(U.ID_T_USER) AS ID_T_USER, U.USER_ID, R.ROL_NME, U.PWD, U.NME, U.REG_DATE, U.REG_TIME
        FROM HMX_KCTC.T_USER U
        LEFT JOIN HMX_KCTC.T_ROLE R ON U.ID_T_ROLE = R.ID_T_ROLE
        WHERE U.USER_ID = '{userId}' AND U.USER_YN = 1";
        }



        public static string SelectRoleName(string userId)
        {
            return $@"
            SELECT TR.ROL_NME
            FROM HMX_KCTC.T_ROLE TR
            JOIN HMX_KCTC.T_USER TU ON TU.ID_T_ROLE = TR.ID_T_ROLE
            WHERE TU.USER_ID  = '{userId}'";
        }

        // t_user 테이블에서 idTUser(ID_T_USER)이 존재 여부 확인
        public static string CheckIfExistUser(string idTUser)
        {
            return $@"
    SELECT 1 FROM HMX_KCTC.T_USER WHERE ID_T_USER = HEXTORAW('{idTUser}') FETCH FIRST 1 ROWS ONLY";
        }
        
        // 사용자 권한 정보 확인(ID_T_ROLE 포함)
        public static string SelectAllUserAuthorities(string userId)
        {
            return $@"
            SELECT TU.ID_T_ROLE, TA.ID_T_MENU, TM.PGM_NME,
                   TA.EXC_ATH, TA.INQ_ATH, TA.ADD_ATH, TA.DEL_ATH, TA.SAV_ATH, TA.XCL_ATH, TA.INI_ATH
            FROM HMX_KCTC.T_USER TU
            JOIN HMX_KCTC.T_AUTHORITY TA ON TU.ID_T_USER = TA.ID_T_USER
            JOIN (SELECT DISTINCT ID_T_MENU, PGM_NME FROM HMX_KCTC.T_MENU) TM ON TA.ID_T_MENU = TM.ID_T_MENU
            WHERE TU.USER_ID = '{userId}'";
        }
        // T_MENU 메뉴 확인
        public static string SelectMenus()
        {
            return @"SELECT TM.ID_T_MENU,TM.PGM_NME FROM FV TM";
        }

        // T_ROLE 계정 유형 확인
        public static string selectRoles()
        {
            return @"SELECT TR.ID_T_ROLE, ROL_NME FROM HMX_KCTC.T_ROLE TR";
        }

    }
}
