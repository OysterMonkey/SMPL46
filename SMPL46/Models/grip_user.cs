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
    
    public partial class grip_user
    {
        public int user_id { get; set; }
        public string UID { get; set; }
        public string PWD { get; set; }
        public string user_name { get; set; }
        public System.DateTime last_modified { get; set; }
        public System.Guid pkUID { get; set; }
        public string reset_pwd { get; set; }
        public string accesstype_code { get; set; }
        public System.DateTime last_pwd_reset { get; set; }
        public bool module_sc { get; set; }
        public bool module_cm { get; set; }
        public bool module_ah { get; set; }
    
        public virtual access_type access_type { get; set; }
    }
}