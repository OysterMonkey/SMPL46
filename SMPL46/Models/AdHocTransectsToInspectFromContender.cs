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
    
    public partial class AdHocTransectsToInspectFromContender
    {
        public System.Guid pkUID { get; set; }
        public int record_id { get; set; }
        public string Ward_Name { get; set; }
        public string District { get; set; }
        public string Street_Name { get; set; }
        public string Limits { get; set; }
        public Nullable<int> contender_reference { get; set; }
        public string Comments_From_Contender { get; set; }
        public System.DateTime DateTime_Added_From_Contender { get; set; }
        public Nullable<double> GPS_Northing { get; set; }
        public Nullable<double> GPS_Easting { get; set; }
        public Nullable<System.DateTime> Inspected_Date { get; set; }
        public string IsInspected { get; set; }
        public System.DateTime last_modified { get; set; }
        public string Inspection_Comment { get; set; }
        public string Cleansing_Required { get; set; }
    }
}