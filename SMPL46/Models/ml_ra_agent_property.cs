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
    
    public partial class ml_ra_agent_property
    {
        public int aid { get; set; }
        public string property_name { get; set; }
        public string property_value { get; set; }
        public System.DateTime last_modified { get; set; }
    
        public virtual ml_ra_agent ml_ra_agent { get; set; }
    }
}
