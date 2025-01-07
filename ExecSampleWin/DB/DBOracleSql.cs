using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExecSampleWin.DB
{
    public class DBOracleSql
    {

        public string SELECT_T_DEVICEMAP_CHECKLIST(ConcurrentDictionary<string, string> param)
        {
            StringBuilder sb = new StringBuilder();

            try
            {
                sb.AppendFormat("SELECT * ");
                sb.AppendFormat("  FROM T_DEVICEMAP_CHECKLIST                   ");



                if (param.ContainsKey("FROM_DATE") & param.ContainsKey("TO_DATE"))
                {
                    string fromDate = param["FROM_DATE"];
                    string toDate = param["TO_DATE"];
                    sb.AppendFormat("WHERE TO_DATE(TEST_DATE, 'YYYYMMDD') BETWEEN TO_DATE('{0}', 'YYYY-MM-DD') AND TO_DATE('{1}', 'YYYY-MM-DD') \r\n", fromDate, toDate);
                }

                //sb.AppendFormat(" WHERE 1 = 1                           ");
                if (param.ContainsKey("SYS_ID"))
                {
                    if (param.TryGetValue("SYS_ID", out string sis_id))
                        sb.AppendFormat("   AND SYS_ID = '{0}'             \r\n", sis_id);
                }
                //sb.AppendFormat(" WHERE 1 = 1                           ");
                if (param.ContainsKey("SITE"))
                {
                    if (param.TryGetValue("SITE", out string site))
                        sb.AppendFormat("   AND SITE = '{0}'             \r\n", site);
                }
                sb.AppendFormat(" ORDER BY PLC_IP ASC");

                return sb.ToString();
            }
            catch (Exception ex)
            {
                //LogUtil.Log(LogUtil._ERROR_LEVEL, this.GetType().Name, ex.ToString());
                return "";
            }
            finally
            {
                sb = null;
            }
        }


        public string SELECT_T_DEVICEMAP_CHECKLIST_OLD(ConcurrentDictionary<string, string> param)
        {
            StringBuilder sb = new StringBuilder();

            try
            {
                sb.AppendFormat("SELECT *         ");
                sb.AppendFormat("  FROM T_DEVICEMAP_CHECKLIST                   ");
                sb.AppendFormat(" WHERE 1 = 1                           ");
                if (param.ContainsKey("SITE"))
                {
                    if (param.TryGetValue("SITE", out string site))
                        sb.AppendFormat("   AND SITE = '{0}'             \r\n", site);
                }
                if (param.ContainsKey("SYS_ID"))
                {
                    if (param.TryGetValue("SYS_ID", out string site))
                        sb.AppendFormat("   AND SYS_ID = '{0}'             \r\n", site);
                }
                sb.AppendFormat(" ORDER BY PLC_IP ASC");

                return sb.ToString();
            }
            catch (Exception ex)
            {
                //LogUtil.Log(LogUtil._ERROR_LEVEL, this.GetType().Name, ex.ToString());
                return "";
            }
            finally
            {
                sb = null;
            }
        }

    }
}
