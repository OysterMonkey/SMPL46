using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using SMPL46.Models;
using SMPL46.ViewModels;

namespace SMPL46.Controllers
{
    public class ContractorMonitoringController : Controller
    {
        private EalingPMS_DataMart_TestEntities1 db = new EalingPMS_DataMart_TestEntities1();

        private AccountDataService ds = new AccountDataService();

        // GET: ContractorMonitoring
        public ActionResult Index()
        {
            var model = new ConMonSelectionViewModel();

            model.ConMonRectifications = null;
            model.StartDate = DateTime.Today;
            model.EndDate = DateTime.Today;

            return View(model);
        }

        [HttpPost]
        [HttpParamAction]
        public ActionResult submitConMonRectifications(ConMonSelectionViewModel model)
        {
            var selectedIds = model.getSelectedIds();

            if (selectedIds.Count > 0)
            {
                StringBuilder strXML = new StringBuilder();
                strXML.Append("<CS_COLLECTION>");
                foreach (var pkuid in selectedIds)
                {
                    strXML.Append("<CSRECORD><pkuid>" + pkuid + "</pkuid><user_id>" + System.Web.HttpContext.Current.Session["username"].ToString() + "</user_id></CSRECORD>");
                }
                strXML.Append("</CS_COLLECTION>");

                string rtnString = ds.InsertConMonScheduledCleanse(strXML.ToString());

                if (!string.IsNullOrWhiteSpace(rtnString)) { ModelState.AddModelError("", rtnString); }
            }

            var model2 = new ConMonSelectionViewModel();

            model2 = GetFailedConMonTransectsFromDb(model);

            return View("Index", model2);
        }

        [HttpPost]
        [HttpParamAction]
        public ActionResult getFailedConMonTransects(ConMonSelectionViewModel model)
        {
            model = GetFailedConMonTransectsFromDb(model);

            return View("Index", model);
        }

        private ConMonSelectionViewModel GetFailedConMonTransectsFromDb(ConMonSelectionViewModel model)
        {
            var StartDateParam = new SqlParameter
            {
                ParameterName = "StartDate",
                Value = model.StartDate
            };

            var EndDateParam = new SqlParameter
            {
                ParameterName = "EndDate",
                Value = model.EndDate
            };

            var model2 = new ConMonSelectionViewModel();

            model2.StartDate = model.StartDate;
            model2.ConMonRectifications = new List<ConMonEditorViewModel>();
            model2.EndDate = model.EndDate;

            var smplRectificationsByDate =
                db.Database.SqlQuery<ContractorMonitoring>("exec GetFailedConMonTransects @StartDate, @EndDate ", StartDateParam, EndDateParam).ToList();

            foreach (var rect in smplRectificationsByDate)
            {
                var RectVM = new ConMonEditorViewModel();

                RectVM.seq_id = rect.seq_id;
                RectVM.pkuid = rect.pkuid;
                RectVM.inspected_date = rect.inspected_date;
                RectVM.ward_name = rect.ward_name;
                RectVM.colour = rect.colour;
                RectVM.freq_code = rect.freq_code;
                RectVM.street_name = rect.street_name;
                RectVM.limits = rect.limits;
                RectVM.grade = rect.grade;
                RectVM.reason_code = rect.reason_code;
                RectVM.inspection_comments = rect.inspection_comments;
                RectVM.inspected_by = rect.inspected_by;
                RectVM.cleansesubmitted = rect.cleansesubmitted;
                RectVM.cleanse_due_on = rect.cleansedueon;

                //set defaults
                RectVM.BackgroundColour = "colorFreqCodeWhite";
                RectVM.Enabled = true;
                RectVM.HiddenEnabled = "true";
                RectVM.Selected = false;

                //if Freq_Code field is: "1xDaily Path"; "1xDaily Street"; "1xDaily Recycling"; "Daily Litter Bin"; then BackGround colour is yellow
                //if Freq_Code field is: "Zone 1"; then BackGround colour is Sky Blue AND Checkbox is Disabled
                //if Rectification field is NOT empty (test for space) then Checkbox is Disabled and Checked
                switch (rect.freq_code)
                {
                    case "1xDaily Path":
                    case "1xDaily Street":
                    case "Daily Litter Bin":
                        RectVM.BackgroundColour = "colorFreqCodeYellow";
                        break;
                    case "Zone 1":
                        RectVM.BackgroundColour = "colorFreqCodeSkyBlue";
                        RectVM.Enabled = false;
                        break;
                    default:
                        break;
                }
                if (!string.IsNullOrWhiteSpace(rect.cleansesubmitted))
                {
                    RectVM.Enabled = false;
                    RectVM.Selected = true;
                }
                model2.ConMonRectifications.Add(RectVM);
            }
            return model2;
        }
    }
}