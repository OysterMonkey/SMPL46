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
    
    public partial class database_source
    {
        public Nullable<int> database_id { get; set; }
        public Nullable<int> database_connection_id { get; set; }
        public string database_description { get; set; }
        public string uid_required { get; set; }
        public string pwd_required { get; set; }
        public System.DateTime last_modified { get; set; }
        public System.Guid pkUID { get; set; }
    }
}
