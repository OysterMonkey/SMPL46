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
    
    public partial class ml_column
    {
        public int version_id { get; set; }
        public int table_id { get; set; }
        public int idx { get; set; }
        public string name { get; set; }
        public string type { get; set; }
    
        public virtual ml_table ml_table { get; set; }
        public virtual ml_script_version ml_script_version { get; set; }
    }
}