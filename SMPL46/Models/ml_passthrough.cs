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
    
    public partial class ml_passthrough
    {
        public string remote_id { get; set; }
        public int run_order { get; set; }
        public int script_id { get; set; }
        public System.DateTime last_modified { get; set; }
    
        public virtual ml_passthrough_script ml_passthrough_script { get; set; }
    }
}
