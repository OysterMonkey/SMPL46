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
    public partial class AH1ReportLink : System.Web.UI.Page
    {
        private AccountDataService ds = new AccountDataService();

        protected void Page_Load(object sender, EventArgs e)
        {
            SetupPage();
        }

        public string PKUID { get; set; }

        private void SetupPage()
        {
            String strInspectionDate = String.Empty;

            PKUID = Request.QueryString["pkuid"];

            if (PKUID == "")
            {
                
            } else
            {
                //show inspection details
                panInspList.Visible = false;
                panInspDetails.Visible = true;

                RefreshInspDetails();

                //set feedback form hyperlink returnurl
                //linkFeedback.HRef = "Feedback.aspx?ReturnUrl=" + Request.Url.ToString();
            }
        }

        private void RefreshInspDetails()
        {
            DataTable dt = ds.GetAHInspectionDetails(PKUID);

            if (dt.Rows.Count > 0)
            {
                lblWard.Text = dt.Rows[0]["Ward_Name"].ToString();
                lblDistrict.Text = dt.Rows[0]["District"].ToString();
                lblStreet.Text = dt.Rows[0]["Street_Name"].ToString();
                lblLimits.Text = dt.Rows[0]["Limits"].ToString();
                lblStdOrDeep.Text = dt.Rows[0]["std_or_deep"].ToString();
                lblFreqCode.Text = dt.Rows[0]["Freq_Code"].ToString();
                //lblSchedCleaningDate.Text = (dt.Rows[0]["Sched_Cleaning_Date_Week_Commencing"]) != DBNull.Value
                //    ? (Convert.ToDateTime(dt.Rows[0]["Sched_Cleaning_Date_Week_Commencing"])).ToString("d MMMM yyyy")
                //    : "";

                if (dt.Rows[0]["Inspected_Date"] == null)
                {
                    lblInspectedDate.Text = "Street has not been inspected.";
                } else
                {
                    lblInspectedDate.Text = (dt.Rows[0]["Inspected_Date"]) != DBNull.Value
                        ? (Convert.ToDateTime(dt.Rows[0]["Inspected_Date"])).ToString("h:mm tt dddd d MMMM yyyy")
                        : "";
                }

                if (dt.Rows[0]["ReInspectionDate"] == null)
                {
                    lblReInspectionDate.Text = "Street has not been re-inspected.";
                } else
                {
                    lblReInspectionDate.Text = (dt.Rows[0]["ReInspectionDate"]) != DBNull.Value
                        ? (Convert.ToDateTime(dt.Rows[0]["ReInspectionDate"])).ToString("h:mm tt dddd d MMMM yyyy")
                        : "";
                }

                lblInspectionGrade.Text =  dt.Rows[0]["InitialGrade"].ToString() + " - " + dt.Rows[0]["Grade_Description"].ToString();

                if  (dt.Rows[0]["ReInspectionGrade"].ToString() == "")
                {
                    lblReInspectionGrade.Text = "Street has not been re-inspected.";
                } else
                {
                    lblReInspectionGrade.Text =  dt.Rows[0]["ReInspectionGrade"].ToString() + " - " + dt.Rows[0]["ReInspectionGrade_Description"].ToString();
                }

                //show inspector details if logged in (not public guest)
                //If Page.User.Identity.IsAuthenticated Then
                lblInspector.Text =  dt.Rows[0]["Inspected_By"].ToString();
                lblReInspector.Text =  dt.Rows[0]["ReInspected_By"].ToString();

                if (lblInspector.Text == "")
                {
                    lblInspector.Text = "&nbsp;";
                }

                if (dt.Rows[0]["InitialReasonCode"].ToString() == "")
                {
                    lblReason.Text = "&nbsp;";
                }else
                {
                    lblReason.Text = dt.Rows[0]["InitialReasonCode"].ToString() + " - " +
                                     dt.Rows[0]["InitialReasonDescription"].ToString();
                }
                        
                if (dt.Rows[0]["ReInspectionReasonCode"].ToString() == "")
                {
                lblReInspectionReason.Text = "&nbsp;";
                }else
                {
                    lblReInspectionReason.Text = dt.Rows[0]["ReInspectionReasonCode"].ToString() + " - " + dt.Rows[0]["ReInspectionReasonDescription"].ToString();
                }
                lblInspectionComments.Text = dt.Rows[0]["Inspection_Comments"].ToString();
                lblReInspectionComments.Text = dt.Rows[0]["ReInspection_comments"].ToString();
                if (lblInspectionComments.Text == "")
                {
                    lblInspectionComments.Text = "&nbsp;";
                }

                //pass ID to photo image control
                if ((dt.Rows[0]["Initial_HasPhoto"].ToString().ToUpper().Substring(0,1)) == "Y")
                {
                    imgInspPhoto.Visible = true;
                    imgInspPhoto.ImageUrl = "AHInspImage.aspx?id=" + PKUID;
                    lblNoInspPhoto.Visible = false;
                } else
                {
                    imgInspPhoto.Visible = false;
                    lblNoInspPhoto.Visible = true;
                }
                if ((String.IsNullOrWhiteSpace(dt.Rows[0]["ReInspect_HasPhoto"].ToString()) ? "N" : (dt.Rows[0]["ReInspect_HasPhoto"].ToString().ToUpper().Substring(0, 1))) == "Y")
                {
                    imgReInspPhoto.Visible = true;
                    imgReInspPhoto.ImageUrl = "AHInspImage.aspx?id=" + dt.Rows[0]["RectificationPKUID"].ToString();
                    lblNoReInspPhoto.Visible = false;
                } else
                {
                    imgReInspPhoto.Visible = false;
                    lblNoReInspPhoto.Visible = true;
                }
            }
            else
            {
                //no record matches the ID number given - do nothing
                imgInspPhoto.Visible = false;
                lblNoInspPhoto.Visible = true;
                imgReInspPhoto.Visible = false;
                lblNoReInspPhoto.Visible = true;
            }
        }

        public static DateTime? TryParse(string text)
        {
            DateTime date;
            if (DateTime.TryParse(text, out date))
            {
                return date;
            }
            else
            {
                return null;
            }
        }
    }
}