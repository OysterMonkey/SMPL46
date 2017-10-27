using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Data.Entity.Infrastructure;
using SMPL46.Models;
using SMPL46.ViewModels;
using Excel;
using FileHelpers;
using NLog;

namespace SMPL46.Controllers
{
    public class ManageTransectsController : Controller
    {
        private EalingPMS_DataMart_TestEntities1 db = new EalingPMS_DataMart_TestEntities1();
        public const int RecordsPerPage = 20;
        private AccountDataService ds = new AccountDataService();
        private static Logger logger = LogManager.GetCurrentClassLogger();

        public ManageTransectsController()  
        {  
            ViewBag.RecordsPerPage = RecordsPerPage;              
        } 

        // GET: ManageTransects
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult PostDownloadFileAction()
        {
            DownloadActionPlanFile();

            return null;
        }

        //public ActionResult PostDownloadFileAction()
        //{
        //    DownloadActionPlanFile();

        //    return View("Index");
        //}

        [HttpPost]
        public ActionResult PostUploadFileAction(HttpPostedFileBase file)
        {
            char[] separatingChars = ".".ToCharArray();
            var user_id = System.Web.HttpContext.Current.Session["username"];

            if (file != null)
            {
                //We have a file to play with

                string[] strarr = file.FileName.Split(separatingChars, StringSplitOptions.RemoveEmptyEntries);

                //Using ExcelDataReader
                using (var stream = file.InputStream)
                {
                    if (strarr[strarr.Length - 1].ToString() == "xls")
                    {
                        //1. Reading from a binary Excel file ('97-2003 format; *.xls)
                        using (var reader = ExcelReaderFactory.CreateBinaryReader(stream))
                        {
                            //We know we have a data reader from the XLS file specified by the user
                            //We first need to delete every record in the Manage_Transects_Master Table
                            db.Database.ExecuteSqlCommand("DELETE FROM Manage_Transects_Master");

                            reader.IsFirstRowAsColumnNames = true;

                            var result = reader.AsDataSet();

                            // The result of each spreadsheet is in result.Tables
                            for (int i = 0; i < result.Tables[0].Rows.Count; i++)
                            {
                                DataRow dr = result.Tables[0].Rows[i];
                                var mtm = new Manage_Transects_Master();

                                mtm.Cleaning_ID = Convert.ToInt32(dr["Cleaning_ID"]);
                                mtm.Street_Name = dr["Street_Name"].ToString();
                                mtm.Limits = dr["Limits"].ToString();
                                mtm.Ward_Name = dr["Ward_Name"].ToString();
                                mtm.District = dr["District"].ToString();
                                mtm.Freq_Code = dr["Freq_Code"].ToString();
                                mtm.Cleaning_Day = GetNullableInt(dr["Cleaning_Day"]);
                                mtm.Cleaning_Week = GetNullableInt(dr["Cleaning_Week"]);
                                mtm.Deep_Week = GetNullableInt(dr["Deep_Week"]);
                                mtm.Deep_Day = GetNullableInt(dr["Deep_Day"]);
                                mtm.Category = dr["Category"].ToString();
                                mtm.Land_Use_Desc = dr["Land_Use_Desc"].ToString();
                                mtm.Zone_Nr = dr["Zone_Nr"].ToString();
                                mtm.Length = (dr["Length"] == DBNull.Value) ? 0 : Convert.ToDouble(dr["Length"]);
                                mtm.USRN = dr["USRN"].ToString();
                                mtm.date_last_modified = DateTime.Now;
                                mtm.user_last_modified = user_id.ToString();

                                db.Manage_Transects_Master.Add(mtm);
                            }
                            try
                            {
                                db.SaveChanges();
                            }
                            catch (DbEntityValidationException ex)
                            {
                                foreach (DbEntityValidationResult item in ex.EntityValidationErrors)
                                {
                                    // Get entry

                                    DbEntityEntry entry = item.Entry;
                                    string entityTypeName = entry.Entity.GetType().Name;

                                    // Display or log error messages
                                    foreach (DbValidationError subItem in item.ValidationErrors)
                                    {
                                        string message = string.Format("SMPL Function 'updateTransect': Error '{0}' occurred in {1} at {2}",
                                                 subItem.ErrorMessage, entityTypeName, subItem.PropertyName);
                                        logger.Warn(message);
                                    }

                                    // Rollback changes
                                    switch (entry.State)
                                    {
                                        case EntityState.Added:
                                            entry.State = EntityState.Detached;
                                            break;
                                        case EntityState.Modified:
                                            entry.CurrentValues.SetValues(entry.OriginalValues);
                                            entry.State = EntityState.Unchanged;
                                            break;
                                        case EntityState.Deleted:
                                            entry.State = EntityState.Unchanged;
                                            break;
                                    }
                                }
                            }
                        }
                    }
                    else if (strarr[strarr.Length - 1].ToString() == "xlsx")
                    {
                        //2. Reading from a OpenXml Excel file (2007 format; *.xlsx)
                        using (var reader = ExcelReaderFactory.CreateOpenXmlReader(stream))
                        {
                            //We know we have a data reader from the XLS file specified by the user
                            //We first need to delete every record in the Manage_Transects_Master Table
                            db.Database.ExecuteSqlCommand("DELETE FROM Manage_Transects_Master");

                            reader.IsFirstRowAsColumnNames = true;

                            var result = reader.AsDataSet();

                            // The result of each spreadsheet is in result.Tables
                            for (int i = 0; i < result.Tables[0].Rows.Count; i++)
                            {
                                DataRow dr = result.Tables[0].Rows[i];
                                var mtm = new Manage_Transects_Master();

                                mtm.Cleaning_ID = Convert.ToInt32(dr["Cleaning_ID"]);
                                mtm.Street_Name = dr["Street_Name"].ToString();
                                mtm.Limits = dr["Limits"].ToString();
                                mtm.Ward_Name = dr["Ward_Name"].ToString();
                                mtm.District = dr["District"].ToString();
                                mtm.Freq_Code = dr["Freq_Code"].ToString();
                                mtm.Cleaning_Day = GetNullableInt(dr["Cleaning_Day"]);
                                mtm.Cleaning_Week = GetNullableInt(dr["Cleaning_Week"]);
                                mtm.Deep_Week = GetNullableInt(dr["Deep_Week"]);
                                mtm.Deep_Day = GetNullableInt(dr["Deep_Day"]);
                                mtm.Category = dr["Category"].ToString();
                                mtm.Land_Use_Desc = dr["Land_Use_Desc"].ToString();
                                mtm.Zone_Nr = dr["Zone_Nr"].ToString();
                                mtm.Length = (dr["Length"] == DBNull.Value) ? 0 : Convert.ToDouble(dr["Length"]);
                                mtm.USRN = dr["USRN"].ToString();
                                mtm.date_last_modified = DateTime.Now;
                                mtm.user_last_modified = user_id.ToString();

                                db.Manage_Transects_Master.Add(mtm);
                            }
                            try
                            {
                                db.SaveChanges();
                            }
                            catch (DbEntityValidationException ex)
                            {
                                foreach (DbEntityValidationResult item in ex.EntityValidationErrors)
                                {
                                    // Get entry

                                    DbEntityEntry entry = item.Entry;
                                    string entityTypeName = entry.Entity.GetType().Name;

                                    // Display or log error messages
                                    foreach (DbValidationError subItem in item.ValidationErrors)
                                    {
                                        string message = string.Format("SMPL Function 'updateTransect': Error '{0}' occurred in {1} at {2}",
                                                 subItem.ErrorMessage, entityTypeName, subItem.PropertyName);
                                        logger.Warn(message);
                                    }

                                    // Rollback changes
                                    switch (entry.State)
                                    {
                                        case EntityState.Added:
                                            entry.State = EntityState.Detached;
                                            break;
                                        case EntityState.Modified:
                                            entry.CurrentValues.SetValues(entry.OriginalValues);
                                            entry.State = EntityState.Unchanged;
                                            break;
                                        case EntityState.Deleted:
                                            entry.State = EntityState.Unchanged;
                                            break;
                                    }
                                }
                            }
                        }
                    }
                }
            }

            System.Web.HttpContext.Current.Session["selectedfiltermodel"] = null;

            return View("Index");
        }

        private int? GetNullableInt(object field)
        {
            int number = 0;

            if (int.TryParse(field.ToString(), out number) == false)
            {
                return null;
            }
            else
            {
                return number;
            }
        }

        private void DownloadActionPlanFile()
        {
            string filePath = "";
            string fileName = string.Format("SMPLTransectsDownload_{0}", DateTime.Now.ToString("yyyyMMdd_HHmm").Replace(" ", "_"));

            var dir = new System.IO.DirectoryInfo(Server.MapPath("~/ManageTransectsDownload/"));
            filePath = Path.Combine(dir.FullName, fileName);

            List<Manage_Transects_Master> trans = (from tr in db.Manage_Transects_Master
                                                   select new
                                                   {
                                                       tr.Cleaning_ID,
                                                       tr.Street_Name,
                                                       tr.Limits,
                                                       tr.Ward_Name,
                                                       tr.District,
                                                       tr.Freq_Code,
                                                       tr.Cleaning_Day,
                                                       tr.Cleaning_Week,
                                                       tr.Deep_Week,
                                                       tr.Deep_Day,
                                                       tr.Category,
                                                       tr.Land_Use_Desc,
                                                       tr.Zone_Nr,
                                                       tr.Length,
                                                       tr.USRN,
                                                       tr.date_last_modified,
                                                       tr.user_last_modified
                                                   }).AsEnumerable()
                         .Select(x => new Manage_Transects_Master {
                             Cleaning_ID = x.Cleaning_ID,
                             Street_Name = x.Street_Name,
                             Limits = x.Limits,
                             Ward_Name = x.Ward_Name,
                             District = x.District,
                             Freq_Code = x.Freq_Code,
                             Cleaning_Day = x.Cleaning_Day,
                             Cleaning_Week = x.Cleaning_Week,
                             Deep_Week = x.Deep_Week,
                             Deep_Day = x.Deep_Day,
                             Category = x.Category,
                             Land_Use_Desc = x.Land_Use_Desc,
                             Zone_Nr = x.Zone_Nr,
                             Length = x.Length,
                             USRN = x.USRN,
                             date_last_modified = x.date_last_modified,
                             user_last_modified = x.user_last_modified
                         }).ToList();

            List<ManageTransectsDownloadModel> transectDownload = new List<ManageTransectsDownloadModel>();

            foreach (var item in trans)
            {
                var transectDownloadLoad = new ManageTransectsDownloadModel();

                transectDownloadLoad.Cleaning_ID = item.Cleaning_ID;
                transectDownloadLoad.Street_Name = item.Street_Name;
                transectDownloadLoad.Limits = item.Limits;
                transectDownloadLoad.Ward_Name = item.Ward_Name;
                transectDownloadLoad.District = item.District;
                transectDownloadLoad.Freq_Code = item.Freq_Code;
                transectDownloadLoad.Cleaning_Day = item.Cleaning_Day;
                transectDownloadLoad.Cleaning_Week = item.Cleaning_Week;
                transectDownloadLoad.Deep_Week = item.Deep_Week;
                transectDownloadLoad.Deep_Day = item.Deep_Day;
                transectDownloadLoad.Category = item.Category;
                transectDownloadLoad.Land_Use_Desc = item.Land_Use_Desc;
                transectDownloadLoad.Zone_Nr = item.Zone_Nr;
                transectDownloadLoad.Length = item.Length;
                transectDownloadLoad.USRN = item.USRN;
                transectDownloadLoad.date_last_modified = item.date_last_modified;
                transectDownloadLoad.user_last_modified = item.user_last_modified;

                transectDownload.Add(transectDownloadLoad);
            }

            var temp = ((IEnumerable<ManageTransectsDownloadModel>)transectDownload).ToList();

            var engine = new FileHelperEngine<ManageTransectsDownloadModel>();

            //TODO - present it to the User
            StringWriter sw = new StringWriter();

            engine.HeaderText = engine.GetFileHeader();
            engine.WriteStream(sw,temp);

            Response.ClearContent();
            Response.AddHeader("content-disposition", string.Format("attachment;filename={0}.csv", fileName));
            Response.ContentType = "text/csv";

            Response.Write(sw.ToString());

            Response.End();

            Response.Flush();

            return;
        }

        // GET: History
        public ActionResult History(int? pageNum)
        {
            pageNum = pageNum ?? 0;

            //ManageTransectsHistoryViewModel model = (ManageTransectsHistoryViewModel)System.Web.HttpContext.Current.Session["transecthistorymodel"] ?? new ManageTransectsHistoryViewModel();
            ManageTransectsHistoryViewModel model = new ManageTransectsHistoryViewModel();

            if (!model.WebGridInit)
            {
                model.WebGridInit = true;
                model.TotalNumberProjects = db.Manage_Transects_Audit.Count();
                model.TransectsHistory = getTransectHistoryListAll();
            }

            model.IsEndOfRecords = false;
            model.TotalNumberProjects = model.TransectsHistory.Count();

            System.Web.HttpContext.Current.Session["transecthistorymodel"] = model;

            return View("History", model);
        }

        // GET: GetTransectDetails
        public ActionResult Details(int? pageNum)
        {
            pageNum = pageNum ?? 0;

            ManageTransectsViewModel model = (ManageTransectsViewModel)System.Web.HttpContext.Current.Session["selectedfiltermodel"] ?? new ManageTransectsViewModel();

            if (!model.WebGridInit)
            {
                model.WebGridInit = true;
                model.TotalNumberProjects = db.Manage_Transects_Master.Count();
                model.Transects = getTransectDetailsListAll();
                model.FilterWards = GetFilterWards(model.Transects);
                model.FilterFreqCodes = GetFilterFrequencies(model.Transects);
            }

            model.IsEndOfRecords = false;
            model.TotalNumberProjects = model.Transects.Count();

            //model.FilterWards = GetFilterWards(model.Transects);
            //model.FilterFreqCodes = GetFilterFrequencies(model.Transects);

            System.Web.HttpContext.Current.Session["selectedfiltermodel"] = model;

            return View("Details", model);
        }

        [HttpPost]
        public ActionResult Details(ManageTransectsViewModel model)
        {
            System.Web.HttpContext.Current.Session["selectedfilterstreet"] = model.FilterStreetStr ?? "";
            System.Web.HttpContext.Current.Session["selectedfilterlimits"] = model.FilterLimitsStr ?? "";
            System.Web.HttpContext.Current.Session["selectedfilterward"] = model.FilterWardCode ?? "";
            System.Web.HttpContext.Current.Session["selectedfilterfreq"] = model.FilterFreqCodeStr ?? "";

            RegenerateWebGrid();

            //var searchStreetStr = model.FilterStreetStr ?? "";
            //var searchLimitsStr = model.FilterLimitsStr ?? "";
            //var searchWardStr = model.FilterWardCode ?? "";
            //var searchFreqCodeStr = model.FilterFreqCodeStr ?? "";

            ////model = (ManageTransectsViewModel)System.Web.HttpContext.Current.Session["selectedfiltermodel"];
            ////List<ManageTransectsModel> transectListCollection = model.Transects;
            //model = new ManageTransectsViewModel();
            //model.WebGridInit = true;
            //model.TotalNumberProjects = db.Manage_Transects_Master.Count();
            //List<ManageTransectsModel> transectListCollection = getTransectDetailsListAll();

            //model.FilterWards = GetFilterWards(transectListCollection);
            //model.FilterFreqCodes = GetFilterFrequencies(transectListCollection);

            //if (transectListCollection != null && transectListCollection.Count > 0)
            //{
            //    if (!string.IsNullOrEmpty(searchStreetStr) && transectListCollection.Count > 0)
            //    {
            //        var searchedlist = (from list in transectListCollection where list.Street_Name.IndexOf(searchStreetStr, StringComparison.OrdinalIgnoreCase) >= 0 select list).ToList();
            //        model.Transects = searchedlist;
            //    } else {
            //        model.Transects = transectListCollection;
            //    }

            //    transectListCollection = model.Transects;
            //    if (!string.IsNullOrEmpty(searchLimitsStr) && transectListCollection.Count > 0)
            //    {
            //        var searchedlist = (from list in transectListCollection where list.Limits.IndexOf(searchLimitsStr, StringComparison.OrdinalIgnoreCase) >= 0 select list).ToList();
            //        model.Transects = searchedlist;
            //    }
            //    else
            //    {
            //        model.Transects = transectListCollection;
            //    }

            //    transectListCollection = model.Transects;
            //    if (!string.IsNullOrEmpty(searchWardStr) && transectListCollection.Count > 0)
            //    {
            //        if (searchWardStr != "ALL WARDS")
            //        {
            //            var searchedlist = (from list in transectListCollection where list.Ward_Name.IndexOf(searchWardStr, StringComparison.OrdinalIgnoreCase) >= 0 select list).ToList();
            //            model.Transects = searchedlist;
            //        }
            //        else
            //        {
            //            model.Transects = transectListCollection;
            //        }
            //    }
            //    else
            //    {
            //        model.Transects = transectListCollection;
            //    }

            //    transectListCollection = model.Transects;
            //    if (!string.IsNullOrEmpty(searchFreqCodeStr) && transectListCollection.Count > 0)
            //    {
            //        if (searchFreqCodeStr != "ALL FREQUENCIES")
            //        {
            //            var searchedlist = (from list in transectListCollection where list.Freq_Code.IndexOf(searchFreqCodeStr.Trim(), StringComparison.OrdinalIgnoreCase) >= 0 select list).ToList();
            //            model.Transects = searchedlist;
            //        }
            //        else
            //        {
            //            model.Transects = transectListCollection;
            //        }
            //    }
            //    else
            //    {
            //        model.Transects = transectListCollection;
            //    }
            //}

            //System.Web.HttpContext.Current.Session["selectedfiltermodel"] = model;

            model = (ManageTransectsViewModel)System.Web.HttpContext.Current.Session["selectedfiltermodel"] ?? new ManageTransectsViewModel();

            return View("Details", model);
        }

//***************************************************************************************************
//EDIT TRANSECT

        public ActionResult EditTransect(int? id)
        {
            EditTransectViewModel model = new EditTransectViewModel();

            if (id == null)
            {
                model.Wards = GetEditWardsList();
                model.Freq_Codes = GetEditFreqList();
                model.LandUses = GetEditLandUseList();
                model.Districts = GetEditDistrictList();
                model.Categories = GetEditCategoryList();
                model.Zone_Nrs = GetEditZoneNrList();
                model.Cleaning_Days = GetEditCleanDayList();
                model.Cleaning_Weeks = GetEditCleanWeekList();
                model.Deep_Days = GetEditDeepDayList();
                model.Deep_Weeks = GetEditDeepWeekList();
            }
            else
            {
                int transectID = id.GetValueOrDefault();

                //Find the data for the Transect ID that has been passed in
                var query = db.Manage_Transects_Master.First(a => a.Cleaning_ID == transectID);

                model.Cleaning_ID = transectID;
                model.Street_Name = query.Street_Name;
                model.Limits = query.Limits;
                model.Ward_Name = query.Ward_Name;
                model.District = query.District;
                model.Freq_Code = query.Freq_Code;
                model.Zone_Nr = query.Zone_Nr;
                model.Category = query.Category;
                model.Cleaning_Day = query.Cleaning_Day ?? 0;
                model.Cleaning_Week = query.Cleaning_Week ?? 0;
                model.Deep_Day = query.Deep_Day ?? 0;
                model.Deep_Week = query.Deep_Week ?? 0;
                model.Land_Use_Desc = query.Land_Use_Desc;
                model.Length = query.Length;
                model.USRN = query.USRN;

                model.Wards = GetEditWardsList();
                model.Freq_Codes = GetEditFreqList();
                model.LandUses = GetEditLandUseList();
                model.Districts = GetEditDistrictList();
                model.Categories = GetEditCategoryList();
                model.Zone_Nrs = GetEditZoneNrList();
                model.Cleaning_Days = GetEditCleanDayList();
                model.Cleaning_Weeks = GetEditCleanWeekList();
                model.Deep_Days = GetEditDeepDayList();
                model.Deep_Weeks = GetEditDeepWeekList();
            }

            return View("Edit", model);
        }

        [HttpPost]
        public ActionResult EditTransect(EditTransectViewModel model, string save, string cancel)
        {
            if (!string.IsNullOrEmpty(save))
            {
                updateTransect(model);
            }
            if (!string.IsNullOrEmpty(cancel))
            {
                ViewBag.Message = "The edit was cancelled!";
            }

            return RedirectToAction("Details");
        }

        private void updateTransect(EditTransectViewModel model)
        {
            //ManageTransectsViewModel MTmodel = new ManageTransectsViewModel();

            //MTmodel = (ManageTransectsViewModel)System.Web.HttpContext.Current.Session["selectedfiltermodel"];

            var x = db.Manage_Transects_Master.Find(model.Cleaning_ID);
            if (x != null)
            {
                x.Street_Name = model.Street_Name ?? "";
                x.Limits = model.Limits ?? "";
                x.Ward_Name = model.Ward_Name ?? "";
                x.District = model.District ?? "";
                x.Freq_Code = model.Freq_Code ?? "";
                x.Zone_Nr = model.Zone_Nr ?? "";
                x.Category = model.Category ?? "";
                x.Cleaning_Day = model.Cleaning_Day;
                x.Cleaning_Week = model.Cleaning_Week;
                x.Deep_Day = model.Deep_Day;
                x.Deep_Week = model.Deep_Week;
                x.Land_Use_Desc = model.Land_Use_Desc;
                x.Length = model.Length;
                x.USRN = model.USRN ?? "";

                try
                {
                    db.SaveChanges();
                }
                catch (DbEntityValidationException ex)
                {
                    foreach (DbEntityValidationResult item in ex.EntityValidationErrors)
                    {
                        // Get entry

                        DbEntityEntry entry = item.Entry;
                        string entityTypeName = entry.Entity.GetType().Name;

                        // Display or log error messages
                        foreach (DbValidationError subItem in item.ValidationErrors)
                        {
                            string message = string.Format("SMPL Function 'updateTransect': Error '{0}' occurred in {1} at {2}",
                                     subItem.ErrorMessage, entityTypeName, subItem.PropertyName);
                            logger.Warn(message);
                        }

                        // Rollback changes
                        switch (entry.State)
                        {
                            case EntityState.Added:
                                entry.State = EntityState.Detached;
                                break;
                            case EntityState.Modified:
                                entry.CurrentValues.SetValues(entry.OriginalValues);
                                entry.State = EntityState.Unchanged;
                                break;
                            case EntityState.Deleted:
                                entry.State = EntityState.Unchanged;
                                break;
                        }
                    }
                    return;
                }

                //We have just done a successful UPDATE of a row in the WebGrid.
                //We need to regenerate the WebGrid rather than simply re-using the existing one.
                RegenerateWebGrid();
            //    List<ManageTransectsModel> transectListCollection = new List<ManageTransectsModel>();

            //    transectListCollection = getTransectDetailsListAll();

            //    var searchStreetStr = (string)(System.Web.HttpContext.Current.Session["selectedfilterstreet"] ?? "");
            //    var searchLimitsStr = (string)(System.Web.HttpContext.Current.Session["selectedfilterlimits"] ?? "");
            //    var searchWardStr = (string)(System.Web.HttpContext.Current.Session["selectedfilterward"] ?? "");
            //    var searchFreqCodeStr = (string)(System.Web.HttpContext.Current.Session["selectedfilterfreq"] ?? "");

            //    if (transectListCollection != null && transectListCollection.Count > 0)
            //    {
            //        if (!string.IsNullOrEmpty(searchStreetStr) && transectListCollection.Count > 0)
            //        {
            //            var searchedlist = (from list in transectListCollection where list.Street_Name.IndexOf(searchStreetStr, StringComparison.OrdinalIgnoreCase) >= 0 select list).ToList();
            //            MTmodel.Transects = searchedlist;
            //        }
            //        else
            //        {
            //            MTmodel.Transects = transectListCollection;
            //        }

            //        transectListCollection = MTmodel.Transects;
            //        if (!string.IsNullOrEmpty(searchLimitsStr) && transectListCollection.Count > 0)
            //        {
            //            var searchedlist = (from list in transectListCollection where list.Limits.IndexOf(searchLimitsStr, StringComparison.OrdinalIgnoreCase) >= 0 select list).ToList();
            //            MTmodel.Transects = searchedlist;
            //        }
            //        else
            //        {
            //            MTmodel.Transects = transectListCollection;
            //        }

            //        transectListCollection = MTmodel.Transects;
            //        if (!string.IsNullOrEmpty(searchWardStr) && transectListCollection.Count > 0)
            //        {
            //            if (searchWardStr != "ALL WARDS")
            //            {
            //                var searchedlist = (from list in transectListCollection where list.Ward_Name.IndexOf(searchWardStr, StringComparison.OrdinalIgnoreCase) >= 0 select list).ToList();
            //                MTmodel.Transects = searchedlist;
            //            }
            //            else
            //            {
            //                MTmodel.Transects = transectListCollection;
            //            }
            //        }
            //        else
            //        {
            //            MTmodel.Transects = transectListCollection;
            //        }

            //        transectListCollection = MTmodel.Transects;
            //        if (!string.IsNullOrEmpty(searchFreqCodeStr) && transectListCollection.Count > 0)
            //        {
            //            if (searchFreqCodeStr != "ALL FREQUENCIES")
            //            {
            //                var searchedlist = (from list in transectListCollection where list.Freq_Code.IndexOf(searchFreqCodeStr.Trim(), StringComparison.OrdinalIgnoreCase) >= 0 select list).ToList();
            //                MTmodel.Transects = searchedlist;
            //            }
            //            else
            //            {
            //                MTmodel.Transects = transectListCollection;
            //            }
            //        }
            //        else
            //        {
            //            MTmodel.Transects = transectListCollection;
            //        }
            //    }
            //}
            //System.Web.HttpContext.Current.Session["selectedfiltermodel"] = MTmodel;
            }
            return;
        }

//***************************************************************************************************
//DELETE TRANSECT

        public ActionResult DeleteTransect(int id)
        {
            deleteTransect(id);

            return RedirectToAction("Details");
        }

        private void deleteTransect(int TransectID)
        {
            //ManageTransectsViewModel model = new ManageTransectsViewModel();

            //model = (ManageTransectsViewModel)System.Web.HttpContext.Current.Session["selectedfiltermodel"];

            var x = (from y in db.Manage_Transects_Master where y.Cleaning_ID == TransectID select y).FirstOrDefault();
            if (x != null)
            {
                db.Manage_Transects_Master.Remove(x);
                try
                {
                    db.SaveChanges();
                }
                catch (DbEntityValidationException ex)
                {
                    foreach (DbEntityValidationResult item in ex.EntityValidationErrors)
                    {
                        // Get entry

                        DbEntityEntry entry = item.Entry;
                        string entityTypeName = entry.Entity.GetType().Name;

                        // Display or log error messages
                        foreach (DbValidationError subItem in item.ValidationErrors)
                        {
                            string message = string.Format("SMPL Function 'updateTransect': Error '{0}' occurred in {1} at {2}",
                                     subItem.ErrorMessage, entityTypeName, subItem.PropertyName);
                            logger.Warn(message);
                        }

                        // Rollback changes
                        switch (entry.State)
                        {
                            case EntityState.Added:
                                entry.State = EntityState.Detached;
                                break;
                            case EntityState.Modified:
                                entry.CurrentValues.SetValues(entry.OriginalValues);
                                entry.State = EntityState.Unchanged;
                                break;
                            case EntityState.Deleted:
                                entry.State = EntityState.Unchanged;
                                break;
                        }
                    }
                    //System.Web.HttpContext.Current.Session["selectedfiltermodel"] = model;
                    //model.editDeleteResult = 0;
                    return;
                }
                //model.editDeleteResult = 1;

                //We have just done a successful delete of a row in the WebGrid.
                //We need to regenerate the WebGrid rather than simply re-using the existing one.
                RegenerateWebGrid();
                //    List<ManageTransectsModel> transectListCollection = new List<ManageTransectsModel>();

                //    transectListCollection = getTransectDetailsListAll();

                //    model.FilterStreetStr = (string)(System.Web.HttpContext.Current.Session["selectedfilterstreet"] ?? "");
                //    model.FilterLimitsStr = (string)(System.Web.HttpContext.Current.Session["selectedfilterlimits"] ?? "");
                //    model.FilterWardCode = (string)(System.Web.HttpContext.Current.Session["selectedfilterward"] ?? "");
                //    model.FilterFreqCodeStr = (string)(System.Web.HttpContext.Current.Session["selectedfilterfreq"] ?? "");

                //    var searchStreetStr = model.FilterStreetStr ?? "";
                //    var searchLimitsStr = model.FilterLimitsStr ?? "";
                //    var searchWardStr = model.FilterWardCode ?? "";
                //    var searchFreqCodeStr = model.FilterFreqCodeStr ?? "";

                //    if (transectListCollection != null && transectListCollection.Count > 0)
                //    {
                //        if (!string.IsNullOrEmpty(searchStreetStr) && transectListCollection.Count > 0)
                //        {
                //            var searchedlist = (from list in transectListCollection where list.Street_Name.IndexOf(searchStreetStr, StringComparison.OrdinalIgnoreCase) >= 0 select list).ToList();
                //            model.Transects = searchedlist;
                //        }
                //        else
                //        {
                //            model.Transects = transectListCollection;
                //        }

                //        transectListCollection = model.Transects;
                //        if (!string.IsNullOrEmpty(searchLimitsStr) && transectListCollection.Count > 0)
                //        {
                //            var searchedlist = (from list in transectListCollection where list.Limits.IndexOf(searchLimitsStr, StringComparison.OrdinalIgnoreCase) >= 0 select list).ToList();
                //            model.Transects = searchedlist;
                //        }
                //        else
                //        {
                //            model.Transects = transectListCollection;
                //        }

                //        transectListCollection = model.Transects;
                //        if (!string.IsNullOrEmpty(searchWardStr) && transectListCollection.Count > 0)
                //        {
                //            if (searchWardStr != "ALL WARDS")
                //            {
                //                var searchedlist = (from list in transectListCollection where list.Ward_Name.IndexOf(searchWardStr, StringComparison.OrdinalIgnoreCase) >= 0 select list).ToList();
                //                model.Transects = searchedlist;
                //            }
                //            else
                //            {
                //                model.Transects = transectListCollection;
                //            }
                //        }
                //        else
                //        {
                //            model.Transects = transectListCollection;
                //        }

                //        transectListCollection = model.Transects;
                //        if (!string.IsNullOrEmpty(searchFreqCodeStr) && transectListCollection.Count > 0)
                //        {
                //            if (searchFreqCodeStr != "ALL FREQUENCIES")
                //            {
                //                var searchedlist = (from list in transectListCollection where list.Freq_Code.IndexOf(searchFreqCodeStr.Trim(), StringComparison.OrdinalIgnoreCase) >= 0 select list).ToList();
                //                model.Transects = searchedlist;
                //            }
                //            else
                //            {
                //                model.Transects = transectListCollection;
                //            }
                //        }
                //        else
                //        {
                //            model.Transects = transectListCollection;
                //        }
                //    }
                //}
                //else
                //{
                //    model.editDeleteResult = 0;
                //}
                //System.Web.HttpContext.Current.Session["selectedfiltermodel"] = model;
            }
            return;
        }

//***************************************************************************************************
//CREATE TRANSECT

        public ActionResult CreateTransect()
        {
            CreateTransectViewModel model = new CreateTransectViewModel();

            //Find the MAX Cleaning_ID and add one to it. This will be the new Cleaning_ID
            var newCleaningID = db.Manage_Transects_Master.Max(a => a.Cleaning_ID);

            model.Cleaning_ID = newCleaningID + 1;
            model.Wards = GetEditWardsList();
            model.Freq_Codes = GetEditFreqList();
            model.LandUses = GetEditLandUseList();
            model.Districts = GetEditDistrictList();
            model.Categories = GetEditCategoryList();
            model.Zone_Nrs = GetEditZoneNrList();
            model.Cleaning_Days = GetEditCleanDayList();
            model.Cleaning_Weeks = GetEditCleanWeekList();
            model.Deep_Days = GetEditDeepDayList();
            model.Deep_Weeks = GetEditDeepWeekList();

            return View("Create", model);
        }

        [HttpPost]
        public ActionResult CreateTransect(CreateTransectViewModel model, string save, string cancel)
        {
            if (!string.IsNullOrEmpty(save))
            {
                insertTransect(model);
            }
            if (!string.IsNullOrEmpty(cancel))
            {
                ViewBag.Message = "The Create was cancelled!";
            }

            return RedirectToAction("Details");
        }

        private void insertTransect(CreateTransectViewModel model)
        {
            //ManageTransectsViewModel MTmodel = new ManageTransectsViewModel();

            //MTmodel = (ManageTransectsViewModel)System.Web.HttpContext.Current.Session["selectedfiltermodel"];

            var newTransect = new Manage_Transects_Master();

            newTransect.Cleaning_ID = model.Cleaning_ID;
            newTransect.Street_Name = model.Street_Name ?? "";
            newTransect.Limits = model.Limits ?? "";
            newTransect.Ward_Name = model.Ward_Name ?? "";
            newTransect.District = model.District ?? "";
            newTransect.Freq_Code = model.Freq_Code ?? "";
            newTransect.Zone_Nr = model.Zone_Nr ?? "";
            newTransect.Category = model.Category ?? "";
            newTransect.Cleaning_Day = model.Cleaning_Day;
            newTransect.Cleaning_Week = model.Cleaning_Week;
            newTransect.Deep_Day = model.Deep_Day;
            newTransect.Deep_Week = model.Deep_Week;
            newTransect.Land_Use_Desc = model.Land_Use_Desc;
            newTransect.Length = model.Length;
            newTransect.USRN = model.USRN ?? "";

            db.Manage_Transects_Master.Add(newTransect);
            try
            {
                db.SaveChanges();
            }
            catch (DbEntityValidationException ex)
            {
                foreach (DbEntityValidationResult item in ex.EntityValidationErrors)
                {
                    // Get entry

                    DbEntityEntry entry = item.Entry;
                    string entityTypeName = entry.Entity.GetType().Name;

                    // Display or log error messages
                    foreach (DbValidationError subItem in item.ValidationErrors)
                    {
                        string message = string.Format("SMPL Function 'updateTransect': Error '{0}' occurred in {1} at {2}",
                                 subItem.ErrorMessage, entityTypeName, subItem.PropertyName);
                        logger.Warn(message);
                    }

                    // Rollback changes
                    switch (entry.State)
                    {
                        case EntityState.Added:
                            entry.State = EntityState.Detached;
                            break;
                        case EntityState.Modified:
                            entry.CurrentValues.SetValues(entry.OriginalValues);
                            entry.State = EntityState.Unchanged;
                            break;
                        case EntityState.Deleted:
                            entry.State = EntityState.Unchanged;
                            break;
                    }
                }
                return;
            }

            //We have just done a successful INSERT of a row in the WebGrid.
            //We need to regenerate the WebGrid rather than simply re-using the existing one.
            RegenerateWebGrid();
            //List<ManageTransectsModel> transectListCollection = new List<ManageTransectsModel>();

            //transectListCollection = getTransectDetailsListAll();

            //var searchStreetStr = (string)(System.Web.HttpContext.Current.Session["selectedfilterstreet"] ?? "");
            //var searchLimitsStr = (string)(System.Web.HttpContext.Current.Session["selectedfilterlimits"] ?? "");
            //var searchWardStr = (string)(System.Web.HttpContext.Current.Session["selectedfilterward"] ?? "");
            //var searchFreqCodeStr = (string)(System.Web.HttpContext.Current.Session["selectedfilterfreq"] ?? "");

            //if (transectListCollection != null && transectListCollection.Count > 0)
            //{
            //    if (!string.IsNullOrEmpty(searchStreetStr) && transectListCollection.Count > 0)
            //    {
            //        var searchedlist = (from list in transectListCollection where list.Street_Name.IndexOf(searchStreetStr, StringComparison.OrdinalIgnoreCase) >= 0 select list).ToList();
            //        MTmodel.Transects = searchedlist;
            //    }
            //    else
            //    {
            //        MTmodel.Transects = transectListCollection;
            //    }

            //    transectListCollection = MTmodel.Transects;
            //    if (!string.IsNullOrEmpty(searchLimitsStr) && transectListCollection.Count > 0)
            //    {
            //        var searchedlist = (from list in transectListCollection where list.Limits.IndexOf(searchLimitsStr, StringComparison.OrdinalIgnoreCase) >= 0 select list).ToList();
            //        MTmodel.Transects = searchedlist;
            //    }
            //    else
            //    {
            //        MTmodel.Transects = transectListCollection;
            //    }

            //    transectListCollection = MTmodel.Transects;
            //    if (!string.IsNullOrEmpty(searchWardStr) && transectListCollection.Count > 0)
            //    {
            //        if (searchWardStr != "ALL WARDS")
            //        {
            //            var searchedlist = (from list in transectListCollection where list.Ward_Name.IndexOf(searchWardStr, StringComparison.OrdinalIgnoreCase) >= 0 select list).ToList();
            //            MTmodel.Transects = searchedlist;
            //        }
            //        else
            //        {
            //            MTmodel.Transects = transectListCollection;
            //        }
            //    }
            //    else
            //    {
            //        MTmodel.Transects = transectListCollection;
            //    }

            //    transectListCollection = MTmodel.Transects;
            //    if (!string.IsNullOrEmpty(searchFreqCodeStr) && transectListCollection.Count > 0)
            //    {
            //        if (searchFreqCodeStr != "ALL FREQUENCIES")
            //        {
            //            var searchedlist = (from list in transectListCollection where list.Freq_Code.IndexOf(searchFreqCodeStr.Trim(), StringComparison.OrdinalIgnoreCase) >= 0 select list).ToList();
            //            MTmodel.Transects = searchedlist;
            //        }
            //        else
            //        {
            //            MTmodel.Transects = transectListCollection;
            //        }
            //    }
            //    else
            //    {
            //        MTmodel.Transects = transectListCollection;
            //    }
            //}

            //System.Web.HttpContext.Current.Session["selectedfiltermodel"] = MTmodel;
            return;
        }

//***************************************************************************************************
        private void RegenerateWebGrid()
        {
            ManageTransectsViewModel MTmodel = new ManageTransectsViewModel();

            MTmodel = (ManageTransectsViewModel)System.Web.HttpContext.Current.Session["selectedfiltermodel"];

            MTmodel.WebGridInit = true;
            MTmodel.TotalNumberProjects = db.Manage_Transects_Master.Count();

            List<ManageTransectsModel> transectListCollection = new List<ManageTransectsModel>();

            transectListCollection = getTransectDetailsListAll();

            MTmodel.FilterWards = GetFilterWards(transectListCollection);
            MTmodel.FilterFreqCodes = GetFilterFrequencies(transectListCollection);

            var searchStreetStr = (string)(System.Web.HttpContext.Current.Session["selectedfilterstreet"] ?? "");
            var searchLimitsStr = (string)(System.Web.HttpContext.Current.Session["selectedfilterlimits"] ?? "");
            var searchWardStr = (string)(System.Web.HttpContext.Current.Session["selectedfilterward"] ?? "");
            var searchFreqCodeStr = (string)(System.Web.HttpContext.Current.Session["selectedfilterfreq"] ?? "");

            MTmodel.FilterStreetStr = searchStreetStr;
            MTmodel.FilterLimitsStr = searchLimitsStr;
            MTmodel.FilterWardCode = searchWardStr;
            MTmodel.FilterFreqCodeStr = searchFreqCodeStr;

            if (transectListCollection != null && transectListCollection.Count > 0)
            {
                if (!string.IsNullOrEmpty(searchStreetStr) && transectListCollection.Count > 0)
                {
                    var searchedlist = (from list in transectListCollection where list.Street_Name.IndexOf(searchStreetStr, StringComparison.OrdinalIgnoreCase) >= 0 select list).ToList();
                    MTmodel.Transects = searchedlist;
                }
                else
                {
                    MTmodel.Transects = transectListCollection;
                }

                transectListCollection = MTmodel.Transects;
                if (!string.IsNullOrEmpty(searchLimitsStr) && transectListCollection.Count > 0)
                {
                    var searchedlist = (from list in transectListCollection where list.Limits.IndexOf(searchLimitsStr, StringComparison.OrdinalIgnoreCase) >= 0 select list).ToList();
                    MTmodel.Transects = searchedlist;
                }
                else
                {
                    MTmodel.Transects = transectListCollection;
                }

                transectListCollection = MTmodel.Transects;
                if (!string.IsNullOrEmpty(searchWardStr) && transectListCollection.Count > 0)
                {
                    if (searchWardStr != "ALL WARDS")
                    {
                        var searchedlist = (from list in transectListCollection where list.Ward_Name.IndexOf(searchWardStr, StringComparison.OrdinalIgnoreCase) >= 0 select list).ToList();
                        MTmodel.Transects = searchedlist;
                    }
                    else
                    {
                        MTmodel.Transects = transectListCollection;
                    }
                }
                else
                {
                    MTmodel.Transects = transectListCollection;
                }

                transectListCollection = MTmodel.Transects;
                if (!string.IsNullOrEmpty(searchFreqCodeStr) && transectListCollection.Count > 0)
                {
                    if (searchFreqCodeStr != "ALL FREQUENCIES")
                    {
                        var searchedlist = (from list in transectListCollection where list.Freq_Code.IndexOf(searchFreqCodeStr.Trim(), StringComparison.OrdinalIgnoreCase) >= 0 select list).ToList();
                        MTmodel.Transects = searchedlist;
                    }
                    else
                    {
                        MTmodel.Transects = transectListCollection;
                    }
                }
                else
                {
                    MTmodel.Transects = transectListCollection;
                }
            }

            System.Web.HttpContext.Current.Session["selectedfiltermodel"] = MTmodel;
        }

        private IEnumerable<SelectListItem> GetFilterWards(List<ManageTransectsModel> transectListCollection)
        {
            return new[] { new SelectListItem { Text = "ALL WARDS", Value = "ALL WARDS" } }.Concat(
                   from s in transectListCollection
                   orderby s.Ward_Name
                   group s.Ward_Name by s.Ward_Name into g
                   select new SelectListItem
                   {
                       Text = g.Key,
                       Value = g.Key
                   });
        }

        private IEnumerable<SelectListItem> GetFilterFrequencies(List<ManageTransectsModel>  transectListCollection)
        {
            return new[] { new SelectListItem { Text = "ALL FREQUENCIES", Value = "ALL FREQUENCIES" } }.Concat(
               from f in transectListCollection
               orderby f.Freq_Code
               group f.Freq_Code by f.Freq_Code into g
               select new SelectListItem
               {
                   Text = g.Key,
                   Value = g.Key
               });

            //return new[] { new SelectListItem { Text = "ALL FREQUENCIES", Value = "0" } }.Concat(
            //       from f in db.Zones_and_Classes_WM
            //       orderby f.Freq_Code 
            //       select new SelectListItem
            //       {
            //           Text = f.Freq_Code,
            //           Value = f.Display_Seq.ToString()
            //       });
        }

        private IEnumerable<SelectListItem> GetEditWardsList()
        {
            return (from s in db.Ref_Wards
                   orderby s.Ward_Description
                    group s.Ward_Description by s.Ward_Description into g
                   select new SelectListItem
                   {
                       Text = g.Key,
                       Value = g.Key
                   });
        }

        private IEnumerable<SelectListItem> GetEditLandUseList()
        {
            return (from s in db.Ref_LandUseClasses
                    orderby s.Land_Use_Desc
                    group s.Land_Use_Desc by s.Land_Use_Desc into g
                    select new SelectListItem
                    {
                        Text = g.Key,
                        Value = g.Key
                    });
        }

        private IEnumerable<SelectListItem> GetEditFreqList()
        {
            return (from f in db.Zones_and_Classes_WM
               orderby f.Freq_Code
               group f.Freq_Code by f.Freq_Code.Trim() into g
               select new SelectListItem
               {
                   Text = g.Key,
                   Value = g.Key
               });
        }

        private IEnumerable<SelectListItem> GetEditDistrictList()
        {
            return (from f in db.Cleaning_Schedule
                    orderby f.District
                    group f.District by f.District into g
                    select new SelectListItem
                    {
                        Text = g.Key,
                        Value = g.Key
                    });
        }

        private IEnumerable<SelectListItem> GetEditCategoryList()
        {
            var result = new[] {
                new SelectListItem { Text = "Bring", Value = "Bring" },
                new SelectListItem { Text = "Car Parks", Value = "Car Parks" },
                new SelectListItem { Text = "Housing", Value = "Housing" },
                new SelectListItem { Text = "Street Bins", Value = "Street Bins" },
                new SelectListItem { Text = "TFL", Value = "TFL" },
                new SelectListItem { Text = "Zone 1", Value = "Zone 1" },
                new SelectListItem { Text = "Zone 2", Value = "Zone 2" },
                new SelectListItem { Text = "Zone 3", Value = "Zone 3" },
                new SelectListItem { Text = "Zone 4", Value = "Zone 4" },
                new SelectListItem { Text = "Zone 5", Value = "Zone 5" },
                new SelectListItem { Text = "Zone 6", Value = "Zone 6" },
                new SelectListItem { Text = "Zone 7", Value = "Zone 7" }
            };
            return result;
        }

        private IEnumerable<SelectListItem> GetEditZoneNrList()
        {
            return (from f in db.Zones_and_Classes_WM
                    orderby f.Zone_Nr
                    group f.Zone_Nr by f.Zone_Nr.Trim() into g
                    select new SelectListItem
                    {
                        Text = g.Key,
                        Value = g.Key
                    });
        }

        private IEnumerable<SelectListItem> GetEditCleanDayList()
        {
            var result = new[] {
                new SelectListItem { Text = "0", Value = "0" },
                new SelectListItem { Text = "1", Value = "1" },
                new SelectListItem { Text = "2", Value = "2" },
                new SelectListItem { Text = "3", Value = "3" },
                new SelectListItem { Text = "4", Value = "4" },
                new SelectListItem { Text = "5", Value = "5" },
                new SelectListItem { Text = "6", Value = "6" },
                new SelectListItem { Text = "7", Value = "7" }
            };
            return result;
        }

        private IEnumerable<SelectListItem> GetEditCleanWeekList()
        {
            var result = new[] {
                new SelectListItem { Text = "0", Value = "0" },
                new SelectListItem { Text = "1", Value = "1" },
                new SelectListItem { Text = "2", Value = "2" },
                new SelectListItem { Text = "3", Value = "3" }
            };
            return result;
        }

        private IEnumerable<SelectListItem> GetEditDeepWeekList()
        {
            var result = new[] {
                new SelectListItem { Text = "0", Value = "0" },
                new SelectListItem { Text = "1", Value = "1" },
                new SelectListItem { Text = "2", Value = "2" },
                new SelectListItem { Text = "3", Value = "3" },
                new SelectListItem { Text = "4", Value = "4" },
                new SelectListItem { Text = "5", Value = "5" },
                new SelectListItem { Text = "6", Value = "6" },
                new SelectListItem { Text = "7", Value = "7" },
                new SelectListItem { Text = "8", Value = "8" },
                new SelectListItem { Text = "9", Value = "9" },
                new SelectListItem { Text = "10", Value = "10" },
                new SelectListItem { Text = "11", Value = "11" },
                new SelectListItem { Text = "12", Value = "12" }
            };
            return result;
        }

        private IEnumerable<SelectListItem> GetEditDeepDayList()
        {
            var result = new[] {
                new SelectListItem { Text = "0", Value = "0" },
                new SelectListItem { Text = "1", Value = "1" },
                new SelectListItem { Text = "2", Value = "2" },
                new SelectListItem { Text = "3", Value = "3" },
                new SelectListItem { Text = "4", Value = "4" },
                new SelectListItem { Text = "5", Value = "5" },
                new SelectListItem { Text = "6", Value = "6" },
                new SelectListItem { Text = "7", Value = "7" },
                new SelectListItem { Text = "8", Value = "8" },
                new SelectListItem { Text = "9", Value = "9" },
                new SelectListItem { Text = "10", Value = "10" }
            };
            return result;
        }

        private List<ManageTransectsModel> getTransectDetailsList(int pageNum)
        {
            int from = (pageNum * RecordsPerPage);  

            List<Manage_Transects_Master> query = (from tr in db.Manage_Transects_Master
                                                   select new
                                                   {
                                                       tr.Cleaning_ID,
                                                       tr.Street_Name,
                                                       tr.Limits,
                                                       tr.Ward_Name,
                                                       tr.District,
                                                       tr.Freq_Code,
                                                       tr.Cleaning_Day,
                                                       tr.Cleaning_Week,
                                                       tr.Deep_Week,
                                                       tr.Deep_Day,
                                                       tr.Category,
                                                       tr.Land_Use_Desc,
                                                       tr.Zone_Nr,
                                                       tr.Length,
                                                       tr.USRN
                                                   }).AsEnumerable()
                         .Select(x => new Manage_Transects_Master
                         {
                             Cleaning_ID = x.Cleaning_ID,
                             Street_Name = x.Street_Name,
                             Limits = x.Limits,
                             Ward_Name = x.Ward_Name,
                             District = x.District,
                             Freq_Code = x.Freq_Code,
                             Cleaning_Day = x.Cleaning_Day,
                             Cleaning_Week = x.Cleaning_Week,
                             Deep_Week = x.Deep_Week,
                             Deep_Day = x.Deep_Day,
                             Category = x.Category,
                             Land_Use_Desc = x.Land_Use_Desc,
                             Zone_Nr = x.Zone_Nr,
                             Length = x.Length,
                             USRN = x.USRN
                         }).Skip(from).Take(RecordsPerPage).ToList();

            List<ManageTransectsModel> transects = new List<ManageTransectsModel>();

            foreach (var item in query)
            {
                var transect = new ManageTransectsModel();

                transect.Cleaning_ID = item.Cleaning_ID;
                transect.Street_Name = item.Street_Name;
                transect.Limits = item.Limits;
                transect.Ward_Name = item.Ward_Name;
                transect.District = item.District;
                transect.Freq_Code = item.Freq_Code;
                transect.Cleaning_Day = item.Cleaning_Day;
                transect.Cleaning_Week = item.Cleaning_Week;
                transect.Deep_Week = item.Deep_Week;
                transect.Deep_Day = item.Deep_Day;
                transect.Category = item.Category;
                transect.Land_Use_Desc = item.Land_Use_Desc;
                transect.Zone_Nr = item.Zone_Nr;
                transect.Length = item.Length;
                transect.USRN = item.USRN;
                transect.date_last_modified = item.date_last_modified;
                transect.user_last_modified = item.user_last_modified;

                transects.Add(transect);
            }

            return transects;
        }

        private List<ManageTransectsHistoryModel> getTransectHistoryListAll()
        {
            List<Manage_Transects_Audit> query = (from tr in db.Manage_Transects_Audit
                                                  orderby tr.date_action descending
                                                   select new
                                                   {
                                                       tr.Manage_Transects_Audit_ID,
                                                       tr.user_id,
                                                       tr.action,
                                                       tr.date_action,
                                                       tr.Orig_Cleaning_ID,
                                                       tr.Orig_Street_Name,
                                                       tr.Orig_Limits,
                                                       tr.Orig_Ward_Name,
                                                       tr.Orig_District,
                                                       tr.Orig_Freq_Code,
                                                       tr.Orig_Cleaning_Day,
                                                       tr.Orig_Cleaning_Week,
                                                       tr.Orig_Deep_Week,
                                                       tr.Orig_Deep_Day,
                                                       tr.Orig_Category,
                                                       tr.Orig_Land_Use_Desc,
                                                       tr.Orig_Zone_Nr,
                                                       tr.Orig_Length,
                                                       tr.Orig_USRN,
                                                       tr.New_Cleaning_ID,
                                                       tr.New_Street_Name,
                                                       tr.New_Limits,
                                                       tr.New_Ward_Name,
                                                       tr.New_District,
                                                       tr.New_Freq_Code,
                                                       tr.New_Cleaning_Day,
                                                       tr.New_Cleaning_Week,
                                                       tr.New_Deep_Week,
                                                       tr.New_Deep_Day,
                                                       tr.New_Category,
                                                       tr.New_Land_Use_Desc,
                                                       tr.New_Zone_Nr,
                                                       tr.New_Length,
                                                       tr.New_USRN
                                                   }).AsEnumerable()
                         .Select(x => new Manage_Transects_Audit
                         {
                             Manage_Transects_Audit_ID = x.Manage_Transects_Audit_ID,
                             user_id = x.user_id,
                             action = x.action,
                             date_action = x.date_action,
                             Orig_Cleaning_ID = x.Orig_Cleaning_ID,
                             Orig_Street_Name = x.Orig_Street_Name,
                             Orig_Limits = x.Orig_Limits,
                             Orig_Ward_Name = x.Orig_Ward_Name,
                             Orig_District = x.Orig_District,
                             Orig_Freq_Code = x.Orig_Freq_Code,
                             Orig_Cleaning_Day = x.Orig_Cleaning_Day,
                             Orig_Cleaning_Week = x.Orig_Cleaning_Week,
                             Orig_Deep_Week = x.Orig_Deep_Week,
                             Orig_Deep_Day = x.Orig_Deep_Day,
                             Orig_Category = x.Orig_Category,
                             Orig_Land_Use_Desc = x.Orig_Land_Use_Desc,
                             Orig_Zone_Nr = x.Orig_Zone_Nr,
                             Orig_Length = x.Orig_Length,
                             Orig_USRN = x.Orig_USRN,
                             New_Cleaning_ID = x.New_Cleaning_ID,
                             New_Street_Name = x.New_Street_Name,
                             New_Limits = x.New_Limits,
                             New_Ward_Name = x.New_Ward_Name,
                             New_District = x.New_District,
                             New_Freq_Code = x.New_Freq_Code,
                             New_Cleaning_Day = x.New_Cleaning_Day,
                             New_Cleaning_Week = x.New_Cleaning_Week,
                             New_Deep_Week = x.New_Deep_Week,
                             New_Deep_Day = x.New_Deep_Day,
                             New_Category = x.New_Category,
                             New_Land_Use_Desc = x.New_Land_Use_Desc,
                             New_Zone_Nr = x.New_Zone_Nr,
                             New_Length = x.New_Length,
                             New_USRN = x.New_USRN
                         }).ToList();

            List<ManageTransectsHistoryModel> transects = new List<ManageTransectsHistoryModel>();

            foreach (var item in query)
            {
                var transect = new ManageTransectsHistoryModel();

                transect.Manage_Transects_Audit_ID = item.Manage_Transects_Audit_ID;
                transect.User_ID = item.user_id;
                transect.action = item.action;
                transect.date_action = item.date_action;
                transect.Orig_Cleaning_ID = item.Orig_Cleaning_ID ?? 0;
                transect.Orig_Street_Name = item.Orig_Street_Name;
                transect.Orig_Limits = item.Orig_Limits;
                transect.Orig_Ward_Name = item.Orig_Ward_Name;
                transect.Orig_District = item.Orig_District;
                transect.Orig_Freq_Code = item.Orig_Freq_Code;
                transect.Orig_Cleaning_Day = item.Orig_Cleaning_Day;
                transect.Orig_Cleaning_Week = item.Orig_Cleaning_Week;
                transect.Orig_Deep_Week = item.Orig_Deep_Week;
                transect.Orig_Deep_Day = item.Orig_Deep_Day;
                transect.Orig_Category = item.Orig_Category;
                transect.Orig_Land_Use_Desc = item.Orig_Land_Use_Desc;
                transect.Orig_Zone_Nr = item.Orig_Zone_Nr;
                transect.Orig_Length = item.Orig_Length;
                transect.Orig_USRN = item.Orig_USRN;
                transect.New_Cleaning_ID = item.New_Cleaning_ID ?? 0;
                transect.New_Street_Name = item.New_Street_Name;
                transect.New_Limits = item.New_Limits;
                transect.New_Ward_Name = item.New_Ward_Name;
                transect.New_District = item.New_District;
                transect.New_Freq_Code = item.New_Freq_Code;
                transect.New_Cleaning_Day = item.New_Cleaning_Day;
                transect.New_Cleaning_Week = item.New_Cleaning_Week;
                transect.New_Deep_Week = item.New_Deep_Week;
                transect.New_Deep_Day = item.New_Deep_Day;
                transect.New_Category = item.New_Category;
                transect.New_Land_Use_Desc = item.New_Land_Use_Desc;
                transect.New_Zone_Nr = item.New_Zone_Nr;
                transect.New_Length = item.New_Length;
                transect.New_USRN = item.New_USRN;

                transects.Add(transect);
            }

            return transects;
        }

        private List<ManageTransectsModel> getTransectDetailsListAll()
        {
            List<Manage_Transects_Master> query = (from tr in db.Manage_Transects_Master
                                                   select new
                                                   {
                                                       tr.Cleaning_ID,
                                                       tr.Street_Name,
                                                       tr.Limits,
                                                       tr.Ward_Name,
                                                       tr.District,
                                                       tr.Freq_Code,
                                                       tr.Cleaning_Day,
                                                       tr.Cleaning_Week,
                                                       tr.Deep_Week,
                                                       tr.Deep_Day,
                                                       tr.Category,
                                                       tr.Land_Use_Desc,
                                                       tr.Zone_Nr,
                                                       tr.Length,
                                                       tr.USRN
                                                   }).AsEnumerable()
                         .Select(x => new Manage_Transects_Master
                         {
                             Cleaning_ID = x.Cleaning_ID,
                             Street_Name = x.Street_Name,
                             Limits = x.Limits,
                             Ward_Name = x.Ward_Name,
                             District = x.District,
                             Freq_Code = x.Freq_Code,
                             Cleaning_Day = x.Cleaning_Day,
                             Cleaning_Week = x.Cleaning_Week,
                             Deep_Week = x.Deep_Week,
                             Deep_Day = x.Deep_Day,
                             Category = x.Category,
                             Land_Use_Desc = x.Land_Use_Desc,
                             Zone_Nr = x.Zone_Nr,
                             Length = x.Length,
                             USRN = x.USRN
                         }).ToList();

            List<ManageTransectsModel> transects = new List<ManageTransectsModel>();

            foreach (var item in query)
            {
                var transect = new ManageTransectsModel();

                transect.Cleaning_ID = item.Cleaning_ID;
                transect.Street_Name = item.Street_Name;
                transect.Limits = item.Limits;
                transect.Ward_Name = item.Ward_Name;
                transect.District = item.District;
                transect.Freq_Code = item.Freq_Code;
                transect.Cleaning_Day = item.Cleaning_Day;
                transect.Cleaning_Week = item.Cleaning_Week;
                transect.Deep_Week = item.Deep_Week;
                transect.Deep_Day = item.Deep_Day;
                transect.Category = item.Category;
                transect.Land_Use_Desc = item.Land_Use_Desc;
                transect.Zone_Nr = item.Zone_Nr;
                transect.Length = item.Length;
                transect.USRN = item.USRN;
                transect.date_last_modified = item.date_last_modified;
                transect.user_last_modified = item.user_last_modified;

                transects.Add(transect);
            }

            return transects;
        }
    }
}