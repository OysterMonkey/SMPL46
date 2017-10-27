using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using SMPL46.Models;
using SMPL46.Services;
using SMPL46.ViewModels;
using Microsoft.Reporting.WebForms;

namespace SMPL46.Controllers
{
    public class ReportsController : Controller
    {
        private EalingPMS_DataMart_TestEntities1 db = new EalingPMS_DataMart_TestEntities1();

        private AccountDataService ds = new AccountDataService();

        // GET: Reports
        public async Task<ActionResult> Index()
        {
            var idParam = new SqlParameter {
                     ParameterName = "user_name",
                     Value = (System.Web.HttpContext.Current.Session["username"] ?? "")
            };

            var smplReportSelectionsByUser =
                db.Database.SqlQuery<SmplReportSelectionByUser>("exec GetReportsListByUser @user_name ", idParam).ToListAsync();

            return View(await smplReportSelectionsByUser);
        }

        // GET: AH1 Report
        public ActionResult AH1(int? id)
        {
            var model = new AH1ParameterViewModel
            {
                Wards = ds.GetWards(),
                StartDate = (DateTime)(System.Web.HttpContext.Current.Session["selectedstartdate"] ?? DateTime.Today.AddMonths(-1)),
                EndDate = (DateTime)(System.Web.HttpContext.Current.Session["selectedenddate"] ?? DateTime.Today.AddDays(1)),
                WardCode = (string)(System.Web.HttpContext.Current.Session["selectedward"] ?? "")
            };
            return View(model);
        }

        // POST: AH1 Report
        [HttpPost]
        public ActionResult AH1(AH1ParameterViewModel model)
        {
            System.Web.HttpContext.Current.Session["selectedstartdate"] = model.StartDate;
            System.Web.HttpContext.Current.Session["selectedenddate"] = model.EndDate;
            System.Web.HttpContext.Current.Session["selectedward"] = model.WardCode;

            ReportingServicesReportViewModel model2 = new ReportingServicesReportViewModel("AH1",
                new List<Microsoft.Reporting.WebForms.ReportParameter>()
                {
                    new Microsoft.Reporting.WebForms.ReportParameter("StartDate", model.StartDate.ToString("d"),false),
                    new Microsoft.Reporting.WebForms.ReportParameter("EndDate", model.EndDate.ToString("d"),false),
                    new Microsoft.Reporting.WebForms.ReportParameter("WardName", model.WardCode,false)
                });

            System.Web.HttpContext.Current.Session["AH1Params"] = model2;

            return RedirectToAction("AH1ReportViewer", "Reports");
        }

        // GET: AH1ReportViewer
        //[OutputCache(NoStore = true, Duration = 0)]
        public ActionResult AH1ReportViewer()
        {
            ReportingServicesReportViewModel rptModel = System.Web.HttpContext.Current.Session["AH1Params"] as ReportingServicesReportViewModel;

            return View(rptModel);
        }

        // POST: AH1Back Report
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult AH1Back(int? id)
        {
            return RedirectToAction("AH1", "Reports");
        }

        // GET: SC1 Report
        public ActionResult SC1(int? id)
        {
            var model = new SC1ParameterViewModel
            {
                StreetPrompt = "Enter known street details:",
                StreetName = (string)(System.Web.HttpContext.Current.Session["selectedstreet"] ?? ""),
                WardCode = (string)(System.Web.HttpContext.Current.Session["selectedward"] ?? ""),
                Wards = ds.GetWards(),
                StreetsFound = null
            };
            return View(model);
        }

        // POST: SC1 Report
        [HttpPost]
        public ActionResult SC1(SC1ParameterViewModel model)
        {
            var idParam1 = new SqlParameter
            {
                ParameterName = "StreetName",
                Value = model.StreetName ?? ""
            };
            System.Web.HttpContext.Current.Session["selectedstreet"] = idParam1.Value;

            var idParam2 = new SqlParameter
            {
                ParameterName = "WardName",
                Value = model.WardCode ?? ""
            };
            System.Web.HttpContext.Current.Session["selectedward"] = idParam2.Value;

            model.StreetsFound = new List<SC1EditorViewModel>();

            var getStreetListContext = db.Database.SqlQuery<GetStreetListContext>("exec GetStreetList @StreetName, @WardName ", idParam1, idParam2).ToList();

            foreach (var street in getStreetListContext)
            {
                var streetVM = new SC1EditorViewModel();

                streetVM.Ward_Name = street.Ward_Name;
                streetVM.District = street.District;
                streetVM.Street_Name = street.Street_Name;
                streetVM.Limits = street.Limits;
                streetVM.streetsection = street.streetsection;

                model.StreetsFound.Add(streetVM);
            }

            if (model.StreetsFound.Count == 0)
            {
                model.StreetPrompt = "No streets found with this name.";
            }
            else
            {
                model.StreetPrompt = "Select your street from list of matches below:";
            }

            return View(model);
        }

        public ActionResult SC1Street(string street)
        {
            var model = new SC1StreetInspection
            {
                CombinedPkuid = ds.GetPkuidFromStreetSection(street),
                //timestamp = DateTime.Now
            };

            System.Web.HttpContext.Current.Session["SC1Params"] = model;

            return RedirectToAction("SC1ReportViewer", "Reports", model);
        }

        // GET: SC1ReportViewer
        //[OutputCache(NoStore = true, Duration = 0)]
        public ActionResult SC1ReportViewer()
        {
            SC1StreetInspection model = System.Web.HttpContext.Current.Session["SC1Params"] as SC1StreetInspection;

            return View(model);
        }

        // GET: SC2 Report
        public ActionResult SC2(int? id)
        {
            var model = new SC2ParameterViewModel
            {
                StartDate = (DateTime)(System.Web.HttpContext.Current.Session["selectedstartdate"] ?? DateTime.Today.AddMonths(-1)),
                EndDate = (DateTime)(System.Web.HttpContext.Current.Session["selectedenddate"] ?? DateTime.Today),
            };
            return View(model);
        }

        // POST: SC2 Report
        [HttpPost]
        public ActionResult SC2(SC2ParameterViewModel model)
        {
            System.Web.HttpContext.Current.Session["selectedstartdate"] = model.StartDate;
            System.Web.HttpContext.Current.Session["selectedenddate"] = model.EndDate;

            ReportingServicesReportViewModel model2 = new ReportingServicesReportViewModel("SC2",
                new List<Microsoft.Reporting.WebForms.ReportParameter>()
                {
                    new Microsoft.Reporting.WebForms.ReportParameter("StartDate", model.StartDate.ToString("d"),false),
                    new Microsoft.Reporting.WebForms.ReportParameter("EndDate", model.EndDate.ToString("d"),false),
                });

            //ViewBag.SC4Params = model2;
            System.Web.HttpContext.Current.Session["SC2Params"] = model2;

            return RedirectToAction("SC2ReportViewer", "Reports");
        }

        // GET: SC2ReportViewer
        //[OutputCache(NoStore = true, Duration = 0)]
        public ActionResult SC2ReportViewer()
        {
            ReportingServicesReportViewModel rptModel = System.Web.HttpContext.Current.Session["SC2Params"] as ReportingServicesReportViewModel;

            return View(rptModel);
        }

        // POST: SC2Back Report
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult SC2Back(int? id)
        {
            return RedirectToAction("SC2", "Reports");
        }

        // GET: SC3 Report
        public ActionResult SC3(int? id)
        {
            var model = new SC1ParameterViewModel
            {
                StreetPrompt = "Enter known street details:",
                StreetName = (string)(System.Web.HttpContext.Current.Session["selectedstreet"] ?? ""),
                WardCode = (string)(System.Web.HttpContext.Current.Session["selectedward"] ?? ""),
                Wards = ds.GetWards(),
                StreetsFound = null
            };
            return View(model);
        }

        // POST: SC3 Report
        [HttpPost]
        public ActionResult SC3(SC1ParameterViewModel model)
        {
            var idParam1 = new SqlParameter
            {
                ParameterName = "StreetName",
                Value = model.StreetName ?? ""
            };
            System.Web.HttpContext.Current.Session["selectedstreet"] = idParam1.Value;

            var idParam2 = new SqlParameter
            {
                ParameterName = "WardName",
                Value = model.WardCode ?? ""
            };
            System.Web.HttpContext.Current.Session["selectedward"] = idParam2.Value;

            model.StreetsFound = new List<SC1EditorViewModel>();

            var getStreetListContext = db.Database.SqlQuery<GetStreetListContext>("exec GetStreetList @StreetName, @WardName ", idParam1, idParam2).ToList();

            foreach (var street in getStreetListContext)
            {
                var streetVM = new SC1EditorViewModel();

                streetVM.Ward_Name = street.Ward_Name;
                streetVM.District = street.District;
                streetVM.Street_Name = street.Street_Name;
                streetVM.Limits = street.Limits;
                streetVM.streetsection = street.streetsection;

                model.StreetsFound.Add(streetVM);
            }

            if (model.StreetsFound.Count == 0)
            {
                model.StreetPrompt = "No streets found with this name.";
            }
            else
            {
                model.StreetPrompt = "Select your street from list of matches below:";
            }

            return View(model);
        }

        // GET: SC4 Report
        public ActionResult SC4(int? id)
        {
            var model = new SC4ParameterViewModel
            {
                Wards = ds.GetWards(),
                StartDate = (DateTime)(System.Web.HttpContext.Current.Session["selectedstartdate"] ?? DateTime.Today.AddMonths(-1)),
                EndDate = (DateTime)(System.Web.HttpContext.Current.Session["selectedenddate"] ?? DateTime.Today),
                WardCode = (string)(System.Web.HttpContext.Current.Session["selectedward"] ?? "")
            };
            return View(model);
        }

        // POST: SC4 Report
        [HttpPost]
        public ActionResult SC4(SC4ParameterViewModel model)
        {
            System.Web.HttpContext.Current.Session["selectedstartdate"] = model.StartDate;
            System.Web.HttpContext.Current.Session["selectedenddate"] = model.EndDate;
            System.Web.HttpContext.Current.Session["selectedward"] = model.WardCode;

            ReportingServicesReportViewModel model2 = new ReportingServicesReportViewModel("SC4", 
                new List<Microsoft.Reporting.WebForms.ReportParameter>()
                {
                    new Microsoft.Reporting.WebForms.ReportParameter("StartDate", model.StartDate.ToString("d"),false),
                    new Microsoft.Reporting.WebForms.ReportParameter("EndDate", model.EndDate.ToString("d"),false),
                    new Microsoft.Reporting.WebForms.ReportParameter("WardName", model.WardCode,false)
                });

            System.Web.HttpContext.Current.Session["SC4Params"] = model2;

            return RedirectToAction("SC4ReportViewer", "Reports");
        }

        // GET: SC4ReportViewer
        //[OutputCache(NoStore = true, Duration = 0)]
        public ActionResult SC4ReportViewer()
        {
            ReportingServicesReportViewModel rptModel = System.Web.HttpContext.Current.Session["SC4Params"] as ReportingServicesReportViewModel;

            return View(rptModel);
        }

        // POST: SC4Back Report
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult SC4Back(int? id)
        {
            return RedirectToAction("SC4", "Reports");
        }

        // GET: SC5 Report
        public ActionResult SC5(int? id)
        {
            var model = new SC5ParameterViewModel
            {
                Wards = ds.GetWards(),
                StartDate = (DateTime)(System.Web.HttpContext.Current.Session["selectedstartdate"] ?? DateTime.Today.AddMonths(-1)),
                EndDate = (DateTime)(System.Web.HttpContext.Current.Session["selectedenddate"] ?? DateTime.Today),
                WardCode = (string)(System.Web.HttpContext.Current.Session["selectedward"] ?? "")
            };
            return View(model);
        }

        // POST: SC5 Report
        [HttpPost]
        public ActionResult SC5(SC5ParameterViewModel model)
        {
            System.Web.HttpContext.Current.Session["selectedstartdate"] = model.StartDate;
            System.Web.HttpContext.Current.Session["selectedenddate"] = model.EndDate;
            System.Web.HttpContext.Current.Session["selectedward"] = model.WardCode;

            ReportingServicesReportViewModel model2 = new ReportingServicesReportViewModel("SC5",
                new List<Microsoft.Reporting.WebForms.ReportParameter>()
                {
                    new Microsoft.Reporting.WebForms.ReportParameter("StartDate", model.StartDate.ToString("d"),false),
                    new Microsoft.Reporting.WebForms.ReportParameter("EndDate", model.EndDate.ToString("d"),false),
                    new Microsoft.Reporting.WebForms.ReportParameter("WardName", model.WardCode,false)
                });

            System.Web.HttpContext.Current.Session["SC5Params"] = model2;

            return RedirectToAction("SC5ReportViewer", "Reports");
        }

        // GET: SC5ReportViewer
        //[OutputCache(NoStore = true, Duration = 0)]
        public ActionResult SC5ReportViewer()
        {
            ReportingServicesReportViewModel rptModel = System.Web.HttpContext.Current.Session["SC5Params"] as ReportingServicesReportViewModel;

            return View(rptModel);
        }

        // POST: SC5Back Report
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult SC5Back(int? id)
        {
            return RedirectToAction("SC5", "Reports");
        }

        // GET: SC5A Report
        public ActionResult SC5A(int? id)
        {
            var model = new SC5AParameterViewModel
            {
                Wards = ds.GetWards(),
                StartDate = (DateTime)(System.Web.HttpContext.Current.Session["selectedstartdate"] ?? DateTime.Today.AddMonths(-1)),
                EndDate = (DateTime)(System.Web.HttpContext.Current.Session["selectedenddate"] ?? DateTime.Today),
                WardCode = (string)(System.Web.HttpContext.Current.Session["selectedward"] ?? "")
            };
            return View(model);
        }

        // POST: SC5A Report
        [HttpPost]
        public ActionResult SC5A(SC5AParameterViewModel model)
        {
            System.Web.HttpContext.Current.Session["selectedstartdate"] = model.StartDate;
            System.Web.HttpContext.Current.Session["selectedenddate"] = model.EndDate;
            System.Web.HttpContext.Current.Session["selectedward"] = model.WardCode;

            ReportingServicesReportViewModel model2 = new ReportingServicesReportViewModel("SC5A",
                new List<Microsoft.Reporting.WebForms.ReportParameter>()
                {
                    new Microsoft.Reporting.WebForms.ReportParameter("StartDate", model.StartDate.ToString("d"),false),
                    new Microsoft.Reporting.WebForms.ReportParameter("EndDate", model.EndDate.ToString("d"),false),
                    new Microsoft.Reporting.WebForms.ReportParameter("WardName", model.WardCode,false)
                });

            System.Web.HttpContext.Current.Session["SC5AParams"] = model2;

            return RedirectToAction("SC5AReportViewer", "Reports");
        }

        // GET: SC5AReportViewer
        //[OutputCache(NoStore = true, Duration = 0)]
        public ActionResult SC5AReportViewer()
        {
            ReportingServicesReportViewModel rptModel = System.Web.HttpContext.Current.Session["SC5AParams"] as ReportingServicesReportViewModel;

            return View(rptModel);
        }

        // POST: SC5ABack Report
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult SC5ABack(int? id)
        {
            return RedirectToAction("SC5A", "Reports");
        }

        // GET: SC6 Report
        public ActionResult SC6(int? id)
        {
            var model = new SC6ParameterViewModel
            {
                Wards = ds.GetWards(),
                //StartDate = DateTime.Today.AddMonths(-1),
                //EndDate = DateTime.Today,
                //WardCode = (string)(System.Web.HttpContext.Current.Session["selectedward"] ?? "")
                StartDate = (DateTime)(System.Web.HttpContext.Current.Session["selectedstartdate"] ?? DateTime.Today.AddMonths(-1)),
                EndDate = (DateTime)(System.Web.HttpContext.Current.Session["selectedenddate"] ?? DateTime.Today),
                WardCode = (string)(System.Web.HttpContext.Current.Session["selectedward"] ?? "")
            };
            return View(model);
        }

        // POST: SC6 Report
        [HttpPost]
        public ActionResult SC6(SC6ParameterViewModel model)
        {
            System.Web.HttpContext.Current.Session["selectedstartdate"] = model.StartDate;
            System.Web.HttpContext.Current.Session["selectedenddate"] = model.EndDate;
            System.Web.HttpContext.Current.Session["selectedward"] = model.WardCode;

            ReportingServicesReportViewModel model2 = new ReportingServicesReportViewModel("SC6",
                new List<Microsoft.Reporting.WebForms.ReportParameter>()
                {
                    new Microsoft.Reporting.WebForms.ReportParameter("StartDate", model.StartDate.ToString("d"),false),
                    new Microsoft.Reporting.WebForms.ReportParameter("EndDate", model.EndDate.ToString("d"),false),
                    new Microsoft.Reporting.WebForms.ReportParameter("Ward_Name", model.WardCode,false)
                });

            System.Web.HttpContext.Current.Session["SC6Params"] = model2;

            return RedirectToAction("SC6ReportViewer", "Reports");
        }

        // GET: SC6ReportViewer
        //[OutputCache(NoStore = true, Duration = 0)]
        public ActionResult SC6ReportViewer()
        {
            //ReportingServicesReportViewModel rptModel = System.Web.HttpContext.Current.Session["SC6Params"] as ReportingServicesReportViewModel;

            //string SSRSusername = System.Configuration.ConfigurationManager.AppSettings["SSRSUsername"];
            //string SSRSpassword = System.Configuration.ConfigurationManager.AppSettings["SSRSPassword"];
            //string SSRSdomain = System.Configuration.ConfigurationManager.AppSettings["SSRSDomain"];
            //string SSRSurl = System.Configuration.ConfigurationManager.AppSettings["SSRSUrl"];
            //string SSRSreportpath = System.Configuration.ConfigurationManager.AppSettings["SSRSReportPath"];

            //IReportServerCredentials irsc = new ReportServerCredentials(SSRSusername, SSRSpassword, SSRSdomain);

            //ReportParameter[] RptParameters = rptModel.parameters;

            //ReportViewer reportViewer = new ReportViewer();
            //reportViewer.ProcessingMode = ProcessingMode.Remote;

            ////Set Look
            //reportViewer.SizeToReportContent = true;
            //reportViewer.Width = Unit.Percentage(99);
            //reportViewer.BorderStyle = BorderStyle.None;

            //reportViewer.ServerReport.ReportServerCredentials = irsc;

            //reportViewer.ServerReport.ReportPath = SSRSreportpath + "SC6Report";
            //reportViewer.ServerReport.ReportServerUrl = new Uri(SSRSurl);

            //// Set the Start Date report parameters for the report
            //reportViewer.ServerReport.SetParameters(new ReportParameter[] { RptParameters[0] });

            //// Set the End Date report parameters for the report
            //reportViewer.ServerReport.SetParameters(new ReportParameter[] { RptParameters[1] });

            //// Set the Ward Name report parameters for the report
            //reportViewer.ServerReport.SetParameters(new ReportParameter[] { RptParameters[2] });

            //ViewBag.ReportViewer = reportViewer;

            //return View();
            ReportingServicesReportViewModel rptModel = System.Web.HttpContext.Current.Session["SC6Params"] as ReportingServicesReportViewModel;

            return View(rptModel);
        }

        // POST: SC6Back Report
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult SC6Back(int? id)
        {
            return RedirectToAction("SC6", "Reports");
        }

        // GET: SC7 Report
        public ActionResult SC7(int? id)
        {
            var model = new SC7ParameterViewModel
            {
                LandUseClasses = ds.GetLandUseClasses(),
                //StartDate = DateTime.Today.AddMonths(-1),
                //EndDate = DateTime.Today,
                StartDate = (DateTime)(System.Web.HttpContext.Current.Session["selectedstartdate"] ?? DateTime.Today.AddMonths(-1)),
                EndDate = (DateTime)(System.Web.HttpContext.Current.Session["selectedenddate"] ?? DateTime.Today),
                LandUseClass = (string)(System.Web.HttpContext.Current.Session["selectedlanduse"] ?? "")
            };
            return View(model);
        }

        // POST: SC7 Report
        [HttpPost]
        public ActionResult SC7(SC7ParameterViewModel model)
        {
            System.Web.HttpContext.Current.Session["selectedstartdate"] = model.StartDate;
            System.Web.HttpContext.Current.Session["selectedenddate"] = model.EndDate;
            System.Web.HttpContext.Current.Session["selectedlanduseclass"] = model.LandUseClass;

            ReportingServicesReportViewModel model2 = new ReportingServicesReportViewModel("SC7",
                new List<Microsoft.Reporting.WebForms.ReportParameter>()
                {
                    new Microsoft.Reporting.WebForms.ReportParameter("StartDate", model.StartDate.ToString("d"),false),
                    new Microsoft.Reporting.WebForms.ReportParameter("EndDate", model.EndDate.ToString("d"),false),
                    new Microsoft.Reporting.WebForms.ReportParameter("LandUseClass", model.LandUseClass,false)
                });

            System.Web.HttpContext.Current.Session["SC7Params"] = model2;

            return RedirectToAction("SC7ReportViewer", "Reports");
        }

        // GET: SC7ReportViewer
        //[OutputCache(NoStore = true, Duration = 0)]
        public ActionResult SC7ReportViewer()
        {
            //ReportingServicesReportViewModel rptModel = System.Web.HttpContext.Current.Session["SC7Params"] as ReportingServicesReportViewModel;

            //string SSRSusername = System.Configuration.ConfigurationManager.AppSettings["SSRSUsername"];
            //string SSRSpassword = System.Configuration.ConfigurationManager.AppSettings["SSRSPassword"];
            //string SSRSdomain = System.Configuration.ConfigurationManager.AppSettings["SSRSDomain"];
            //string SSRSurl = System.Configuration.ConfigurationManager.AppSettings["SSRSUrl"];
            //string SSRSreportpath = System.Configuration.ConfigurationManager.AppSettings["SSRSReportPath"];

            //IReportServerCredentials irsc = new ReportServerCredentials(SSRSusername, SSRSpassword, SSRSdomain);

            //ReportParameter[] RptParameters = rptModel.parameters;

            //ReportViewer reportViewer = new ReportViewer();
            //reportViewer.ProcessingMode = ProcessingMode.Remote;

            ////Set Look
            //reportViewer.SizeToReportContent = true;
            //reportViewer.Width = Unit.Percentage(99);
            //reportViewer.BorderStyle = BorderStyle.None;

            //reportViewer.ServerReport.ReportServerCredentials = irsc;

            //reportViewer.ServerReport.ReportPath = SSRSreportpath + "SC7Report";
            //reportViewer.ServerReport.ReportServerUrl = new Uri(SSRSurl);

            //// Set the Start Date report parameters for the report
            //reportViewer.ServerReport.SetParameters(new ReportParameter[] { RptParameters[0] });

            //// Set the End Date report parameters for the report
            //reportViewer.ServerReport.SetParameters(new ReportParameter[] { RptParameters[1] });

            //// Set the Ward Name report parameters for the report
            //reportViewer.ServerReport.SetParameters(new ReportParameter[] { RptParameters[2] });

            //ViewBag.ReportViewer = reportViewer;

            //return View();
            ReportingServicesReportViewModel rptModel = System.Web.HttpContext.Current.Session["SC7Params"] as ReportingServicesReportViewModel;

            return View(rptModel);
        }

        // POST: SC7Back Report
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult SC7Back(int? id)
        {
            return RedirectToAction("SC7", "Reports");
        }

        // GET: SC8 Report
        public ActionResult SC8(int? id)
        {
            var model = new SC8ParameterViewModel
            {
                Inspectors = ds.GetInspectorList(),
                StartDate = (DateTime)(System.Web.HttpContext.Current.Session["selectedstartdate"] ?? DateTime.Today.AddMonths(-1)),
                EndDate = (DateTime)(System.Web.HttpContext.Current.Session["selectedenddate"] ?? DateTime.Today),
                Inspector = (string)(System.Web.HttpContext.Current.Session["selectedinspector"] ?? "")
            };
            return View(model);
        }

        // POST: SC8 Report
        [HttpPost]
        public ActionResult SC8(SC8ParameterViewModel model)
        {
            System.Web.HttpContext.Current.Session["selectedstartdate"] = model.StartDate;
            System.Web.HttpContext.Current.Session["selectedenddate"] = model.EndDate;
            System.Web.HttpContext.Current.Session["selectedinspector"] = model.Inspector;

            ReportingServicesReportViewModel model2 = new ReportingServicesReportViewModel("SC8",
                new List<Microsoft.Reporting.WebForms.ReportParameter>()
                {
                    new Microsoft.Reporting.WebForms.ReportParameter("StartDate", model.StartDate.ToString("d"),false),
                    new Microsoft.Reporting.WebForms.ReportParameter("EndDate", model.EndDate.ToString("d"),false),
                    new Microsoft.Reporting.WebForms.ReportParameter("LoginName", model.Inspector,false)
                });

            System.Web.HttpContext.Current.Session["SC8Params"] = model2;

            return RedirectToAction("SC8ReportViewer", "Reports");
        }

        // GET: SC8ReportViewer
        //[OutputCache(NoStore = true, Duration = 0)]
        public ActionResult SC8ReportViewer()
        {
            //ViewData.Clear();

            //ReportingServicesReportViewModel rptModel = System.Web.HttpContext.Current.Session["SC8Params"] as ReportingServicesReportViewModel;

            //string SSRSusername = System.Configuration.ConfigurationManager.AppSettings["SSRSUsername"];
            //string SSRSpassword = System.Configuration.ConfigurationManager.AppSettings["SSRSPassword"];
            //string SSRSdomain = System.Configuration.ConfigurationManager.AppSettings["SSRSDomain"];
            //string SSRSurl = System.Configuration.ConfigurationManager.AppSettings["SSRSUrl"];
            //string SSRSreportpath = System.Configuration.ConfigurationManager.AppSettings["SSRSReportPath"];

            //IReportServerCredentials irsc = new ReportServerCredentials(SSRSusername, SSRSpassword, SSRSdomain);

            //ReportParameter[] RptParameters = rptModel.parameters;

            //ReportViewer reportViewer = new ReportViewer();
            //reportViewer.ProcessingMode = ProcessingMode.Remote;

            ////Set Look
            //reportViewer.SizeToReportContent = true;
            //reportViewer.Width = Unit.Percentage(99);
            //reportViewer.BorderStyle = BorderStyle.None;

            //reportViewer.ServerReport.ReportServerCredentials = irsc;

            //reportViewer.ServerReport.ReportPath = SSRSreportpath + "SC8Report";
            //reportViewer.ServerReport.ReportServerUrl = new Uri(SSRSurl);

            //// Set the Start Date report parameters for the report
            //reportViewer.ServerReport.SetParameters(new ReportParameter[] { RptParameters[0] });

            //// Set the End Date report parameters for the report
            //reportViewer.ServerReport.SetParameters(new ReportParameter[] { RptParameters[1] });

            //// Set the Ward Name report parameters for the report
            //reportViewer.ServerReport.SetParameters(new ReportParameter[] { RptParameters[2] });

            //ViewBag.ReportViewer = reportViewer;

            //return View();
            ReportingServicesReportViewModel rptModel = System.Web.HttpContext.Current.Session["SC8Params"] as ReportingServicesReportViewModel;

            return View(rptModel);
        }

        // POST: SC8Back Report
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult SC8Back(int? id)
        {
            return RedirectToAction("SC8", "Reports");
        }

        // GET: SC9 Report
        public ActionResult SC9(int? id)
        {
            var SC9Model = new SC9ParameterViewModel();
            var SFModel = new StreetFinderViewModel();

            switch (id)
            {
                case 0:
                    SC9Model = new SC9ParameterViewModel
                    {
                        StartDate = (DateTime)(System.Web.HttpContext.Current.Session["selectedstartdate"] ?? DateTime.Today.AddDays(-(6*7))),
                        EndDate = (DateTime)(System.Web.HttpContext.Current.Session["selectedenddate"] ?? DateTime.Today),
                    };

                    SFModel = new StreetFinderViewModel
                    {
                        StreetPrompt = "Enter known street details:",
                        StreetName = (string)(System.Web.HttpContext.Current.Session["selectedstreet"] ?? ""),
                        WardCode = (string)(System.Web.HttpContext.Current.Session["selectedward"] ?? ""),
                        Wards = ds.GetWards(),
                        //StartDate = (DateTime)(System.Web.HttpContext.Current.Session["selectedstartdate"] ?? DateTime.Today.AddDays(-(6*7))),
                        //EndDate = (DateTime)(System.Web.HttpContext.Current.Session["selectedenddate"] ?? DateTime.Today)
                    };
                    break;
                case 1:
                    SC9Model = new SC9ParameterViewModel
                    {
                        StartDate = (DateTime)(System.Web.HttpContext.Current.Session["selectedstartdate"] ?? DateTime.Today.AddDays(-(6 * 7))),
                        EndDate = (DateTime)(System.Web.HttpContext.Current.Session["selectedenddate"] ?? DateTime.Today),
                    };

                    SFModel = new StreetFinderViewModel
                    {
                        StreetPrompt = "Enter known street details:",
                        StreetName = (string)(System.Web.HttpContext.Current.Session["selectedstreet"] ?? ""),
                        WardCode = (string)(System.Web.HttpContext.Current.Session["selectedward"] ?? ""),
                        Wards = ds.GetWards(),
                        //StartDate = (DateTime)(System.Web.HttpContext.Current.Session["selectedstartdate"] ?? DateTime.Today.AddDays(-(6 * 7))),
                        //EndDate = (DateTime)(System.Web.HttpContext.Current.Session["selectedenddate"] ?? DateTime.Today)
                    };
                    break;
                case 2:
                    SC9Model = new SC9ParameterViewModel
                    {
                        StartDate = (DateTime)(System.Web.HttpContext.Current.Session["selectedstartdate"] ?? DateTime.Today.AddDays(-(6 * 7))),
                        EndDate = (DateTime)(System.Web.HttpContext.Current.Session["selectedenddate"] ?? DateTime.Today),
                    };

                    SFModel = new StreetFinderViewModel
                    {
                        StreetPrompt = "Enter known street details:",
                        StreetName = (string)(System.Web.HttpContext.Current.Session["selectedstreet"] ?? ""),
                        WardCode = (string)(System.Web.HttpContext.Current.Session["selectedward"] ?? ""),
                        Wards = ds.GetWards(),
                        //StartDate = (DateTime)(System.Web.HttpContext.Current.Session["selectedstartdate"] ?? DateTime.Today.AddDays(-(6 * 7))),
                        //EndDate = (DateTime)(System.Web.HttpContext.Current.Session["selectedenddate"] ?? DateTime.Today)
                    };
                    break;
            }

            var model = new SC9StreetFinderViewModel
            {
                SC9VM = SC9Model, 
                StreetFinderVM = SFModel
            };

            return View(model);
        }

        // POST: SC9 Report
        [HttpPost]
        public ActionResult SC9(SC9StreetFinderViewModel model)
        {
            System.Web.HttpContext.Current.Session["selectedstartdate"] = model.SC9VM.StartDate;
            System.Web.HttpContext.Current.Session["selectedenddate"] = model.SC9VM.EndDate;

            var idParam1 = new SqlParameter
            {
                ParameterName = "StreetName",
                Value = model.StreetFinderVM.StreetName ?? ""
            };
            System.Web.HttpContext.Current.Session["selectedstreet"] = idParam1.Value;

            var idParam2 = new SqlParameter
            {
                ParameterName = "WardName",
                Value = model.StreetFinderVM.WardCode ?? ""
            };
            System.Web.HttpContext.Current.Session["selectedward"] = idParam2.Value;

            model.StreetFinderVM.StreetsFound = new List<StreetFinderEditorViewModel>();

            var getStreetListContext = db.Database.SqlQuery<GetStreetListContext>("exec GetStreetList @StreetName, @WardName ", idParam1, idParam2).ToList();

            foreach (var street in getStreetListContext)
            {
                var streetVM = new StreetFinderEditorViewModel();

                streetVM.Ward_Name = street.Ward_Name;
                streetVM.District = street.District;
                streetVM.Street_Name = street.Street_Name;
                streetVM.Limits = street.Limits;
                streetVM.streetsection = street.streetsection;

                model.StreetFinderVM.StreetsFound.Add(streetVM);
            }

            if (model.StreetFinderVM.StreetsFound.Count == 0)
            {
                model.StreetFinderVM.StreetPrompt = "No streets found with this name.";
            }
            else
            {
                model.StreetFinderVM.StreetPrompt = "Select your street from list of matches below:";
            }

            return View(model);
        }

        // GET: SC9Street Report
        public ActionResult SC9Street(string street)
        {
            string strWardName;
            string strStreetName = "";
            string strLimits = "";

            ExtractStreetLimitsFromSection(street, out strWardName, out strStreetName, out strLimits);

            ReportingServicesReportViewModel model2 = new ReportingServicesReportViewModel("SC9",
                new List<Microsoft.Reporting.WebForms.ReportParameter>()
                {
                    new Microsoft.Reporting.WebForms.ReportParameter("StartDate", System.Web.HttpContext.Current.Session["selectedstartdate"].ToString(),false),
                    new Microsoft.Reporting.WebForms.ReportParameter("EndDate", System.Web.HttpContext.Current.Session["selectedenddate"].ToString(),false),
                    new Microsoft.Reporting.WebForms.ReportParameter("Ward_name", strWardName,false),
                    new Microsoft.Reporting.WebForms.ReportParameter("Street_name", strStreetName,false),
                    new Microsoft.Reporting.WebForms.ReportParameter("Limits", strLimits,false)
                });

            System.Web.HttpContext.Current.Session["SC9Params"] = model2;

            return RedirectToAction("SC9ReportViewer", "Reports");
        }

        private void ExtractStreetLimitsFromSection(string strStreetSection, out string wardName, out string street, out string limits)
        {
            char[] separatingChars = "__".ToCharArray();

            string[] strarr = strStreetSection.Split(separatingChars,StringSplitOptions.RemoveEmptyEntries);

            //return the extracted ward, street and limits
            wardName = strarr[0];
            street = strarr[2];
            limits = strarr[3];
        }

        // GET: SC9ReportViewer
        //[OutputCache(NoStore = true, Duration = 0)]
        public ActionResult SC9ReportViewer()
        {
            ReportingServicesReportViewModel rptModel = System.Web.HttpContext.Current.Session["SC9Params"] as ReportingServicesReportViewModel;

            return View(rptModel);
        }

        // POST: SC9Back Report
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult SC9Back(int? id)
        {
            return RedirectToAction("SC9", "Reports", new { id = 0 });
        }

        //// POST: SC9 Report
        //[HttpPost]
        //public ActionResult StreetFinder(StreetFinderViewModel model)
        //{
        //    System.Web.HttpContext.Current.Session["selectedstartdate"] = model.StartDate;
        //    System.Web.HttpContext.Current.Session["selectedenddate"] = model.EndDate;

        //    var idParam1 = new SqlParameter
        //    {
        //        ParameterName = "StreetName",
        //        Value = model.StreetName ?? ""
        //    };
        //    System.Web.HttpContext.Current.Session["selectedstreet"] = idParam1.Value;

        //    var idParam2 = new SqlParameter
        //    {
        //        ParameterName = "WardName",
        //        Value = model.WardCode ?? ""
        //    };
        //    System.Web.HttpContext.Current.Session["selectedward"] = idParam2.Value;

        //    model.StreetsFound = new List<StreetFinderEditorViewModel>();

        //    var getStreetListContext = db.Database.SqlQuery<GetStreetListContext>("exec GetStreetList @StreetName, @WardName ", idParam1, idParam2).ToList();

        //    foreach (var street in getStreetListContext)
        //    {
        //        var streetVM = new StreetFinderEditorViewModel();

        //        streetVM.Ward_Name = street.Ward_Name;
        //        streetVM.District = street.District;
        //        streetVM.Street_Name = street.Street_Name;
        //        streetVM.Limits = street.Limits;
        //        streetVM.streetsection = street.streetsection;

        //        model.StreetsFound.Add(streetVM);
        //    }

        //    if (model.StreetsFound.Count == 0)
        //    {
        //        model.StreetPrompt = "No streets found with this name.";
        //    }
        //    else
        //    {
        //        model.StreetPrompt = "Select your street from list of matches below:";
        //    }

        //    return View(model);
        //}

        // GET: SC10 Report
        public ActionResult SC10(int? id)
        {
            var model = new SC10ParameterViewModel
            {
                Wards = ds.GetWards(),
                Frequencies = ds.GetFrequencies(),
                StartDate = (DateTime)(System.Web.HttpContext.Current.Session["selectedstartdate"] ?? DateTime.Today.AddMonths(-1)),
                EndDate = (DateTime)(System.Web.HttpContext.Current.Session["selectedenddate"] ?? DateTime.Today),
                WardCode = (string)(System.Web.HttpContext.Current.Session["selectedward"] ?? ""),
                Frequency = (string)(System.Web.HttpContext.Current.Session["selectedfrequency"] ?? "")
            };
            return View(model);
        }

        // POST: SC10 Report
        [HttpPost]
        public ActionResult SC10(SC10ParameterViewModel model)
        {
            System.Web.HttpContext.Current.Session["selectedstartdate"] = model.StartDate;
            System.Web.HttpContext.Current.Session["selectedenddate"] = model.EndDate;
            System.Web.HttpContext.Current.Session["selectedward"] = model.WardCode;
            System.Web.HttpContext.Current.Session["selectedfrequency"] = model.Frequency;

            ReportingServicesReportViewModel model2 = new ReportingServicesReportViewModel("SC10",
                new List<Microsoft.Reporting.WebForms.ReportParameter>()
                {
                    new Microsoft.Reporting.WebForms.ReportParameter("StartDate", model.StartDate.ToString("d"),false),
                    new Microsoft.Reporting.WebForms.ReportParameter("EndDate", model.EndDate.ToString("d"),false),
                    new Microsoft.Reporting.WebForms.ReportParameter("WardName", model.WardCode,false),
                    new Microsoft.Reporting.WebForms.ReportParameter("Freq_Code", model.Frequency,false)
                });

            System.Web.HttpContext.Current.Session["SC10Params"] = model2;

            return RedirectToAction("SC10ReportViewer", "Reports");
        }

        // GET: SC10ReportViewer
        //[OutputCache(NoStore = true, Duration = 0)]
        public ActionResult SC10ReportViewer()
        {
            ReportingServicesReportViewModel rptModel = System.Web.HttpContext.Current.Session["SC10Params"] as ReportingServicesReportViewModel;

            return View(rptModel);
        }

        [HttpPost]
        [MultipleButton(Name = "action", Argument = "back")]
        public ActionResult SC10Back()
        {
            return RedirectToAction("SC10", "Reports");
        }

        [HttpPost]
        [MultipleButton(Name = "action", Argument = "export")]
        public void SC10Export()
        {
            ReportingServicesReportViewModel rptModel = System.Web.HttpContext.Current.Session["SC10Params"] as ReportingServicesReportViewModel;

            if (rptModel != null)
            {
                ExportClientsListToCSV("UnCheckedTransects", ds.GetUnCheckedTransects((Convert.ToDateTime(rptModel.parameters[0].Values[0])), (Convert.ToDateTime(rptModel.parameters[1].Values[0])), (rptModel.parameters[2].Values[0]), (rptModel.parameters[3].Values[0])));
            }
        }

        // GET: SC11 Report
        public ActionResult SC11(int? id)
        {
            var model = new SC11ParameterViewModel
            {
                Wards = ds.GetWards(),
                StartDate = (DateTime)(System.Web.HttpContext.Current.Session["selectedstartdate"] ?? DateTime.Today.AddMonths(-1)),
                EndDate = (DateTime)(System.Web.HttpContext.Current.Session["selectedenddate"] ?? DateTime.Today),
                WardCode = (string)(System.Web.HttpContext.Current.Session["selectedward"] ?? "")
            };
            return View(model);
        }

        // POST: SC11 Report
        [HttpPost]
        public ActionResult SC11(SC11ParameterViewModel model)
        {
            System.Web.HttpContext.Current.Session["selectedstartdate"] = model.StartDate;
            System.Web.HttpContext.Current.Session["selectedenddate"] = model.EndDate;
            System.Web.HttpContext.Current.Session["selectedward"] = model.WardCode;

            ReportingServicesReportViewModel model2 = new ReportingServicesReportViewModel("SC11",
                new List<Microsoft.Reporting.WebForms.ReportParameter>()
                {
                    new Microsoft.Reporting.WebForms.ReportParameter("StartDate", model.StartDate.ToString("d"),false),
                    new Microsoft.Reporting.WebForms.ReportParameter("EndDate", model.EndDate.ToString("d"),false),
                    new Microsoft.Reporting.WebForms.ReportParameter("Ward_Name", model.WardCode,false)
                });

            System.Web.HttpContext.Current.Session["SC11Params"] = model2;

            return RedirectToAction("SC11ReportViewer", "Reports");
        }

        // GET: SC11ReportViewer
        //[OutputCache(NoStore = true, Duration = 0)]
        public ActionResult SC11ReportViewer()
        {
            ReportingServicesReportViewModel rptModel = System.Web.HttpContext.Current.Session["SC11Params"] as ReportingServicesReportViewModel;

            return View(rptModel);
        }

        // POST: SC11Back Report
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult SC11Back(int? id)
        {
            return RedirectToAction("SC11", "Reports");
        }

        // GET: SC12 Report
        public ActionResult SC12(int? id)
        {
            var model = new SC12ParameterViewModel
            {
                StartDate = (DateTime)(System.Web.HttpContext.Current.Session["selectedstartdate"] ?? DateTime.Today.AddMonths(-1)),
                EndDate = (DateTime)(System.Web.HttpContext.Current.Session["selectedenddate"] ?? DateTime.Today),
            };
            return View(model);
        }

        // POST: SC12 Report
        [HttpPost]
        public void SC12(SC12ParameterViewModel model)
        {
            ExportClientsListToCSV("FailedTransectsCSVExport", ds.GetFailedTransectsInCSVFormat(model.StartDate, model.EndDate));
        }

        private void ExportClientsListToCSV(string fileName, DataTable dt)
        {

            StringWriter sw = new StringWriter();
            string columnHeaders = string.Empty;

            foreach (DataColumn dc in dt.Columns)
            {
                columnHeaders = columnHeaders + dc.ColumnName.ToString() + ", ";
            }

            sw.WriteLine(columnHeaders);

            foreach (DataRow dr in dt.Rows)
            {
                string columnValues = string.Empty;
                foreach (DataColumn dc in dt.Columns)
                {
                    columnValues = columnValues + dr[dc.ColumnName].ToString().Replace(",", "") + ", ";
                }
                sw.WriteLine(columnValues);
            }

            Response.ClearContent();
            Response.AddHeader("content-disposition", string.Format("attachment;filename={0}.csv", fileName));
            Response.ContentType = "text/csv";

            Response.Write(sw.ToString());

            Response.End();

        }

        // GET: SCNotices Report
        public ActionResult SCNotices(int? id)
        {
            var model = new SCNoticesParameterViewModel
            {
                StartDate = (DateTime)(System.Web.HttpContext.Current.Session["selectedstartdate"] ?? DateTime.Today.AddMonths(-1)),
                EndDate = (DateTime)(System.Web.HttpContext.Current.Session["selectedenddate"] ?? DateTime.Today),
            };
            return View(model);
        }

        // POST: SCNotices Report
        [HttpPost]
        public ActionResult SCNotices(SCNoticesParameterViewModel model)
        {
            System.Web.HttpContext.Current.Session["selectedstartdate"] = model.StartDate;
            System.Web.HttpContext.Current.Session["selectedenddate"] = model.EndDate;

            ReportingServicesReportViewModel model2 = new ReportingServicesReportViewModel("SCNotices",
                new List<Microsoft.Reporting.WebForms.ReportParameter>()
                {
                    new Microsoft.Reporting.WebForms.ReportParameter("StartDate", model.StartDate.ToString("d"),false),
                    new Microsoft.Reporting.WebForms.ReportParameter("EndDate", model.EndDate.ToString("d"),false),
                });

            //ViewBag.SC4Params = model2;
            System.Web.HttpContext.Current.Session["SCNoticesParams"] = model2;

            return RedirectToAction("SCNoticesReportViewer", "Reports");
        }

        // GET: SCNoticesReportViewer
        //[OutputCache(NoStore = true, Duration = 0)]
        public ActionResult SCNoticesReportViewer()
        {
            ReportingServicesReportViewModel rptModel = System.Web.HttpContext.Current.Session["SCNoticesParams"] as ReportingServicesReportViewModel;

            return View(rptModel);
        }

        // POST: SCNoticesBack Report
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult SCNoticesBack(int? id)
        {
            return RedirectToAction("SCNotices", "Reports");
        }

        // GET: SCNotices Report
        public ActionResult SCNoticesA(int? id)
        {
            var model = new SCNoticesAParameterViewModel
            {
                StartDate = (DateTime)(System.Web.HttpContext.Current.Session["selectedstartdate"] ?? DateTime.Today.AddMonths(-1)),
                EndDate = (DateTime)(System.Web.HttpContext.Current.Session["selectedenddate"] ?? DateTime.Today),
            };
            return View(model);
        }

        // POST: SC12 Report
        [HttpPost]
        public void SCNoticesA(SCNoticesAParameterViewModel model)
        {
            var filename = "RectificationNoticeReport_between_" + model.StartDate.ToString("dd-MMM-yy") + "_and_" + model.EndDate.ToString("dd-MMM-yy");
            ExportClientsListToCSV(filename, ds.GetNoticeReportAsCSV(model.StartDate, model.EndDate));
        }

        // GET: EH1 Report
        public ActionResult EH1(int? id)
        {
            var model = new EH1ParameterViewModel
            {
                Areas = ds.GetAreas(),
                StartDate = (DateTime)(System.Web.HttpContext.Current.Session["selectedstartdate"] ?? DateTime.Today.AddMonths(-1)),
                EndDate = (DateTime)(System.Web.HttpContext.Current.Session["selectedenddate"] ?? DateTime.Today),
                AreaCode = (string)(System.Web.HttpContext.Current.Session["selectedarea"] ?? "")

            };
            return View(model);
        }

        // POST: EH1 Report
        [HttpPost]
        public ActionResult EH1(EH1ParameterViewModel model)
        {
            System.Web.HttpContext.Current.Session["selectedstartdate"] = model.StartDate;
            System.Web.HttpContext.Current.Session["selectedenddate"] = model.EndDate;
            System.Web.HttpContext.Current.Session["selectedarea"] = model.AreaCode;

            ReportingServicesReportViewModel model2 = new ReportingServicesReportViewModel("EH1",
                new List<Microsoft.Reporting.WebForms.ReportParameter>()
                {
                    new Microsoft.Reporting.WebForms.ReportParameter("StartDate", model.StartDate.ToString("d"),false),
                    new Microsoft.Reporting.WebForms.ReportParameter("EndDate", model.EndDate.ToString("d"),false),
                    new Microsoft.Reporting.WebForms.ReportParameter("Area_Name", model.AreaCode,false)
                });

            System.Web.HttpContext.Current.Session["EH1Params"] = model2;

            return RedirectToAction("EH1ReportViewer", "Reports");
        }

        // GET: EH1ReportViewer
        //[OutputCache(NoStore = true, Duration = 0)]
        public ActionResult EH1ReportViewer()
        {
            ReportingServicesReportViewModel rptModel = System.Web.HttpContext.Current.Session["EH1Params"] as ReportingServicesReportViewModel;

            return View(rptModel);
        }

        // POST: EH1Back Report
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult EH1Back(int? id)
        {
            return RedirectToAction("EH1", "Reports");
        }

        // GET: CM1 Report
        public ActionResult CM1(int? id)
        {
            var model = new CM1ParameterViewModel
            {
                Wards = ds.GetWards(),
                StartDate = (DateTime)(System.Web.HttpContext.Current.Session["selectedstartdate"] ?? DateTime.Today.AddMonths(-1)),
                EndDate = (DateTime)(System.Web.HttpContext.Current.Session["selectedenddate"] ?? DateTime.Today),
                WardCode = (string)(System.Web.HttpContext.Current.Session["selectedward"] ?? "")

            };
            return View(model);
        }

        // POST: CM1 Report
        [HttpPost]
        public ActionResult CM1(CM1ParameterViewModel model)
        {
            System.Web.HttpContext.Current.Session["selectedstartdate"] = model.StartDate;
            System.Web.HttpContext.Current.Session["selectedenddate"] = model.EndDate;
            System.Web.HttpContext.Current.Session["selectedward"] = model.WardCode;

            ReportingServicesReportViewModel model2 = new ReportingServicesReportViewModel("CM1",
                new List<Microsoft.Reporting.WebForms.ReportParameter>()
                {
                    new Microsoft.Reporting.WebForms.ReportParameter("StartDate", model.StartDate.ToString("d"),false),
                    new Microsoft.Reporting.WebForms.ReportParameter("EndDate", model.EndDate.ToString("d"),false),
                    new Microsoft.Reporting.WebForms.ReportParameter("Ward_Name", model.WardCode,false)
                });

            System.Web.HttpContext.Current.Session["CM1Params"] = model2;

            return RedirectToAction("CM1ReportViewer", "Reports");
        }

        // GET: CM1ReportViewer
        //[OutputCache(NoStore = true, Duration = 0)]
        public ActionResult CM1ReportViewer()
        {
            ReportingServicesReportViewModel rptModel = System.Web.HttpContext.Current.Session["CM1Params"] as ReportingServicesReportViewModel;

            return View(rptModel);
        }

        // POST: CM1Back Report
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult CM1Back(int? id)
        {
            return RedirectToAction("CM1", "Reports");
        }

        // GET: CM2 Report
        public ActionResult CM2(int? id)
        {
            DateTime todaylastmonth = DateTime.Today.AddMonths(-1);
            DateTime firstDayOfLastMonth = new DateTime(todaylastmonth.Year, todaylastmonth.Month, 1);
            var model = new CM2ParameterViewModel
            {
                StartDate = (DateTime)(System.Web.HttpContext.Current.Session["selectedstartdate"] ?? firstDayOfLastMonth),
                EndDate = (DateTime)(System.Web.HttpContext.Current.Session["selectedenddate"] ?? firstDayOfLastMonth.AddMonths(1).AddDays(-1)),
            };
            return View(model);
        }

        // POST: CM2 Report
        [HttpPost]
        public ActionResult CM2(CM2ParameterViewModel model)
        {
            System.Web.HttpContext.Current.Session["selectedstartdate"] = model.StartDate;
            System.Web.HttpContext.Current.Session["selectedenddate"] = model.EndDate;

            ReportingServicesReportViewModel model2 = new ReportingServicesReportViewModel("CM2",
                new List<Microsoft.Reporting.WebForms.ReportParameter>()
                {
                    new Microsoft.Reporting.WebForms.ReportParameter("StartDate", model.StartDate.ToString("d"),false),
                    new Microsoft.Reporting.WebForms.ReportParameter("EndDate", model.EndDate.ToString("d"),false),
                });

            System.Web.HttpContext.Current.Session["CM2Params"] = model2;

            return RedirectToAction("CM2ReportViewer", "Reports");
        }

        // GET: CM2ReportViewer
        //[OutputCache(NoStore = true, Duration = 0)]
        public ActionResult CM2ReportViewer()
        {
            ReportingServicesReportViewModel rptModel = System.Web.HttpContext.Current.Session["CM2Params"] as ReportingServicesReportViewModel;

            return View(rptModel);
        }

        // POST: CM2Back Report
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult CM2Back(int? id)
        {
            return RedirectToAction("CM2", "Reports");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        public ActionResult CM3(int? id)
        {
            DateTime todaylastmonth = DateTime.Today.AddMonths(-1);
            DateTime firstDayOfLastMonth = new DateTime(todaylastmonth.Year, todaylastmonth.Month, 1);
            var model = new CM3ParameterViewModel
            {
                StartDate = (DateTime)(System.Web.HttpContext.Current.Session["selectedstartdate"] ?? firstDayOfLastMonth),
                EndDate = (DateTime)(System.Web.HttpContext.Current.Session["selectedenddate"] ?? firstDayOfLastMonth.AddMonths(1).AddDays(-1)),
            };
            return View(model);
        }

        // POST: CM3 Report
        [HttpPost]
        public ActionResult CM3(CM3ParameterViewModel model)
        {
            System.Web.HttpContext.Current.Session["selectedstartdate"] = model.StartDate;
            System.Web.HttpContext.Current.Session["selectedenddate"] = model.EndDate;

            ReportingServicesReportViewModel model2 = new ReportingServicesReportViewModel("CM3",
                new List<Microsoft.Reporting.WebForms.ReportParameter>()
                {
                    new Microsoft.Reporting.WebForms.ReportParameter("StartDate", model.StartDate.ToString("d"),false),
                    new Microsoft.Reporting.WebForms.ReportParameter("EndDate", model.EndDate.ToString("d"),false),
                });

            System.Web.HttpContext.Current.Session["CM3Params"] = model2;

            return RedirectToAction("CM3ReportViewer", "Reports");
        }

        // GET: CM3ReportViewer
        //[OutputCache(NoStore = true, Duration = 0)]
        public ActionResult CM3ReportViewer()
        {
            ReportingServicesReportViewModel rptModel = System.Web.HttpContext.Current.Session["CM3Params"] as ReportingServicesReportViewModel;

            return View(rptModel);
        }

        // POST: CM3Back Report
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult CM3Back(int? id)
        {
            return RedirectToAction("CM3", "Reports");
        }

    }
}
