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
    
    public partial class VW_Rpt_1_Rectifications_Site_Cleaning_ADS
    {
        public string Cleaning_ID { get; set; }
        public string Ward_name { get; set; }
        public string Street_Name { get; set; }
        public string Limits { get; set; }
        public string Inspector_name { get; set; }
        public Nullable<System.DateTime> Scheduled_Cleaning_Date { get; set; }
        public Nullable<System.DateTime> Inspected_Date { get; set; }
        public string Grade { get; set; }
        public System.Guid pkUID { get; set; }
    }
}