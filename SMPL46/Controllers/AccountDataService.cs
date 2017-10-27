using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web.Mvc;
using Microsoft.Ajax.Utilities;
using SMPL46.Models;

namespace SMPL46.Controllers
{
    public class AccountDataService
    {
        //private EalingPMS_DataMart_TestEntities1 SmplDB = new EalingPMS_DataMart_TestEntities1();

        public AccountDataService()
        {
        }

        public string loginResetPWD(string Username, string Password)
        {
            string userPWD = "";
            bool userResetPWD = false;
            DateTime userDateResetPWD = DateTime.Now;

            //get MD5 Hash encoded string of passed in Password
            string hashpassword = getMd5Hash(Password);

            using (EalingPMS_DataMart_TestEntities1 SmplDB = new EalingPMS_DataMart_TestEntities1())
            {
                var login = from gu in SmplDB.grip_user where gu.UID == Username && gu.PWD == hashpassword && (gu.accesstype_code == "WEB_PDA" || gu.accesstype_code == "WEB_ONLY") select new { gu.PWD, gu.reset_pwd, gu.last_pwd_reset };

                if (login.Count() > 0)
                {
                    foreach (var user in login)
                    {
                        userPWD = user.PWD;
                        userResetPWD = user.reset_pwd.Equals("Y") ? true : false;
                        userDateResetPWD = Convert.ToDateTime(user.last_pwd_reset.ToString("yyyy-MM-dd"));
                        System.Web.HttpContext.Current.Session["UserRoles"] = GetRoleObject(Username, Password);
                    }
                }
                else
                {
                    return "User not Found";
                }

                if (userResetPWD)
                {
                    if ((DateTime.Now - userDateResetPWD).TotalDays >= 60)
                    {
                        return "Reset";
                    }
                }
            }

            return "";
        }

        public bool updateGripUser(string Username, string OLDPassword, string NEWPassword)
        {
            bool updateGripUser = false;
            DateTime userDateResetPWD = DateTime.Now;

            //get MD5 Hash encoded string of passed in Password
            string hashOLDpassword = getMd5Hash(OLDPassword);
            string hashNEWpassword = getMd5Hash(NEWPassword);

            using (EalingPMS_DataMart_TestEntities1 SmplDB = new EalingPMS_DataMart_TestEntities1())
            {
                var query = (from gu in SmplDB.grip_user where gu.UID == Username && gu.PWD == hashOLDpassword select gu).FirstOrDefault();

                if (query != null)
                {
                    query.PWD = hashNEWpassword;
                    query.reset_pwd = "Y";
                    query.last_pwd_reset = userDateResetPWD;

                    //foreach (grip_user gu in query)
                    //{
                    //    gu.PWD = hashNEWpassword;
                    //    gu.reset_pwd = "Y";
                    //    gu.last_pwd_reset = userDateResetPWD;
                    //}

                    // Submit the changes to the database.
                    try
                    {
                        SmplDB.SaveChanges();
                        updateGripUser = true;
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e);
                        // Provide for exceptions.
                    }
                }
            }
            return updateGripUser;
        }

        public IEnumerable<SelectListItem> GetWards()
        {
            using (EalingPMS_DataMart_TestEntities1 SmplDB = new EalingPMS_DataMart_TestEntities1())
            {
                var wardlist = SmplDB.Database.SqlQuery<GetWardListContext>("exec GetWardList").ToList();
                var rtn = wardlist.Select(x => new SelectListItem() { Text = x.Ward_Description, Value = x.Ward_Description }).ToList();

                //var wards = SmplDB.Ref_Wards.Select(x => new SelectListItem() { Text = x.Ward_Description, Value = x.Ward_Code }).ToList();
                //wards.Insert(0, new SelectListItem() {Text = "ALL WARDS", Value = "ALL WARDS"});
                return rtn;
            }
        }

        public IEnumerable<SelectListItem> GetAreas()
        {
            using (EalingPMS_DataMart_TestEntities1 SmplDB = new EalingPMS_DataMart_TestEntities1())
            {
                var arealist = SmplDB.Database.SqlQuery<GetAreaListContext>("exec GetAreaList").ToList();
                var rtn = arealist.Select(x => new SelectListItem() { Text = x.Area_Name, Value = x.Area_Name }).ToList();

                return rtn;
            }
        }

        public IEnumerable<SelectListItem> GetFrequencies()
        {
            using (EalingPMS_DataMart_TestEntities1 SmplDB = new EalingPMS_DataMart_TestEntities1())
            {
                var freqlist = SmplDB.Database.SqlQuery<GetFreqCodeListContext>("exec GetFreqCodeList").ToList();
                var rtn = freqlist.Select(x => new SelectListItem() { Text = x.Freq_Code, Value = x.Freq_Code }).ToList();

                return rtn;
            }
        }

        public IEnumerable<SelectListItem> GetLandUseClasses()
        {
            using (EalingPMS_DataMart_TestEntities1 SmplDB = new EalingPMS_DataMart_TestEntities1())
            {
                var landuselist = SmplDB.Database.SqlQuery<GetLandUseClassListContext>("exec GetLandUseClasses").ToList();
                var rtn = landuselist.Select(x => new SelectListItem() { Text = x.Land_Use_Desc, Value = x.Land_Use_Desc }).ToList();

                return rtn;
            }
        }

        public IEnumerable<SelectListItem> GetInspectorList()
        {
            using (EalingPMS_DataMart_TestEntities1 SmplDB = new EalingPMS_DataMart_TestEntities1())
            {
                var inspectorlist = SmplDB.Database.SqlQuery<GetInspectorListContext>("exec GetInspectorList").ToList();
                var rtn = inspectorlist.Select(x => new SelectListItem() { Text = x.Inspector_Name, Value = x.Login_Name }).ToList();

                return rtn;
            }
        }

        public DataTable GetInspectionPhoto(string strID)
        {
            using (EalingPMS_DataMart_TestEntities1 SmplDB = new EalingPMS_DataMart_TestEntities1())
            {
                DataTable dt = new DataTable();
                var conn = SmplDB.Database.Connection;
                var connectionState = conn.State;

                try
                {
                    if (connectionState != ConnectionState.Open)
                        conn.Open();
                    using (var cmd = conn.CreateCommand())
                    {
                        cmd.CommandTimeout = 120;
                        cmd.CommandText = "SELECT Inspection_Photo FROM Cleaning_Schedule WHERE pkUID = '" + strID + "' ";
                        cmd.CommandType = CommandType.Text;
                        using (var reader = cmd.ExecuteReader())
                        {
                            dt.Load(reader);
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    if (connectionState != ConnectionState.Open)
                        conn.Close();
                }
                return dt;
            }
        }

        public DataTable GetCMInspectionPhoto(string strID)
        {
            using (EalingPMS_DataMart_TestEntities1 SmplDB = new EalingPMS_DataMart_TestEntities1())
            {
                DataTable dt = new DataTable();
                var conn = SmplDB.Database.Connection;
                var connectionState = conn.State;

                try
                {
                    if (connectionState != ConnectionState.Open)
                        conn.Open();
                    using (var cmd = conn.CreateCommand())
                    {
                        cmd.CommandTimeout = 120;
                        cmd.CommandText = "SELECT Inspection_Photo FROM ConMon_Schedule WHERE pkUID = '" + strID + "' ";
                        cmd.CommandType = CommandType.Text;
                        using (var reader = cmd.ExecuteReader())
                        {
                            dt.Load(reader);
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    if (connectionState != ConnectionState.Open)
                        conn.Close();
                }
                return dt;
            }
        }

        public DataTable GetAHInspectionPhoto(string strID)
        {
            using (EalingPMS_DataMart_TestEntities1 SmplDB = new EalingPMS_DataMart_TestEntities1())
            {
                DataTable dt = new DataTable();
                var conn = SmplDB.Database.Connection;
                var connectionState = conn.State;

                try
                {
                    if (connectionState != ConnectionState.Open)
                        conn.Open();
                    using (var cmd = conn.CreateCommand())
                    {
                        cmd.CommandTimeout = 120;
                        cmd.CommandText = "SELECT Inspection_Photo FROM AdHoc_Inspections WHERE pkUID = '" + strID + "' ";
                        cmd.CommandType = CommandType.Text;
                        using (var reader = cmd.ExecuteReader())
                        {
                            dt.Load(reader);
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    if (connectionState != ConnectionState.Open)
                        conn.Close();
                }
                return dt;
            }
        }

        public string GetPkuidFromStreetSection(string strStreetSection)
        {
            using (EalingPMS_DataMart_TestEntities1 SmplDB = new EalingPMS_DataMart_TestEntities1())
            {
                var conn = SmplDB.Database.Connection;
                var connectionState = conn.State;

                char[] separatingChars = "__".ToCharArray();

                string[] strarr = strStreetSection.Split(separatingChars, StringSplitOptions.RemoveEmptyEntries);

                //return the extracted ward, street and limits
                var wardName = strarr[0];
                var district = strarr[1];
                var street = strarr[2];
                var limits = strarr[3];

                try
                {
                    if (connectionState != ConnectionState.Open)
                        conn.Open();
                    using (var cmd = conn.CreateCommand())
                    {
                        cmd.CommandTimeout = 120;
                        //  "SELECT cast(InitialpkUID as varchar (40)) + '__' + ISNULL(cast(ReInspectionpkUID as varchar (40)), 'NULL') CombinedpkUID" +
                        cmd.CommandText =
                            "SELECT cast(InitialpkUID as varchar (40)) CombinedpkUID" +
                            " FROM VW_Consol_AllRecords " +
                            " WHERE  Ward_Name = '" + wardName + "' " +
                            " AND    District = '" + district + "' " +
                            " AND    Street_Name = '" + street + "' " +
                            " AND    Limits = '" + limits + "' " +
                            " AND    Scheduled_Cleaning_Date < getdate() " +
                            " ORDER BY Scheduled_Cleaning_Date DESC ";
                        cmd.CommandType = CommandType.Text;
                        using (var reader = cmd.ExecuteReader())
                        {
                            if (reader.HasRows)
                            {
                                reader.Read();
                                return reader["CombinedpkUID"].ToString();
                            }
                            else
                            {
                                return "";
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    if (connectionState != ConnectionState.Open)
                        conn.Close();
                }
            }
        }

        public DataTable GetNotificationData(string PKUID)
        {
            using (EalingPMS_DataMart_TestEntities1 SmplDB = new EalingPMS_DataMart_TestEntities1())
            {
                SqlParameter prm = null;
                DataTable dt = new DataTable();
                var conn = SmplDB.Database.Connection;
                var connectionState = conn.State;
                try
                {
                    if (connectionState != ConnectionState.Open)
                        conn.Open();
                    using (var cmd = conn.CreateCommand())
                    {
                        cmd.CommandTimeout = 120;
                        cmd.CommandText = "GetNoticeInfo_by_PKUID";
                        cmd.CommandType = CommandType.StoredProcedure;
                        prm = new SqlParameter("@pkuid", SqlDbType.VarChar);
                        prm.Value = PKUID;
                        cmd.Parameters.Add(prm);
                        using (var reader = cmd.ExecuteReader())
                        {
                            dt.Load(reader);
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    if (connectionState != ConnectionState.Open)
                        conn.Close();
                }
                return dt;
            }
        }

        public DataTable GetNotificationDataByPKUIDNoticeTypeCode(string PKUID, string ParentPKUID, string NoticeTypeCode, int NoticeFrequencyLinkID)
        {
            using (EalingPMS_DataMart_TestEntities1 SmplDB = new EalingPMS_DataMart_TestEntities1())
            {
                SqlParameter prm = null;
                DataTable dt = new DataTable();
                var conn = SmplDB.Database.Connection;
                var connectionState = conn.State;
                try
                {
                    if (connectionState != ConnectionState.Open)
                        conn.Open();
                    using (var cmd = conn.CreateCommand())
                    {
                        cmd.CommandTimeout = 120;
                        cmd.CommandText = "GetNoticeText_by_PKUID_NoticeTypeCode";
                        cmd.CommandType = CommandType.StoredProcedure;
                        prm = new SqlParameter("@pkuid", SqlDbType.UniqueIdentifier);
                        prm.Value = new Guid(PKUID);
                        cmd.Parameters.Add(prm);
                        prm = new SqlParameter("@parent_pkuid", SqlDbType.UniqueIdentifier);
                        prm.Value = new Guid(ParentPKUID); ;
                        cmd.Parameters.Add(prm);
                        prm = new SqlParameter("@NoticeTypeCode", SqlDbType.VarChar);
                        prm.Value = NoticeTypeCode;
                        cmd.Parameters.Add(prm);
                        prm = new SqlParameter("@NoticeFrequencyLinkID", SqlDbType.Int);
                        prm.Value = NoticeFrequencyLinkID;
                        cmd.Parameters.Add(prm);
                        using (var reader = cmd.ExecuteReader())
                        {
                            dt.Load(reader);
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    if (connectionState != ConnectionState.Open)
                        conn.Close();
                }
                return dt;
            }
        }

        public DataTable GetInspectionDetails(string PKUID)
        {
            using (EalingPMS_DataMart_TestEntities1 SmplDB = new EalingPMS_DataMart_TestEntities1())
            {
                SqlParameter prm = null;
                DataTable dt = new DataTable();
                var conn = SmplDB.Database.Connection;
                var connectionState = conn.State;
                try
                {
                    if (connectionState != ConnectionState.Open)
                        conn.Open();
                    using (var cmd = conn.CreateCommand())
                    {
                        cmd.CommandTimeout = 120;
                        cmd.CommandText = "GetInspectionDetails";
                        cmd.CommandType = CommandType.StoredProcedure;
                        prm = new SqlParameter("@GUID", SqlDbType.VarChar);
                        prm.Value = PKUID;
                        cmd.Parameters.Add(prm);
                        using (var reader = cmd.ExecuteReader())
                        {
                            dt.Load(reader);
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    if (connectionState != ConnectionState.Open)
                        conn.Close();
                }
                return dt;
            }
        }

        public DataTable GetCMInspectionDetails(string PKUID)
        {
            using (EalingPMS_DataMart_TestEntities1 SmplDB = new EalingPMS_DataMart_TestEntities1())
            {
                SqlParameter prm = null;
                DataTable dt = new DataTable();
                var conn = SmplDB.Database.Connection;
                var connectionState = conn.State;
                try
                {
                    if (connectionState != ConnectionState.Open)
                        conn.Open();
                    using (var cmd = conn.CreateCommand())
                    {
                        cmd.CommandTimeout = 120;
                        cmd.CommandText = "GetCMInspectionDetails";
                        cmd.CommandType = CommandType.StoredProcedure;
                        prm = new SqlParameter("@GUID", SqlDbType.VarChar);
                        prm.Value = PKUID;
                        cmd.Parameters.Add(prm);
                        using (var reader = cmd.ExecuteReader())
                        {
                            dt.Load(reader);
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    if (connectionState != ConnectionState.Open)
                        conn.Close();
                }
                return dt;
            }
        }

        public DataTable GetAHInspectionDetails(string PKUID)
        {
            using (EalingPMS_DataMart_TestEntities1 SmplDB = new EalingPMS_DataMart_TestEntities1())
            {
                SqlParameter prm = null;
                DataTable dt = new DataTable();
                var conn = SmplDB.Database.Connection;
                var connectionState = conn.State;
                try
                {
                    if (connectionState != ConnectionState.Open)
                        conn.Open();
                    using (var cmd = conn.CreateCommand())
                    {
                        cmd.CommandTimeout = 120;
                        cmd.CommandText = "GetAHInspectionDetails";
                        cmd.CommandType = CommandType.StoredProcedure;
                        prm = new SqlParameter("@GUID", SqlDbType.VarChar);
                        prm.Value = PKUID;
                        cmd.Parameters.Add(prm);
                        using (var reader = cmd.ExecuteReader())
                        {
                            dt.Load(reader);
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    if (connectionState != ConnectionState.Open)
                        conn.Close();
                }
                return dt;
            }
        }

        public DataTable GetNoticeReportAsCSV(DateTime StartDate, DateTime EndDate)
        {
            using (EalingPMS_DataMart_TestEntities1 SmplDB = new EalingPMS_DataMart_TestEntities1())
            {
                SqlParameter prm = null;
                DataTable dt = new DataTable();
                var conn = SmplDB.Database.Connection;
                var connectionState = conn.State;
                try
                {
                    if (connectionState != ConnectionState.Open)
                        conn.Open();
                    using (var cmd = conn.CreateCommand())
                    {
                        cmd.CommandTimeout = 120;
                        cmd.CommandText = "GetNoticeReportAsCSV";
                        cmd.CommandType = CommandType.StoredProcedure;
                        prm = new SqlParameter("@from", SqlDbType.DateTime);
                        prm.Value = StartDate;
                        cmd.Parameters.Add(prm);
                        prm = new SqlParameter("@to", SqlDbType.DateTime);
                        prm.Value = EndDate;
                        cmd.Parameters.Add(prm);
                        using (var reader = cmd.ExecuteReader())
                        {
                            dt.Load(reader);
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    if (connectionState != ConnectionState.Open)
                        conn.Close();
                }
                return dt;
            }
        }

        public DataTable GetFailedTransectsInCSVFormat(DateTime StartDate, DateTime EndDate)
        {
            using (EalingPMS_DataMart_TestEntities1 SmplDB = new EalingPMS_DataMart_TestEntities1())
            {
                SqlParameter prm = null;
                DataTable dt = new DataTable();
                var conn = SmplDB.Database.Connection;
                var connectionState = conn.State;
                try
                {
                    if (connectionState != ConnectionState.Open)
                        conn.Open();
                    using (var cmd = conn.CreateCommand())
                    {
                        cmd.CommandTimeout = 120;
                        cmd.CommandText = "GetFailedTransectsInCSVFormat";
                        cmd.CommandType = CommandType.StoredProcedure;
                        prm = new SqlParameter("@StartDate", SqlDbType.DateTime);
                        prm.Value = StartDate;
                        cmd.Parameters.Add(prm);
                        prm = new SqlParameter("@EndDate", SqlDbType.DateTime);
                        prm.Value = EndDate;
                        cmd.Parameters.Add(prm);
                        using (var reader = cmd.ExecuteReader())
                        {
                            dt.Load(reader);
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    if (connectionState != ConnectionState.Open)
                        conn.Close();
                }
                return dt;
            }
        }

        public DataTable GetUnCheckedTransects(DateTime StartDate, DateTime EndDate, string WardName, string FreqCode)
        {
            using (EalingPMS_DataMart_TestEntities1 SmplDB = new EalingPMS_DataMart_TestEntities1())
            {
                SqlParameter prm = null;
                DataTable dt = new DataTable();
                var conn = SmplDB.Database.Connection;
                var connectionState = conn.State;
                try
                {
                    if (connectionState != ConnectionState.Open)
                        conn.Open();
                    using (var cmd = conn.CreateCommand())
                    {
                        cmd.CommandTimeout = 120;
                        cmd.CommandText = "UnCheckedTransects";
                        cmd.CommandType = CommandType.StoredProcedure;
                        prm = new SqlParameter("@StartDate", SqlDbType.DateTime);
                        prm.Value = StartDate;
                        cmd.Parameters.Add(prm);
                        prm = new SqlParameter("@EndDate", SqlDbType.DateTime);
                        prm.Value = EndDate;
                        cmd.Parameters.Add(prm);
                        prm = new SqlParameter("@WardName", SqlDbType.VarChar);
                        prm.Size = 80;
                        prm.Value = WardName;
                        cmd.Parameters.Add(prm);
                        prm = new SqlParameter("@Freq_Code", SqlDbType.VarChar);
                        prm.Size = 30;
                        prm.Value = FreqCode;
                        cmd.Parameters.Add(prm);
                        using (var reader = cmd.ExecuteReader())
                        {
                            dt.Load(reader);
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    if (connectionState != ConnectionState.Open)
                        conn.Close();
                }
                return dt;
            }
        }

        public DataTable CleaningSummaryByWard(DateTime StartDate, DateTime EndDate, string WardCode)
        {
            using (EalingPMS_DataMart_TestEntities1 SmplDB = new EalingPMS_DataMart_TestEntities1())
            {
                SqlParameter prm = null;
                DataTable dt = new DataTable();
                var conn = SmplDB.Database.Connection;
                var connectionState = conn.State;
                try
                {
                    if (connectionState != ConnectionState.Open)
                        conn.Open();
                    using (var cmd = conn.CreateCommand())
                    {
                        cmd.CommandTimeout = 120;
                        cmd.CommandText = "CleaningSummaryByWard";
                        cmd.CommandType = CommandType.StoredProcedure;
                        prm = new SqlParameter("@StartDate", SqlDbType.DateTime);
                        prm.Value = StartDate;
                        cmd.Parameters.Add(prm);
                        prm = new SqlParameter("@EndDate", SqlDbType.DateTime);
                        prm.Value = EndDate;
                        cmd.Parameters.Add(prm);
                        prm = new SqlParameter("@Ward_Name", SqlDbType.VarChar);
                        prm.Size = 80;
                        prm.Value = WardCode;
                        cmd.Parameters.Add(prm);
                        using (var reader = cmd.ExecuteReader())
                        {
                            dt.Load(reader);
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    if (connectionState != ConnectionState.Open)
                        conn.Close();
                }
                return dt;
            }
        }

        public DataTable CleaningSummaryByWardSubReport(DateTime StartDate, DateTime EndDate, string WardCode)
        {
            using (EalingPMS_DataMart_TestEntities1 SmplDB = new EalingPMS_DataMart_TestEntities1())
            {
                SqlParameter prm = null;
                DataTable dt = new DataTable();
                var conn = SmplDB.Database.Connection;
                var connectionState = conn.State;
                try
                {
                    if (connectionState != ConnectionState.Open)
                        conn.Open();
                    using (var cmd = conn.CreateCommand())
                    {
                        cmd.CommandTimeout = 120;
                        cmd.CommandText = "CleaningSummaryByWardSubReport";
                        cmd.CommandType = CommandType.StoredProcedure;
                        prm = new SqlParameter("@StartDate", SqlDbType.DateTime);
                        prm.Value = StartDate;
                        cmd.Parameters.Add(prm);
                        prm = new SqlParameter("@EndDate", SqlDbType.DateTime);
                        prm.Value = EndDate;
                        cmd.Parameters.Add(prm);
                        prm = new SqlParameter("@WardName", SqlDbType.VarChar);
                        prm.Size = 80;
                        prm.Value = WardCode;
                        cmd.Parameters.Add(prm);
                        using (var reader = cmd.ExecuteReader())
                        {
                            dt.Load(reader);
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    if (connectionState != ConnectionState.Open)
                        conn.Close();
                }
                return dt;
            }
        }

        public DataTable CleaningSummaryByWardAGradesSubReport(DateTime StartDate, DateTime EndDate, string WardCode)
        {
            using (EalingPMS_DataMart_TestEntities1 SmplDB = new EalingPMS_DataMart_TestEntities1())
            {
                SqlParameter prm = null;
                DataTable dt = new DataTable();
                var conn = SmplDB.Database.Connection;
                var connectionState = conn.State;
                try
                {
                    if (connectionState != ConnectionState.Open)
                        conn.Open();
                    using (var cmd = conn.CreateCommand())
                    {
                        cmd.CommandTimeout = 120;
                        cmd.CommandText = "CleaningSummaryByWardAGradesSubReport";
                        cmd.CommandType = CommandType.StoredProcedure;
                        prm = new SqlParameter("@StartDate", SqlDbType.DateTime);
                        prm.Value = StartDate;
                        cmd.Parameters.Add(prm);
                        prm = new SqlParameter("@EndDate", SqlDbType.DateTime);
                        prm.Value = EndDate;
                        cmd.Parameters.Add(prm);
                        prm = new SqlParameter("@WardName", SqlDbType.VarChar);
                        prm.Size = 80;
                        prm.Value = WardCode;
                        cmd.Parameters.Add(prm);
                        using (var reader = cmd.ExecuteReader())
                        {
                            dt.Load(reader);
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    if (connectionState != ConnectionState.Open)
                        conn.Close();
                }
                return dt;
            }
        }

        public DataTable FailReasonByWard(DateTime StartDate, DateTime EndDate, string WardCode)
        {
            using (EalingPMS_DataMart_TestEntities1 SmplDB = new EalingPMS_DataMart_TestEntities1())
            {
                SqlParameter prm = null;
                DataTable dt = new DataTable();
                var conn = SmplDB.Database.Connection;
                var connectionState = conn.State;
                try
                {
                    if (connectionState != ConnectionState.Open)
                        conn.Open();
                    using (var cmd = conn.CreateCommand())
                    {
                        cmd.CommandTimeout = 120;
                        cmd.CommandText = "FailReasonByWard";
                        cmd.CommandType = CommandType.StoredProcedure;
                        prm = new SqlParameter("@StartDate", SqlDbType.DateTime);
                        prm.Value = StartDate;
                        cmd.Parameters.Add(prm);
                        prm = new SqlParameter("@EndDate", SqlDbType.DateTime);
                        prm.Value = EndDate;
                        cmd.Parameters.Add(prm);
                        prm = new SqlParameter("@WardName", SqlDbType.VarChar);
                        prm.Size = 80;
                        prm.Value = WardCode;
                        cmd.Parameters.Add(prm);
                        using (var reader = cmd.ExecuteReader())
                        {
                            dt.Load(reader);
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    if (connectionState != ConnectionState.Open)
                        conn.Close();
                }
                return dt;
            }
        }

        public DataTable ZonePerformanceReport(DateTime StartDate, DateTime EndDate, string LandUseClass)
        {
            using (EalingPMS_DataMart_TestEntities1 SmplDB = new EalingPMS_DataMart_TestEntities1())
            {
                SqlParameter prm = null;
                DataTable dt = new DataTable();
                var conn = SmplDB.Database.Connection;
                var connectionState = conn.State;
                try
                {
                    if (connectionState != ConnectionState.Open)
                        conn.Open();
                    using (var cmd = conn.CreateCommand())
                    {
                        cmd.CommandTimeout = 120;
                        cmd.CommandText = "ZonePerformanceReport";
                        cmd.CommandType = CommandType.StoredProcedure;
                        prm = new SqlParameter("@StartDate", SqlDbType.DateTime);
                        prm.Value = StartDate;
                        cmd.Parameters.Add(prm);
                        prm = new SqlParameter("@EndDate", SqlDbType.DateTime);
                        prm.Value = EndDate;
                        cmd.Parameters.Add(prm);
                        prm = new SqlParameter("@LandUseClass", SqlDbType.VarChar);
                        prm.Size = 80;
                        prm.Value = LandUseClass;
                        cmd.Parameters.Add(prm);
                        using (var reader = cmd.ExecuteReader())
                        {
                            dt.Load(reader);
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    if (connectionState != ConnectionState.Open)
                        conn.Close();
                }
                return dt;
            }
        }

        public DataTable AdhocReportByInspector(DateTime StartDate, DateTime EndDate, string LoginName)
        {
            using (EalingPMS_DataMart_TestEntities1 SmplDB = new EalingPMS_DataMart_TestEntities1())
            {
                SqlParameter prm = null;
                DataTable dt = new DataTable();
                var conn = SmplDB.Database.Connection;
                var connectionState = conn.State;
                try
                {
                    if (connectionState != ConnectionState.Open)
                        conn.Open();
                    using (var cmd = conn.CreateCommand())
                    {
                        cmd.CommandTimeout = 120;
                        cmd.CommandText = "AdhocReportByInspector";
                        cmd.CommandType = CommandType.StoredProcedure;
                        prm = new SqlParameter("@StartDate", SqlDbType.DateTime);
                        prm.Value = StartDate;
                        cmd.Parameters.Add(prm);
                        prm = new SqlParameter("@EndDate", SqlDbType.DateTime);
                        prm.Value = EndDate;
                        cmd.Parameters.Add(prm);
                        prm = new SqlParameter("@LoginName", SqlDbType.VarChar);
                        prm.Size = 80;
                        prm.Value = LoginName;
                        cmd.Parameters.Add(prm);
                        using (var reader = cmd.ExecuteReader())
                        {
                            dt.Load(reader);
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    if (connectionState != ConnectionState.Open)
                        conn.Close();
                }
                return dt;
            }
        }

        public DataTable AdhocReportByInspectorSubReport(DateTime StartDate, DateTime EndDate, string WardName, string Inspector)
        {
            using (EalingPMS_DataMart_TestEntities1 SmplDB = new EalingPMS_DataMart_TestEntities1())
            {
                SqlParameter prm = null;
                DataTable dt = new DataTable();
                var conn = SmplDB.Database.Connection;
                var connectionState = conn.State;
                try
                {
                    if (connectionState != ConnectionState.Open)
                        conn.Open();
                    using (var cmd = conn.CreateCommand())
                    {
                        cmd.CommandTimeout = 120;
                        cmd.CommandText = "AdHocReportByInspectorSubReport";
                        cmd.CommandType = CommandType.StoredProcedure;
                        prm = new SqlParameter("@StartDate", SqlDbType.DateTime);
                        prm.Value = StartDate;
                        cmd.Parameters.Add(prm);
                        prm = new SqlParameter("@EndDate", SqlDbType.DateTime);
                        prm.Value = EndDate;
                        cmd.Parameters.Add(prm);
                        prm = new SqlParameter("@WardName", SqlDbType.VarChar);
                        prm.Size = 80;
                        prm.Value = WardName;
                        cmd.Parameters.Add(prm);
                        prm = new SqlParameter("@Inspector", SqlDbType.VarChar);
                        prm.Size = 80;
                        prm.Value = Inspector;
                        cmd.Parameters.Add(prm);
                        using (var reader = cmd.ExecuteReader())
                        {
                            dt.Load(reader);
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    if (connectionState != ConnectionState.Open)
                        conn.Close();
                }
                return dt;
            }
        }

        public DataTable AdhocReportByStreet(DateTime StartDate, DateTime EndDate, string WardName, string StreetName, string Limits)
        {
            using (EalingPMS_DataMart_TestEntities1 SmplDB = new EalingPMS_DataMart_TestEntities1())
            {
                SqlParameter prm = null;
                DataTable dt = new DataTable();
                var conn = SmplDB.Database.Connection;
                var connectionState = conn.State;
                try
                {
                    if (connectionState != ConnectionState.Open)
                        conn.Open();
                    using (var cmd = conn.CreateCommand())
                    {
                        cmd.CommandTimeout = 120;
                        cmd.CommandText = "AdhocReportByStreet";
                        cmd.CommandType = CommandType.StoredProcedure;
                        prm = new SqlParameter("@StartDate", SqlDbType.DateTime);
                        prm.Value = StartDate;
                        cmd.Parameters.Add(prm);
                        prm = new SqlParameter("@EndDate", SqlDbType.DateTime);
                        prm.Value = EndDate;
                        cmd.Parameters.Add(prm);
                        prm = new SqlParameter("@Ward_name", SqlDbType.VarChar);
                        prm.Size = 80;
                        prm.Value = WardName;
                        cmd.Parameters.Add(prm);
                        prm = new SqlParameter("@Street_name", SqlDbType.VarChar);
                        prm.Size = 80;
                        prm.Value = StreetName;
                        cmd.Parameters.Add(prm);
                        prm = new SqlParameter("@Limits", SqlDbType.VarChar);
                        prm.Size = 80;
                        prm.Value = Limits;
                        cmd.Parameters.Add(prm);
                        using (var reader = cmd.ExecuteReader())
                        {
                            dt.Load(reader);
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    if (connectionState != ConnectionState.Open)
                        conn.Close();
                }
                return dt;
            }
        }

        public string InsertRectification(string strXML)
        {
            string rtnMessage = "";

            using (EalingPMS_DataMart_TestEntities1 SmplDB = new EalingPMS_DataMart_TestEntities1())
            {
                SqlParameter prm = null;
                DataTable dt = new DataTable();
                var conn = SmplDB.Database.Connection;
                var connectionState = conn.State;
                try
                {
                    if (connectionState != ConnectionState.Open)
                        conn.Open();
                    using (var cmd = conn.CreateCommand())
                    {
                        cmd.CommandTimeout = 120;
                        cmd.CommandText = "InsertRectification";
                        cmd.CommandType = CommandType.StoredProcedure;
                        prm = new SqlParameter("@CSXML", SqlDbType.VarChar);
                        prm.Value = strXML;
                        cmd.Parameters.Add(prm);
                        using (var reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                switch ((int)reader[0])
                                {
                                    case 0:
                                        //0 = OK
                                        rtnMessage = "Update Successful.";
                                        break;
                                    case 3:
                                        //3 = failed to make an update
                                        rtnMessage = "Update Failed.";
                                        break;
                                    case 4:
                                        //4 = no records selected for rectification
                                        rtnMessage = "No records selected for rectification.";
                                        break;
                                }
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    if (connectionState != ConnectionState.Open)
                        conn.Close();
                }
            }
            return rtnMessage;
        }

        public string InsertConMonScheduledCleanse(string strXML)
        {
            string rtnMessage = "";

            using (EalingPMS_DataMart_TestEntities1 SmplDB = new EalingPMS_DataMart_TestEntities1())
            {
                SqlParameter prm = null;
                DataTable dt = new DataTable();
                var conn = SmplDB.Database.Connection;
                var connectionState = conn.State;
                try
                {
                    if (connectionState != ConnectionState.Open)
                        conn.Open();
                    using (var cmd = conn.CreateCommand())
                    {
                        cmd.CommandTimeout = 120;
                        cmd.CommandText = "InsertConMonSchedCleanse";
                        cmd.CommandType = CommandType.StoredProcedure;
                        prm = new SqlParameter("@CSXML", SqlDbType.VarChar);
                        prm.Value = strXML;
                        cmd.Parameters.Add(prm);
                        using (var reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                switch ((int)reader[0])
                                {
                                    case 0:
                                        //0 = OK
                                        rtnMessage = "Update Successful.";
                                        break;
                                    case 3:
                                        //3 = failed to make an update
                                        rtnMessage = "Update Failed.";
                                        break;
                                    case 4:
                                        //4 = no records selected for rectification
                                        rtnMessage = "No records selected for rectification.";
                                        break;
                                }
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    if (connectionState != ConnectionState.Open)
                        conn.Close();
                }
            }
            return rtnMessage;
        }

        public string InsertAdHocScheduledCleanse(string strXML)
        {
            string rtnMessage = "";

            using (EalingPMS_DataMart_TestEntities1 SmplDB = new EalingPMS_DataMart_TestEntities1())
            {
                SqlParameter prm = null;
                DataTable dt = new DataTable();
                var conn = SmplDB.Database.Connection;
                var connectionState = conn.State;
                try
                {
                    if (connectionState != ConnectionState.Open)
                        conn.Open();
                    using (var cmd = conn.CreateCommand())
                    {
                        cmd.CommandTimeout = 120;
                        cmd.CommandText = "InsertAdHocSchedCleanseV2";
                        cmd.CommandType = CommandType.StoredProcedure;
                        prm = new SqlParameter("@CSXML", SqlDbType.VarChar);
                        prm.Value = strXML;
                        cmd.Parameters.Add(prm);
                        using (var reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                switch ((int)reader[0])
                                {
                                    case 0:
                                        //0 = OK
                                        rtnMessage = "Update Successful.";
                                        break;
                                    case 3:
                                        //3 = failed to make an update
                                        rtnMessage = "Update Failed.";
                                        break;
                                    case 4:
                                        //4 = no records selected for rectification
                                        rtnMessage = "No records selected for rectification.";
                                        break;
                                }
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    if (connectionState != ConnectionState.Open)
                        conn.Close();
                }
            }
            return rtnMessage;
        }

        public DataTable ContractorMonitoringSummaryByWard(DateTime StartDate, DateTime EndDate, string WardCode)
        {
            using (EalingPMS_DataMart_TestEntities1 SmplDB = new EalingPMS_DataMart_TestEntities1())
            {
                SqlParameter prm = null;
                DataTable dt = new DataTable();
                var conn = SmplDB.Database.Connection;
                var connectionState = conn.State;
                try
                {
                    if (connectionState != ConnectionState.Open)
                        conn.Open();
                    using (var cmd = conn.CreateCommand())
                    {
                        cmd.CommandTimeout = 120;
                        cmd.CommandText = "ContractorMonitoringSummaryByWard";
                        cmd.CommandType = CommandType.StoredProcedure;
                        prm = new SqlParameter("@StartDate", SqlDbType.DateTime);
                        prm.Value = StartDate;
                        cmd.Parameters.Add(prm);
                        prm = new SqlParameter("@EndDate", SqlDbType.DateTime);
                        prm.Value = EndDate;
                        cmd.Parameters.Add(prm);
                        prm = new SqlParameter("@Ward_Name", SqlDbType.VarChar);
                        prm.Size = 80;
                        prm.Value = WardCode;
                        cmd.Parameters.Add(prm);
                        using (var reader = cmd.ExecuteReader())
                        {
                            dt.Load(reader);
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    if (connectionState != ConnectionState.Open)
                        conn.Close();
                }
                return dt;
            }
        }

        public DataTable ContractorMonitoringSummaryByWardSubReport(DateTime StartDate, DateTime EndDate, string WardCode)
        {
            using (EalingPMS_DataMart_TestEntities1 SmplDB = new EalingPMS_DataMart_TestEntities1())
            {
                SqlParameter prm = null;
                DataTable dt = new DataTable();
                var conn = SmplDB.Database.Connection;
                var connectionState = conn.State;
                try
                {
                    if (connectionState != ConnectionState.Open)
                        conn.Open();
                    using (var cmd = conn.CreateCommand())
                    {
                        cmd.CommandTimeout = 120;
                        cmd.CommandText = "ContractorMonitoringSummaryByWardSubReport";
                        cmd.CommandType = CommandType.StoredProcedure;
                        prm = new SqlParameter("@StartDate", SqlDbType.DateTime);
                        prm.Value = StartDate;
                        cmd.Parameters.Add(prm);
                        prm = new SqlParameter("@EndDate", SqlDbType.DateTime);
                        prm.Value = EndDate;
                        cmd.Parameters.Add(prm);
                        prm = new SqlParameter("@WardName", SqlDbType.VarChar);
                        prm.Size = 80;
                        prm.Value = WardCode;
                        cmd.Parameters.Add(prm);
                        using (var reader = cmd.ExecuteReader())
                        {
                            dt.Load(reader);
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    if (connectionState != ConnectionState.Open)
                        conn.Close();
                }
                return dt;
            }
        }

        public List<string> GetRoleObject(string Username, string Password)
        {
            List<string> rtnList = new List<string>();
            


            //get MD5 Hash encoded string of passed in Password
            string hashpassword = getMd5Hash(Password);

            using (EalingPMS_DataMart_TestEntities1 SmplDB = new EalingPMS_DataMart_TestEntities1())
            {
                var userroles = from gr in SmplDB.grip_role
                                from gur in gr.grip_user_role
                                join gu in SmplDB.grip_user on gur.user_id equals gu.user_id
                                where gr.role_id == gur.role_id
                                && gu.UID == Username && gu.PWD == hashpassword
                                select gr.role_code;

                //var userroles = from gr in SmplDB.grip_role
                //    join gur in SmplDB.grip_user_role on gur.role_id equals gr.role_id
                //    join gu in SmplDB.grip_user on gur.user_id equals gu.user_id
                //    where gu.UID == Username && gu.PWD == hashpassword
                //    select new {role_code = gr.role_code};
                rtnList = userroles.ToList();
            }

            return rtnList;
        }

        private string getMd5Hash(string input)
        {
            // byte array representation of that string
            byte[] encodedPassword = new UTF8Encoding().GetBytes(input);

            // need MD5 to calculate the hash
            byte[] hash = ((HashAlgorithm)CryptoConfig.CreateFromName("MD5")).ComputeHash(encodedPassword);

            // string representation (similar to UNIX format)
            string encoded = BitConverter.ToString(hash)
                // without dashes
               .Replace("-", string.Empty)
                // make lowercase
               .ToLower();

            return encoded;
        }
    }
}
