//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace SMPL46.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class ml_passthrough_status
    {
        public int status_id { get; set; }
        public string remote_id { get; set; }
        public int run_order { get; set; }
        public int script_id { get; set; }
        public string script_status { get; set; }
        public Nullable<int> error_code { get; set; }
        public string error_text { get; set; }
        public System.DateTime remote_run_time { get; set; }
    }
}
