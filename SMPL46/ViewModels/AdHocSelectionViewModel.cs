using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SMPL46.ViewModels
{
    public class AdHocSelectionViewModel
    {
        public AdHocSelectionViewModel()
        {
            AdHocRectifications = new List<AdHocEditorViewModel>();
        }

        [Required]
        [Display(Name = "Start Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime StartDate { get; set; }

        [Required]
        [Display(Name = "End Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Compare("StartDate", ErrorMessage = "The Start Date must be older than the End Date.")]
        public DateTime EndDate { get; set; }

        public List<AdHocEditorViewModel> AdHocRectifications { get; set; }

        public List<Guid> getSelectedIds()
        {
            // Return a List containing the Id's of the selected people:
            //return (from p in this.AdHocRectifications where p.Selected && p.Enabled select p.pkuid).ToList();
            return (from p in this.AdHocRectifications where p.Selected && (p.HiddenEnabled != "true") select p.pkuid).ToList();
        }
    }
}