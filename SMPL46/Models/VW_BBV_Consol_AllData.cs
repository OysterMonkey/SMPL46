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
    
    public partial class VW_BBV_Consol_AllData
    {
        public string Category { get; set; }
        public string Ward_Name { get; set; }
        public string Street_Name { get; set; }
        public string Cleaning_ID { get; set; }
        public Nullable<System.DateTime> Inspect_Scheduled_Cleaning_Date { get; set; }
        public Nullable<System.DateTime> Final_Inspection_Date { get; set; }
        public string Final_Grade { get; set; }
        public Nullable<System.DateTime> Inspected_Date { get; set; }
        public string Inspect_Grade { get; set; }
        public Nullable<System.DateTime> Rect_Date { get; set; }
        public string Rect_Grade { get; set; }
        public System.Guid Inspect_pkUID { get; set; }
        public Nullable<System.Guid> Rect_pkUID { get; set; }
    }
}