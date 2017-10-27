using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SMPL46.Models
{
    public class ReportsSelectionViewModels
    {
        [Key]
        public string SmplReportSelection_code { get; set; }
        public string role_description { get; set; }
        public string SmplReportSelection_name { get; set; }
    }
}