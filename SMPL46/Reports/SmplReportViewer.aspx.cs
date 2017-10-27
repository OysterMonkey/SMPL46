using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.Reporting.WebForms;
using SMPL46.Services;

namespace SMPL46.Reports
{
    public partial class SmplReportViewer : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string ReportPrefix = Request.QueryString["Name"];
                if (ReportPrefix != null)
                {
                    string SSRSusername = System.Configuration.ConfigurationManager.AppSettings["SSRSUsername"];
                    string SSRSpassword = System.Configuration.ConfigurationManager.AppSettings["SSRSPassword"];
                    string SSRSdomain = System.Configuration.ConfigurationManager.AppSettings["SSRSDomain"];
                    string SSRSurl = System.Configuration.ConfigurationManager.AppSettings["SSRSUrl"];
                    string SSRSreportpath = System.Configuration.ConfigurationManager.AppSettings["SSRSReportPath"];

                    ReportingServicesReportViewModel rptModel = System.Web.HttpContext.Current.Session[string.Format("{0}Params", ReportPrefix)] as ReportingServicesReportViewModel;
                    ReportParameter[] RptParameters = rptModel.parameters;

                    IReportServerCredentials irsc = new ReportServerCredentials(SSRSusername, SSRSpassword, SSRSdomain);

                    //string ReportServerURL = Request.QueryString["ServerURL"];
                    //if (ReportServerURL == null)
                    //    ReportServerURL = @"http://localhost/reportserver";

                    rptViewer.ProcessingMode = Microsoft.Reporting.WebForms.ProcessingMode.Remote;
                    rptViewer.ServerReport.ReportServerCredentials = irsc;

                    rptViewer.ShowToolBar = true;
                    rptViewer.AsyncRendering = true;
                    rptViewer.SizeToReportContent = true;
                    rptViewer.ShowParameterPrompts = false;
                    rptViewer.ServerReport.ReportServerUrl = new Uri(SSRSurl);
                    rptViewer.ServerReport.ReportPath = string.Format("{0}{1}Report", SSRSreportpath, ReportPrefix);

                    // Set report parameters for the report
                    foreach (ReportParameter rpt in RptParameters)
                    {
                        rptViewer.ServerReport.SetParameters(new ReportParameter[] { rpt });
                    }
                }
            }
        }
    }
}