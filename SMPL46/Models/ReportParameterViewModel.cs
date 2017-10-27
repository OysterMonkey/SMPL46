using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using SMPL46.ViewModels;

namespace SMPL46.Models
{
    public class AH1ParameterViewModel
    {
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

        [Required]
        [Editable(false)]
        [Display(Name = "Ward")]
        public string WardCode { get; set; }
        public IEnumerable<System.Web.Mvc.SelectListItem> Wards { get; set; }
    }

    public class SC1ParameterViewModel
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

        public List<SC1EditorViewModel> StreetsFound { get; set; }
    }

    public class SC2ParameterViewModel
    {
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
    }

    public class SC4ParameterViewModel
    {
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

        [Required]
        [Editable(false)]
        [Display(Name = "Ward")]
        public string WardCode { get; set; }
        public IEnumerable<System.Web.Mvc.SelectListItem> Wards { get; set; }
    }

    public class SC5ParameterViewModel
    {
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

        [Required]
        [Editable(false)]
        [Display(Name = "Ward")]
        public string WardCode { get; set; }
        public IEnumerable<System.Web.Mvc.SelectListItem> Wards { get; set; }
    }

    public class SC5AParameterViewModel
    {
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

        [Required]
        [Editable(false)]
        [Display(Name = "Ward")]
        public string WardCode { get; set; }
        public IEnumerable<System.Web.Mvc.SelectListItem> Wards { get; set; }
    }

    public class SC6ParameterViewModel
    {
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

        [Required]
        [Editable(false)]
        [Display(Name = "Ward")]
        public string WardCode { get; set; }
        public IEnumerable<System.Web.Mvc.SelectListItem> Wards { get; set; }
    }

    public class SC7ParameterViewModel
    {
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

        [Required]
        [Editable(false)]
        [Display(Name = "Land Use")]
        public string LandUseClass { get; set; }
        public IEnumerable<System.Web.Mvc.SelectListItem> LandUseClasses { get; set; }
    }

    public class SC8ParameterViewModel
    {
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

        [Required]
        [Editable(false)]
        [Display(Name = "Inspector")]
        public string Inspector { get; set; }
        public IEnumerable<System.Web.Mvc.SelectListItem> Inspectors { get; set; }
    }

    public class SC9ParameterViewModel
    {
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

    }

    public class SC10ParameterViewModel
    {
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

        [Required]
        [Editable(false)]
        [Display(Name = "Ward")]
        public string WardCode { get; set; }
        public IEnumerable<System.Web.Mvc.SelectListItem> Wards { get; set; }

        [Required]
        [Editable(false)]
        [Display(Name = "Frequency")]
        public string Frequency { get; set; }
        public IEnumerable<System.Web.Mvc.SelectListItem> Frequencies { get; set; }
    }

    public class SC11ParameterViewModel
    {
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

        [Required]
        [Editable(false)]
        [Display(Name = "Ward")]
        public string WardCode { get; set; }
        public IEnumerable<System.Web.Mvc.SelectListItem> Wards { get; set; }
    }

    public class SC12ParameterViewModel
    {
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
    }

    public class SCNoticesParameterViewModel
    {
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
    }

    public class SCNoticesAParameterViewModel
    {
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
    }

    public class EH1ParameterViewModel
    {
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

        [Required]
        [Editable(false)]
        [Display(Name = "Area")]
        public string AreaCode { get; set; }
        public IEnumerable<System.Web.Mvc.SelectListItem> Areas { get; set; }
    }

    public class CM1ParameterViewModel
    {
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

        [Required]
        [Editable(false)]
        [Display(Name = "Ward")]
        public string WardCode { get; set; }
        public IEnumerable<System.Web.Mvc.SelectListItem> Wards { get; set; }
    }

    public class CM2ParameterViewModel
    {
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
    }

    public class CM3ParameterViewModel
    {
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
    }
}