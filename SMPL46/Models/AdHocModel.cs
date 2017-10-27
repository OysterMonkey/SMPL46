using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SMPL46.Models
{
    public class AdHocModel
    {
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
        public int? contender_reference { get; set; }
        public string cleansesubmitted { get; set; }
        public DateTime? cleansedueon { get; set; }
        public DateTime? nextschedcleanse { get; set; }
    }
}