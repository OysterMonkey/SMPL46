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
    
    public partial class ml_table_script
    {
        public int version_id { get; set; }
        public int table_id { get; set; }
        public string @event { get; set; }
        public int script_id { get; set; }
    
        public virtual ml_script ml_script { get; set; }
        public virtual ml_script_version ml_script_version { get; set; }
        public virtual ml_table ml_table { get; set; }
    }
}