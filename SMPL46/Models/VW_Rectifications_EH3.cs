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
    
    public partial class VW_Rectifications_EH3
    {
        public string Cleaning_ID { get; set; }
        public string Zone_Nr { get; set; }
        public string Freq_Code { get; set; }
        public string Std_or_Deep { get; set; }
        public string Area_Name { get; set; }
        public string Estate_Name { get; set; }
        public string Street_Name { get; set; }
        public string Limits { get; set; }
        public string Inspector_name { get; set; }
        public Nullable<System.DateTime> Scheduled_Cleaning_Date { get; set; }
        public Nullable<System.DateTime> Inspected_Date { get; set; }
        public string Grade { get; set; }
        public string Reason_Code { get; set; }
        public byte[] Inspection_Photo { get; set; }
        public string District { get; set; }
        public string Inspection_Comments { get; set; }
        public System.Guid pkUID { get; set; }
        public string IsRectification { get; set; }
        public Nullable<System.Guid> RectificationPKUID { get; set; }
    }
}