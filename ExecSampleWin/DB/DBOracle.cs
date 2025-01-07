using Newtonsoft.Json.Linq;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Concurrent;
using System.Data;

namespace ExecSampleWin.DB
{
    public class DbOracle
    {
        string _ConnectionString;

        string _ConnectionIp;
        int _ConnectionPort;

        string _ConnectionSID;

        string _ConnectionID;
        string _ConnectionPassword;

        bool _ConnectionPool;
        int _ConnectionMaxPooSize;
        int _ConnectionMinPooSize;

        int _ConnectionTimeout;

        #region "Properties"
        public string ConnectionIp
        {
            get { return _ConnectionIp; }
            set { _ConnectionIp = value; }
        }

        public int ConnectionPort
        {
            get { return _ConnectionPort; }
            set { _ConnectionPort = value; }
        }

        public string ConnectionSID
        {
            get { return _ConnectionSID; }
            set { _ConnectionSID = value; }
        }

        public string ConnectionID
        {
            get { return _ConnectionID; }
            set { _ConnectionID = value; }
        }

        public string ConnectionPassword
        {
            get { return _ConnectionPassword; }
            set { _ConnectionPassword = value; }
        }

        public bool ConnectionPoll
        {
            get { return _ConnectionPool; }
            set { _ConnectionPool = value; }
        }

        public int ConnectionMaxPooSize
        {
            get { return _ConnectionMaxPooSize; }
            set { _ConnectionMaxPooSize = value; }
        }

        public int ConnectionMinPooSize
        {
            get { return _ConnectionMinPooSize; }
            set { _ConnectionMinPooSize = value; }
        }

        public int ConnectionTimeout
        {
            get { return _ConnectionTimeout; }
            set { _ConnectionTimeout = value; }
        }

        public bool ConnectionStatus
        {
            get { return GetConnection(); }
        }

        public string ConnectionString
        {
            get { return SetConnectionString(); }
        }

        #endregion


        public DbOracle()
        {
        }

        public string SetConnectionString()
        {
            _ConnectionString = $"Data Source = (DESCRIPTION = (ADDRESS_LIST = (ADDRESS = (PROTOCOL = TCP)" +
                                $"(HOST = {ConnectionIp})" +
                                $"(PORT = {ConnectionPort})))(CONNECT_DATA=(SERVER=DEDICATED)" +
                                $"(SERVICE_NAME = {ConnectionSID}))); " +
                                $"User ID = {ConnectionID}; " +
                                $"Password = {ConnectionPassword};" +
                                $"Connection Timeout = {ConnectionTimeout};" +
                                $"Pooling = {ConnectionPoll};" +
                                $"Min Pool Size = {ConnectionMinPooSize};" +
                                $"Max Pool Size = {ConnectionMaxPooSize};";

            return _ConnectionString;
        }

        public Boolean GetConnection()
        {
            bool result = false;
            try
            {
                using (OracleConnection connection = new OracleConnection(_ConnectionString))
                {
                    connection.Open();
                    if (connection.State != ConnectionState.Open)
                    {
                        result = false;
                    }
                    else
                    {
                        result = true;
                    }
                    connection.Close();
                }
                return result;
            }
            catch (Exception ex)
            {
                //LogUtil.Log(LogUtil._ERROR_LEVEL, this.GetType().Name, ex.Message);
                return result;
            }
        }

        public DataTable SelectSQL(string sql)
        {
            DataTable DT = new DataTable();

            try
            {
                using (OracleConnection connection = new OracleConnection(_ConnectionString))
                {
                    connection.Open();
                    using (OracleCommand command = new OracleCommand(sql, connection))
                    {
                        using (OracleDataAdapter adapter = new OracleDataAdapter(command))
                        {
                            adapter.SelectCommand.CommandTimeout = 1000;

                            adapter.Fill(DT);
                        }
                    }
                    connection.Close();
                }
            }
            catch (Exception ex)
            {
                //LogUtil.Log(LogUtil._ERROR_LEVEL, this.GetType().Name, sql + "\r\n" + ex.Message);
            }
            return DT;
        }


        public byte[] SelectSQLRawColumn(string sql)
        {
            byte[] byteValue = null;

            try
            {
                using (OracleConnection connection = new OracleConnection(_ConnectionString))
                {
                    connection.Open();
                    using (OracleCommand command = new OracleCommand(sql, connection))
                    {

                        using (OracleDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                byte[] rawValue = (byte[])reader.GetValue(0);
                                // rawValue 변수에 RAW 값이 저장됩니다.
                                byteValue = rawValue;
                            }
                        }
                    }
                    connection.Close();
                }
            }
            catch (Exception ex)
            {
                //LogUtil.Log(LogUtil._ERROR_LEVEL, this.GetType().Name, sql + "\r\n" + ex.Message);
            }
            return byteValue;
        }

        public int ExcuteSQL(string sql)
        {
            int cnt = 0;

            try
            {
                using (OracleConnection connection = new OracleConnection(_ConnectionString))
                {
                    connection.Open();
                    using (OracleCommand command = new OracleCommand(sql, connection))
                    {
                        cnt = command.ExecuteNonQuery();
                    }
                    connection.Close();
                }
            }
            catch (Exception ex)
            {
                cnt = 0;
                //LogUtil.Log(LogUtil._ERROR_LEVEL, this.GetType().Name, sql + "\r\n" + ex.Message);
            }

            return cnt;
        }

        public ConcurrentDictionary<string, string> ExcutePLSQL(string sql)
        {
            ConcurrentDictionary<string, string> dicReturnValue = new ConcurrentDictionary<string, string>();

            int cnt = 0;

            try
            {
                using (OracleConnection connection = new OracleConnection(_ConnectionString))
                {
                    connection.Open();
                    using (OracleCommand command = new OracleCommand(sql, connection))
                    {
                        cnt = command.ExecuteNonQuery();
                        dicReturnValue.Clear();
                        dicReturnValue["P_SUB_CODE"] = cnt.ToString();
                        dicReturnValue["P_ERROR_CODE"] = "S";
                        dicReturnValue["P_ERROR_MESSAGE"] = "";
                    }
                    connection.Close();
                }
            }
            catch (Exception ex)
            {
                cnt = 0;
                //LogUtil.Log(LogUtil._ERROR_LEVEL, this.GetType().Name, sql + "\r\n" + ex.Message);
                dicReturnValue.Clear();
                dicReturnValue["P_SUB_CODE"] = cnt.ToString();
                dicReturnValue["P_ERROR_CODE"] = "E";
                dicReturnValue["P_ERROR_MESSAGE"] = ex.Message;
                return dicReturnValue;
            }

            return dicReturnValue;
        }


        // 기존 SelectSQL에서 데이터가 없어도 컬럼처리를 위해서 신규 생성
        public DataTable SelectSQLDatatable(string sql)
        {
            DataTable ReturnDT;
            IDataReader idr = null;

            ReturnDT = null;

            try
            {
                using (OracleConnection connection = new OracleConnection(_ConnectionString))
                {
                    connection.Open();
                    OracleCommand cmd = connection.CreateCommand();

                    using (OracleCommand command = connection.CreateCommand())
                    {
                        command.CommandText = sql;
                        command.CommandType = CommandType.Text;

                        using (OracleDataAdapter adapter = new OracleDataAdapter(command))
                        {
                            idr = command.ExecuteReader();

                            int fieldCount = idr.FieldCount;

                            DataTable DT = new DataTable();
                            for (int nLoop = 0; nLoop < fieldCount; nLoop++)
                            {
                                DT.Columns.Add(idr.GetName(nLoop), idr.GetFieldType(nLoop));
                            }
                            DT.BeginLoadData();
                            object[] values = new object[fieldCount];
                            while (idr.Read())
                            {
                                idr.GetValues(values);
                                DT.LoadDataRow(values, true);
                            }
                            DT.EndLoadData();
                            idr.Close();
                            ReturnDT = DT;
                        }
                    }
                    connection.Close();
                }
                return ReturnDT;
            }
            catch (Exception ex)
            {
                //LogUtil.Log(LogUtil._ERROR_LEVEL, this.GetType().Name, ex.Message);
                ReturnDT = null;
                return ReturnDT;
            }
        }

        #region CALL PROCEDURE

        public ConcurrentDictionary<string, string> SP_CALL(string storedProcedureID, JObject jsonData)
        {
            OracleConnection connection = null;

            ConcurrentDictionary<string, string> hstReturnValue = new ConcurrentDictionary<string, string>();
            hstReturnValue.Clear();

            try
            {
                using (connection = new OracleConnection(_ConnectionString))
                {
                    connection.Open();
                    OracleCommand cmd = connection.CreateCommand();

                    using (OracleCommand command = connection.CreateCommand())
                    {
                        command.CommandText = storedProcedureID;
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.Add("PV_JSON_I", OracleDbType.NVarchar2).Direction = ParameterDirection.Input;
                        command.Parameters.Add("PV_JSON_O", OracleDbType.NVarchar2, 20000).Direction = ParameterDirection.Output;
                        command.Parameters.Add("PV_RETCOD_O", OracleDbType.NVarchar2, 20000).Direction = ParameterDirection.Output;
                        command.Parameters.Add("PV_RETMSG_O", OracleDbType.NVarchar2, 20000).Direction = ParameterDirection.Output;

                        command.Parameters["PV_JSON_I"].Value = jsonData;

                        using (OracleDataAdapter adapter = new OracleDataAdapter(command))
                        {
                            command.ExecuteNonQuery();

                            hstReturnValue.Clear();
                            hstReturnValue["PV_JSON_O"] = command.Parameters["PV_JSON_O"].Value.ToString();
                            hstReturnValue["PV_RETCOD_O"] = command.Parameters["PV_RETCOD_O"].Value.ToString();
                            hstReturnValue["PV_RETMSG_O"] = command.Parameters["PV_RETMSG_O"].Value.ToString();

                            if (command.Parameters["PV_RETCOD_O"].Value.ToString() != "0")
                            {
                                //LogUtil.Log(LogUtil._ERROR_LEVEL, this.GetType().Name, command.Parameters["PV_RETMSG_O"].Value.ToString());
                            }
                        }
                    }
                    connection.Close();

                    return hstReturnValue;
                }
            }
            catch (Exception ex)
            {
                //LogUtil.Log(LogUtil._ERROR_LEVEL, this.GetType().Name, ex.Message);
                hstReturnValue["PV_JSON_O"] = "";
                hstReturnValue["PV_RETCOD_O"] = "999";
                hstReturnValue["PV_RETMSG_O"] = "StoredProcedure 호출에러 : " + ex.Message;
                connection.Close();
            }
            finally
            {
                connection.Close();
                connection = null;
            }
            return hstReturnValue;
        }

        #endregion
    }

}
