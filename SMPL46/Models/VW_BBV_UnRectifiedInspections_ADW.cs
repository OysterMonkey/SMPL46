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
    
    public partial class VW_BBV_UnRectifiedInspections_ADW
    {
        public string Inspect_Ward_Name { get; set; }
        public string Inspect_Cleaning_ID { get; set; }
        public string Inspect_Street_Name { get; set; }
        public string Inspect_Limits { get; set; }
        public string Inspector_name { get; set; }
        public Nullable<System.DateTime> Inspect_Scheduled_Cleaning_Date { get; set; }
        public Nullable<System.DateTime> Inspected_Date { get; set; }
        public string Inspect_Grade { get; set; }
        public System.Guid Inspect_pkUID { get; set; }
    }
}
