using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using Microsoft.Reporting.WebForms;

namespace SMPL46.Services
{
    public class ReportingServicesReportViewModel
    {
        #region Constructor
        public ReportingServicesReportViewModel(String reportPath, List<ReportParameter> Parameters)
        {
            ReportPath = reportPath;
            parameters = Parameters.ToArray();
        }
        public ReportingServicesReportViewModel()
        {
        }
        #endregion Constructor

        #region Public Properties
        public ReportServerCredentials ServerCredentials { get { return new ReportServerCredentials(); } }
        public String ReportPath { get; set; }
        //public Uri ReportServerURL { get { return new Uri(WebConfigurationManager.AppSettings["ReportServerUrl"]); } }
        public ReportParameter[] parameters { get; set; }
        private string UploadDirectory = HttpContext.Current.Server.MapPath("~/App_Data/UploadTemp/");
        private string TempDirectory = HttpContext.Current.Server.MapPath("~/tempFiles/");
        #endregion
    }
}