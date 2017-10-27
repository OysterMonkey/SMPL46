using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SMPL46.ViewModels
{
    public class AdHocEditorViewModel
    {
        public bool Selected { get; set; }
        public bool Enabled { get; set; }
        public string HiddenEnabled { get; set; }
        public string BackgroundColour { get; set; }

        public int seq_id { get; set; }
        public Guid pkuid { get; set; }
        public DateTime inspected_date { get; set; }
        public string ward_name { get; set; }
        public string colour { get; set; }
        public string freq_code { get; set; }
        public string street_name { get; set; }
        public string limits { get; set; }
        public string grade { get; set; }
        public string reason_code { get; set; }
        public string inspection_comments { get; set; }
        public string inspected_by { get; set; }
        public string isfromcontenderreportedtransect { get; set; }
        public int contenderreference { get; set; }
        public string cleansesubmitted { get; set; }
        public DateTime? cleanse_due_on { get; set; }
        public DateTime? next_sched_cleanse { get; set; }
    }
}