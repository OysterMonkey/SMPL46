using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SMPL46.Controllers;

namespace SMPL46.Reports
{
    public partial class PrintNotice : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            NoticeDatalayer noticeDataLayer = new NoticeDatalayer();
            NoticeData objNoticeData = new NoticeData();

            noticeDataLayer.PKUID = Request.QueryString["pkuid"];
            noticeDataLayer.NoticeTypeCode = Request.QueryString["notice_type"];
            noticeDataLayer.Parent_PKUID = Request.QueryString["parent_pkuid"];

            if (Request.QueryString["notice_frequency_link_id"] != null)
            {
                noticeDataLayer.NoticeFrequencyLinkID = Convert.ToInt32(Request.QueryString["notice_frequency_link_id"]);
            }

            objNoticeData.NoticeDataTable = noticeDataLayer.NoticeDataTable;
            DataTable noticeData = objNoticeData.NoticeDataTable;

            NoticeHelper noticeHelper = new NoticeHelper(noticeData);

            litContent.Text = noticeHelper.ConvertNoticeText();
        }
    }

    public class NoticeHelper
    {
        private DataTable noticeCollection;

        public NoticeHelper(DataTable noticeCollection)
        {
            this.noticeCollection = noticeCollection;
        }

        public string ConvertNoticeText()
        {
            var convertNoticeText = String.Empty;
            DataRow drNoticeData;

            if (noticeCollection.Rows.Count > 0)
            {
                drNoticeData = noticeCollection.Rows[0];
                convertNoticeText = drNoticeData["Notice_text_desc"].ToString();
                convertNoticeText = convertNoticeText.Replace("[Site]", drNoticeData["street_name"].ToString());
                convertNoticeText = convertNoticeText.Replace("[Location]", drNoticeData["Limits"].ToString());
                convertNoticeText = convertNoticeText.Replace("[Rectification_Number]", drNoticeData["RectificationNumber"].ToString());
                convertNoticeText = convertNoticeText.Replace("[Default_Number]", drNoticeData["Default_Number"].ToString());

                if ((drNoticeData["Inspected_Date"]) != DBNull.Value)
                {
                    convertNoticeText = convertNoticeText.Replace("[Date_Inserted]",(drNoticeData["Inspected_Date"]) != DBNull.Value ? (Convert.ToDateTime(drNoticeData["Inspected_Date"])).ToString("dddd d MMMM yyyy") : "");
                    convertNoticeText = convertNoticeText.Replace("[Time_Inserted]",(drNoticeData["Inspected_Date"]) != DBNull.Value ? (Convert.ToDateTime(drNoticeData["Inspected_Date"])).ToString("h:mm tt") : "");
                }
                else
                {
                    convertNoticeText = convertNoticeText.Replace("[Date_Inserted]", "Unknown");
                    convertNoticeText = convertNoticeText.Replace("[Time_Inserted]", "Unknown");
                }

                if ((drNoticeData["Notice_served"]) != DBNull.Value)
                {
                    convertNoticeText = convertNoticeText.Replace("[Date_Issued]",(drNoticeData["Notice_served"]) != DBNull.Value ? (Convert.ToDateTime(drNoticeData["Notice_served"])).ToString("dddd d MMMM yyyy") : "");
                    convertNoticeText = convertNoticeText.Replace("[Time_Issued]",(drNoticeData["Notice_served"]) != DBNull.Value ? (Convert.ToDateTime(drNoticeData["Notice_served"])).ToString("h:mm tt") : "");
                } else
                {
                    convertNoticeText = convertNoticeText.Replace("[Date_Issued]", "Unknown");
                    convertNoticeText = convertNoticeText.Replace("[Time_Issued]", "Unknown");
                }

                if ((drNoticeData["Rectification_Notice_Issue_Date"])!= DBNull.Value)
                {
                    convertNoticeText = convertNoticeText.Replace("[Rectification_Date_Issued]",(drNoticeData["Rectification_Notice_Issue_Date"]) != DBNull.Value ? (Convert.ToDateTime(drNoticeData["Rectification_Notice_Issue_Date"])).ToString("h:mm tt dddd d MMMM yyyy") : "");
                }else
                {
                    convertNoticeText = convertNoticeText.Replace("[Rectification_Date_Issued]", "Unknown");
                }
            }

            return convertNoticeText;
        }
    }

    public class NoticeDatalayer 
    {
        private AccountDataService ds = new AccountDataService();

        public DataTable NoticeDataTable
        {
            get { return GetNotificationData(); }
            set { }
        }

        public string PKUID { get; set; }
        public string Parent_PKUID { get; set; }
        public string NoticeTypeCode { get; set; }
        public int NoticeFrequencyLinkID { get; set; }

        private DataTable GetNotificationData()
        {
            return ds.GetNotificationDataByPKUIDNoticeTypeCode(PKUID, Parent_PKUID, NoticeTypeCode, NoticeFrequencyLinkID);
        }
    }

    public class NoticeData 
    {
        //DataTable INoticeData.NoticeData { get; set; }
        //DataTable INoticeData.noticeData { get; set; }

        public DataTable NoticeDataTable{ get; set; }
    }

    //public interface INoticeData
    //{
    //    DataTable NoticeData { get; set; }
    //}
}