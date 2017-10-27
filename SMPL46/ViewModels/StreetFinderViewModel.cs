using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using SMPL46.Models;

namespace SMPL46.ViewModels
{
    public class StreetFinderViewModel
    {
        [Editable(false)]
        [Display(Name = "Enter known street details:")]
        public string StreetPrompt { get; set; }

        [Required]
        [Editable(false)]
        [Display(Name = "Ward")]
        public string WardCode { get; set; }
        public IEnumerable<System.Web.Mvc.SelectListItem> Wards { get; set; }

        [Required(ErrorMessage = "Please enter full or partial Street Name")]
        [Display(Name = "Street Name")]
        public string StreetName { get; set; }

        public List<StreetFinderEditorViewModel> StreetsFound { get; set; }

        //public DateTime StartDate { get; set; }
        //public DateTime EndDate { get; set; }
    }
}