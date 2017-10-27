using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using SMPL46.Models;


namespace SMPL46.ViewModels
{
    public class ManageTransectsHistoryViewModel
    {
        public ManageTransectsHistoryViewModel()
        {
            TransectsHistory = new List<ManageTransectsHistoryModel>();
        }

        public int TotalNumberProjects { get; set; }

        public bool IsEndOfRecords { get; set; }

        private bool webGridInit = false;
        public bool WebGridInit 
        {
            get { return webGridInit; }
            set { webGridInit = value; }
        }

        public int editDeleteResult { get; set; }
        public int editUpdateResult { get; set; }
        public int editInsertResult { get; set; }

        [Display(Name = "Street Name")]
        public string FilterStreetStr { get; set; }

        [Display(Name = "Street Limits")]
        public string FilterLimitsStr { get; set; }

        [Editable(false)]
        [Display(Name = "Ward")]
        public string FilterWardCode { get; set; }
        public IEnumerable<System.Web.Mvc.SelectListItem> FilterWards { get; set; }

        [Editable(false)]
        [Display(Name = "Freq. Code")]
        public string FilterFreqCodeStr { get; set; }
        public IEnumerable<System.Web.Mvc.SelectListItem> FilterFreqCodes { get; set; }
        
        public List<ManageTransectsHistoryModel> TransectsHistory { get; set; }

    }
}