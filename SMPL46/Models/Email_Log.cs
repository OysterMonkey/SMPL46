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
    
    public partial class Email_Log
    {
        public int Email_Log_ID { get; set; }
        public System.DateTime Email_Sent_time { get; set; }
        public int Email_Contact_ID { get; set; }
        public string Email_Body_text { get; set; }
        public int Email_Role_ID { get; set; }
    
        public virtual Email_Contact Email_Contact { get; set; }
        public virtual Email_Role Email_Role { get; set; }
    }
}
