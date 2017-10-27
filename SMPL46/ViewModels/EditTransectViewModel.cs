using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SMPL46.ViewModels
{
    public class EditTransectViewModel
    {
        public int Cleaning_ID { get; set; }

        [Required]
        [Editable(true)]
        [Display(Name = "Street")]
        public string Street_Name { get; set; }

        [Required]
        [Editable(true)]
        [Display(Name = "Limits")]
        public string Limits { get; set; }

        [Required]
        [Editable(true)]
        [Display(Name = "Ward")]
        public string Ward_Name { get; set; }
        public IEnumerable<System.Web.Mvc.SelectListItem> Wards { get; set; }

        [Required]
        [Editable(true)]
        [Display(Name = "District")]
        public string District { get; set; }
        public IEnumerable<System.Web.Mvc.SelectListItem> Districts { get; set; }

        [Required]
        [Editable(true)]
        [Display(Name = "Freq. Code")]
        public string Freq_Code { get; set; }
        public IEnumerable<System.Web.Mvc.SelectListItem> Freq_Codes { get; set; }

        [Required]
        [Display(Name = "Clean Day of Week")]
        public int Cleaning_Day { get; set; }
        public IEnumerable<System.Web.Mvc.SelectListItem> Cleaning_Days { get; set; }

        [Required]
        [Display(Name = "Clean Week")]
        public int Cleaning_Week { get; set; }
        public IEnumerable<System.Web.Mvc.SelectListItem> Cleaning_Weeks { get; set; }

        [Required]
        [Display(Name = "Deep Week")]
        public int Deep_Week { get; set; }
        public IEnumerable<System.Web.Mvc.SelectListItem> Deep_Weeks { get; set; }

        [Required]
        [Display(Name = "Deep Day of Week")]
        public int Deep_Day { get; set; }
        public IEnumerable<System.Web.Mvc.SelectListItem> Deep_Days { get; set; }

        [Required]
        [Editable(true)]
        [Display(Name = "Category")]
        public string Category { get; set; }
        public IEnumerable<System.Web.Mvc.SelectListItem> Categories { get; set; }

        [Required]
        [Editable(true)]
        [Display(Name = "Land Use")]
        public string Land_Use_Desc { get; set; }
        public IEnumerable<System.Web.Mvc.SelectListItem> LandUses { get; set; }

        [Required]
        [Editable(true)]
        [Display(Name = "Zone Nr.")]
        public string Zone_Nr { get; set; }
        public IEnumerable<System.Web.Mvc.SelectListItem> Zone_Nrs { get; set; }

        public Nullable<double> Length { get; set; }
        public string USRN { get; set; }
        public Nullable<System.DateTime> date_last_modified { get; set; }
        public string user_last_modified { get; set; }
    }
}