using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Sql;
using System.Data.SqlClient;
using System.Data;

namespace ElemnetUi_Vue.JS_Mvc.BLL
{
    public class SqlHelper
    {
        public static string sqlConnectStr
        {
            get {
                return System.Configuration.ConfigurationManager.AppSettings["sqlConnectStr"].ToString();
            }
        }
        public static DataSet ExcuteSql(string sql,out string errorMsg)
        {
            errorMsg = string.Empty;
            DataSet dataSet = new DataSet();
            try
            {
                using (SqlConnection sqlConnection = new SqlConnection(sqlConnectStr))
                {
                    if (sqlConnection.State == ConnectionState.Closed)
                    {
                        sqlConnection.Open();
                    }
                    SqlCommand sqlCommand = new SqlCommand(sql, sqlConnection);
                    SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand);
                    sqlDataAdapter.Fill(dataSet);
                }
            }
            catch(Exception ex)
            {
                errorMsg = ex.ToString();
            }
            return dataSet;
        }

        /// <summary>
        /// 验证账号密码
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="pwd"></param>
        /// <param name="errorMsg"></param>
        public static void CallProcLogin(string userName,string pwd,out string errorMsg)
        {
            errorMsg = string.Empty;
            try
            {
                using (SqlConnection sqlConnection = new SqlConnection(sqlConnectStr))
                {
                    if (sqlConnection.State == System.Data.ConnectionState.Closed)
                    {
                        sqlConnection.Open();
                    }
                    SqlCommand sqlCommand = new SqlCommand("ProcLogin",sqlConnection);
                    sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
                    sqlCommand.Parameters.Add("userName", System.Data.SqlDbType.VarChar);
                    sqlCommand.Parameters["userName"].Direction = System.Data.ParameterDirection.Input;
                    sqlCommand.Parameters["userName"].Value = userName;

                    sqlCommand.Parameters.Add("userPwd", System.Data.SqlDbType.VarChar);
                    sqlCommand.Parameters["userPwd"].Direction = System.Data.ParameterDirection.Input;
                    sqlCommand.Parameters["userPwd"].Value = pwd;

                    sqlCommand.Parameters.Add("returnMsg", System.Data.SqlDbType.VarChar);
                    sqlCommand.Parameters["returnMsg"].Direction = System.Data.ParameterDirection.Output;
                    sqlCommand.Parameters["returnMsg"].Size = 100;

                    sqlCommand.ExecuteNonQuery();
                    errorMsg = sqlCommand.Parameters["returnMsg"].Value.ToString().Trim();
                }
            }
            catch(Exception ex)
            {
                errorMsg = ex.ToString();
            }
        }

        /// <summary>
        /// 修改用户密码
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="userPwdOld"></param>
        /// <param name="userPwdNew"></param>
        /// <param name="errorMsg"></param>
        public static void CallProcEditUserPwd(string userName,string userPwdOld,string userPwdNew,out string errorMsg)
        {
            errorMsg = string.Empty;
            try
            {
                using (SqlConnection sqlConnection = new SqlConnection(sqlConnectStr))
                {
                    if (sqlConnection.State == System.Data.ConnectionState.Closed)
                    {
                        sqlConnection.Open();
                    }
                    SqlCommand sqlCommand = new SqlCommand("ProcEditUserPwd", sqlConnection);
                    sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
                    sqlCommand.Parameters.Add("userName", System.Data.SqlDbType.VarChar);
                    sqlCommand.Parameters["userName"].Direction = System.Data.ParameterDirection.Input;
                    sqlCommand.Parameters["userName"].Value = userName;

                    sqlCommand.Parameters.Add("userPwdOld", System.Data.SqlDbType.VarChar);
                    sqlCommand.Parameters["userPwdOld"].Direction = System.Data.ParameterDirection.Input;
                    sqlCommand.Parameters["userPwdOld"].Value = userPwdOld;

                    sqlCommand.Parameters.Add("userPwdNew", System.Data.SqlDbType.VarChar);
                    sqlCommand.Parameters["userPwdNew"].Direction = System.Data.ParameterDirection.Input;
                    sqlCommand.Parameters["userPwdNew"].Value = userPwdNew;

                    sqlCommand.Parameters.Add("returnMsg", System.Data.SqlDbType.VarChar);
                    sqlCommand.Parameters["returnMsg"].Direction = System.Data.ParameterDirection.Output;
                    sqlCommand.Parameters["returnMsg"].Size = 100;

                    sqlCommand.ExecuteNonQuery();
                    errorMsg = sqlCommand.Parameters["returnMsg"].Value.ToString().Trim();
                }
            }
            catch(Exception ex)
            {
                errorMsg = ex.ToString();
            }
        }

        /// <summary>
        /// 获取用户信息
        /// </summary>
        /// <param name="errorMsg"></param>
        /// <returns></returns>
        public static DataSet CallProcGetAllUser(out string errorMsg)
        {
            errorMsg = string.Empty;
            DataSet dataSet = new DataSet();
            try
            {
                using (SqlConnection sqlConnection = new SqlConnection(sqlConnectStr))
                {
                    if (sqlConnection.State == System.Data.ConnectionState.Closed)
                    {
                        sqlConnection.Open();
                    }
                    SqlCommand sqlCommand = new SqlCommand("ProcGetAllUser", sqlConnection);
                    sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
                    SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand);
                    sqlDataAdapter.Fill(dataSet);
                }
            }
            catch(Exception ex)
            {
                errorMsg = ex.ToString();
            }
            return dataSet;
        }
    }
}